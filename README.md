# StudentApi

A fresher-level **ASP.NET Core 10 Web API** demonstrating layered architecture with CRUD operations, Entity Framework Core, SQL Server integration, JWT authentication, and role-based authorization. Ideal for portfolio, interviews, and learning.

## 🎯 Features

- **RESTful API** - Clean REST endpoints with proper HTTP semantics
- **JWT Authentication** - Secure token-based authentication with bearer tokens
- **Role-Based Authorization** - Admin and User role authorization
- **Entity Framework Core** - ORM for SQL Server database operations
- **Repository Pattern** - Clean separation of data access concerns
- **Service Layer** - Business logic encapsulation
- **Dependency Injection** - ASP.NET Core built-in DI container
- **Data Validation** - Input validation using DataAnnotations
- **Global Exception Handling** - Middleware for consistent error responses
- **CORS** - Cross-Origin Resource Sharing for development
- **Scalar/OpenAPI** - Modern interactive API documentation
- **Async/Await** - Asynchronous operations throughout

## 🏗️ Architecture

Layered architecture with clear separation of concerns:

```
Client (HTTP Request)
  ↓
Controller Layer (HTTP handling, routing, authorization)
  ↓
Service Layer (Business logic, DTO mapping, validation)
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
│   ├── AuthController.cs              # Login endpoint
│   ├── HomeController.cs               # Health check
│   └── StudentController.cs            # Student CRUD endpoints
│
├── Models/
│   └── Student.cs                      # Student entity
│
├── DTOs/
│   ├── LoginDto.cs                     # Login request
│   ├── StudentCreateDto.cs             # Create student request
│   ├── StudentUpdateDto.cs             # Update student request
│   └── StudentResponseDto.cs           # Student response
│
├── Services/
│   ├── IStudentService.cs              # Student service interface
│   ├── StudentService.cs               # Student business logic
│   ├── IAuthService.cs                 # Auth service interface
│   └── AuthService.cs                  # JWT generation & validation
│
├── Repositories/
│   ├── IStudentRepository.cs           # Repository interface
│   └── StudentRepository.cs            # Data access implementation
│
├── Data/
│   └── ApplicationDbContext.cs         # Entity Framework DbContext
│
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs  # Global error handling
│
├── Properties/
│   └── launchSettings.json             # Launch configuration
│
├── Program.cs                          # Application configuration
├── appsettings.json                    # Configuration settings
├── appsettings.Development.json        # Development settings
└── StudentApi.http                     # HTTP test file
```

## 🚀 Getting Started

### Prerequisites

- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **SQL Server** - Local or remote instance (Express edition is free)
- **Visual Studio 2026** or Visual Studio Code with C# extension

### 1. Database Setup

Create the StudentDB database and Students table using SQL Server Management Studio:

```sql
-- Create database
CREATE DATABASE StudentDB;

-- Use database
USE StudentDB;

-- Create Students table
CREATE TABLE Students (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL UNIQUE,
	Age INT NOT NULL,
	Course NVARCHAR(100) NOT NULL,
	CreatedAt DATETIME DEFAULT GETUTCDATE()
);
```

Or run EF Core migrations (if seed data scripts are available):

```bash
dotnet ef database update
```

### 2. Configure User Secrets (Development)

User Secrets store sensitive configuration locally during development. They are NOT committed to Git.

#### Set JWT Secret for Development:

```bash
dotnet user-secrets init --project StudentApi.csproj
```

Then set your JWT key:

```bash
dotnet user-secrets set "Jwt:Key" "your-development-secret-key-minimum-32-characters-for-HS256" --project StudentApi.csproj
```

**Important**: The key must be **at least 32 characters** for HMAC SHA256 signing.

Example of a valid key:
```
MySecureDevKeyFor32CharactersMinimum!
```

#### Verify Secret is Set:

```bash
dotnet user-secrets list --project StudentApi.csproj
```

You should see:
```
Jwt:Key = your-development-secret-key-minimum-32-characters-for-HS256
```

### 3. Configure Production (Environment Variables)

For production deployment, set the JWT key via environment variables:

```bash
# On Windows (PowerShell)
$env:JWT_Key="your-production-secret-key-minimum-32-characters"

# On Linux/macOS
export JWT_Key="your-production-secret-key-minimum-32-characters"
```

Or add to your deployment platform's secrets/environment configuration (Azure Key Vault, GitHub Secrets, Docker Compose, etc.).

### 4. Run the Application

```bash
cd StudentApi
dotnet run
```

The application will start on `http://localhost:5070` by default (or `https://localhost:7047` for HTTPS).

