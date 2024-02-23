using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public interface ICaptureTransactionStrategy
    {
        ECaptureType CaptureType { get; }
        EGateway Gateway { get; }

        Task<Result> CaptureAsync(CaptureTransactionContext context, CancellationToken cancellationToken = default);
    }
}
