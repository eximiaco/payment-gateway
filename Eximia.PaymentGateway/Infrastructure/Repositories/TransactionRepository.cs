using Dapper;
using Eximia.PaymentGateway.Domain.Transactions;
using Npgsql;
using System.Data;

namespace Eximia.PaymentGateway.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PostgresConnectionStringFactory _connectionStringFactory;

        public TransactionRepository(PostgresConnectionStringFactory connectionStringFactory)
        {
            _connectionStringFactory = connectionStringFactory;
        }

        public async Task InsertAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            var sql = @"INSERT INTO transactions (
                            client_id,
                            gateway,
                            amount,
                            status,
                            created_at,
                            payer_document_number,
                            payer_name,
                            capture_type,
                            gateway_transaction_id,
                            gateway_status,
                            gateway_message,
                            gateway_answered_at)
                        VALUES (
                            @ClientId,
                            @Gateway,
                            @Amount,
                            @Status,
                            @CreatedAt,
                            @PayerDocumentNumber,
                            @PayerName,
                            @CaptureType,
                            @GatewayTransactionId,
                            @GatewayStatus,
                            @GatewayMessage,
                            @GatewayAnsweredAt)";

            var param = new DynamicParameters();
            param.Add("@ClientId", transaction.ClientId, DbType.String, ParameterDirection.Input);
            param.Add("@Gateway", transaction.Gateway, DbType.Int16, ParameterDirection.Input);
            param.Add("@Amount", transaction.Amount, DbType.Decimal, ParameterDirection.Input);
            param.Add("@Status", transaction.Status, DbType.Int16, ParameterDirection.Input);
            param.Add("@CreatedAt", transaction.CreatedAt, DbType.DateTime, ParameterDirection.Input);
            param.Add("@PayerDocumentNumber", transaction.Payer.DocumentNumber, DbType.String, ParameterDirection.Input);
            param.Add("@PayerName", transaction.Payer.Name, DbType.String, ParameterDirection.Input);
            param.Add("@CaptureType", transaction.CaptureResult.Type, DbType.Int16, ParameterDirection.Input);
            param.Add("@GatewayTransactionId", transaction.CaptureResult.GatewayTransactionId, DbType.String, ParameterDirection.Input);
            param.Add("@GatewayStatus", transaction.CaptureResult.GatewayStatus, DbType.String, ParameterDirection.Input);
            param.Add("@GatewayMessage", transaction.CaptureResult.Message, DbType.String, ParameterDirection.Input);
            param.Add("@GatewayAnsweredAt", transaction.CaptureResult.GatewayAnsweredAt, DbType.DateTime, ParameterDirection.Input);

            await using var connection = new NpgsqlConnection(_connectionStringFactory.CreateConnectionString().ConnectionString);
            await connection.ExecuteAsync(sql, param).WaitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
