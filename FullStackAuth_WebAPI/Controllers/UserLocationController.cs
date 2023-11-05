using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Services;
using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLocationController : ControllerBase
    {
        private readonly GooglePlacesService _googlePlacesService;

        public UserLocationController(GooglePlacesService googlePlacesService)
        {
            _googlePlacesService = googlePlacesService ?? throw new ArgumentNullException(nameof(googlePlacesService));
        }

        // address=24%20Sussex%20Drive%20Ottawa%20ON
        [HttpGet("byAddress")]
        public async Task<ActionResult<UserLocation>> GetUserLocationAsync([FromQuery] string address)
        {
            try
            {
                var userLocation = await _googlePlacesService.GetUserLocationAsync(address);
                return Ok(userLocation);
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
