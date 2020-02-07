using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WriterPlatformApp.DAL.DB;

namespace WriterPlatformApp.DAL.Repostiory
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private ApplicationContext db;
        private DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationContext db)
        {
            this.db = db;
            dbSet = db.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
        }

        public void Delete(int id)
        {
            TEntity item = FindById(id);
            dbSet.Remove(item);
        }

        public TEntity FindById(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }

        public IQueryable<TEntity> Include(params string[] navigationProperty)
        {
            var query =  GetAll().AsQueryable();
            foreach (var item in navigationProperty)
            {
                query = query.Include(item);
            }
            return query;
        }

        public void Save()
        {
            db.SaveChanges();
        }


        public void Update(TEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
