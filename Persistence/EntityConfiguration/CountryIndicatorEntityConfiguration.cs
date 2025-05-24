using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfiguration
{
    public class CountryIndicatorEntityConfiguration : IEntityTypeConfiguration<CountryIndicator>
    {
        public void Configure(EntityTypeBuilder<CountryIndicator> builder)
        {
            #region Base Configuration
            builder.ToTable("CountryIndicators");

            builder.HasKey(ci => ci.Id);
            #endregion

            #region Properties Configuration
            builder.Property(ci => ci.Year)
                   .IsRequired();

            builder.Property(ci => ci.Value)
                   .IsRequired()
                   .HasColumnType("decimal(18,4)");

            builder.HasIndex(ci => new { ci.CountryId, ci.MacroIndicatorId, ci.Year })
                   .IsUnique();
            #endregion

            #region Relationships Configuration
            builder.HasOne(ci => ci.Country)
                   .WithMany(c => c.Indicators)
                   .HasForeignKey(ci => ci.CountryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.MacroIndicator)
                   .WithMany(mi => mi.CountryIndicators)
                   .HasForeignKey(ci => ci.MacroIndicatorId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}