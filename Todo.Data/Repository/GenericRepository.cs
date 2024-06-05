using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TodoContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(TodoContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public GenericResponse<T> Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return GenericResponse<T>.SuccessResponse(entity, null);
            }
            catch (Exception ex)
            {
                return GenericResponse<T>.ErrorResponse(ex.Message);
            }

        }

        public GenericResponse<bool> DeleteById(int id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                if (entity == null)
                {
                    return GenericResponse<bool>.ErrorResponse($"ID {id} ile öğe bulunamadı.");
                }

                _dbSet.Remove(entity);
                _context.SaveChanges();
                return GenericResponse<bool>.SuccessResponse(true, null);
            }
            catch (Exception ex)
            {

                return GenericResponse<bool>.ErrorResponse(ex.Message);
            }

        }

        public GenericResponse<IQueryable<T>> GetAll()
        {
            try
            {
                return GenericResponse<IQueryable<T>>.SuccessResponse(_dbSet.AsNoTracking(), null);
            }
            catch (Exception ex)
            {
                return GenericResponse<IQueryable<T>>.ErrorResponse(ex.Message);
            }
        }

        public GenericResponse<T> GetById(int id)
        {
            try
            {
                return GenericResponse<T>.SuccessResponse(_dbSet.Find(id), null);

            }
            catch (Exception ex)
            {
                return GenericResponse<T>.ErrorResponse(ex.Message);
            }
        }
        public GenericResponse<bool> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                var result = _context.SaveChanges();
                if (result <= 0)
                    return GenericResponse<bool>.ErrorResponse("Güncelleme işlemi sırasında hata.");

                return GenericResponse<bool>.SuccessResponse(true,null);
            }
            catch (Exception ex)
            {
                return GenericResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public GenericResponse<IQueryable<T>> Where(Expression<Func<T, bool>> expression)
        {
            try
            {
                var returnList = _dbSet.Where(expression).AsNoTracking();
                return GenericResponse<IQueryable<T>>.SuccessResponse(returnList,null);
            }
            catch (Exception ex)
            {
               return GenericResponse<IQueryable<T>>.ErrorResponse(ex.Message);
            }
        }
    }
}
