using CustomerManagement.Application.Interfaces;
using MediatR;

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
            return await _repository.UpdateCustomerAsync(request.Id, request.FullName, request.Email, request.AdressJson);
        }
    }
}
