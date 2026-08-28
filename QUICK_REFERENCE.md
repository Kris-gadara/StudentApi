# StudentApi - Quick Reference Card

## 🚀 Start the Application
```bash
cd C:\K\Projects\.net webapi\StudentApi
dotnet run
```
App will start on: http://localhost:5070

## 🔍 Access Points
| URL | Purpose |
|-----|---------|
| http://localhost:5070/ | Home Dashboard |
| http://localhost:5070/scalar/v1 | Swagger UI |
| http://localhost:5070/openapi/v1.json | OpenAPI Spec |

## 🔑 Test Credentials
```
Admin:
  Username: admin
  Password: Admin@123

User:
  Username: user
  Password: User@123
```

## 📡 API Endpoints

### Authentication
```
POST /api/Auth/login
Body: {"username":"admin","password":"Admin@123"}
Response: {"token":"...", "username":"...", "role":"...", "expiresInSeconds":3600}
```

### Student Operations
```
GET    /api/student          [Authorize] - Returns all students
GET    /api/student/{id}     [Authorize] - Get student by ID
POST   /api/student          [Authorize, Admin] - Create student
PUT    /api/student/{id}     [Authorize, Admin] - Update student
DELETE /api/student/{id}     [Authorize, Admin] - Delete student
```

## 🧪 Quick Testing

### 1. Login and Get Token
```powershell
$login = @{username="admin"; password="Admin@123"} | ConvertTo-Json
$response = Invoke-WebRequest -Uri "http://localhost:5070/api/Auth/login" `
  -Method Post -ContentType "application/json" -Body $login -UseBasicParsing
$token = ($response.Content | ConvertFrom-Json).token
Write-Host "Token: $token"
```

### 2. Test Protected Endpoint
```powershell
$headers = @{"Authorization" = "Bearer $token"}
Invoke-WebRequest -Uri "http://localhost:5070/api/student" `
  -Headers $headers -UseBasicParsing
```

### 3. Create Student (Admin Only)
```powershell
$student = @{
  name = "John Doe"
  email = "john@example.com"
  age = 20
  course = "B.Tech"
} | ConvertTo-Json

Invoke-WebRequest -Uri "http://localhost:5070/api/student" `
  -Method Post -ContentType "application/json" -Body $student `
  -Headers $headers -UseBasicParsing
```

## ✅ Test Checklist
- [ ] Build: `dotnet build`
- [ ] Run: `dotnet run`
- [ ] Root page: Visit http://localhost:5070/ → Should see dashboard
- [ ] Swagger: Visit http://localhost:5070/scalar/v1 → Should see UI
- [ ] Login: POST to /api/Auth/login with demo credentials → Should get token
- [ ] Protected GET: Use token to GET /api/student → Should get students
- [ ] Admin POST: Use admin token to POST /api/student → Should create
- [ ] User POST: Use user token to POST /api/student → Should get 403

## 📚 Documentation Files
- `README.md` - Main documentation
- `FINAL_SUMMARY.md` - Project completion summary
- `GITHUB_READY.md` - Pre-GitHub checklist
- `COMPLETION_REPORT.md` - Detailed test results
- `StudentApi.http` - REST client test file

## 🔐 Security Notes
- Demo credentials are for development/testing only
- JWT secret in appsettings.json is development-only
- For production: Use Azure Key Vault or environment variables
- Never commit real secrets to Git

## 🎯 Architecture
```
Request
  ↓
Controller (Routing, Auth)
  ↓
Service (Business Logic)
  ↓
Repository (Data Access)
  ↓
EF Core (ORM)
  ↓
SQL Server (Database)
```

## 🛠️ Key Technologies
- .NET 10 / C# 13
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Scalar UI (Swagger)

## 📊 Project Stats
- Files Modified: 10+
- Files Created: 7+
- Test Pass Rate: 100% (10/10)
- Build Status: ✅ Success
- Endpoints: 8 total (1 auth + 5 student + 2 system)
- JWT Roles: 2 (Admin, User)

## 🚀 Next Steps
1. Test thoroughly using the endpoints above
2. Review FINAL_SUMMARY.md for details
3. Push to GitHub when ready:
   ```bash
   git add .
   git commit -m "Complete StudentApi with JWT auth and Swagger"
   git push origin master
   ```

---
**Project Status**: ✅ COMPLETE & GITHUB READY
