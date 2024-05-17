using BusinessObject.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO;

public abstract class BaseDao<T, TKey> where T : class
{
    protected BaseDao(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    protected DbSet<T> DbSet { get; set; } = null!;
    protected DbContext DbContext { get; set; } = null!;

    public virtual T? this[TKey id] => DbSet.Find(id);

    public virtual IQueryable<T> GetAll()
    {
        return DbSet;
    }

    public virtual void BeginTransaction()
    {
        DbContext.Database.BeginTransaction();
    }

    public virtual async Task<T?> GetByIdAsync(TKey id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void Save()
    {
        DbContext.SaveChanges();
    }

    public virtual void Commit()
    {
        DbContext.Database.CommitTransaction();
    }

    public virtual void Rollback()
    {
        DbContext.Database.RollbackTransaction();
    }
}

public abstract class BaseDao<T> : BaseDao<T, int> where T : class
{
    protected BaseDao(DbContext dbContext) : base(dbContext)
    {
    }
}
