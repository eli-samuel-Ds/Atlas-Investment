using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfiguration
{
    public class SimulationMacroIndicatorEntityConfiguration : IEntityTypeConfiguration<SimulationMacroIndicator>
    {
        public void Configure(EntityTypeBuilder<SimulationMacroIndicator> builder)
        {
            #region Base Configuration
            builder.ToTable("SimulationMacroIndicators");

            builder.HasKey(se => se.Id);
            #endregion

            #region Properties Configuration
            builder.Property(se => se.Weight)
                   .IsRequired()
                   .HasColumnType("decimal(5,4)");

            builder.HasIndex(se => se.MacroIndicatorId)
                   .IsUnique();
            #endregion

            #region Relationships Configuration
            builder.HasOne(se => se.MacroIndicator)
                   .WithMany(mi => mi.SimulationEntries)
                   .HasForeignKey(se => se.MacroIndicatorId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}