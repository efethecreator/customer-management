using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> AddCustomerAsync(Customer customer);

    }
}
