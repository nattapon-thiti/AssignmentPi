using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Models.ResponseModels
{
    public abstract class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T Data { get; private set; } // external class or method cannot set this 
        public BaseResponse() { }
        public BaseResponse(T data)
        {
            this.IsSuccess = true;
            this.Data = data;
        }
        public BaseResponse(bool isSuccess, string? message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
        public BaseResponse(bool isSuccess, string? message, T data)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
        }
    }
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object Data { get; private set; } // external class or method cannot set this 
        public BaseResponse() { }
        public BaseResponse(bool isSuccess, string? message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}
