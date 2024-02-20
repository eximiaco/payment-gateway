using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Transactions.Commands.DTOs;
using MediatR;

namespace Eximia.PaymentGateway.Domain.Transactions.Commands
{
    public record CaptureTransactionCommand(
        string ClientId,
        Payer Payer,
        decimal Amount,
        ECaptureType CaptureType,
        ChargeByCreditCardDto? CreditCard,
        ChargeByPixDto? Pix) : IRequest<Result<Transaction>>;
}
