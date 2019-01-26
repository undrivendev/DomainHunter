using Mds.Common.Base;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public interface IDomainChecker
    {
        Task<DomainStatus> GetStatus(Domain domain);
    }
}
