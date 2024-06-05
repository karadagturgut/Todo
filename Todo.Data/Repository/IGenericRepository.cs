using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data
{
    public interface IGenericRepository<T> where T : class
    {
        GenericResponse<T> Add(T entity);
        GenericResponse<T> GetById(int id);
        GenericResponse<IQueryable<T>> GetAll();
        GenericResponse<bool> Update(T entity);
        GenericResponse<bool> DeleteById(int id);
        GenericResponse<IQueryable<T>> Where(Expression<Func<T, bool>> expression);

    }
}
