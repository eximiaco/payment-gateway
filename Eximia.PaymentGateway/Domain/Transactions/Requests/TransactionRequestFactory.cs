using Eximia.PaymentGateway.Domain.Transactions.Commands;
using System.ComponentModel;

namespace Eximia.PaymentGateway.Domain.Transactions.Requests
{
    public static class TransactionRequestFactory
    {
        public static ITransactionRequest Create(CaptureTransactionCommand command)
        {
            return command.CaptureType switch
            {
                ECaptureType.CreditCard => new CreditCardRequest(command.CreditCard!.Token, command.CreditCard.Installments),
                ECaptureType.Pix => new PixRequest(command.Pix!.Key),
                _ => throw new InvalidEnumArgumentException(nameof(command.CaptureType), (int)command.CaptureType, command.CaptureType.GetType())
            };
        }
    }
}
