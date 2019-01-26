using Mds.Common.Base;
using Mds.Common.Logging;
using System;
using System.Threading.Tasks;
using Whois;

namespace DomainHunter.BLL
{
    public class WhoisDomainNameChecker : IDomainChecker
    {
        private readonly ILogger _logger;

        public WhoisDomainNameChecker(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<Result<bool>> CheckName(string name, string tld)
        {
            var finalName = $"{name}.{tld}";
            var whois = new WhoisLookup();
            string response = null;
            try
            {
                response = (await whois.LookupAsync(finalName)).Content;
            }
            catch (Exception)
            {
                return Result.FailedResult<bool>(new Error(ErrorCode.GENERIC_ERROR, ErrorLevel.Error, "error while checking the domain"));
            }
           
            return Result.SuccessResult(!String.IsNullOrWhiteSpace(response) && response.Substring(0, 12).ToLowerInvariant() == "no match for");
        }
    }
}
