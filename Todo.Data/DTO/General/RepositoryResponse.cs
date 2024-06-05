using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T? Data { get; set; }
        public int? Count { get; set; }

        public static GenericResponse<T> SuccessResponse<T>(T? data, int? count)
        {
            return new GenericResponse<T>() { IsSuccess = true, Data = data, Count = count };
        }
        public static GenericResponse<T> ErrorResponse(string? message)
        {
            return new GenericResponse<T>() { IsSuccess = false, ErrorMessage = message };
        }
    }
}
