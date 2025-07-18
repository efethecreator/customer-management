using MediatR;

namespace CustomerManagement.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AdressJson { get; set; } = null!;
    }
}
