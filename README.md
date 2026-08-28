# StudentApi

A modern **ASP.NET Core 10 Web API** project demonstrating enterprise-level architecture patterns, JWT authentication, authorization, and best practices suitable for portfolio, interviews, and production use.

## 🎯 Features

- **RESTful API** - Clean REST endpoints with proper HTTP semantics
- **JWT Authentication** - Secure token-based authentication with role-based authorization
- **Entity Framework Core** - ORM for SQL Server database operations
- **Repository Pattern** - Clean separation of data access concerns
- **Service Layer** - Business logic encapsulation
- **Dependency Injection** - Built-in ASP.NET Core DI container
- **Data Validation** - Input validation using DataAnnotations
- **Global Exception Handling** - Middleware for consistent error responses
- **CORS** - Cross-Origin Resource Sharing for development
- **Swagger/OpenAPI** - Interactive API documentation with JWT support
- **Async/Await** - Asynchronous operations throughout

## 🏗️ Architecture

The project follows a **layered architecture**:

```
Client (HTTP Request)
  ↓
Controller Layer (HTTP handling, routing)
  ↓
Service Layer (Business logic, DTO mapping)
  ↓
Repository Layer (Data access abstraction)
  ↓
Entity Framework Core (ORM)
  ↓
SQL Server (StudentDB)
```

## 📁 Project Structure

```
StudentApi/
├── Controllers/
│   ├── AuthController.cs          # Login endpoint
│   └── StudentController.cs        # Student CRUD endpoints
│
├── Models/
│   └── Student.cs                 # Student entity
│
├── DTOs/
│   ├── LoginDto.cs                # Login request
│   ├── StudentCreateDto.cs        # Create student request
│   ├── StudentUpdateDto.cs        # Update student request
│   └── StudentResponseDto.cs      # Student response
│
├── Services/
│   ├── IStudentService.cs         # Student service interface
│   ├── StudentService.cs          # Student business logic
│   ├── IAuthService.cs            # Auth service interface
│   └── AuthService.cs             # JWT generation logic
│
├── Repositories/
│   ├── IStudentRepository.cs      # Repository interface
│   └── StudentRepository.cs       # Data access implementation
│
├── Data/
│   └── ApplicationDbContext.cs    # Entity Framework DbContext
│
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs  # Global error handling
│
├── Program.cs                     # Application configuration
├── appsettings.json               # Configuration settings
├── appsettings.Development.json   # Development settings
└── README.md                      # This file
```

## 🚀 Getting Started

### Prerequisites

- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **SQL Server** - Local or remote instance
- **Visual Studio 2026** or Visual Studio Code

### Database Setup

1. **Create the database and table** using SQL Server Management Studio:

```sql
-- Create database
CREATE DATABASE StudentDB;

-- Use database
USE StudentDB;

-- Create Students table
CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Course NVARCHAR(100) NOT NULL
);
```

2. **Update connection string** in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Running the Application

1. **Clone the repository**:
```bash
git clone https://github.com/Kris-gadara/StudentApi.git
cd StudentApi
```

2. **Restore NuGet packages**:
```bash
dotnet restore
```

3. **Build the project**:
```bash
dotnet build
```

4. **Run the application**:
```bash
dotnet run
```

5. **Access the API**:
   - **Scalar UI (Swagger)**: http://localhost:5070/scalar/v1
   - **OpenAPI JSON**: http://localhost:5070/openapi/v1.json
   - **API Base URL**: http://localhost:5070/api
   - **Home Page**: http://localhost:5070/

## 🔐 Authentication & Authorization

### Demo Credentials

The project includes hardcoded demo credentials for learning purposes:

| Username | Password | Role |
|----------|----------|------|
| admin | Admin@123 | Admin |
| user | User@123 | User |

> ⚠️ **IMPORTANT**: These credentials are for development/demo only. In production, use Azure Key Vault, environment variables, or a proper identity provider.

### JWT Configuration

JWT settings are configured in `appsettings.json`:

