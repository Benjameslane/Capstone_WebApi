using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackAuth_WebAPI.Services
{
    public class UsersService
    {
        private readonly ApplicationDbContext _context;

        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get store owners
        public async Task<IEnumerable<User>> GetStoreOwners()
        {
            try
            {
                var storeOwners = await _context.Users
                    .Where(fi => fi.UserType == "Store Owner")
                    .ToListAsync();

                return storeOwners;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }
    }
}