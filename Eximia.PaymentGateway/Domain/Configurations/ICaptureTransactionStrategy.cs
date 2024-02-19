using Eximia.PaymentGateway.Domain.Transactions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public interface ICaptureTransactionStrategy
    {
        ECaptureType CaptureType { get; }
        EGateway Gateway { get; }

        Task CaptureAsync(IServiceProvider serviceProvider, Transaction transaction, CancellationToken cancellationToken = default);
    }
}
