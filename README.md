# Customer Management API

This project is a customer management system developed using .NET Core 8.0, CQRS, Dapper, and SQL Server. The application runs in a container-based Docker environment and can be easily tested through the Swagger interface.

## Features

- Layered architecture (API, Application, Domain, Infrastructure)  
- CQRS design pattern (Command / Query separation)  
- Independent operation management with MediatR  
- Fast and lightweight data access with Dapper  
- Stored Procedure usage on SQL Server  
- Storing address data in JSON format (AdressJson)  
- Swagger UI integration  
- Quick setup with Docker Compose  

## Technologies

- .NET Core 8.0  
- CQRS Pattern  
- MediatR  
- Dapper  
- SQL Server  
- Stored Procedure  
- Swagger  
- Docker & Docker Compose  

## Project Architecture

```
├── CustomerManagement.API
│   └── Controller, Program.cs
├── CustomerManagement.Application
│   └── CQRS (Commands & Queries), Handlers, Interfaces (ICustomerRepository)
├── CustomerManagement.Domain
│   └── Entities
├── CustomerManagement.Infrastructure
│   └── Repository

```

---

## Setup and Run

Before starting the project, make sure Docker is installed on your system.

### Steps:

```bash
docker compose up --build
```

Once the application is up and running, the Swagger interface can be accessed at:

```
http://localhost:8080/swagger
```

## Database Structure

**Table: Customers**

| Column      | Data Type       |
|-------------|-----------------|
| Id          | INT, Identity   |
| FullName    | NVARCHAR(100)   |
| Email       | NVARCHAR(100)   |
| AdressJson  | NVARCHAR(MAX)   |

## Stored Procedures

| Name           | Description              |
|----------------|--------------------------|
| AddCustomer    | Adds a new customer      |
| GetAllCustomers| Lists all customers      |

Note: The creation of this table and the procedures is automatically handled in `Program.cs` when the application starts.

## Technical Notes

- `AdressJson` stores the Address model in JSON format.  
- Data access is performed efficiently using Dapper with SQL Server.  
- The CQRS pattern separates Command and Query operations into different Handler classes.  
- Database, table, and procedure checks are handled in `Program.cs`.  

