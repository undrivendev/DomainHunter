namespace DomainHunter.BLL.Whois
{
    public class BaseServerSelector
    {
        protected readonly ServerSelectorOptions _options;

        public BaseServerSelector(ServerSelectorOptions options)
        {
            _options = options;
        }
    }
}