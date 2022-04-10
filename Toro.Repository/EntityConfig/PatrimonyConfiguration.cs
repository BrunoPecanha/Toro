using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toro.Domain.Entity;

namespace Toro.Repository.EntityConfig {
    public class PatrimonyConfiguration : IEntityTypeConfiguration<Patrimony> {

        public void Configure(EntityTypeBuilder<Patrimony> builder) {

            builder
             .ToTable("Patrimony")
             .HasKey(x => x.Id);

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();


            builder
            .Property(c => c.AccountAmount)
            .HasColumnName("AccountAmount");

            builder
             .Property(a => a.InvestorId)
             .HasColumnName("InvestorId");
        }
    }
}
