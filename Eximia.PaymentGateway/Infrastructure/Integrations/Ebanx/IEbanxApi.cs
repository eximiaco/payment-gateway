using Eximia.PaymentGateway.Infrastructure.Integrations.Captures;
using Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx.DTOs;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Ebanx
{
    public interface IEbanxApi
    {
        Task<GatewayCaptureResult> CaptureCreditCardAsync(CaptureEbanxCreditCardDto request, CancellationToken cancellationToken = default);
    }
}
