using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public sealed class Configuration : Entity<int>
    {
        public Configuration(int id, string clientId, ECaptureType captureType, ICaptureTransactionStrategy captureTransactionStrategy) : base(id)
        {
            ClientId = clientId;
            CaptureType = captureType;
            CaptureTransactionStrategy = captureTransactionStrategy;
        }

        public string ClientId { get; }
        public ECaptureType CaptureType { get; }
        public ICaptureTransactionStrategy CaptureTransactionStrategy { get; }
    }
}
