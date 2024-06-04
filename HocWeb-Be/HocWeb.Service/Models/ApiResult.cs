using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Models
{
    public class ApiResult<T> where T : class
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; } = 200;
        public List<string> Errors { get; set; }
        public ApiResult()
        {

        }
        public ApiResult(T data)
        {
            Data = data;
        }
    }
}
