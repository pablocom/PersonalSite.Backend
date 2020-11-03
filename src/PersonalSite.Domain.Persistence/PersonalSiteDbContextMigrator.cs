using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public class PersonalSiteDbContextMigrator : IMigrator
    {
        private readonly PersonalSiteDbContext _context;
        public PersonalSiteDbContextMigrator(PersonalSiteDbContext context)
        {
            _context = context;
        }

        public void Migrate(string targetMigration = null)
        {
            System.Console.WriteLine("Starting migrations...");
            _context.Database.Migrate();
        }

        public async Task MigrateAsync(string targetMigration = null, CancellationToken cancellationToken = new CancellationToken())
        {
            System.Console.WriteLine("Starting migrations...");
            await _context.Database.MigrateAsync(cancellationToken: cancellationToken);
        }

        public string GenerateScript(string fromMigration = null, string toMigration = null, bool idempotent = false)
        {
            return _context.Database.GenerateCreateScript();
        }
    }
}