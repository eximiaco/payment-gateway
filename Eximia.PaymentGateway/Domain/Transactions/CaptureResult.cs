using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Transactions
{
    public sealed class CaptureResult : Entity<int>
    {
        public CaptureResult(ECaptureType type, string gatewayTransactionId, string gatewayStatus, string message, DateTime gatewayAnsweredAt)
        {
            Type = type;
            GatewayTransactionId = gatewayTransactionId;
            GatewayStatus = gatewayStatus;
            Message = message;
            GatewayAnsweredAt = gatewayAnsweredAt;
        }

        public ECaptureType Type { get; }
        public string GatewayTransactionId { get; }
        public string GatewayStatus { get; }
        public string Message { get; }
        public DateTime GatewayAnsweredAt { get; }

        public static CaptureResult CreateCreditCard(string gatewayTransactionId, string gatewayStatus, string message, DateTime gatewayAnsweredAt)
            => new CaptureResult(ECaptureType.CreditCard, gatewayTransactionId, gatewayStatus, message, gatewayAnsweredAt);

        public static CaptureResult CreatePix(string gatewayTransactionId, string gatewayStatus, string message, DateTime gatewayAnsweredAt)
            => new CaptureResult(ECaptureType.Pix, gatewayTransactionId, gatewayStatus, message, gatewayAnsweredAt);
    }
}
