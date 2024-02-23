using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Configurations.Auth;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures.Auth
{
    public class AuthWithClientIdStrategy : IAuthStrategy
    {
        public AuthWithClientIdStrategy(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; }

        public async Task<Result<Dictionary<EAuthOptions, string>>> AuthAsync(CancellationToken cancellationToken = default)
        {
            // Auth with clientId

            return new Dictionary<EAuthOptions, string>()
            {
                 { EAuthOptions.ApiKey, "asdasdasdasd65a1sd-16a5" }
            };
        }
    }
}
