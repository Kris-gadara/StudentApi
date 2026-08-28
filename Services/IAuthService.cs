namespace StudentApi.Services
{
    public interface IAuthService
    {
        string? GenerateToken(string username, string role);
        bool ValidateCredentials(string username, string password);
    }
}
