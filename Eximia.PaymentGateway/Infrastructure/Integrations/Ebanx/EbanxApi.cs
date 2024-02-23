using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Transactions;
using Eximia.PaymentGateway.Infrastructure.Integrations.Captures;
using Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx.DTOs;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx
{
    public class EbanxApi
    {
        public async Task<Result<GatewayCaptureResult>> CaptureCreditCardAsync(CaptureEbanxCreditCardDto request, CancellationToken cancellationToken = default)
        {
            await Task.Delay(2000, cancellationToken).ConfigureAwait(false);
            return new GatewayCaptureResult(
                EStatus.Confirmed,
                CaptureResult.CreateCreditCard(Guid.NewGuid().ToString(), "Confirmed", "Sucesso!", DateTime.UtcNow));
        }
    }
}
