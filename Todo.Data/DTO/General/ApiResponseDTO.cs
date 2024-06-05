using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.DTO
{
    public class ApiResponseDTO
    {
        public object? Data { get; set; }
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; }

        public static ApiResponseDTO Success(object data, string? message)
        {
            return new ApiResponseDTO { Data = data, Message = message, IsSuccess = true };
        }

        public static ApiResponseDTO Failed(string? message)
        {
            return new ApiResponseDTO { Data = null, Message = message, IsSuccess = false };
        }

    }
}
