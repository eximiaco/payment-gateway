using Eximia.PaymentGateway.Domain.Configurations.Auth;
using Eximia.PaymentGateway.Domain.Transactions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public record CaptureTransactionContext(
        IServiceProvider ServiceProvider,
        Transaction Transaction,
        Dictionary<EAuthOptions, string> AuthResult);
}
