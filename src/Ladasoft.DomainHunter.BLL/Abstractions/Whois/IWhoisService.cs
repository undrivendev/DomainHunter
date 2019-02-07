using Ladasoft.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainHunter.BLL.Whois
{
    public interface IWhoisService
    {
        Task<Result<string>> GetWhoisResponseForDomain(Domain domain);
    }
}
