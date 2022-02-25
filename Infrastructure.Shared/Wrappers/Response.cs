using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Wrappers
{
    public class Response<T>
    {
        public Response(bool succeeded, string? message = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Response(T? data, string? message)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
