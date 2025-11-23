using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkZen.Api.Data;

namespace WorkZen.Api.Tests.Integration
{
    public class WorkZenApiFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection _connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext existente (SQLite físico)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                );

                if (descriptor != null)
                    services.Remove(descriptor);

                // Criar banco SQLite inteiramente em memória
                _connection = new SqliteConnection("DataSource=:memory:;Cache=Shared");
                _connection.Open();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                // Construir o provider e aplicar migrações
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // GARANTE que todas as tabelas serão criadas antes de rodar os testes
                    db.Database.EnsureCreated();
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection?.Dispose();
        }
    }
}