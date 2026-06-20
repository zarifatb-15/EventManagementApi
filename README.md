# Event Management API

A RESTful Web API built with ASP.NET Core for managing events, organizers, tickets, and user accounts. The project includes authentication, authorization, file uploads, validation, exception handling, and unit testing practices commonly used in modern backend development.

---

## Features

- User Registration & Login
- JWT Authentication
- Refresh Token Support
- Email Confirmation
- Forgot Password / Reset Password
- Role-Based Authorization
- Event Management (CRUD)
- Organizer Management (CRUD)
- Ticket Management (CRUD)
- Banner & Logo Upload
- FluentValidation
- AutoMapper
- Repository Pattern
- Service Layer Architecture
- Global Exception Handling Middleware
- Unit Testing (In Progress)
- Swagger Documentation

---

## Tech Stack

| Technology | Purpose |
|------------|----------|
| ASP.NET Core Web API | Backend Framework |
| Entity Framework Core | ORM |
| SQL Server | Database |
| ASP.NET Identity | Authentication & Authorization |
| JWT | Access Tokens |
| AutoMapper | Object Mapping |
| FluentValidation | Request Validation |
| Swagger | API Documentation |
| xUnit | Unit Testing |
| Moq | Mocking |

---

## Project Structure

```text
WebApplicationApi
│
├── Controllers
├── Services
├── Repositories
├── Data
├── Entity
├── Dtos
├── Validators
├── Profiles
├── Helpers
├── Extensions
├── Attributes
├── Middleware
└── UnitTests
```

---

## Authentication Endpoints

| Method | Endpoint |
|----------|----------|
| POST | /api/Account/register |
| POST | /api/Account/login |
| GET | /api/Account/profile |
| GET | /api/Account/forgotpassword |
| POST | /api/Account/resetpassword |
| GET | /api/Account/confirmemail |
| POST | /api/Account/refreshtoken |
| POST | /api/Account/revoke |

---

## Event Endpoints

| Method | Endpoint |
|----------|----------|
| GET | /api/Events |
| POST | /api/Events |
| PUT | /api/Events/{id} |
| DELETE | /api/Events/{id} |
| POST | /api/Events/{id}/banner |
| GET | /api/Events/{eventId}/tickets |
| POST | /api/Events/{eventId}/tickets |
| GET | /api/Events/{eventId}/organizer |

---

## Organizer Endpoints

| Method | Endpoint |
|----------|----------|
| GET | /api/Organizers |
| POST | /api/Organizers |
| GET | /api/Organizers/{id} |
| PUT | /api/Organizers/{id} |
| DELETE | /api/Organizers/{id} |
| POST | /api/Organizers/{id}/logo |
| GET | /api/Organizers/{organizerId}/events |

---

## Ticket Endpoints

| Method | Endpoint |
|----------|----------|
| GET | /api/Tickets |
| POST | /api/Tickets |
| GET | /api/Tickets/{id} |
| PUT | /api/Tickets/{id} |
| DELETE | /api/Tickets/{id} |

---

## Validation

Request validation is implemented using FluentValidation to ensure incoming data is validated before business logic execution.

---

## Exception Handling

The application uses a custom global exception middleware to provide consistent API responses and centralized error handling.

---

## File Upload

The API supports:

- Event Banner Upload
- Organizer Logo Upload

Uploaded files are stored under:

```text
wwwroot/uploads
```

---

## Running the Project

### Clone Repository

```bash
git clone https://github.com/zarifatb-15/taskapi.git
```

### Navigate to Project

```bash
cd taskapi
```

### Restore Packages

```bash
dotnet restore
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run Application

```bash
dotnet run
```

Swagger UI:

```text
https://localhost:{port}/swagger
```

---

## Unit Testing

The project includes a dedicated UnitTests project.

Current status:

- Basic test infrastructure created
- Additional service and controller tests planned

---

## Future Improvements

- Clean Architecture
- Redis Caching
- Docker Support
- CI/CD Pipeline
- Azure Deployment
- Integration Testing
- Logging with Serilog

---

## Author

**Zarifa Babayeva**

GitHub:
https://github.com/zarifatb-15

---

## License

This project is created for educational and portfolio purposes.
