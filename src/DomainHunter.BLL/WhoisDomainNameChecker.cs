using DomainHunter.BLL.Whois;
using Mds.Common.Base;
using Mds.Common.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public class WhoisDomainChecker : IDomainChecker
    {
        private readonly ILogger _logger;
        private readonly IWhoisService _whoisService;

        public WhoisDomainChecker(ILogger logger, IWhoisService whoisService)
        {
            _logger = logger;
            _whoisService = whoisService;
        }

        public async Task<(DomainStatus, DateTime?)> GetStatusAndExpirationDate(Domain domain)
        {
            var whoisResponse = "";
            try
            {
                whoisResponse = await _whoisService.GetWhoisResponseForDomain(domain);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                _logger.Log(new LogEntry(LoggingEventType.Warning, $"error while checking domain {domain}"));
                return (DomainStatus.Error, null);
            }
            return ExtractDataFromWhoisResponse(whoisResponse);
        }

        private (DomainStatus, DateTime?) ExtractDataFromWhoisResponse(string whoisResponse)
        {
            DateTime? expirationDate = null;
            var status = IsTaken(whoisResponse) ? DomainStatus.Free : DomainStatus.Taken;
            if (status == DomainStatus.Taken)
            {
                expirationDate = ParseExpirationDate(whoisResponse);
            }
            return (status, expirationDate);
        }

        private bool IsTaken(string whoisResponse) => !String.IsNullOrWhiteSpace(whoisResponse) && whoisResponse.Substring(0, 12).ToLowerInvariant() == "no match for";
        private DateTime? ParseExpirationDate(string whoisResponse)
        {
            return DateTime.Parse(Regex.Match(whoisResponse, @"(?<=Registrar Registration Expiration Date: ).+\r").Value.Trim());
        }

    }
}
