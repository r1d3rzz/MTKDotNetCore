namespace BlogWebAPI.ResponseModel
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public bool IsError { get { return !IsSuccess; } }

        public bool IsValidationError { get { return Type == EnumType.ValidationError; } }

        public bool IsSystemError { get { return Type == EnumType.SystemError; } }

        public bool IsNetworkError { get { return Type == EnumType.NetworkError; } }

        public EnumType Type { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public static Result<T> Success(T data, string message = "")
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumType.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<List<T>> Success(List<T> data, string message = "")
        {
            return new Result<List<T>>
            {
                IsSuccess = true,
                Type = EnumType.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<T> ValidationError(string message = "")
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumType.ValidationError,
                Message = message
            };
        }

        public static Result<T> SystemError(string message = "")
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumType.SystemError,
                Message = message
            };
        }

        public static Result<T> NetworkError(string message = "")
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumType.NetworkError,
                Message = message
            };
        }

    }

    public enum EnumType
    {
        None,
        Success,
        ValidationError,
        SystemError,
        NetworkError,
    }
}
