using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public interface IDomainNameChecker
    {
        Task<bool> CheckName(string name, string tld);
    }
}
