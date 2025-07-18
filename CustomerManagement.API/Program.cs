using Microsoft.OpenApi.Models; 
using MediatR;
using System.Reflection;
using System.Data;
using Microsoft.Data.SqlClient;
using CustomerManagement.Application.Interfaces;
using CustomerManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Management API", Version = "v1" });
});

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(CustomerManagement.Application.Customers.Commands.CreateCustomer.CreateCustomerCommand));

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/weatherforecast", () => "Hello World");

app.Run();
