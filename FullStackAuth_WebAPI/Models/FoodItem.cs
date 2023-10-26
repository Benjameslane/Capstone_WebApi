namespace FullStackAuth_WebAPI.Models
{
   
    public class FoodItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ItemName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }

}
