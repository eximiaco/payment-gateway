using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Transactions.Commands.DTOs;
using MediatR;

namespace Eximia.PaymentGateway.Domain.Transactions.Commands
{
    public record CaptureTransactionCommand(
        string ClientId,
        Payer Payer,
        decimal Amount,
        ECaptureType ChargeType,
        ChargeByCreditCardDto? CreditCard) : IRequest<Result<Transaction>>;
}
