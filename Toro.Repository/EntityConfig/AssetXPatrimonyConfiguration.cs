using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toro.Domain.Entity;

namespace Toro.Repository.EntityConfig {
    public class AssetXPatrimonyConfiguration : IEntityTypeConfiguration<AssetXPatrimony> {

        public void Configure(EntityTypeBuilder<AssetXPatrimony> builder) {

            builder
             .ToTable("AssetXPatrimony")
             .HasKey(x => new { x.PatrimonyId, x.AssetId });

            builder
            .Property(c => c.Amount)
            .HasColumnName("Amount")
            .IsRequired();

            builder
           .Property(c => c.RegisteringDate)
           .HasColumnName("RegisteringDate")
           .IsRequired();

            builder
            .HasOne<Asset>(x => x.Asset)
            .WithMany(x => x.AssetXPatrimony)
            .HasForeignKey(b => b.AssetId);

            builder
            .HasOne<Patrimony>(x => x.Patrimony)
            .WithMany(x => x.AssetXPatrimony)
            .HasForeignKey(b => b.PatrimonyId);

        }
    }
}
