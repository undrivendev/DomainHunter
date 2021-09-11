using Dapper.Contrib.Extensions;
using System;

namespace DomainHunter.DAL
{
    [Table("domain")]
    public class PsqlDomainDto : BasePsqlDto
    {
        public string name { get; set; }
        public string tld { get; set; }
        public DateTime? expiration { get; set; }
        public DateTime? @checked { get; set; }

        [Write(false)]
        [Computed]
        public PsqlDomainStatusDto status { get; set; }
    }
}
