using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Configurations.Auth
{
    public interface IAuthStrategy
    {
        Task<Result<Dictionary<EAuthOptions, string>>> AuthAsync(CancellationToken cancellationToken = default);
    }
}
