using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        //LINQ Expression
        IEnumerable<TEntity> Get(
                 Expression<Func<TEntity, bool>> filter = null,
                 Func<IQueryable<TEntity>,
                 IOrderedQueryable<TEntity>> orderBy = null,
                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null); //for using relative data from other tables
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        void SetStateModified(TEntity entity);
    }
}
