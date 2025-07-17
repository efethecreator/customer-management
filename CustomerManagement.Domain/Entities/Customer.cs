namespace CustomerManagement.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AdressJson { get; set; } = null!;
    }
}