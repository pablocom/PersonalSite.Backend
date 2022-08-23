using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Persistence;

public class PersonalSiteDbContextMigrator : IMigrator
{
    private readonly PersonalSiteDbContext _context;
    private readonly ILogger<PersonalSiteDbContextMigrator> _logger;

    public PersonalSiteDbContextMigrator(PersonalSiteDbContext context, ILogger<PersonalSiteDbContextMigrator> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Migrate(string targetMigration = null)
    {
        _logger.LogInformation("Starting migrations...");
        _context.Database.Migrate();
    }

    public async Task MigrateAsync(string targetMigration = null, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation("Starting migrations...");
        await _context.Database.MigrateAsync(cancellationToken: cancellationToken);
    }

    public string GenerateScript(string fromMigration = null, string toMigration = null,
        MigrationsSqlGenerationOptions options = MigrationsSqlGenerationOptions.Default)
    {
        return _context.Database.GenerateCreateScript();
    }
}
