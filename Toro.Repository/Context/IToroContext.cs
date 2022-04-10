using Microsoft.EntityFrameworkCore;
using Toro.Domain.Entity;

namespace Toro.Repository.Context {
    public interface IToroContext {
        DbSet<Investor> Investor { get; }
        DbSet<Patrimony> Patrimony { get; }
        DbSet<Asset> Asset { get; set; }
        int SaveChanges();
    }
}
