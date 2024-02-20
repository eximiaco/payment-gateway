using Eximia.PaymentGateway.Domain;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Domain.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe;
using Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe.DTOs;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures
{
    public class CaptureTransactionByPagarMePixStrategy : ICaptureTransactionStrategy
    {
        public ECaptureType CaptureType => ECaptureType.Pix;
        public EGateway Gateway => EGateway.PagarMe;

        public async Task CaptureAsync(IServiceProvider serviceProvider, Transaction transaction, CancellationToken cancellationToken = default)
        {
            var pagarMe = serviceProvider.GetRequiredService<PagarMeApi>();

            var request = new CapturePagarMePixDto(); // Create gateway request body
            var gatewayResult = await pagarMe.CapturePixAsync(request, cancellationToken).ConfigureAwait(false);

            transaction.Capture(gatewayResult.Status, gatewayResult.CaptureResult);
        }
    }
}
