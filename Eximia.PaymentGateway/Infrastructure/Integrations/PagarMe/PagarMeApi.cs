using Eximia.PaymentGateway.Domain.Transactions;
using Eximia.PaymentGateway.Infrastructure.Integrations.Captures;
using Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe.DTOs;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe
{
    public class PagarMeApi
    {
        public async Task<GatewayCaptureResult> CapturePixAsync(CapturePagarMePixDto request, CancellationToken cancellationToken = default)
        {
            await Task.Delay(2000, cancellationToken).ConfigureAwait(false);
            return new GatewayCaptureResult(
                EStatus.Confirmed,
                CaptureResult.CreatePix(Guid.NewGuid().ToString(), "Confirmed", "Sucesso!", DateTime.UtcNow));
        }
    }
}
