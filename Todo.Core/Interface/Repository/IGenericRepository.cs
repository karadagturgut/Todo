using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;

namespace Todo.Core
{
    public interface IGenericRepository<T> where T : class
    {
        #region EF
        GenericResponse<T> Add(T entity);
        GenericResponse<T> GetById(int id);
        GenericResponse<IQueryable<T>> GetAll();
        GenericResponse<bool> Update(T entity);
        GenericResponse<bool> DeleteById(int id);
        GenericResponse<IQueryable<T>> Where(Expression<Func<T, bool>> expression);
        public IQueryable<T> WhereQueryable(Expression<Func<T, bool>> expression);

        #endregion


        #region Dapper
        GenericResponse<IEnumerable<T>> StoreProcedure<T>(BaseRequest entity);
        GenericResponse<IEnumerable<T>> Query(BaseRequest entity);
        #endregion
    }
}
