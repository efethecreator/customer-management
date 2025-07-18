using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(int id, string fullName, string email, string adressJson);
        Task<bool> DeleteCustomerAsync(int id);

    }
}
