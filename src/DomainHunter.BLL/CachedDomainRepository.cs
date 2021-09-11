using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public class CachedDomainRepository : IDomainRepository
    {
        HashSet<string> _cache = new HashSet<string>();

        private readonly IDomainRepository _decoratedImplementation;

        public CachedDomainRepository(IDomainRepository decoratedImplementation)
        {
            _decoratedImplementation = decoratedImplementation;

            InitializeCache().Wait();
        }

        private async Task InitializeCache()
        {
            _cache = new HashSet<string>((await _decoratedImplementation.GetAll()).Select(e => e.ToString()));
        }

        public Task<IEnumerable<Domain>> GetAll()
            => _decoratedImplementation.GetAll();

        public async Task<int> Insert(Domain domain)
        {
            var newId = await _decoratedImplementation.Insert(domain);
            _cache.Add(domain.ToString());
            return newId;
        }

        /// <summary>
        /// real benefit of cached class
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Task<bool> IsChecked(Domain domain)
            => Task.FromResult(_cache.Contains(domain.ToString()));
        
    }
}
