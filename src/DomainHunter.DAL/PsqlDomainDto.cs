using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.DAL
{
    [Table("domain")]
    public class PsqlDomainDto : BasePsqlDto
    {
        public string name { get; set; }
        public string tld { get; set; }
        public byte status { get; set; }
        public DateTime? expiration { get; set; }
        public DateTime? @checked { get; set; }
    }
}
