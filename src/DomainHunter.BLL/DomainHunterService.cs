﻿using System;
using System.Threading.Tasks;
using DomainHunter.BLL.Common;

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
            try
            {
                var generatedName = _randomNameGenerator.GenerateName(_parameters.Length);
                var currentDomain = new Domain() { Name = generatedName, Tld = _parameters.Tld };
                if (!(await _domainRepository.IsChecked(currentDomain)))
                {
                    var domainData = await _domainChecker.GetStatusAndExpirationDate(currentDomain);
                    currentDomain.Status = domainData.Item1;
                    currentDomain.Expiration = domainData.Item2;

                    if (currentDomain.Status.NoWhois)
                    {
                        _logger.Log($"found free domain {currentDomain}");
                    }
                    currentDomain.Checked = DateTime.UtcNow;
                    await _domainRepository.Insert(currentDomain);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

    }
}
