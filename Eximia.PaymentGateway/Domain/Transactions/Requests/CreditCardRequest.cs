namespace Eximia.PaymentGateway.Domain.Transactions.Requests
{
    public record CreditCardRequest(string Token, int Installments) : ITransactionRequest;
}