```json
"Jwt": {
  "Key": "this-is-a-development-only-secret-key-change-in-production-use-environment-variables-or-azure-keyvault",
  "Issuer": "StudentApi",
  "Audience": "StudentApiClient",
  "ExpiresInMinutes": 60
}
```

**For Production**:
1. Use `dotnet user-secrets` or environment variables
2. Use Azure Key Vault
3. Use a strong, randomly generated key (minimum 32 characters)

## 🔑 Authentication Flow

### 1. Login and Get Token

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin@123"
}
```

**Response (200 OK)**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "role": "Admin",
  "expiresInSeconds": 3600
}
```

### 2. Use Token in Subsequent Requests

Add the token to the `Authorization` header:

```http
GET /api/student
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 3. Token Expiration

- Tokens expire after **60 minutes** (configurable)
- Expired tokens return **401 Unauthorized**
- Request a new token via login endpoint

## 🔎 Swagger / OpenAPI Documentation

### Accessing Swagger UI

1. **Start the application**:
   ```bash
   dotnet run
   ```

2. **Open Scalar UI** (recommended modern Swagger alternative):
   - Navigate to: http://localhost:5070/scalar/v1
   - View and test all API endpoints
   - Use the Authorize button to add JWT token

3. **Alternative - OpenAPI JSON**:
   - Raw OpenAPI spec: http://localhost:5070/openapi/v1.json
   - Use with Postman, Insomnia, or other API clients

### Testing with Swagger UI

1. Click **Authorize** button
2. Log in via `/api/Auth/login` endpoint
3. Copy the returned **token** value
4. Paste token in Authorize popup (without "Bearer " prefix)
5. Click **Authorize** - all requests now include JWT
6. Test other endpoints directly from the UI

### Endpoint Documentation

All endpoints are documented in the Swagger UI with:
- Request/response examples
- Required parameters
- Authorization requirements
- HTTP status codes

## 📡 API Endpoints

### Authentication Endpoints

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin@123"
}
```

**Responses**:
- `200 OK` - Login successful, returns JWT token
- `401 Unauthorized` - Invalid credentials

---

### Student Endpoints

#### Get All Students
```http
GET /api/student
Authorization: Bearer <token>
```

**Requirements**: `[Authorize]` - Any authenticated user

**Response (200 OK)**:
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "age": 21,
    "course": "Computer Science"
  }
]
```

#### Get Student by ID
```http
GET /api/student/{id}
Authorization: Bearer <token>
```

**Requirements**: `[Authorize]` - Any authenticated user

**Responses**:
- `200 OK` - Student found
- `401 Unauthorized` - Missing/invalid token
- `404 Not Found` - Student not found

#### Create Student
```http
POST /api/student
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Jane Doe",
  "email": "jane@example.com",
  "age": 20,
  "course": "Data Science"
}
```

**Requirements**: `[Authorize(Roles = "Admin")]` - Admin only

**Responses**:
- `201 Created` - Student created successfully
- `400 Bad Request` - Invalid input data
- `401 Unauthorized` - Missing/invalid token
- `403 Forbidden` - Insufficient permissions (not Admin)

#### Update Student
```http
PUT /api/student/{id}
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Jane Smith",
  "email": "jane.smith@example.com",
  "age": 21,
  "course": "Machine Learning"
}
```

**Requirements**: `[Authorize(Roles = "Admin")]` - Admin only

**Responses**:
- `204 No Content` - Update successful
- `400 Bad Request` - Invalid input data
- `401 Unauthorized` - Missing/invalid token
- `403 Forbidden` - Insufficient permissions (not Admin)
- `404 Not Found` - Student not found

#### Delete Student
```http
DELETE /api/student/{id}
Authorization: Bearer <token>
```

**Requirements**: `[Authorize(Roles = "Admin")]` - Admin only

**Responses**:
- `204 No Content` - Delete successful
- `401 Unauthorized` - Missing/invalid token
- `403 Forbidden` - Insufficient permissions (not Admin)
- `404 Not Found` - Student not found

---

## 🧪 Testing with Swagger UI

### 1. Start the Application
```bash
dotnet run
```

### 2. Open Swagger UI
Navigate to `https://localhost:7001` (or your configured port)

