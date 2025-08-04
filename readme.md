# LoanSimulator

A clean architecture application built with .NET 9 and Angular, following DDD and CQRS principles.

## Features

- 🏗️ Layered architecture (Domain, Application, Infrastructure, API)
- ⚡ CQRS with MediatR
- 🗄️ EF Core with automated migrations
- ✅ FluentValidation integration
- 📚 Swagger/OpenAPI documentation
- 🖥️ Angular frontend
- 🧪 Unit & integration tests
- 🐳 Docker Compose ready (API + SQL Server + Frontend)

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Optional) [Azure Data Studio](https://aka.ms/azuredatastudio) for database management

---

## 🚀 Getting Started

### Option 1: Full Docker Setup (Recommended)
```bash
# 1. Build and start all services
docker-compose up -d --build

# 2. Access services:
# - Frontend: http://localhost:4040
# - API Docs: http://localhost:8080
# - Database: localhost,1433 (SA_PASSWORD=YourStrong!Passw0rd)

# 3. Stop services
docker-compose down# LoanSimulator

A clean architecture application built with .NET 9 and Angular, following DDD and CQRS principles.

## Features

- 🏗️ Layered architecture (Domain, Application, Infrastructure, API)
- ⚡ CQRS with MediatR
- 🗄️ EF Core with automated migrations
- ✅ FluentValidation integration
- 📚 Swagger/OpenAPI documentation
- 🖥️ Angular frontend
- 🧪 Unit & integration tests
- 🐳 Docker Compose ready (API + SQL Server + Frontend)

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Optional) [Azure Data Studio](https://aka.ms/azuredatastudio) for database management

---

## 🚀 Getting Started

### Option 1: Full Docker Setup (Recommended)
```bash
# 1. Build and start all services
docker-compose up -d --build

# 2. Access services:
# - Frontend: http://localhost:4040
# - API Docs: http://localhost:8080
# - Database: localhost,1433 (SA_PASSWORD=YourStrong!Passw0rd)

# 3. Stop services
docker-compose down