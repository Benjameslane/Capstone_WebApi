using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

        public async Task<FoodItem> AddFoodItemAsync(FoodItemDto foodItemDto, int userId)
        {
            if (foodItemDto == null || userId <= 0)
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

       
    }
}