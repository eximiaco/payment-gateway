using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Domain.Transactions.Commands;
using Eximia.PaymentGateway.Domain.Transactions.Requests;
using Eximia.PaymentGateway.Infrastructure.Integrations.Captures;
using Eximia.PaymentGateway.Infrastructure.Integrations.Captures.Auth;
using MediatR;

namespace Eximia.PaymentGateway.Domain.Transactions.Handlers
{
    public class CaptureTransactionCommandHandler : IRequestHandler<CaptureTransactionCommand, Result<Transaction>>
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly CaptureTransactionWithAuthDecorator _captureTransactionWithAuthDecorator;

        public CaptureTransactionCommandHandler(
            IConfigurationRepository configurationRepository,
            ITransactionRepository transactionRepository,
            CaptureTransactionWithAuthDecorator captureTransactionWithAuthDecorator)
        {
            _configurationRepository = configurationRepository;
            _transactionRepository = transactionRepository;
            _captureTransactionWithAuthDecorator = captureTransactionWithAuthDecorator;
        }

        public async Task<Result<Transaction>> Handle(CaptureTransactionCommand command, CancellationToken cancellationToken)
        {
            var configuration = await _configurationRepository
                .GetByClientIdAndCaptureTypeAsync(command.ClientId, command.CaptureType, cancellationToken)
                .ConfigureAwait(false);
            if (configuration.HasNoValue)
                return Result.Failure<Transaction>("Client not configured");

            var transaction = Transaction.Create(
                command.ClientId,
                configuration.Value.CaptureTransactionStrategy.Gateway,
                command.Amount,
                command.Payer,
                TransactionRequestFactory.Create(command));

            var result = await _captureTransactionWithAuthDecorator.CaptureAsync(configuration.Value, transaction, cancellationToken).ConfigureAwait(false);
            if (result.IsFailure)
                return Result.Failure<Transaction>(result.Error);

            await _transactionRepository.InsertAsync(transaction, cancellationToken).ConfigureAwait(false);
            return transaction;
        }
    }
}
