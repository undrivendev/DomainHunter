using DomainHunter.BLL.Whois;
using System;
using System.Threading.Tasks;
using DomainHunter.BLL.Common;

namespace DomainHunter.BLL
{
    /// <summary>
    /// http://howtointernet.net/dnsrecords.html
    /// </summary>
    public class WhoisDomainChecker : IDomainChecker
    {
        private readonly ILogger _logger;
        private readonly IWhoisService _whoisService;
        private readonly IWhoisResponseParser _whoisResponseParser;

        public WhoisDomainChecker(
            ILogger logger, 
            IWhoisService whoisService,
            IWhoisResponseParser whoisResponseParser)
        {
            _logger = logger;
            _whoisService = whoisService;
            _whoisResponseParser = whoisResponseParser;
        }

        public async Task<(DomainStatus, DateTime?)> GetStatusAndExpirationDate(Domain domain)
        {
            var whoisResult = await _whoisService.GetWhoisResponseForDomain(domain);
            if (whoisResult.Success)
            {
                return ExtractDataFromWhoisResponse(whoisResult.Data);
            }

            return (new DomainStatus() { Error = true }, null);
        }

        private (DomainStatus, DateTime?) ExtractDataFromWhoisResponse(string whoisResponse)
        {
            DateTime? expirationDate = null;
            var status = new DomainStatus();

            if (_whoisResponseParser.ParseIsNoMatch(whoisResponse)) //free
            {
                status.NoWhois = true;
            }
            else //taken
            {
                expirationDate = _whoisResponseParser.ParseRegistrarExpirationDate(whoisResponse);
                status = _whoisResponseParser.GetDomainStatus(whoisResponse);
            }
            return (status, expirationDate);
        }
    }
}
