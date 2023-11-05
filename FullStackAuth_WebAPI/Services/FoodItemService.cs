using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackAuth_WebAPI.Services
{
    public class FoodItemService
    {
        private readonly ApplicationDbContext _context;

        public FoodItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FoodItem> GetFoodItemByIdAsync(int id)
        {
            return await _context.FoodItems.FindAsync(id);
        }

        public async Task<FoodItem> AddFoodItemAsync(FoodItemDto foodItemDto, string userId)
        {
            if (foodItemDto == null || userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            // Convert DTO to entity
            var foodItem = new FoodItem
            {
                ItemName = foodItemDto.ItemName,
                ExpirationDate = foodItemDto.ExpirationDate,
                UserId = userId,
                Quantity = foodItemDto.Quantity,
                Category = foodItemDto.Category
            };

            try
            {
                await _context.FoodItems.AddAsync(foodItem);
                await _context.SaveChangesAsync();

                return foodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        // Get expiring food items within 7 days
        public async Task<IEnumerable<FoodItem>> GetExpiringFoodItemsAsync(string userId)
        {
            if (userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            try
            {
                var currentDate = DateTime.Now;
                var sevenDaysFromNow = currentDate.AddDays(7);

                var expiringItems = await _context.FoodItems
                    .Where(fi => fi.UserId == userId && fi.ExpirationDate >= currentDate && fi.ExpirationDate <= sevenDaysFromNow)
                    .OrderBy(fi => fi.ExpirationDate)
                    .ToListAsync();

                return expiringItems;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<IEnumerable<FoodItem>> GetUserInventoryAsync(string userId)
        {
            if (userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            try
            {
               var userInventory = await _context.FoodItems
                    .Where(fi => fi.UserId == userId)
                    .OrderBy(fi => fi.ExpirationDate)
                    .ToListAsync();

                return userInventory;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<FoodItem> UpdateFoodItemByIdAsync(int foodId, FoodItemDto foodItemDto, string userId)
        {
            var foodItem = await _context.FoodItems.FindAsync(foodId);

            if (foodItem == null || foodItemDto == null)
            {
                // Handle Not Found Food item
                return null;
            }

            if (foodItem.UserId != userId)
            {
                // Handle Unauthorized User
                return null;
            }

            try
            {
                foodItem.ItemName = foodItemDto.ItemName;
                foodItem.ExpirationDate = foodItemDto.ExpirationDate;
                foodItem.Quantity = foodItemDto.Quantity;
                foodItem.Category = foodItemDto.Category;
                await _context.SaveChangesAsync();

                return foodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<FoodItem> DeleteFoodItemByIdAsync(int foodId, string userId)
        {
            var foodItem = await _context.FoodItems.FindAsync(foodId);

            if (foodItem == null)
            {
                // Handle Not Found Food item
                return null;
            }

            if (foodItem.UserId != userId)
            {
                // Handle Unauthorized User
                return null;
            }

            try
            {
                _context.FoodItems.Remove(foodItem);
                await _context.SaveChangesAsync();

                return foodItem;
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