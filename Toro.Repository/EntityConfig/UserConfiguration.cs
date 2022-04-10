using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toro.Domain.Entity;

namespace Toro.Repository.EntityConfig {
    public class UserConfiguration : IEntityTypeConfiguration<User> {

        public void Configure(EntityTypeBuilder<User> builder) {

            builder
             .ToTable("User")
             .HasKey(x => x.Id);

            builder
           .Property(c => c.Login)
           .HasColumnName("Login")
           .IsRequired();

            builder
           .Property(c => c.Password)
           .HasColumnName("Password")
           .IsRequired();

            builder
            .Property(c => c.RegisteringDate)
            .HasColumnName("RegisteringDate")
            .IsRequired();

            builder
           .Property(a => a.InvestorId)
           .HasColumnName("InvestorId");        
        }
    }
}
