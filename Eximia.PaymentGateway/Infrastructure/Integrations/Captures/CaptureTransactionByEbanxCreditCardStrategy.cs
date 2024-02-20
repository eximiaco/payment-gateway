using Eximia.PaymentGateway.Domain;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Domain.Transactions;
using Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx;
using Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures
{
    public class CaptureTransactionByEbanxCreditCardStrategy : ICaptureTransactionStrategy
    {
        public ECaptureType CaptureType => ECaptureType.CreditCard;
        public EGateway Gateway => EGateway.Ebanx;

        public async Task CaptureAsync(IServiceProvider serviceProvider, Transaction transaction, CancellationToken cancellationToken = default)
        {
            var ebanxApi = serviceProvider.GetRequiredService<EbanxApi>();

            var request = new CaptureEbanxCreditCardDto(); // Create gateway request body
            var gatewayResult = await ebanxApi.CaptureCreditCardAsync(request, cancellationToken).ConfigureAwait(false);

            transaction.Capture(gatewayResult.Status, gatewayResult.CaptureResult);
        }
    }
}
