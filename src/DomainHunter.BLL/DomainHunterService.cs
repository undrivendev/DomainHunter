using System.Threading;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public class DomainHunterService
    {
        private readonly IDomainNameChecker _domainNameChecker;
        private readonly IRandomNameGenerator _randomNameGenerator;
        private readonly IDomainSaver _domainSaver;
        private readonly DomainHunterParameters _parameters;

        public DomainHunterService(
            IDomainNameChecker domainNameChecker,
            IRandomNameGenerator randomNameGenerator,
            IDomainSaver domainSaver,
            DomainHunterParameters parameters
            )
        {
            _domainNameChecker = domainNameChecker;
            _randomNameGenerator = randomNameGenerator;
            _domainSaver = domainSaver;
            _parameters = parameters;
        }

        public async Task HuntName()
        {
            while (true)
            {
                var currentName = _randomNameGenerator.GenerateName(_parameters.Length);
                if (await _domainNameChecker.CheckName(currentName, _parameters.Tld))
                {
                    _domainSaver.SaveDomain(currentName);
                }
                Thread.Sleep(_parameters.SleepMs);
            }
        }

    }
}
