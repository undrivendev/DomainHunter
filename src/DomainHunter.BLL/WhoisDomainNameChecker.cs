using Mds.Common.Base;
using Mds.Common.Logging;
using System;
using System.Threading.Tasks;
using Whois;
using Whois.Models;

namespace DomainHunter.BLL
{
    public class WhoisDomainChecker : IDomainChecker
    {
        private readonly ILogger _logger;

        public WhoisDomainChecker(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<(DomainStatus, DateTime?)> GetStatusAndExpirationDate(Domain domain)
        {
            var whois = new WhoisLookup();
            WhoisResponse whoisResponse = null;
            try
            {
                whoisResponse = (await whois.LookupAsync(domain.ToString()));
                if (!IsTaken(whoisResponse) && whoisResponse.ParsedResponse == null)
                {
                    throw new Exception("parsed response is null");
                }
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

        private (DomainStatus, DateTime?) ExtractDataFromWhoisResponse(WhoisResponse whoisResponse)
        {
            DateTime? expirationDate = null;
            var status = IsTaken(whoisResponse) ? DomainStatus.Free : DomainStatus.Taken;
            if (status == DomainStatus.Taken)
            {
                expirationDate = whoisResponse.ParsedResponse.Expiration;
            }
            return (status, expirationDate);
        }

        private bool IsTaken(WhoisResponse whoisResponse) => !String.IsNullOrWhiteSpace(whoisResponse.Content) && whoisResponse.Content.Substring(0, 12).ToLowerInvariant() == "no match for";
        
    }
}
