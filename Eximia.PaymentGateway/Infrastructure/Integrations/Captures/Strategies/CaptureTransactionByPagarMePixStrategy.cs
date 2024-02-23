using Eximia.PaymentGateway.Domain;
using Eximia.PaymentGateway.Domain.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe;
using Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe.DTOs;
using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures.Strategies
{
    public class CaptureTransactionByPagarMePixStrategy : ICaptureTransactionStrategy
    {
        public ECaptureType CaptureType => ECaptureType.Pix;
        public EGateway Gateway => EGateway.PagarMe;

        public async Task<Result> CaptureAsync(CaptureTransactionContext context, CancellationToken cancellationToken = default)
        {
            var pagarMe = context.ServiceProvider.GetRequiredService<PagarMeApi>();

            // Create gateway request body
            // Use auth result
            var request = new CapturePagarMePixDto();
            var gatewayResult = await pagarMe.CapturePixAsync(request, cancellationToken).ConfigureAwait(false);
            if (gatewayResult.IsFailure)
                return gatewayResult;

            context.Transaction.Capture(gatewayResult.Value.Status, gatewayResult.Value.CaptureResult);
            return Result.Success();
        }
    }
}
