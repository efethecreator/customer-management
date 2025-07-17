using MediatR;

namespace CustomerManagement.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int> //customerId dönecek!!
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AdressJson { get; set; } = null!;
    }
}