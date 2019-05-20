using Empyreal.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
