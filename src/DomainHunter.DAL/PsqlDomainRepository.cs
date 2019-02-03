using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using DomainHunter.BLL;
using Ladasoft.Common.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainHunter.DAL
{
    public class PsqlDomainRepository : IDomainRepository
    {
        private readonly PsqlParameters _psqlParameters;
        private readonly IMapper _mapper;

        public PsqlDomainRepository(PsqlParameters psqlParameters, IMapper mapper)
        {
            _psqlParameters = psqlParameters;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain>> GetAll()
        {
            using (var conn = new NpgsqlConnection(_psqlParameters.ConnectionString))
            {
                return (await conn.GetAllAsync<PsqlDomainDto>()).Select(_mapper.Map<PsqlDomainDto, Domain>);
            }
        }

        public async Task<int> Insert(Domain domain)
        {
            using (var conn = new NpgsqlConnection(_psqlParameters.ConnectionString))
            {
                return await conn.InsertAsync(_mapper.Map<Domain, PsqlDomainDto>(domain));
            }
        }

        public async Task<bool> IsChecked(Domain domain)
        {
            using (var conn = new NpgsqlConnection(_psqlParameters.ConnectionString))
            {
                return await conn.ExecuteScalarAsync<bool>("select exists(select 1 from domain where name = @name and tld = @tld)", new { name = domain.Name, tld = domain.Tld });
            }
        }
    }
}
