namespace DomainHunter.BLL.Common
{
    public sealed class Error
    {
        public Error(ErrorCode code, ErrorLevel level, string message = null, object data = null)
        {
            Level = level;
            Code = code;
            Message = message;
            Data = data;
        }

        public ErrorLevel Level { get; private set; }
        public ErrorCode Code { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }
}