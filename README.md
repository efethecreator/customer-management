# Customer Management API

Bu proje, .NET Core, CQRS, Dapper ve SQL Server mimarisiyle geliştirilmiş bir müşteri yönetim sistemidir. Docker ortamında container bazlı çalışır ve Swagger arayüzü ile test edilebilir.

---

## Teknolojiler

- **.NET Core 8.0** 
- **CQRS Pattern**   
- **MediatR**  
- **Dapper** 
- **SQL Server**  
- **Stored Procedure** 
- **Swagger**   
- **Docker & Docker Compose** 

---

# Proje Mimarisi
├── CustomerManagement.API
│   └── Controller, Program.cs
├── CustomerManagement.Application
│   └── CQRS (Commands & Queries), Handlers, Interfaces (ICustomerRepository)
├── CustomerManagement.Domain
│   └── Entities
├── CustomerManagement.Infrastructure
│   └── Repository

---

# Kurulum ve Çalıştırma

Docker çalıştığından emin olun.

Aşağıdaki komutu kullanarak projeyi ayağa kaldırın:

docker compose up --build

Uygulama ayağa kalktığında Swagger arayüzü şu adreste çalışır:

http://localhost:8080/swagger

🛠️ Veritabanı Yapısı
Tablo: Customers
Kolon	Tipi
Id	INT, Identity
FullName	NVARCHAR(100)
Email	NVARCHAR(100)
AdressJson	NVARCHAR(MAX)

Stored Procedure'ler
SP Adı	Açıklama
AddCustomer	Yeni müşteri ekler
GetAllCustomers	Tüm müşterileri listeler

Uygulama başlatılırken tablo ve SP'ler Program.cs dosyasındaki kod ile otomatik oluşturulur.

Ek Bilgiler
AdressJson, Address modelini JSON formatında saklar.

Dapper üzerinden SQL Server ile haberleşir.

Program.cs içinde hem CustomerManagementDb veritabanı hem de tablo ve prosedür kontrolleri yapılır.

GET ve POST işlemleri MediatR handler'ları üzerinden CQRS mimarisiyle ayrıştırılmıştır.
