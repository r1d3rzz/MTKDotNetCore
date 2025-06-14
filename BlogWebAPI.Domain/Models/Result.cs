using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebAPI.Domain.Models
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public EnumType Type { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public string? Message { get; set; }

        public static Result<T> Success(T data, string message = "")
        {
            return new Result<T>()
            {
                Data = data,
                Type = EnumType.Success,
                IsSuccess = true,
                Message = message
            };
        }

        public static Result<T> SystemError(string message = "", T? data = default)
        {
            return new Result<T>()
            {
                Data = data,
                Type = EnumType.SystemError,
                IsSuccess = false,
                Message = message
            };
        }

        public static Result<T> ValidationError(string message = "", T? data = default)
        {
            return new Result<T>()
            {
                Type = EnumType.ValidationError,
                IsSuccess = false,
                Message = message
            };
        }

        public static Result<T> Failure(string message = "")
        {
            return new Result<T>()
            {
                Type = EnumType.Failure,
                IsSuccess = false,
                Message = message
            };
        }
    }

    public enum EnumType
    {
        None,
        Success,
        SystemError,
        Failure,
        ValidationError
    };
}
