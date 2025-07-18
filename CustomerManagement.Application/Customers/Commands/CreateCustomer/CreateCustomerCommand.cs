using CustomerManagement.Domain.Entities;
using MediatR;

namespace CustomerManagement.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public AddressDetails Address { get; set; } = null!;
    }
}
