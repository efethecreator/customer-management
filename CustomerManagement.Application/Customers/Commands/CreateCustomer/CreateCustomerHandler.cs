using CustomerManagement.Application.Interfaces;
using CustomerManagement.Domain.Entities;
using MediatR;

namespace CustomerManagement.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FullName = request.FullName,
                Email = request.Email,
                AdressJson = request.AdressJson
            };

            var createdCustomerId = await _customerRepository.AddCustomerAsync(customer);
            return createdCustomerId;
        }
    }
}
