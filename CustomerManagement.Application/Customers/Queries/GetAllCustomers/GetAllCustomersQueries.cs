using CustomerManagement.Domain.Entities;
using MediatR;

namespace CustomerManagement.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>> { }
    
}
