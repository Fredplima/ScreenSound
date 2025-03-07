namespace ScreenSound.API
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System.Threading;
    using System.Threading.Tasks;

    public class DbContextHealthCheck<TContext> : IHealthCheck where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public DbContextHealthCheck(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Tenta executar uma consulta simples para verificar se o banco de dados está acessível
                var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);
                if (canConnect)
                {
                    return HealthCheckResult.Healthy("O banco de dados está acessível.");
                }
                else
                {
                    return HealthCheckResult.Unhealthy("Não foi possível conectar ao banco de dados.");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Erro ao verificar a saúde do banco de dados.", ex);
            }
        }
    }
}
