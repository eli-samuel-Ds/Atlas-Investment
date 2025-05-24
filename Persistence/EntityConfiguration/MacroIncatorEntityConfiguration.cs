using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfiguration
{
    public class MacroIncatorEntityConfiguration : IEntityTypeConfiguration<MacroIndicator>
    {
        public void Configure(EntityTypeBuilder<MacroIndicator> builder)
        {
            #region Base Configuration
            builder.ToTable("MacroIndicators");

            builder.HasKey(mi => mi.Id);
            #endregion

            #region Properties Configuration
            builder.Property(mi => mi.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            
            builder.Property(mi => mi.Weight)
                   .IsRequired()
                   .HasColumnType("decimal(5,4)");
           
            builder.Property(mi => mi.IsHigherBetter)
                   .IsRequired();
            #endregion
            
            #region Relationships Configuration
            builder.HasMany(mi => mi.CountryIndicators)
                   .WithOne(ci => ci.MacroIndicator)
                   .HasForeignKey(ci => ci.MacroIndicatorId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(mi => mi.SimulationEntries)
                   .WithOne(se => se.MacroIndicator)
                   .HasForeignKey(se => se.MacroIndicatorId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}