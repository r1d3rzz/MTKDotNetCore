using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepoAPI.Domain.Models
{
    public class Result<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsError { get { return !IsSuccess; } }

        public ResultType Type { get; set; }

        public string Message { get; set; }

        public static Result<T> Success(T data, string message)
        {
            return new Result<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                Type = ResultType.Success
            };
        }

        public static Result<T> SystemError(string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = ResultType.SystemError,
                Message = message
            };
        }

        public static Result<T> ValidationError(string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = ResultType.ValidationError,
                Message = message
            };
        }
    }

    public enum ResultType
    {
        None,
        Success,
        SystemError,
        ValidationError
    }
}
