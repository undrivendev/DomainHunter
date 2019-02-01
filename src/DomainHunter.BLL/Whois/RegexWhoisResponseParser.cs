using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DomainHunter.BLL.Whois
{
    public class RegexWhoisResponseParser : IWhoisResponseParser
    {
        public bool IsValidResponse(string input)
        {
            return ParseIsNoMatch(input) || input.Substring(input.IndexOf(input.First(c => Regex.IsMatch(c.ToString(), @"\S"))), 11).ToLowerInvariant() == "domain name";
        }

        public bool ParseIsNoMatch(string input)
        {
            return input.Substring(input.IndexOf(input.First(c => Regex.IsMatch(c.ToString(), @"\S"))), 12).ToLowerInvariant() == "no match for";
        }

        public DateTime ParseRegistrarExpirationDate(string input)
        {
            return DateTime.Parse(new Regex(@"(?<=Expiration.*:).+", RegexOptions.IgnoreCase).Match(input).Value.Trim()); ;
        }

        public string ParseRegistrarServerName(string input)
        {
            return new Regex(@"(?<=Registrar WHOIS Server: ).+", RegexOptions.IgnoreCase).Match(input).Value.Trim();
        }

        public DomainStatus GetDomainStatus(string input)
        {
            throw new NotImplementedException();
        }

      
    }
}
