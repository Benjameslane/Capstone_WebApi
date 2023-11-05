using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackAuth_WebAPI.Services
{
    public class StoreFoodItemService
    {
        private readonly ApplicationDbContext _context;

        public StoreFoodItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StoreFoodItem> GetStoreFoodItemByIdAsync(int id)
        {
            return await _context.StoreFoodItems.FindAsync(id);
        }

        public async Task<StoreFoodItem> AddStoreFoodItemAsync(StoreFoodItemDto storeFoodItemDto, string userId)
        {
            if (storeFoodItemDto == null || userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            // Convert DTO to entity
            var storeFoodItem = new StoreFoodItem
            {
                ItemName = storeFoodItemDto.ItemName,
                ExpirationDate = storeFoodItemDto.ExpirationDate,
                UserId = userId,
                Quantity = storeFoodItemDto.Quantity,
                Category = storeFoodItemDto.Category,
                Status = storeFoodItemDto.Status,
                Price = storeFoodItemDto.Price,
                Listed = storeFoodItemDto.Listed,
                Discounted = storeFoodItemDto.Discounted,
            };

            try
            {
                await _context.StoreFoodItems.AddAsync(storeFoodItem);
                await _context.SaveChangesAsync();

                return storeFoodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<IEnumerable<StoreFoodItem>> GetStoreInventoryAsync(string userId)
        {
            if (userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            try
            {
               var storeInventory = await _context.StoreFoodItems
                    .Where(fi => fi.UserId == userId)
                    .OrderBy(fi => fi.ExpirationDate)
                    .ToListAsync();

                return storeInventory;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<IEnumerable<StoreFoodItem>> GetDiscountedStoreInventoryAsync(string userId)
        {
            if (userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            try
            {
                var storeInventory = await _context.StoreFoodItems
                     .Where(fi => fi.UserId == userId && fi.Discounted == 1 && fi.Listed == 1)
                     .OrderBy(fi => fi.ExpirationDate)
                     .ToListAsync();

                return storeInventory;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        // Get listed store food items
        public async Task<IEnumerable<StoreFoodItem>> GetListedStoreFoodItemsAsync(string userId)
        {
            if (userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            try
            {
                var listedStoreFoodItem = await _context.StoreFoodItems
                     .Where(fi => fi.UserId == userId && fi.Listed == 1)
                     .OrderBy(fi => fi.ExpirationDate)
                     .ToListAsync();

                return listedStoreFoodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        // Get store food items labeled as "1" in Discounted column
        public async Task<IEnumerable<StoreFoodItem>> GetDiscountedStoreFoodItemsAsync(string userId)
        {
            if (userId == string.Empty)
            {
                // Handle invalid input
                return null;
            }

            try
            {
                var discountedStoreFoodItem = await _context.StoreFoodItems
                     .Where(fi => fi.UserId == userId && fi.Discounted == 1 && fi.Listed == 1)
                     .OrderBy(fi => fi.ExpirationDate)
                     .ToListAsync();

                return discountedStoreFoodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<StoreFoodItem> UpdateStoreFoodItemByIdAsync(int foodId, StoreFoodItemDto storeFoodItemDto, string userId)
        {
            var storeFoodItem = await _context.StoreFoodItems.FindAsync(foodId);

            if (storeFoodItem == null || storeFoodItemDto == null)
            {
                // Handle Not Found Food item
                return null;
            }

            if (storeFoodItem.UserId != userId)
            {
                // Handle Unauthorized User
                return null;
            }

            try
            {
                storeFoodItem.ItemName = storeFoodItemDto.ItemName;
                storeFoodItem.ExpirationDate = storeFoodItemDto.ExpirationDate;
                storeFoodItem.Quantity = storeFoodItemDto.Quantity;
                storeFoodItem.Category = storeFoodItemDto.Category;
                storeFoodItem.Price = storeFoodItemDto.Price;
                storeFoodItem.Status = storeFoodItemDto.Status;
                storeFoodItem.Listed = storeFoodItemDto.Listed;
                storeFoodItem.Discounted = storeFoodItemDto.Discounted;
                await _context.SaveChangesAsync();

                return storeFoodItem;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);  // Consider using a logging library here

                return null;
            }
        }

        public async Task<StoreFoodItem> DeleteStoreFoodItemByIdAsync(int foodId, string userId)
        {
            var storeFoodItem = await _context.StoreFoodItems.FindAsync(foodId);

            if (storeFoodItem == null)
            {
                // Handle Not Found Food item
                return null;
            }

            if (storeFoodItem.UserId != userId)
            {
                // Handle Unauthorized User
                return null;
            }

            try
            {
                _context.StoreFoodItems.Remove(storeFoodItem);
                await _context.SaveChangesAsync();

                return storeFoodItem;
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