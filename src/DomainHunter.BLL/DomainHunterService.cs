using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public class DomainHunterService
    {
        private readonly IDomainChecker _domainChecker;
        private readonly IRandomNameGenerator _randomNameGenerator;
        private readonly IDomainRepository _domainRepository;
        private readonly DomainHunterParameters _parameters;

        public DomainHunterService(
            IDomainChecker domainChecker,
            IRandomNameGenerator randomNameGenerator,
            IDomainRepository domainRepository,
            DomainHunterParameters parameters
            )
        {
            _domainChecker = domainChecker;
            _randomNameGenerator = randomNameGenerator;
            _domainRepository = domainRepository;
            _parameters = parameters;
        }

        public async Task HuntName()
        {
            while (true)
            {
                var currentName = _randomNameGenerator.GenerateName(_parameters.Length);
                var currentDomain = new Domain() { Name = currentName, Timestamp = DateTime.UtcNow };

                var result = await _domainChecker.CheckName(currentName, _parameters.Tld);
                if (result.Success)
                {
                    if (result.Data)
                    {
                        currentDomain.Status = DomainStatus.Free;
                    }
                    else
                    {
                        currentDomain.Status = DomainStatus.Taken;
                    }
                }
                else
                {
                    currentDomain.Status = DomainStatus.Error;
                }
                await _domainRepository.Insert(currentDomain);

                Thread.Sleep(_parameters.SleepMs);
            }
        }

    }
}
