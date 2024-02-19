using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public sealed class Configuration : Entity<int>
    {
        public Configuration(int id, string clientId, ECaptureType chargeType, ICaptureTransactionStrategy captureTransactionStrategy) : base(id)
        {
            ClientId = clientId;
            ChargeType = chargeType;
            CaptureTransactionStrategy = captureTransactionStrategy;
        }

        public string ClientId { get; }
        public ECaptureType ChargeType { get; }
        public ICaptureTransactionStrategy CaptureTransactionStrategy { get; }
    }
}
