namespace Eximia.PaymentGateway.Domain.Transactions
{
    public enum EStatus
    {
        Created = 1,
        Confirmed = 2,
        Denied = 3,
        Canceled = 4,
        Pending = 5,
        Failure = 6
    }
}
