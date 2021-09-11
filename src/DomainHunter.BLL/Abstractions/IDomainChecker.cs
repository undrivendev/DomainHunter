using System;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public interface IDomainChecker
    {
        Task<(DomainStatus, DateTime?)> GetStatusAndExpirationDate(Domain domain);
    }
}
