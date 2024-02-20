using Microsoft.Extensions.Options;
using Npgsql;

namespace Eximia.PaymentGateway.Infrastructure.Repositories
{
    public class PostgresConnectionStringFactory
    {
        private readonly DatabaseConfiguration _configuration;

        public PostgresConnectionStringFactory(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            _configuration = databaseConfiguration.Value;
        }

        public NpgsqlConnectionStringBuilder CreateConnectionString()
            => new NpgsqlConnectionStringBuilder()
            {
                Host = _configuration!.Host,
                Port = _configuration.Port,
                Username = _configuration.User,
                Password = _configuration.Password,
                Database = _configuration.DatabaseName,
                SslMode = _configuration.DisableSsl ? SslMode.Disable : SslMode.Allow,
                Pooling = _configuration.Pooling,
                MinPoolSize = _configuration.MinPoolSize,
                MaxPoolSize = _configuration.MaxPoolSize,
                Timeout = _configuration.Timeout,
                ConnectionIdleLifetime = _configuration.ConnectionIdleLifetime,
                Multiplexing = _configuration.Multiplexing
            };
    }

    public record DatabaseConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public bool DisableSsl { get; set; }
        public bool Pooling { get; set; }
        public int MaxPoolSize { get; set; }
        public int MinPoolSize { get; set; }
        public int Timeout { get; set; }
        public int ConnectionIdleLifetime { get; set; }
        public bool Multiplexing { get; set; }
    }
}
