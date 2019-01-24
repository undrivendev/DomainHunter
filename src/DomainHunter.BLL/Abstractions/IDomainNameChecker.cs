namespace DomainHunter.BLL
{
    public interface IDomainNameChecker
    {
        bool CheckName(string name, string tld);
    }
}
