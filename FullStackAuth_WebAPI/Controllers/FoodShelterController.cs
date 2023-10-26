using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Services;
using System.Threading.Tasks;
using System;
using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodSheltersController : ControllerBase
    {
        private readonly GooglePlacesService _googlePlacesService;

        public FoodSheltersController(GooglePlacesService googlePlacesService)
        {
            _googlePlacesService = googlePlacesService ?? throw new ArgumentNullException(nameof(googlePlacesService));
        }

        [HttpGet("nearest")]
        public async Task<ActionResult<IEnumerable<FoodShelter>>> GetNearestFoodShelters([FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                var foodShelters = await _googlePlacesService.GetNearbyFoodSheltersAsync(latitude, longitude);
                return Ok(foodShelters);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
