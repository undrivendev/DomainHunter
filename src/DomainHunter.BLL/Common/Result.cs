using System.Collections.Generic;

namespace DomainHunter.BLL.Common
{
    public sealed class Result<T>
    {
        public Result(bool success, params Error[] errors)
            : this(success, default(T), errors)
        {
        }

        public Result(T data)
            : this(true, data, null)
        {
        }

        public Result(bool success, T data, params Error[] errors)
        {
            Data = data;
            Success = success;
            Errors = errors;
        }

        public T Data { get; private set; }
        public IEnumerable<Error> Errors { get; private set; }
        public bool Success { get; private set; }
    }
}