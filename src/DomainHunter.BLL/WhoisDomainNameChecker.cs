using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DomainHunter.BLL
{
    public class WhoisDomainNameChecker : IDomainNameChecker
    {
        public bool CheckName(string name, string tld)
        {
            var whois = new WhoisLookup();
            var response = await whois.LookupAsync("github.com");
            return response.Content;
            return false;
        }
    }
}
