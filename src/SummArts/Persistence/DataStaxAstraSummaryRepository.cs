using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cassandra.Data.Linq;
using Persistence.Interface;
using SummArts.Models;

namespace SummArts.Persistence
{
    public class DataStaxAstraSummaryRepository : IRepository<Summary, int>
    {
        private readonly IDataStaxAstraDbConnection _connection;
        private readonly Table<Summary> _entities;
        public DataStaxAstraSummaryRepository(IDataStaxAstraDbConnection connection)
        {
            _connection = connection;
            _entities = new Table<Summary>(_connection.Session);
        }

        public long Count(Expression<Func<Summary, bool>> p)
        {
            return _entities.Where(p).Count<Summary>().Execute();
        }

        public Summary Create(Summary entity)
        {
            var total = (int)_entities.Count<Summary>().Execute(); //auto increment logic
            entity.Id = total + 1;

            _entities.Insert(entity).Execute();
            return entity;
        }

        public Summary Get(int id)
        {
            return _entities.AllowFiltering().Where(x => x.Id == id).FirstOrDefault().Execute();
        }

        public Summary Get(Expression<Func<Summary, bool>> p)
        {
            return _entities.AllowFiltering().Where(p).FirstOrDefault().Execute();
        }

        public IList<Summary> GetAll()
        {
            return _entities.Execute().ToList();
        }

        public IList<Summary> GetAll(Expression<Func<Summary, bool>> p)
        {
            return _entities.Where(p).Execute().ToList();
        }

        public void Remove(int id)
        {
            _entities.Where(x => x.Id == id).Delete().Execute();
        }

        public void Update(Summary entity)
        {
            _entities.Where(u => u.Id == entity.Id)
                     .Select(u => new Summary {
                         Title = entity.Title,
                         SummaryText = entity.SummaryText,
                         RawText = entity.RawText, 
                         SourceUrl= entity.SourceUrl,
                         Category = entity.Category,
                         Sentiment = entity.Sentiment
                     })
                     .Update()
                     .Execute();
        }
    }
}