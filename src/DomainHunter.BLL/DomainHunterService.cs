using Mds.Common.Logging;
using System;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public class DomainHunterService
    {
        private readonly ILogger _logger;
        private readonly IDomainChecker _domainChecker;
        private readonly IRandomNameGenerator _randomNameGenerator;
        private readonly IDomainRepository _domainRepository;
        private readonly DomainHunterParameters _parameters;

        public DomainHunterService(
            ILogger logger,
            IDomainChecker domainChecker,
            IRandomNameGenerator randomNameGenerator,
            IDomainRepository domainRepository,
            DomainHunterParameters parameters
            )
        {
            _logger = logger;
            _domainChecker = domainChecker;
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
                var domainData = await _domainChecker.GetStatusAndExpirationDate(currentDomain);
                currentDomain.Status = domainData.Item1;
                currentDomain.Expiration = domainData.Item2;

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
