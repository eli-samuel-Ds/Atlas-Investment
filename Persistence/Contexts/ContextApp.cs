using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System.Reflection;

namespace Persistence.Contexts
{
    public class ContextApp : DbContext
    {
        public ContextApp(DbContextOptions<ContextApp> options) : base(options) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryIndicator> CountryIndicators { get; set; }
        public DbSet<MacroIndicator> MacroIndicators { get; set; }
        public DbSet<ReturnRateConfig> ReturnRateConfigs { get; set; }
        public DbSet<SimulationMacroIndicator> SimulationMacroIndicators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
