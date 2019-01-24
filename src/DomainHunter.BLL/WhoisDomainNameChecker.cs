using System.Threading.Tasks;
using Whois;

namespace DomainHunter.BLL
{
    public class WhoisDomainNameChecker : IDomainNameChecker
    {
        public async Task<bool> CheckName(string name, string tld)
        {
            var finalName = $"{name}.{tld}";
            var whois = new WhoisLookup();
            var response = await whois.LookupAsync(finalName);
            return response.Content.Substring(0, 12).ToLowerInvariant() == "no match for";
        }
    }
}
