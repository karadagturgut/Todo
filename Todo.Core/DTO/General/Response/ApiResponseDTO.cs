using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class ApiResponseDTO
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponseDTO Success(object data, string? message)
        {
            return new ApiResponseDTO { Data = data, Message = message, IsSuccess = true, StatusCode = 200 };
        }

        public static ApiResponseDTO SuccessAdded(object data, string? message)
        {
            return new ApiResponseDTO { Data = data, Message = message, IsSuccess = true, StatusCode = 201 };
        }

        public static ApiResponseDTO NoContent(object data, string? message)
        {
            return new ApiResponseDTO { Data = data, Message = message, IsSuccess = true, StatusCode = 204 };
        }

        public static ApiResponseDTO Unauthorized(string? message)
        {
            return new ApiResponseDTO { Data = null, Message = message, IsSuccess = false, StatusCode = 403 };
        }
        public static ApiResponseDTO Failed(string? message)
        {
            return new ApiResponseDTO { Data = null, Message = message, IsSuccess = false, StatusCode = 500 };
        }
        
    }
}
