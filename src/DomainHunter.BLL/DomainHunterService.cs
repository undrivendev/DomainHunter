using Mds.Common.Logging;
using System;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public class DomainHunterService
    {
        private readonly ILogger _logger;
        private readonly IDomainChecker _domainStatusChecker;
        private readonly IRandomNameGenerator _randomNameGenerator;
        private readonly IDomainRepository _domainRepository;
        private readonly DomainHunterParameters _parameters;

        public DomainHunterService(
            ILogger logger,
            IDomainChecker domainStatusChecker,
            IRandomNameGenerator randomNameGenerator,
            IDomainRepository domainRepository,
            DomainHunterParameters parameters
            )
        {
            _logger = logger;
            _domainStatusChecker = domainStatusChecker;
            _randomNameGenerator = randomNameGenerator;
            _domainRepository = domainRepository;
            _parameters = parameters;
        }

        public async Task HuntName()
        {
            var currentBaseName = _randomNameGenerator.GenerateName(_parameters.Length);
            var finalName = $"{currentBaseName}.{_parameters.Tld}";
            var currentDomain = new Domain() { Name = finalName };
            if (!(await _domainRepository.IsChecked(currentDomain)))
            {
                currentDomain.Status = await _domainStatusChecker.GetStatus(currentDomain);
                if (currentDomain.Status == DomainStatus.Free)
                {
                    _logger.Log($"found free domain {currentBaseName}.{_parameters.Tld}");
                }
                currentDomain.Timestamp = DateTime.UtcNow;
                await _domainRepository.Insert(currentDomain);
            }
        }

    }
}
