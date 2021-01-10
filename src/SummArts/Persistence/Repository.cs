using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Persistence.Interface;
using SummArts.Models;

namespace Persistence
{
    public class Repository<E> : IRepository<E, int> where E : Entity<int>
    {
        public long Count(Expression<Func<E, bool>> p)
        {
            throw new NotImplementedException();
        }

        public E Create(E entity)
        {
            throw new NotImplementedException();
        }

        public E Get(int id)
        {
            throw new NotImplementedException();
        }

        public E Get(Expression<Func<E, bool>> p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<E> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<E> GetAll(Expression<Func<E, bool>> p)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(E entity)
        {
            throw new NotImplementedException();
        }
    }
}