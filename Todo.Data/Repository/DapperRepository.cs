using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Dapper;
namespace Todo.Data
{
    public partial class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public GenericResponse<IEnumerable<T>> StoreProcedure<T>(BaseRequest entity)
        {
            try
            {
                var result = _connection.Query<T>(entity.MethodName, entity.Parameters, commandType: System.Data.CommandType.StoredProcedure);
                return GenericResponse<IEnumerable<T>>.SuccessResponse(result, result.Count());
            }
            catch
            {
                return GenericResponse<IEnumerable<T>>.ErrorResponse("Veriler getirilirken hata oluştu. lütfen sonra tekrar deneyiniz.");
            }
        }

        public GenericResponse<IEnumerable<T>> Query(BaseRequest entity)
        {
            try
            {
                var result = _connection.Query<T>(entity.Query, entity.Parameters);
                return GenericResponse<IEnumerable<T>>.SuccessResponse(result, result.Count());
            }
            catch
            {
                return GenericResponse<IEnumerable<T>>.ErrorResponse("Veriler getirilirken hata oluştu. lütfen sonra tekrar deneyiniz.");
            }
        }
    }
}
