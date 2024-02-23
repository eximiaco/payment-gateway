using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx;
using Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures.Strategies
{
    public class CaptureTransactionByEbanxCreditCardStrategy : ICaptureTransactionStrategy
    {
        public ECaptureType CaptureType => ECaptureType.CreditCard;
        public EGateway Gateway => EGateway.Ebanx;

        public async Task<Result> CaptureAsync(CaptureTransactionContext context, CancellationToken cancellationToken = default)
        {
            var ebanxApi = context.ServiceProvider.GetRequiredService<EbanxApi>();

            // Create gateway request body
            // Use auth result
            var request = new CaptureEbanxCreditCardDto();
            var gatewayResult = await ebanxApi.CaptureCreditCardAsync(request, cancellationToken).ConfigureAwait(false);
            if (gatewayResult.IsFailure)
                return gatewayResult;

            context.Transaction.Capture(gatewayResult.Value.Status, gatewayResult.Value.CaptureResult);
            return Result.Success();
        }
    }
}
