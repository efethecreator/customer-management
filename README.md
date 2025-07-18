# Customer Management API

Bu proje, .NET Core 8.0, CQRS, Dapper ve SQL Server kullanılarak geliştirilmiş bir **müşteri yönetim sistemidir**. Uygulama **Docker** ortamında container bazlı olarak çalışır ve **Swagger** arayüzü ile kolayca test edilebilir.

---

## Özellikler

- Katmanlı mimari yapısı (API, Application, Domain, Infrastructure)
- CQRS tasarım deseni (Command / Query ayrımı)
- MediatR ile bağımsız işlem yönetimi
- Dapper ile hızlı ve sade veri erişimi
- SQL Server üzerinde Stored Procedure kullanımı
- JSON formatında adres verisi saklama (AdressJson)
- Swagger UI entegrasyonu
- Docker Compose ile hızlı kurulum

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

## Proje Mimarisi

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

## Kurulum ve Çalıştırma

> Projeyi ayağa kaldırmadan önce sisteminizde **Docker** kurulu olduğundan emin olun.

### Adımlar:

```bash
docker compose up --build
```

Uygulama başlatıldığında Swagger arayüzü aşağıdaki adresten erişilebilir olur:

```
http://localhost:8080/swagger
```

---

## Veritabanı Yapısı

**Tablo: `Customers`**

| Kolon       | Veri Tipi         |
|-------------|-------------------|
| Id          | INT, Identity      |
| FullName    | NVARCHAR(100)      |
| Email       | NVARCHAR(100)      |
| AdressJson  | NVARCHAR(MAX)      |

---

## Stored Procedure'ler

| Adı              | Açıklama                     |
|------------------|------------------------------|
| `AddCustomer`     | Yeni müşteri ekler           |
| `GetAllCustomers` | Tüm müşterileri listeler     |

> Not: Uygulama başlatıldığında **Program.cs** dosyasında bu tablo ve prosedürlerin otomatik oluşturulması sağlanır.

---

## 📌 Teknik Notlar

- `AdressJson`, `Address` modelini **JSON** formatında saklar.
- **Dapper** kullanılarak `SQL Server` ile hızlı veri erişimi sağlanır.
- CQRS ile GET ve POST işlemleri ayrı handler'lar üzerinden yönetilir.
- `Program.cs` içinde veritabanı, tablo ve prosedür kontrolleri gerçekleştirilir.

