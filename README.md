# Factory Management System API

Backend APIs for a Factory Management System built with **.NET (Web API)**, **Entity Framework Core (Code First)**, **PostgreSQL**, **JWT Authentication**, and **Swagger**.

## Features
- JWT authentication (Login)
- JWT claims include: `UserId`, `Username`, `Role`
- All endpoints require JWT except login
- Role-based authorization:
  - Admin: full access
  - User: read-only / limited access
- DTO-based request/response models
- Swagger UI with Bearer token support

## Getting Started
1. Configure database connection string in `appsettings.json`:
   - `ConnectionStrings:DefaultConnection`

2. Configure JWT settings in `appsettings.json`:
   - `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`

3. Run migrations / start the API.

## Swagger
Open:
- `https://localhost:7047/swagger/index.html`

## Notes
- Do not commit real secrets (DB passwords / JWT keys) to GitHub.
