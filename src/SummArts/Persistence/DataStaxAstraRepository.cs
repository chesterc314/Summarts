using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cassandra.Data.Linq;
using Persistence.Interface;
using SummArts.Models;

namespace SummArts.Persistence
{
    public class DataStaxAstraRepository<E> : IRepository<E, int> where E : Entity<int>
    {
        private readonly IDataStaxAstraDbConnection _connection;
        private readonly Table<E> _entities;
        public DataStaxAstraRepository(IDataStaxAstraDbConnection connection)
        {
            _connection = connection;
            _entities = new Table<E>(_connection.Session);
        }

        public long Count(Expression<Func<E, bool>> p)
        {
            return _entities.Where(p).Count<E>().Execute();
        }

        public E Create(E entity)
        {
            _entities.Insert(entity).Execute();
            return entity;
        }

        public E Get(int id)
        {
            return _entities.FirstOrDefault(x => x.Id == id).Execute();
        }

        public E Get(Expression<Func<E, bool>> p)
        {
            return _entities.FirstOrDefault(p).Execute();
        }

        public IList<E> GetAll()
        {
            return _entities.Execute().ToList();
        }

        public IList<E> GetAll(Expression<Func<E, bool>> p)
        {
            return _entities.Where(p).Execute().ToList();
        }

        public void Remove(int id)
        {
            _entities.DeleteIf(x => x.Id == id);
        }

        public void Update(E entity)
        {
            _entities.Where(u => u.Id == entity.Id)
                     .Select(u => entity)
                     .Update()
                     .Execute();
        }
    }
}