You should see:
```
info: Microsoft.Hosting.Lifetime[14]
	  Now listening on: http://localhost:5070
info: Microsoft.Hosting.Lifetime[0]
	  Application started. Press Ctrl+C to shut down.
```

### 5. Access Scalar API Documentation

Open your browser and navigate to:

```
http://localhost:5070/scalar/v1
```

This is the modern, interactive API documentation powered by Scalar, a beautiful alternative to traditional Swagger UI.

You can:
- View all endpoints with descriptions
- Test endpoints directly in the browser
- See request/response examples
- Add Bearer tokens for authorization

## 🔐 Authentication & Authorization

### Login (Get JWT Token)

1. **Open Scalar UI** at `http://localhost:5070/scalar/v1`
2. **Find the Auth Controller** → `POST /api/auth/login`
3. **Click "Try it out"** and enter credentials:

**Admin User:**
```json
{
  "username": "admin",
  "password": "Admin@123"
}
```

**Regular User:**
```json
{
  "username": "user",
  "password": "User@123"
}
```

4. **Copy the token** from the response (a long JWT string)

### Use JWT Token for Protected Endpoints

In Scalar UI:

1. **Find a protected endpoint** (e.g., `GET /api/student`)
2. **Click the lock icon** (🔒) or look for "Authorization" section
3. **Add Bearer Token:**
   - Paste your copied token (without quotes)
   - Or in curl: `Authorization: Bearer <token>`

Example with curl:

```bash
curl -H "Authorization: Bearer <your-token-here>" http://localhost:5070/api/student
```

### Admin-Only Endpoints

Some endpoints require Admin role:

```bash
# Endpoint requires Admin role
POST /api/student          # Create (Admin only)
PUT /api/student/{id}      # Update (Admin only)
DELETE /api/student/{id}   # Delete (Admin only)
```

## 📊 API Endpoints

### Authentication

| Endpoint | Method | Auth | Status | Description |
|----------|--------|------|--------|-------------|
| `/api/auth/login` | POST | No | 200 | Request JWT token (admin or user credentials) |

### Students (All require Bearer Token)

| Endpoint | Method | Auth | Role | Status | Description |
|----------|--------|------|------|--------|-------------|
| `/api/student` | GET | Yes | Any | 200 | Get all students |
| `/api/student/{id}` | GET | Yes | Any | 200/404 | Get student by ID |
| `/api/student` | POST | Yes | Admin | 201/400 | Create new student |
| `/api/student/{id}` | PUT | Yes | Admin | 200/404 | Update student |
| `/api/student/{id}` | DELETE | Yes | Admin | 204/404 | Delete student |

### Documentation

| Endpoint | Method | Auth | Status | Description |
|----------|--------|------|--------|-------------|
| `/scalar/v1` | GET | No | 200 | Scalar API UI |
| `/openapi/v1.json` | GET | No | 200 | OpenAPI JSON schema |

### Health Check

| Endpoint | Method | Auth | Status | Description |
|----------|--------|------|--------|-------------|
| `/` | GET | No | 200 | Health check / welcome message |

## 🧪 Testing the API

### Using Scalar UI (Recommended)

1. Navigate to `http://localhost:5070/scalar/v1`
2. Use "Try it out" for each endpoint
3. Automatically handles Bearer token
4. See live request/response examples

### Using REST Client Extension (VS Code)

Open `StudentApi.http` and use the VSCode REST Client extension to test:

```http
### Login and get token
POST http://localhost:5070/api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin@123"
}

### Get all students
@token=<paste-token-here>
GET http://localhost:5070/api/student
Authorization: Bearer @token
```

### Using cURL

```bash
# Login
curl -X POST http://localhost:5070/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"Admin@123"}'

# Copy the token from response, then:

# Get all students
curl -H "Authorization: Bearer <your-token>" \
  http://localhost:5070/api/student

# Create new student (Admin only)
curl -X POST http://localhost:5070/api/student \
  -H "Authorization: Bearer <your-token>" \
  -H "Content-Type: application/json" \
  -d '{
	"name": "John Doe",
	"email": "john@example.com",
	"age": 21,
	"course": "Computer Science"
  }'
```

### Using Postman

1. Create a new request in Postman
2. **Login Endpoint:**
   - Method: POST
   - URL: `http://localhost:5070/api/auth/login`
   - Body (JSON):
	 ```json
	 {
	   "username": "admin",
	   "password": "Admin@123"
	 }
	 ```
   - Send and copy the `token` value

3. **Protected Endpoints:**
   - Add Authorization tab → Bearer Token
   - Paste the token
   - Test any Student endpoint

## 🔧 Configuration

### Development Configuration

**appsettings.Development.json** overrides `appsettings.json` during development:

