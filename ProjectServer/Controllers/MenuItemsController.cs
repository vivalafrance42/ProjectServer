using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodModel;
using ProjectServer.DTO;

namespace ProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MenuItemsController(FoodSourceContext context) : ControllerBase
    {


        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            return await context.MenuItems.ToListAsync();
        }

        [HttpGet("GetItemsPurchased")]
        public async Task<ActionResult<IEnumerable<FoodPlaceItemsPurchased>>> GetItemsPurchased()
        {
            IQueryable<FoodPlaceItemsPurchased> x = from c in context.FoodPlaces
                                                    select new FoodPlaceItemsPurchased
                                                    {
                                                        PlaceName = c.PlaceName,
                                                        FoodPlaceId = c.FoodPlaceId,
                                                        ItemsPurchased = c.MenuItems.Sum(t => t.ItemsPurchased)
                                                    };
            return await x.ToListAsync();
        }

        [HttpGet("GetItemsPurchased2")]
        public async Task<ActionResult<IEnumerable<FoodPlaceItemsPurchased>>> GetItemsPurchased2()
        {
            IQueryable<FoodPlaceItemsPurchased> x = context.FoodPlaces.Select(c =>
                                              new FoodPlaceItemsPurchased
                                              {
                                                  PlaceName = c.PlaceName,
                                                  FoodPlaceId = c.FoodPlaceId,
                                                  ItemsPurchased = c.MenuItems.Sum(t => t.ItemsPurchased)
                                              });
            return await x.ToListAsync();
        }
    }
}
