# OmicronTestCase

Backend test case implementation developed with ASP.NET Core Web API and SQL Server.

## Features

- Product CRUD operations
- Category CRUD operations
- Product filtering endpoint
- Business rule validations
- Dockerized environment
- Swagger API documentation
- Layered architecture (Controller / Service / Repository)

---

# Technologies

- ASP.NET Core 10 Web API
- Entity Framework Core
- SQL Server 2022
- Docker & Docker Compose
- Swagger / OpenAPI

---

# Project Structure

```text
Controllers/
Services/
Repositories/
DTOs/
Models/
Data/
```

---

# Product Filter Endpoint

The API supports product filtering based on:

- Keyword search on:
    - Product title
    - Product description
    - Category name

- Stock quantity range:
    - Minimum stock
    - Maximum stock

Example:

```http
GET /api/products/filter?keyword=laptop&minStock=5&maxStock=20
```

---

# Running the Project

### Start containers

```bash
docker compose up --build
```

### Swagger URL

```text
http://localhost:8080/swagger
```

---

# SQL Server Setup

The project uses SQL Server 2022 container.

Connection string:

```json
"DefaultConnection": "Server=sqlserver,1433;Database=RuleWayDb;User Id=sa;Password=RuleWay123!;TrustServerCertificate=True;"
```

---

# Database Initialization

Since migrations are not used in this project, database tables should be created manually.

## Create Database

```sql
CREATE DATABASE RuleWayDb;
GO
```

## Create Categories Table

```sql
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    MinStockQuantity INT NOT NULL
);
GO
```

## Create Products Table

```sql
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CategoryId INT NOT NULL,
    StockQuantity INT NOT NULL,
    IsLive BIT NOT NULL,

    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id)
        ON DELETE NO ACTION
);
GO
```

---

# API Endpoints

## Categories

| Method | Endpoint |
|---|---|
| GET | /api/categories |
| GET | /api/categories/{id} |
| POST | /api/categories |
| PUT | /api/categories/{id} |
| DELETE | /api/categories/{id} |

---

## Products

| Method | Endpoint |
|---|---|
| GET | /api/products |
| GET | /api/products/{id} |
| GET | /api/products/filter |
| POST | /api/products |
| PUT | /api/products/{id} |
| DELETE | /api/products/{id} |

---

# Example Responses

## Create Product

```json
{
  "id": 1,
  "title": "Gaming Laptop",
  "description": "RTX 4080 Gaming Laptop",
  "categoryId": 1,
  "categoryName": "Electronics",
  "stockQuantity": 10,
  "isLive": true
}
```

---

# Swagger Documentation

Swagger UI is enabled for API testing and documentation.

```text
http://localhost:8080/swagger
```

---

# Screenshots

API response screenshots are included in the repository.

---

