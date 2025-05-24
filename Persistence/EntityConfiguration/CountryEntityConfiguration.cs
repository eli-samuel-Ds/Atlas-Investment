using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfiguration
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            #region Base Configuration
            builder.ToTable("Countries");

            builder.HasKey(c => c.Id);
            #endregion

            #region Properties Configuration
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            
            builder.Property(c => c.IsoCode)
                   .IsRequired()
                   .HasMaxLength(2)
                   .IsFixedLength();
            
            builder.HasIndex(c => c.IsoCode)
                   .IsUnique();
            #endregion

            #region Relationships Configuration
            builder.HasMany(c => c.Indicators)
                   .WithOne(i => i.Country)
                   .HasForeignKey(i => i.CountryId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}