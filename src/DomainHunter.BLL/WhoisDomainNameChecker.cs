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

        public async Task<DomainStatus> GetStatus(Domain domain)
        {
            var whois = new WhoisLookup();
            string response = null;
            try
            {
                response = (await whois.LookupAsync(domain.Name)).Content;
            }
            catch (Exception)
            {
                _logger.Log(new LogEntry(LoggingEventType.Warning, $"error while checking domain {domain}"));
                return DomainStatus.Error;
            }

            return !String.IsNullOrWhiteSpace(response) && response.Substring(0, 12).ToLowerInvariant() == "no match for" ? DomainStatus.Free : DomainStatus.Taken;
        }
    }
}
