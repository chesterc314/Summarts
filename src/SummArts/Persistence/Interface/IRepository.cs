using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SummArts.Models;

namespace Persistence.Interface
{
    public interface IRepository<T, IdType> where T : Entity<IdType>
    {
        T Get(IdType id);
        T Get(Expression<Func<T, bool>> p);
        IList<T> GetAll();
        IList<T> GetAll(Expression<Func<T, bool>> p);
        void Update(T entity);
        T Create(T entity);
        void Remove(IdType id);
        long Count(Expression<Func<T, bool>> p);
    }
}