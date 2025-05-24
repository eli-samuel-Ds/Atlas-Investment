using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfiguration
{
    public class ReturnRateConfigEntityConfiguration : IEntityTypeConfiguration<ReturnRateConfig>
    {
        public void Configure(EntityTypeBuilder<ReturnRateConfig> builder)
        {
            #region Base Configuration
            builder.ToTable("ReturnRateConfigs");

            builder.HasKey(rr => rr.Id);
            #endregion

            #region Properties Configuration
            builder.Property(rr => rr.MinRate)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)");

            builder.Property(rr => rr.MaxRate)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)");
            #endregion

            #region Relationships Configuration
            #endregion
        }
    }
}