using Eximia.PaymentGateway.Infrastructure.Integrations.Captures;
using Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe.DTOs;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.PagarMe
{
    public interface IPagarMeApi
    {
        Task<GatewayCaptureResult> CapturePixAsync(CapturePagarMePixDto request, CancellationToken cancellationToken = default);
    }
}
