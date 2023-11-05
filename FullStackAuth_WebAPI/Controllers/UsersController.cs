using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Services;
using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        // GET /Users/storeOwners
        [HttpGet("storeOwners")]
        public async Task<ActionResult<UserInfo>> GetUserLocationAsync()
        {
            try
            {
                var storeOwners = await _usersService.GetStoreOwners();

                var storeOwnersDetails = storeOwners.Select(storeOwner => new UserInfo
                {
                    UserId = storeOwner.Id,
                    FirstName = storeOwner.FirstName,
                    LastName = storeOwner.LastName,
                    Email = storeOwner.Email,
                });

                return Ok(storeOwnersDetails);
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
