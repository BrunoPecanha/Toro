using Microsoft.EntityFrameworkCore;
using Toro.Domain;
using Toro.Repository.Context;

namespace Toro.Repository {

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class {
        // Cria uma instância de acesso ao BD.
        protected ToroContext Db = new ToroContext(new DbContextOptions<ToroContext>(), null);

        public void Add(T obj) {
            Db.Set<T>().Add(obj);
            Db.SaveChanges();
        }

        public void Dispose() {
            Db.Dispose();
        }
        public void Remove(T obj) {
            Db.Set<T>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(T obj) {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
        public T GetById(int id) {
            return Db.Set<T>().Find(id);
        }
    }
}

