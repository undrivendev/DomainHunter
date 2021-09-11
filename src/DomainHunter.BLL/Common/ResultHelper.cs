namespace DomainHunter.BLL.Common
{
    public static class Result
    {
        public static Result<T> FailedResult<T>(params Error[] errors) => new Result<T>(false, errors);
        public static Result<T> SuccessResult<T>(T data, params Error[] errors) => new Result<T>(true, data, errors);

        public static Result<Unit> SuccessResult(params Error[] errors) => new Result<Unit>(true, default(Unit), errors);
        public static Result<Unit> FailedResult(params Error[] errors) => new Result<Unit>(false, default(Unit), errors);
    }
}