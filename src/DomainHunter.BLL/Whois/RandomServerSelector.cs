using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainHunter.BLL.Whois
{
    public class RandomServerSelector : BaseServerSelector, IServerSelector
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public RandomServerSelector(ServerSelectorOptions options, IRandomNumberGenerator randomNumberGenerator)
            : base(options)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string GetServer()
            => _options.Servers[_randomNumberGenerator.GenerateRandomNumber(_options.Servers.Length - 1)];
    }
}
