using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using SummArts.Models;

namespace SummArts.Persistence
{
    public class SQLLiteSummaryRepository : IRepository<Summary, int>
    {
        private readonly SummArtsContext _context;

        public SQLLiteSummaryRepository(SummArtsContext context)
        {
            _context = context;
        }
        public long Count(Expression<Func<Summary, bool>> p)
        {
            return _context.Summary.Count(p);
        }

        public Summary Create(Summary entity)
        {
            _context.Summary.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Summary Get(int id)
        {
            return _context.Summary.FirstOrDefault(m => m.Id == id);
        }

        public Summary Get(Expression<Func<Summary, bool>> p)
        {
            return _context.Summary.FirstOrDefault(p);
        }

        public IList<Summary> GetAll()
        {
            return _context.Summary.OrderByDescending(s => s.UpdatedDate).ToList();
        }

        public IList<Summary> GetAll(Expression<Func<Summary, bool>> p)
        {
            return _context.Summary.Where(p).OrderByDescending(s => s.UpdatedDate).ToList();
        }

        public void Remove(int id)
        {
            var summary = this.Get(id);
            _context.Summary.Remove(summary);
            _context.SaveChanges();
        }

        public void Update(Summary entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}