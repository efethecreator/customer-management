using CustomerManagement.Application.Interfaces;
using MediatR;

namespace CustomerManagement.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteCustomerAsync(request.Id);
        }
    }
}
