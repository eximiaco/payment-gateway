namespace Eximia.PaymentGateway.Domain.Configurations.Auth
{
    public record AuthResult(string? ApiKey, string? Username, string? Password)
    {
        public static AuthResult CreateApiKey(string apiKey) => new AuthResult(apiKey, string.Empty, string.Empty);
        public static AuthResult CreateBasic(string username, string password) => new AuthResult(string.Empty, username, password);
    }
}
