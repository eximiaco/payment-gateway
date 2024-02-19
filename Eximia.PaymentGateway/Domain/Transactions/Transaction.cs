using CSharpFunctionalExtensions;

namespace Eximia.PaymentGateway.Domain.Transactions
{
    public sealed class Transaction : Entity<int>
    {
        public Transaction(
            int id,
            string clientId,
            EGateway gateway,
            decimal amount,
            EStatus status,
            DateTime createdAt,
            Payer payer,
            CaptureResult captureResult) : base(id)
        {
            ClientId = clientId;
            Gateway = gateway;
            Amount = amount;
            Status = status;
            CreatedAt = createdAt;
            Payer = payer;
            CaptureResult = captureResult;
        }

        public string ClientId { get; }
        public EGateway Gateway { get; }
        public decimal Amount { get; }
        public EStatus Status { get; private set; }
        public DateTime CreatedAt { get; }
        public Payer Payer { get; }
        public CaptureResult CaptureResult { get; private set; }

        public static Transaction Create(string clientId, EGateway gateway, decimal amount, Payer payer)
            => new Transaction(0, clientId, gateway, amount, EStatus.Created, DateTime.UtcNow, payer, null!);

        public void Capture(EStatus status, CaptureResult captureResult)
        {
            Status = status;
            CaptureResult = captureResult;
        }
    }
}
