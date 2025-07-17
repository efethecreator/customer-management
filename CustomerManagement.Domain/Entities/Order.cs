namespace CustomerManagement.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }           
 
    }
}