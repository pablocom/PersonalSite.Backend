using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

namespace PersonalSite.Persistence.Npgsql;

public class PersonalSiteDbMigrator : IMigrator
{
    private readonly PersonalSiteDbContext _context;
    private readonly ILogger<PersonalSiteDbMigrator> _logger;

    public PersonalSiteDbMigrator(PersonalSiteDbContext context, ILogger<PersonalSiteDbMigrator> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Migrate(string? targetMigration = null)
    {
        _logger.LogInformation("Starting migrations...");
        _context.Database.Migrate();
    }

    public async Task MigrateAsync(string? targetMigration = null, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation("Starting migrations...");
        await _context.Database.MigrateAsync(cancellationToken: cancellationToken);
    }

    public string GenerateScript(string? fromMigration = null, string? toMigration = null,
        MigrationsSqlGenerationOptions options = MigrationsSqlGenerationOptions.Default)
    {
        return _context.Database.GenerateCreateScript();
    }
}