### 3. Login
- Click **Try it out** on the `/api/auth/login` endpoint
- Enter demo credentials:
  ```json
  {
    "username": "admin",
    "password": "Admin@123"
  }
  ```
- Copy the `token` from the response

### 4. Authorize in Swagger
- Click the **Authorize** button (lock icon) at the top
- Paste the token: `Bearer <your-token>`
- Click **Authorize**

### 5. Test Protected Endpoints
- All Student endpoints now work with your token
- Try GET, POST, PUT, DELETE operations

## 📊 Validation Rules

### Student Create/Update DTO

| Field | Rule | Error Message |
|-------|------|---------------|
| Name | Required, 2-100 chars | "Name is required" / "Name must be between 2 and 100 characters" |
| Email | Required, valid format | "Email is required" / "Email format is invalid" |
| Age | Required, 18-100 | "Age is required" / "Age must be between 18 and 100" |
| Course | Required, 1-100 chars | "Course is required" / "Course must be between 1 and 100 characters" |

**Invalid request example**:
```http
POST /api/student
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "J",           // Too short
  "email": "invalid",    // Invalid format
  "age": 15,            // Too young
  "course": ""          // Empty
}
```

**Response (400 Bad Request)**:
```json
{
  "errors": {
    "Name": ["Name must be between 2 and 100 characters"],
    "Email": ["Email format is invalid"],
    "Age": ["Age must be between 18 and 100"],
    "Course": ["'Course' must not be empty."]
  },
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400
}
```

## 🔄 HTTP Status Codes

| Code | Meaning | Endpoint |
|------|---------|----------|
| 200 | OK | GET requests, Login success |
| 201 | Created | POST student created successfully |
| 204 | No Content | PUT/DELETE successful |
| 400 | Bad Request | Invalid input data |
| 401 | Unauthorized | Missing/invalid JWT token |
| 403 | Forbidden | Valid token but insufficient permissions |
| 404 | Not Found | Resource not found |
| 500 | Internal Server Error | Unhandled exception |

## 🛡️ Security Best Practices

### What's Implemented

✅ JWT-based authentication  
✅ Role-based authorization  
✅ Input validation  
✅ HTTPS redirection  
✅ CORS configuration  
✅ Global exception handling (no stack traces in production)  
✅ Secure password stored in config (development only)  

### What to Add for Production

- [ ] Move secrets to Azure Key Vault or environment variables
- [ ] Use user secrets manager during development
- [ ] Implement refresh token mechanism
- [ ] Add rate limiting middleware
- [ ] Enable HTTPS only
- [ ] Implement audit logging
- [ ] Use proper database password storage (hashing)
- [ ] Add password reset/change functionality
- [ ] Implement 2FA authentication
- [ ] Use HTTPS certificates

## 📦 NuGet Packages

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.11" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.11" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.11" />
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.11" />
  <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.19.2" />
  <PackageReference Include="Swashbuckle.AspNetCore" Version="7.2.0" />