```json
{
  "Logging": {
	"LogLevel": {
	  "Default": "Information",
	  "Microsoft.AspNetCore": "Warning"
	}
  }
}
```

JWT secret is loaded from User Secrets (not in this file).

### JWT Configuration

All JWT settings are in `appsettings.json`:

```json
"Jwt": {
  "Key": "PLACEHOLDER_REPLACE_WITH_APPSETTINGS_json_OR_ENVIRONMENT_VARIABLE_JWT_Key",
  "Issuer": "StudentApi",
  "Audience": "StudentApiClient",
  "ExpiresInMinutes": 60
}
```

- **Key**: Read from User Secrets (dev) or Environment Variable (prod)
- **Issuer**: Who creates the token (your API)
- **Audience**: Who should accept the token (your API clients)
- **ExpiresInMinutes**: Token validity duration (60 minutes)

### Database Configuration

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Change `localhost` or `Database=StudentDB` as needed for your SQL Server instance.

## 🐛 Troubleshooting

### "JWT Key is not configured"

**Error**: `InvalidOperationException: JWT Key is not configured`

**Solution**: Set the User Secret:

```bash
dotnet user-secrets set "Jwt:Key" "your-secret-key-minimum-32-characters" --project StudentApi.csproj
```

### "Cannot connect to database"

**Error**: `InvalidOperationException: Named connection string 'DefaultConnection' not found`

**Solution**:
1. Verify SQL Server is running
2. Update connection string in `appsettings.json`
3. Ensure StudentDB exists

### "Unauthorized" on protected endpoints

**Possible causes**:
1. Token not included in `Authorization: Bearer <token>`
2. Token is expired (default: 60 minutes)
3. Wrong token (from different app/issuer)

**Solution**: Regenerate token by logging in again.

### Port already in use

**Error**: `System.IO.IOException: Failed to bind to address`

**Solution**: Change port in `Properties/launchSettings.json` or kill process on port 5070.

## 📚 Technology Stack

- **.NET 10** - Latest LTS version, built-in dependency injection and middleware
- **ASP.NET Core** - Web API framework
- **Entity Framework Core 10** - ORM for database operations
- **SQL Server** - Relational database
- **JWT Bearer** - Token-based authentication
- **Scalar/OpenAPI** - Interactive API documentation
- **DataAnnotations** - Built-in validation attributes

## 🎓 Learning Outcomes

This project demonstrates:
- ✅ RESTful API design with proper HTTP semantics
- ✅ Layered architecture (Controllers → Services → Repositories)
- ✅ JWT authentication and Bearer token authorization
- ✅ Role-based access control (Admin/User)
- ✅ Entity Framework Core with SQL Server
- ✅ Repository pattern for data access
- ✅ Dependency injection and inversion of control
- ✅ Global exception handling middleware
- ✅ DTOs for request/response validation
- ✅ CORS configuration for cross-origin requests
- ✅ Async/await for non-blocking operations
- ✅ User Secrets for secure local development
- ✅ Environment-based configuration

## 📝 Environment Setup Checklist

- [ ] .NET 10 SDK installed
- [ ] SQL Server installed and running
- [ ] StudentDB database created
- [ ] User Secrets initialized: `dotnet user-secrets init`
- [ ] JWT key set: `dotnet user-secrets set "Jwt:Key" "your-key"`
- [ ] Connection string updated in `appsettings.json`
- [ ] `dotnet run` starts without errors
- [ ] Scalar UI opens at `http://localhost:5070/scalar/v1`
- [ ] Login returns valid JWT token
- [ ] Protected endpoints accept bearer token
- [ ] CRUD operations work (create, read, update, delete)

## 🚢 Production Deployment

### Set JWT Secret via Environment Variable

Before deploying, ensure your production environment has:

```bash
JWT_Key=<your-production-secret-key>
```

The application will read this automatically via `IConfiguration`.

### Steps

1. Build release: `dotnet publish -c Release`
2. Deploy binaries to production server
3. Set production environment variables in your deployment platform
4. Run application (container, IIS, etc.)
5. Verify Scalar UI and endpoints are accessible
6. Test JWT login with production credentials

## 📧 Demo Credentials

For testing purposes only (not for production):

| Username | Password  | Role  |
|----------|-----------|-------|
| admin    | Admin@123 | Admin |
| user     | User@123  | User  |

## 📄 License

This is a learning/portfolio project. Use for educational and interview purposes.

## 🔗 Repository

GitHub: [https://github.com/Kris-gadara/StudentApi](https://github.com/Kris-gadara/StudentApi)

---

**Status**: Learning/portfolio project | **Version**: 1.0 | **.NET Target**: 10.0
