using System.Text.Json;

namespace CustomerManagement.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AdressJson { get; set; } = null!;

        public AddressDetails? Address { get; set; }

        public void SerializeAddress()
        {
            AdressJson = JsonSerializer.Serialize(Address);
        }

        public void DeserializeAddress()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(AdressJson) && AdressJson != "null")
                {
                    Address = JsonSerializer.Deserialize<AddressDetails>(AdressJson);
                }
            }
            catch
            {
                Address = null;
            }
        }

    }

    public class AddressDetails
    {
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
    }
}
