# QuantityMeasurementApp

A scalable and extensible **Quantity Measurement Application** built in **C# / .NET**, developed progressively through **UC1–UC18**.  
This project demonstrates strong fundamentals of **Object-Oriented Programming (OOP)**, **SOLID principles**, **generic programming**, **unit conversion**, **arithmetic operations**, **N-Tier architecture**, **database integration**, **ASP.NET Core Web API**, and **backend security using JWT + Google OAuth2**.

The QuantityMeasurementApp class is responsible for checking the equality of two numerical values measured in feet within the Quantity Measurement Application. It ensures accurate comparisons and handles various edge cases.

---

## 🚀 Current Status
- ✅ UC1 – UC18 Completed
- 🌐 REST API built with ASP.NET Core Web API
- 🗄️ Database integration using ADO.NET / EF Core with SQL Server
- 🔐 Authentication & User Management implemented
- 📘 Swagger enabled for API testing and documentation

---

## 📌 Project Overview
`QuantityMeasurementApp` started from simple **feet measurement equality checks** and evolved into a full-featured **multi-category quantity measurement system**.

It now supports:
- Equality comparison between measurements
- Unit-to-unit conversion
- Arithmetic operations on quantities
- Multi-category support (Length, Weight, Volume, Temperature)
- Generic and scalable quantity design
- Clean N-Tier architecture
- Database persistence
- RESTful APIs
- Secure authentication and authorization

This project reflects a strong progression from **core OOP concepts** to **real-world backend engineering practices**.

---

## ✨ Key Features
- Quantity equality and floating-point comparison
- Unit conversion across supported units
- Arithmetic operations:
  - Addition
  - Subtraction
  - Division
- Support for multiple measurement categories:
  - Length
  - Weight
  - Volume
  - Temperature
- Generic quantity abstraction for scalability
- Validation for cross-category operations
- Database persistence with SQL Server
- RESTful APIs with ASP.NET Core
- JWT authentication and role-based authorization
- Google OAuth2 login
- Security utilities for hashing and encryption/decryption

---

## 🛠️ Tech Stack
- **C# / .NET / ASP.NET Core**
- **ASP.NET Core Web API**
- **Entity Framework Core / ADO.NET**
- **MS SQL Server**
- **Swagger / Postman**
- **JWT Authentication**
- **Google OAuth 2.0**
- **NUnit / MSTest**

---

## 📂 Use Cases Covered
The project was implemented progressively across **18 use cases**:

- **UC1 – UC4:** Measurement equality, encapsulation, generic quantity design, extended unit support  
- **UC5 – UC8:** Unit conversion, arithmetic operations, target unit handling, refactoring for scalability  
- **UC9 – UC14:** Multi-category support (Weight, Volume, Temperature), subtraction/division, centralized arithmetic logic, interface refactoring  
- **UC15:** N-Tier architecture refactoring with DTOs, services, repositories, and dependency injection  
- **UC16:** Database integration using ADO.NET / SQL Server  
- **UC17:** ASP.NET Core backend with REST APIs, EF Core, Swagger, and backend architecture  
- **UC18:** Google Authentication, JWT Security, User Management, REST API Security, Encryption & Hashing  

---

## 🏗️ Architecture
The project follows a clean **N-Tier Architecture** for maintainability and scalability.

### Layers:
- **Controllers / Presentation Layer**
- **DTOs / Contracts**
- **Services / Business Logic**
- **Repositories / Data Access Layer**
- **Models / Entities**
- **DbContext / Persistence**
- **Security Utilities**

This architecture ensures:
- Clear separation of concerns
- Thin controllers
- Reusable business logic
- Better testability
- Easy future scalability

---

## 🔐 Security Features (UC18)
UC18 introduces a secure authentication and authorization module with:

- **JWT Bearer Authentication**
- **Google OAuth 2.0 Login**
- **Role-Based Authorization** (`Admin`, `User`)
- **Secure Password Hashing**
- **SHA256 Hashing Utility**
- **AES Encryption / Decryption**
- **Protected REST APIs**
- **Swagger Bearer Token Support**

### Example Auth APIs
- `POST /api/auth/register`
- `POST /api/auth/login`
- `POST /api/auth/google-login`

### Example User Management APIs
- `GET /api/users`
- `GET /api/users/{id}`
- `PUT /api/users/{id}/role`
- `PUT /api/users/{id}/activate`
- `PUT /api/users/{id}/deactivate`

---

## 🧪 Testing
The project emphasizes correctness and maintainability through testing.

### Includes:
- Unit testing for equality and conversion logic
- Arithmetic operation validation
- Category mismatch validation
- API testing using Swagger / Postman
- Authentication flow testing
- Protected endpoint authorization testing

### Tools:
- **NUnit / MSTest**
- **Swagger UI**
- **Postman**

---

## ▶️ How to Run
```bash
git clone <your-repository-url>
cd QuantityMeasurementApp
dotnet restore
dotnet ef database update
dotnet run
