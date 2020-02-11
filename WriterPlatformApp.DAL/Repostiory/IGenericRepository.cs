using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace WriterPlatformApp.DAL.Repostiory
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        /**
         * SELECT * FROM table;
         * */
        IEnumerable<TEntity> GetAll();

        /**
         * SELECT * FROM table WHERE id = :id;
         * */
        TEntity FindById(int? id);

        /**
         *  INSERT
         * */
        void Create(TEntity item);

        /**
         *  UPDATE
         * */
        void Update(TEntity item);

        /**
         *  DELETE
         * */
        void Delete(int id);

        /**
         *  COMMIT or SaveChanges
         * */
        void Save();

        /**
         *  JOIN
         * */
        IQueryable<TEntity> Include(params string[] navigationProperty);

        DbEntityEntry<TEntity> SaveSingleField(TEntity item);
    }
}
