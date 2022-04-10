using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toro.Domain.Entity;

namespace Toro.Repository.EntityConfig {
    public class InvestorConfiguration : IEntityTypeConfiguration<Investor> {

        public void Configure(EntityTypeBuilder<Investor> builder) {

            builder
             .ToTable("Investor")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
           .Property(c => c.Cpf)
           .HasColumnName("Cpf")
           .IsRequired();

            builder
            .HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<Investor>(ad => ad.UserId);
        }
    }
}
