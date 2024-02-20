using CSharpFunctionalExtensions;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Domain.Transactions.Commands;
using Eximia.PaymentGateway.Domain.Transactions.Requests;
using MediatR;

namespace Eximia.PaymentGateway.Domain.Transactions.Handlers
{
    public class CaptureTransactionCommandHandler : IRequestHandler<CaptureTransactionCommand, Result<Transaction>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CaptureTransactionCommandHandler(
            IServiceProvider serviceProvider,
            IConfigurationRepository configurationRepository,
            ITransactionRepository transactionRepository)
        {
            _serviceProvider = serviceProvider;
            _configurationRepository = configurationRepository;
            _transactionRepository = transactionRepository;
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

            await configuration.Value.CaptureTransactionStrategy.CaptureAsync(_serviceProvider, transaction, cancellationToken).ConfigureAwait(false);
            await _transactionRepository.InsertAsync(transaction, cancellationToken).ConfigureAwait(false);
            return transaction;
        }
    }
}
