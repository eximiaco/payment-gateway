namespace Eximia.PaymentGateway.Domain.Transactions.Commands.DTOs
{
    public record ChargeByCreditCardDto(string Token, int Installments);
}
