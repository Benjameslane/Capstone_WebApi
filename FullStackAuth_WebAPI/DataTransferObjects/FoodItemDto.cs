namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class FoodItemDto
    {
        public string ItemName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}