# HahnCreditSim

This project demonstrates a clean architecture application built with .NET 9 and TypeScript, following Domain-Driven Design (DDD) and CQRS principles. It is developed by Hahn Softwareentwicklung GmbH.

## Features

- Layered backend architecture: Domain, Application, Infrastructure, API
- CQRS with commands and queries
- EF Core with migrations for data persistence
- Validation using FluentValidation
- Basic domain events implemented
- Swagger/OpenAPI for API documentation
- Frontend built with Angular
- Unit tests included
- Docker Compose setup for API and database

## Getting Started

### Prerequisites

- .NET 9 SDK
- Node.js and npm
- Docker and Docker Compose

### Setup

1. **Backend API**

```bash
cd HahnCreditSim.API
dotnet run --launch-profile http
