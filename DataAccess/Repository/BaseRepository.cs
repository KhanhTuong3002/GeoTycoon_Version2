using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class
    {
        protected BaseRepository(BaseDao<T, TKey> dao)
        {
            Dao = dao;
        }

        protected BaseDao<T, TKey> Dao { get; }

        public virtual T? this[TKey id] => Dao[id];

        public virtual void Add(T entity)
        {
            Dao.Add(entity);
            Dao.Save();
        }

        public virtual void Update(T entity)
        {
            Dao.Update(entity);
            Dao.Save();
        }

        public virtual void Delete(T entity)
        {
            Dao.Delete(entity);
            Dao.Save();
        }

        public virtual IQueryable<T> GetAll()
        {
            return Dao.GetAll();
        }
    }

    public abstract class BaseRepository<T> : BaseRepository<T, int> where T : class
    {
        protected BaseRepository(BaseDao<T> dao) : base(dao)
        {
        }
    }
}
