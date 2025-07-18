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
            return customers;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@Email", customer.Email);
            parameters.Add("@AdressJson", customer.AdressJson); 

            var newId = await _dbConnection.ExecuteScalarAsync<int>(
                "AddCustomer",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return newId;
        }

        public async Task<bool> UpdateCustomerAsync(int id, string fullName, string email, string adressJson)
        {
            var sql = @"UPDATE Customers 
            SET FullName = @FullName, Email = @Email, AdressJson = @AdressJson 
            WHERE Id = @Id";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id, FullName = fullName, Email = email, AdressJson = adressJson });
            return result > 0;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var sql = "DELETE FROM Customers WHERE Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }


    }


}
