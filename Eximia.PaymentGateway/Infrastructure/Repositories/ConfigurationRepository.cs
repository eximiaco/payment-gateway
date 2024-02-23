using CSharpFunctionalExtensions;
using Dapper;
using Eximia.PaymentGateway.Domain;
using Eximia.PaymentGateway.Domain.Configurations;
using Eximia.PaymentGateway.Domain.Configurations.Auth;
using Npgsql;
using System.Data;

namespace Eximia.PaymentGateway.Infrastructure.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly PostgresConnectionStringFactory _connectionStringFactory;

        public ConfigurationRepository(PostgresConnectionStringFactory connectionStringFactory)
        {
            _connectionStringFactory = connectionStringFactory;
        }

        public async Task<Maybe<Configuration>> GetByClientIdAndCaptureTypeAsync(string clientId, ECaptureType captureType, CancellationToken cancellationToken = default)
        {
            var sql = @"SELECT  id as Id,
                                client_id as ClientId,
                                capture_type as CaptureType,
                                capture_transaction_strategy as CaptureTransactionStrategy,
                                auth_strategy as AuthStrategy
                        FROM configurations
                        WHERE client_id = @ClientId
                            AND capture_type = @CaptureType";

            var param = new DynamicParameters();
            param.Add("@ClientId", clientId, DbType.String, ParameterDirection.Input);
            param.Add("@CaptureType", captureType, DbType.Int16, ParameterDirection.Input);

            await using var connection = new NpgsqlConnection(_connectionStringFactory.CreateConnectionString().ConnectionString);
            var configuration = await connection.QueryFirstOrDefaultAsync<ConfigurationDto>(sql, param).WaitAsync(cancellationToken).ConfigureAwait(false);
            if (configuration is null)
                return Maybe.None;

            return new Configuration(
                configuration.Id,
                configuration.ClientId,
                (ECaptureType)configuration.CaptureType,
                configuration.CaptureTransactionStrategy.ToNameTypeObject<ICaptureTransactionStrategy>(),
                configuration.AuthStrategy.ToNameTypeObject<IAuthStrategy>());
        }

        public record ConfigurationDto(int Id, string ClientId, int CaptureType, string CaptureTransactionStrategy, string AuthStrategy);
    }
}
