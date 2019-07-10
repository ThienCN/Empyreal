using Empyreal.Interfaces.Entities;
using Korzh.EasyQuery.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Empyreal.Entities
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _dbset;

        public Repository(EmpyrealContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);

        }

        public void Update(T entity)
        {
            _dbset.Update(entity);

        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbset.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _dbset.AsNoTracking().FromSql(query, parameters);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public IEnumerable<T> Search(string text)
        {
            return _dbset.FullTextSearchQuery(text);
        }

        public void ExecWithStoreProcedureUpdate(string query, params object[] parameters)
        {
            _dbset.FromSql(query, parameters);
        }
    }
}
