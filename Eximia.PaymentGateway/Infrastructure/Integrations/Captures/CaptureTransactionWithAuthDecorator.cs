using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Domain.Transactions;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures
{
    public class CaptureTransactionWithAuthDecorator
    {
        private readonly IServiceProvider _serviceProvider;

        public CaptureTransactionWithAuthDecorator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result> CaptureAsync(Configuration configuration, Transaction transaction, CancellationToken cancellationToken = default)
        {
            var authResult = await configuration.AuthStrategy.AuthAsync(cancellationToken).ConfigureAwait(false);
            if (authResult.IsFailure)
                return authResult;

            CaptureTransactionContext context = new(_serviceProvider, transaction, authResult.Value);
            var result = await configuration.CaptureTransactionStrategy.CaptureAsync(context, cancellationToken).ConfigureAwait(false);
            if (result.IsFailure)
                return result;

            return Result.Success();
        }
    }
}
