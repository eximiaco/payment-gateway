using Eximia.PaymentGateway.Domain.Transactions;

namespace Eximia.PaymentGateway.Infrastructure.Integrations.Captures
{
    public record GatewayCaptureResult(EStatus Status, CaptureResult CaptureResult);
}
