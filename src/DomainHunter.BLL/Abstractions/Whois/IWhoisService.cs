using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainHunter.BLL.Whois
{
    public interface IWhoisService
    {
        Task<string> GetWhoisResponseForDomain(Domain domain);
    }
}
