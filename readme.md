# LoanSimulator

A clean architecture application built with .NET 9 and TypeScript (Angular), following Domain-Driven Design (DDD) and CQRS principles.

## Features

- Layered backend architecture: Domain, Application, Infrastructure, API  
- CQRS with commands and queries  
- EF Core with migrations  
- Validation with FluentValidation  
- Swagger/OpenAPI documentation  
- Angular frontend client  
- Unit tests  
- Docker Compose setup for API and database  

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Node.js & npm](https://nodejs.org/)  
- [Docker & Docker Compose](https://www.docker.com/)  

---

## Run Backend (API)

```bash
cd LoanSimulator.API
dotnet restore
dotnet ef database update
dotnet run --launch-profile "LoanSimulator"
# LoanSimulator

A clean architecture application built with .NET 9 and TypeScript (Angular), following Domain-Driven Design (DDD) and CQRS principles.

## Features

- Layered backend architecture: Domain, Application, Infrastructure, API  
- CQRS with commands and queries  
- EF Core with migrations  
- Validation with FluentValidation  
- Swagger/OpenAPI documentation  
- Angular frontend client  
- Unit tests  
- Docker Compose setup for API and database  

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Node.js & npm](https://nodejs.org/)  
- [Docker & Docker Compose](https://www.docker.com/)  

---

## Run Backend (API)

```bash
cd LoanSimulator.API
dotnet restore
dotnet ef database update
dotnet run --launch-profile "LoanSimulator"
