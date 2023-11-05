namespace FullStackAuth_WebAPI.Models
{
    public class StoreFoodItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ItemName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public byte Listed { get; set; }
        public byte Discounted { get; set; }
    }

}
