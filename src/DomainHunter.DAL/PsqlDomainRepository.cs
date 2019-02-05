using Dapper;
using Dapper.Contrib.Extensions;
using DomainHunter.BLL;
using Ladasoft.Common.Base;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
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
                var sql = @"SELECT * FROM domain d INNER JOIN domainstatus ds ON d.id = ds.domainid";
                return (await conn.QueryAsync<PsqlDomainDto, PsqlDomainStatusDto, PsqlDomainDto>(sql,
                    (domain, status) => 
                    {
                        domain.status = status;
                        return domain;
                    }))
                    .Select(_mapper.Map<PsqlDomainDto, Domain>)
                    .ToList();
            }
        }

        public async Task<int> Insert(Domain domain)
        {
            var dto = _mapper.Map<Domain, PsqlDomainDto>(domain);
            using (var conn = new NpgsqlConnection(_psqlParameters.ConnectionString))
            {
                var id = await conn.InsertAsync(dto);
                dto.status.domainid = id;
                await conn.InsertAsync(dto.status);
                return id;
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
