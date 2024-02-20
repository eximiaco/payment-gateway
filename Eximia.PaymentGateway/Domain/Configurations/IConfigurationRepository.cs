using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public interface IConfigurationRepository
    {
        Task<Maybe<Configuration>> GetByClientIdAndCaptureTypeAsync(string clientId, ECaptureType chargeType, CancellationToken cancellationToken = default);
    }
}
