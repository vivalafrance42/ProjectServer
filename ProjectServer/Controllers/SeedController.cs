﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodModel;
using CsvHelper.Configuration;
using System.Globalization;
using ProjectServer.Data;
using Microsoft.Extensions.Hosting;
using CsvHelper;
using Microsoft.AspNetCore.Identity;

namespace WeatherServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(FoodSourceContext db, IHostEnvironment environment, UserManager<FoodPlaceMenuItemsUser> userManager) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/FoodPlaceMenuItems.csv");

        [HttpPost("User")]
        public async Task<ActionResult> SeedUser()
        {
            (string name, string email) = ("user1", "comp584@csun.edu");
            FoodPlaceMenuItemsUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";
            }
            _ = await userManager.CreateAsync(user, "P@ssw0rd!")
                ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("MenuItem")]
        public async Task<ActionResult<MenuItem>> SeedMenuItem()
        {
            Dictionary<string, FoodPlace> FoodPlaces = await db.FoodPlaces//.AsNoTracking()
               .ToDictionaryAsync(c => c.PlaceName);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int menuItemCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<FoodPlaceMenuItemsCsv>? records = csv.GetRecords<FoodPlaceMenuItemsCsv>();
                foreach (FoodPlaceMenuItemsCsv record in records)
                {
                    if (!FoodPlaces.TryGetValue(record.FoodPlace, out FoodPlace? value))
                    {
                        Console.WriteLine($"Not found FoodPlace for {record.MenuItem}");
                        return NotFound(record);
                    }

                    if (!record.ItemsPurchased.HasValue /*|| string.IsNullOrEmpty(record.MenuItem_ascii)*/)
                    {
                        Console.WriteLine($"Skipping {record.MenuItem}");
                        continue;
                    }
                    MenuItem menuItem = new()
                    {
                        ItemName = record.MenuItem,
                        ItemsPurchased = (int)record.ItemsPurchased.Value,
                        FoodPlaceId = value.FoodPlaceId,
                        Price = record.Price
                    };
                    db.MenuItems.Add(menuItem);
                    menuItemCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(menuItemCount);
        }

        [HttpPost("FoodPlace")]
        public async Task<ActionResult<MenuItem>> SeedFoodPlace()
        {
            // create a lookup dictionary containing all the FoodPlaces already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, FoodPlace> FoodPlacesByName = db.FoodPlaces
                .AsNoTracking().ToDictionary(x => x.PlaceName, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<FoodPlaceMenuItemsCsv> records = csv.GetRecords<FoodPlaceMenuItemsCsv>().ToList();
            foreach (FoodPlaceMenuItemsCsv record in records)
            {
                if (FoodPlacesByName.ContainsKey(record.FoodPlace))
                {
                    continue;
                }

                FoodPlace FoodPlace = new()
                {
                    PlaceName = record.FoodPlace,
                    PlaceInitials2 = record.PlaceInitials2
                };
                await db.FoodPlaces.AddAsync(FoodPlace);
                FoodPlacesByName.Add(record.FoodPlace, FoodPlace);
            }

            await db.SaveChangesAsync();

            return new JsonResult(FoodPlacesByName.Count);
        }
    }
}