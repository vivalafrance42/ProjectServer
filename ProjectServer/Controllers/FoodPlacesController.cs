using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodModel;
using Microsoft.AspNetCore.Authorization;

namespace ProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodPlacesController(FoodSourceContext context) : ControllerBase
    {
        

        // GET: api/FoodPlaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodPlace>>> GetFoodPlaces()
        {
            return await context.FoodPlaces.ToListAsync();
        }

        // GET: api/FoodPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodPlace>> GetFoodPlace(int id)
        {
            var foodPlace = await context.FoodPlaces.FindAsync(id);

            if (foodPlace == null)
            {
                return NotFound();
            }

            return foodPlace;
        }

        [HttpGet("FoodPlaceMenuItems/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItemsByFoodPlace(int id)
        {
            return await context.MenuItems.Where(c => c.FoodPlaceId == id).ToListAsync();
        }


        // PUT: api/FoodPlaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodPlace(int id, FoodPlace foodPlace)
        {
            if (id != foodPlace.FoodPlaceId)
            {
                return BadRequest();
            }

            context.Entry(foodPlace).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodPlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FoodPlaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodPlace>> PostFoodPlace(FoodPlace foodPlace)
        {
            context.FoodPlaces.Add(foodPlace);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetFoodPlace", new { id = foodPlace.FoodPlaceId }, foodPlace);
        }

        // DELETE: api/FoodPlaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodPlace(int id)
        {
            var foodPlace = await context.FoodPlaces.FindAsync(id);
            if (foodPlace == null)
            {
                return NotFound();
            }

            context.FoodPlaces.Remove(foodPlace);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodPlaceExists(int id)
        {
            return context.FoodPlaces.Any(e => e.FoodPlaceId == id);
        }
    }
}
