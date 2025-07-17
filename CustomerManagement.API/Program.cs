using Microsoft.OpenApi.Models; 
using MediatR;
using System.Reflection;
using System.Data;
using Microsoft.Data.SqlClient;
using CustomerManagement.Application.Interfaces;
using CustomerManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Management API", Version = "v1" });
});

// Controller'ları tanıt
builder.Services.AddControllers();

// MediatR
builder.Services.AddMediatR(typeof(CustomerManagement.Application.Customers.Commands.CreateCustomer.CreateCustomerCommand));

// Dapper - SQL bağlantısı
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Bağlantısı
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Swagger Aktif Et
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseHttpsRedirection();

// Controller route'larını aktif et
app.MapControllers();

// Örnek endpoint (opsiyonel)
app.MapGet("/weatherforecast", () => "Hello World");

app.Run();
