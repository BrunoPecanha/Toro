using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toro.Domain.Entity;

namespace Toro.Repository.EntityConfig {
    public class AssetConfiguration : IEntityTypeConfiguration<Asset> {

        public void Configure(EntityTypeBuilder<Asset> builder) {

            builder
             .ToTable("Asset")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
             .Property(c => c.CurrentPrice)
             .HasColumnName("CurrentPrice")
             .IsRequired();

            builder
            .Property(c => c.Id)
            .HasColumnName("Symbol");
        }
    }
}
