using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Configurations.Auth;

namespace Eximia.PaymentGateway.Domain.Configurations
{
    public sealed class Configuration : Entity<int>
    {
        public Configuration(
            int id,
            string clientId,
            ECaptureType captureType,
            ICaptureTransactionStrategy captureTransactionStrategy,
            IAuthStrategy authStrategy) : base(id)
        {
            ClientId = clientId;
            CaptureType = captureType;
            CaptureTransactionStrategy = captureTransactionStrategy;
            AuthStrategy = authStrategy;
        }

        public string ClientId { get; }
        public ECaptureType CaptureType { get; }
        public ICaptureTransactionStrategy CaptureTransactionStrategy { get; }
        public IAuthStrategy AuthStrategy { get; }
    }
}
