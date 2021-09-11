using DomainHunter.BLL.Whois;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL.Whois
{
    public interface IWhoisResponseParser
    {
        bool IsValidResponse(string input);
        bool ParseIsNoMatch(string input);
        DateTime ParseRegistrarExpirationDate(string input);
        string ParseRegistrarServerName(string input);
        DomainStatus GetDomainStatus(string input);        
    }
}
