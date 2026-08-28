using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers
{
    /// <summary>
    /// Home controller - provides information about the API
    /// </summary>
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Welcome page with links to API documentation
        /// </summary>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var htmlContent = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>StudentApi - ASP.NET Core Web API</title>
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
        }
        .container {
            background: white;
            border-radius: 10px;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
            padding: 50px;
            max-width: 600px;
            width: 100%;
        }
        h1 {
            color: #333;
            margin-bottom: 10px;
            font-size: 2.5em;
        }
        .subtitle {
            color: #666;
            margin-bottom: 30px;
            font-size: 1.1em;
        }
        .badge {
            display: inline-block;
            background: #667eea;
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 0.9em;
            margin-bottom: 20px;
        }
        .links {
            display: grid;
            gap: 15px;
            margin-top: 30px;
        }
        .link {
            display: flex;
            align-items: center;
            padding: 15px;
            border: 2px solid #e0e0e0;
            border-radius: 8px;
            text-decoration: none;
            color: #333;
            transition: all 0.3s ease;
        }
        .link:hover {
            border-color: #667eea;
            background: #f5f5ff;
            transform: translateX(5px);
        }
        .link-icon {
            font-size: 1.5em;
            margin-right: 15px;
            min-width: 30px;
        }
        .link-content h3 {
            margin: 0;
            color: #667eea;
            font-size: 1.1em;
        }
        .link-content p {
            margin: 5px 0 0 0;
            color: #999;
            font-size: 0.9em;
        }
        .credentials {
            margin-top: 40px;
            padding: 20px;
            background: #f5f5f5;
            border-left: 4px solid #667eea;
            border-radius: 5px;
        }
        .credentials h3 {
            color: #333;
            margin-bottom: 15px;
        }
        .credential-item {
            margin-bottom: 10px;
            font-size: 0.95em;
        }
        .credential-item strong {
            color: #667eea;
        }
        .footer {
            margin-top: 30px;
            text-align: center;
            color: #999;
            font-size: 0.9em;
        }
        .status {
            display: flex;
            gap: 10px;
            margin-top: 20px;
            flex-wrap: wrap;
        }
        .status-item {
            flex: 1;
            min-width: 120px;
            text-align: center;
            padding: 10px;
            background: #f0f0f0;
            border-radius: 5px;
        }
        .status-item .label {
            color: #999;
            font-size: 0.85em;
            margin-bottom: 5px;
        }
        .status-item .value {
            color: #333;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""badge"">✓ ASP.NET Core 10 API</div>
        <h1>StudentApi</h1>
        <p class=""subtitle"">RESTful Web API with JWT Authentication & Authorization</p>

        <div class=""status"">
            <div class=""status-item"">
                <div class=""label"">Status</div>
                <div class=""value"" style=""color: #4caf50;"">🟢 Running</div>
            </div>
            <div class=""status-item"">
                <div class=""label"">Environment</div>
                <div class=""value"">Development</div>
            </div>
            <div class=""status-item"">
                <div class=""label"">Auth</div>
                <div class=""value"">JWT Enabled</div>
            </div>
        </div>

        <div class=""links"">
            <a href=""/openapi/v1.json"" class=""link"" target=""_blank"">
                <div class=""link-icon"">📋</div>
                <div class=""link-content"">
                    <h3>OpenAPI Specification</h3>
                    <p>View the complete API specification in JSON format</p>
                </div>
            </a>

            <a href=""https://localhost:7047/openapi/v1.json"" class=""link"" target=""_blank"">
                <div class=""link-icon"">🔒</div>
                <div class=""link-content"">
                    <h3>HTTPS (Secure)</h3>
                    <p>Access API via HTTPS on port 7047</p>
                </div>
            </a>

            <a href=""#"" onclick=""alert('Import the OpenAPI spec (http://localhost:5070/openapi/v1.json) into Postman or Insomnia to test endpoints'); return false;"" class=""link"">
                <div class=""link-icon"">🧪</div>
                <div class=""link-content"">
                    <h3>Test Endpoints</h3>
                    <p>Import OpenAPI spec into Postman or Insomnia</p>
                </div>
            </a>

            <a href=""https://github.com/Kris-gadara/StudentApi"" class=""link"" target=""_blank"">
                <div class=""link-icon"">🔗</div>
                <div class=""link-content"">
                    <h3>GitHub Repository</h3>
                    <p>View source code on GitHub</p>
                </div>
            </a>
        </div>

        <div class=""credentials"">
            <h3>📝 Demo Credentials (Login)</h3>
            <div class=""credential-item"">
                <strong>Admin Account:</strong> username: <code>admin</code> | password: <code>Admin@123</code>
            </div>
            <div class=""credential-item"">
                <strong>User Account:</strong> username: <code>user</code> | password: <code>User@123</code>
            </div>
        </div>

        <div class=""credentials"">
            <h3>🔗 API Endpoints</h3>
            <div class=""credential-item"">
                <strong>Login:</strong> POST <code>/api/auth/login</code>
            </div>
            <div class=""credential-item"">
                <strong>Get All Students:</strong> GET <code>/api/student</code>
            </div>
            <div class=""credential-item"">
                <strong>Get Student by ID:</strong> GET <code>/api/student/{id}</code>
            </div>
            <div class=""credential-item"">
                <strong>Create Student:</strong> POST <code>/api/student</code> (Admin only)
            </div>
            <div class=""credential-item"">
                <strong>Update Student:</strong> PUT <code>/api/student/{id}</code> (Admin only)
            </div>
            <div class=""credential-item"">
                <strong>Delete Student:</strong> DELETE <code>/api/student/{id}</code> (Admin only)
            </div>
        </div>

        <div class=""footer"">
            <p>Built with ASP.NET Core 10 • Entity Framework Core • SQL Server • JWT Authentication</p>
        </div>
    </div>
</body>
</html>
";
            return Content(htmlContent, "text/html");
        }
    }
}
