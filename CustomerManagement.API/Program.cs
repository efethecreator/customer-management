using Microsoft.OpenApi.Models;
using MediatR;
using System.Data;
using Microsoft.Data.SqlClient;
using CustomerManagement.Application.Interfaces;
using CustomerManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Management API", Version = "v1" });
});

builder.Services.AddMediatR(typeof(CustomerManagement.Application.Customers.Commands.CreateCustomer.CreateCustomerCommand));

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var defaultConnStr = config.GetConnectionString("DefaultConnection");
    var masterConnStr = config.GetConnectionString("MasterConnection");

    try
    {
        using var masterConnection = new SqlConnection(masterConnStr);
        int retry = 0;
        while (retry < 10)
        {
            try
            {
                masterConnection.Open();
                break;
            }
            catch (SqlException)
            {
                retry++;
                Console.WriteLine($"SQL server hazır degil, tekrar deneniyor ({retry})");
                Thread.Sleep(3000);
            }
        }

        // veritabanını olustur
        var createDbCmd = new SqlCommand(@"
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CustomerManagementDb')
BEGIN
    CREATE DATABASE CustomerManagementDb;
END", masterConnection);
        createDbCmd.ExecuteNonQuery();
        masterConnection.Close();

        // tabloları ve spleri baslangıcta olustur
        using var defaultConnection = new SqlConnection(defaultConnStr);
        defaultConnection.Open();

        var setupCmd = new SqlCommand(@"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
BEGIN
    CREATE TABLE Customers (
        Id INT PRIMARY KEY IDENTITY,
        FullName NVARCHAR(100),
        Email NVARCHAR(100),
        AdressJson NVARCHAR(MAX)
    );
END;

IF OBJECT_ID('GetAllCustomers', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE GetAllCustomers
    AS
    BEGIN
        SELECT * FROM Customers;
    END
    ');
END;

IF OBJECT_ID('AddCustomer', 'P') IS NOT NULL
    DROP PROCEDURE AddCustomer;

EXEC('
CREATE PROCEDURE AddCustomer
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @AdressJson NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Customers (FullName, Email, AdressJson)
    VALUES (@FullName, @Email, @AdressJson);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
');
", defaultConnection);

        setupCmd.ExecuteNonQuery();
        Console.WriteLine("Veritabanı hazır.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Veritabanı kurulumu basarısız:");
        Console.WriteLine(ex.Message);
    }
}

app.Run();
