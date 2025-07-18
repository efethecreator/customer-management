using CustomerManagement.Application.Interfaces;
using CustomerManagement.Domain.Entities;
using MediatR;

namespace CustomerManagement.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly ICustomerRepository _repository;

        public GetAllCustomersHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllCustomersAsync();
        }
    }
}
