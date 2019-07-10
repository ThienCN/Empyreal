using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Empyreal.Interfaces.Entities
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> expression);
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
        void ExecWithStoreProcedureUpdate(string query, params object[] parameters);
        T Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        IEnumerable<T> Search(string text);

    }
}
