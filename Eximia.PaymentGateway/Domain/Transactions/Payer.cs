namespace Eximia.PaymentGateway.Domain.Transactions
{
    public readonly record struct Payer(
        string DocumentNumber,
        string Name);
}
