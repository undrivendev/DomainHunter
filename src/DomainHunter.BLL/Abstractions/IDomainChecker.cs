using Mds.Common.Base;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public interface IDomainChecker
    {
        Task<Result<bool>> CheckName(string name, string tld);
    }
}
