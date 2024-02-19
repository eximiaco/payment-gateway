using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public interface IConfigurationRepository
    {
        Task<Maybe<Configuration>> GetByClientIdAndChargeTypeAsync(string clientId, ECaptureType chargeType, CancellationToken cancellationToken = default);
    }
}
