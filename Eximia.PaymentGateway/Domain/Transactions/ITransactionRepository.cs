namespace Eximia.PaymentGateway.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task InsertAsync(Transaction transaction, CancellationToken cancellationToken = default);
    }
}
