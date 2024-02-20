namespace Eximia.PaymentGateway.Domain.Transactions.Requests
{
    public record PixRequest(string Key) : ITransactionRequest;
}