</ItemGroup>
```

## 🐛 Troubleshooting

### "Connection string not configured"
- **Error**: `JWT Key is not configured`
- **Solution**: Ensure `appsettings.json` has all JWT settings

### "Database connection failed"
- **Error**: Login failed for user/connection timeout
- **Solution**: Check connection string and SQL Server is running

### "401 Unauthorized on protected endpoint"
- **Error**: "The authorization header is missing or invalid"
- **Solution**: Include `Authorization: Bearer <token>` header

## ✨ Interview-Ready Features

This project demonstrates:

- ✅ Clean Architecture principles
- ✅ SOLID principles
- ✅ Async/await programming
- ✅ Dependency Injection
- ✅ Repository Pattern
- ✅ Service Layer abstraction
- ✅ JWT authentication mechanism
- ✅ Role-based authorization
- ✅ Global exception handling
- ✅ Data validation with DataAnnotations
- ✅ OpenAPI/Swagger documentation
- ✅ RESTful API design

## 🧪 Testing & Verification

### Quick Test Checklist

1. **Build Project**:
   ```bash
   dotnet build
   ```
   ✅ Expected: Build successful, no errors

2. **Run Application**:
   ```bash
   dotnet run
   ```
   ✅ Expected: App starts on http://localhost:5070

3. **Verify Root Page**:
   - Open browser: http://localhost:5070/
   ✅ Expected: Welcome page with dashboard

4. **Test Swagger UI**:
   - Open browser: http://localhost:5070/scalar/v1
   ✅ Expected: Interactive API documentation

5. **Test Login**:
   ```bash
   curl -X POST http://localhost:5070/api/Auth/login \
     -H "Content-Type: application/json" \
     -d '{"username":"admin","password":"Admin@123"}'
   ```
   ✅ Expected: Returns JWT token, username, role, expiresInSeconds

6. **Test Protected Endpoint**:
   - Use token from login step
   ```bash
   curl http://localhost:5070/api/student \
     -H "Authorization: Bearer <your-token-here>"
   ```
   ✅ Expected: Returns list of students (200 OK)

7. **Test Unauthorized Access**:
   ```bash
   curl http://localhost:5070/api/student
   ```
   ✅ Expected: 401 Unauthorized

8. **Test Admin-Only Endpoint**:
   - Login as admin (see step 5)
   - Try POST with admin token
   ✅ Expected: Successfully creates student (201 Created)
   - Login as user and repeat
   ✅ Expected: 403 Forbidden

### Expected Test Results

| Endpoint | Method | Auth | Status | Description |
|----------|--------|------|--------|-------------|
| / | GET | No | 200 | Home page dashboard |
| /openapi/v1.json | GET | No | 200 | OpenAPI specification |
| /scalar/v1 | GET | No | 200 | Swagger UI |
| /api/Auth/login | POST | No | 200/401 | Login with credentials |
| /api/student | GET | Yes | 200 | Get all students |
| /api/student/{id} | GET | Yes | 200/404 | Get student by ID |
| /api/student | POST | Admin | 201 | Create student (admin only) |
| /api/student/{id} | PUT | Admin | 204 | Update student (admin only) |
| /api/student/{id} | DELETE | Admin | 204 | Delete student (admin only) |

## 🐛 Troubleshooting

### "401 Unauthorized on protected endpoints"
- **Issue**: Missing or invalid JWT token
- **Solution**: Make sure to login first and use the returned token in Authorization header

### "403 Forbidden on Admin endpoint"
- **Error**: "User does not have permission"
- **Solution**: Login with admin account (username: `admin`)

### "Port already in use"
- **Error**: "Address already in use"
- **Solution**: Change port in `Properties/launchSettings.json`

## 📚 Key Technologies

- **Framework**: ASP.NET Core 10
- **Language**: C# 13
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI with Scalar UI
- **Architecture**: Repository Pattern, Service Layer, Dependency Injection

## 🤝 Contributing

This is a portfolio/educational project. For improvements or fixes:

1. Create a feature branch
2. Make your changes
3. Test thoroughly
4. Submit a pull request

## 📝 License

This project is open source and available under the MIT License.

## ✨ Interview-Ready Features

This project demonstrates:

- ✅ Clean Architecture principles
- ✅ SOLID principles
- ✅ Async/await programming
- ✅ Dependency Injection
- ✅ Repository Pattern
- ✅ Service Layer abstraction
- ✅ JWT authentication mechanism
- ✅ Role-based authorization
- ✅ Input validation
- ✅ Error handling
- ✅ REST API best practices
- ✅ Entity Framework Core
- ✅ SQL Server integration
- ✅ Code organization and structure

## 🚀 Future Enhancements

Consider adding:
- Pagination and filtering for student list
- Search functionality
- Logging with Serilog
- Unit and integration tests
- API versioning
- Caching strategy
- Background jobs with Hangfire
- Event sourcing
- CQRS pattern
- Distributed tracing

---

**Created**: 2024  
**Last Updated**: 2024  
**ASP.NET Core Version**: 10  
**Status**: Production-ready portfolio project
