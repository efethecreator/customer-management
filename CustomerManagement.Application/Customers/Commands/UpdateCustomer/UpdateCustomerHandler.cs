using CustomerManagement.Application.Interfaces;
using CustomerManagement.Domain.Entities;
using MediatR;
using System.Text.Json;

namespace CustomerManagement.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            
            string addressJson = JsonSerializer.Serialize(request.Address);

            return await _repository.UpdateCustomerAsync(request.Id, request.FullName, request.Email, addressJson);
        }
    }
}
