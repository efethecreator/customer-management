using CustomerManagement.Application.Interfaces;
using CustomerManagement.Domain.Entities;
using Dapper;
using System.Data;
using System.Text.Json;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _dbConnection.QueryAsync<Customer>(
                "GetAllCustomers",
                commandType: CommandType.StoredProcedure
            );

            foreach (var customer in customers)
            {
                if (!string.IsNullOrEmpty(customer.AdressJson))
                {
                    // Eğer Adress nesnesi tanımlıysa deserialize edebilirsin.
                    // Örnek: var address = JsonSerializer.Deserialize<Address>(customer.AdressJson);
                }
            }

            return customers;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@Email", customer.Email);
            parameters.Add("@AdressJson", customer.AdressJson); // DB'deki kolonla birebir olmalı

            var newId = await _dbConnection.ExecuteScalarAsync<int>(
                "AddCustomer",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return newId;
        }
    }
}
