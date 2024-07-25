using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class BaseRequest
    {
        public string? MethodName { get; set; }
        public DynamicParameters Parameters { get; set; }
        public string? Query { get; set; }

        public BaseRequest()
        {
                
        }
        public BaseRequest(string methodName, DynamicParameters parameters)
        {
           MethodName = methodName;
           Parameters = parameters;
        }

        public BaseRequest(string query, DynamicParameters parameters, bool isQuery)
        {
            Query = query;
            Parameters = parameters;
        }
    }
}
