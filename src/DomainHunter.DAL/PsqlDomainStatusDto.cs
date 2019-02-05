using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.DAL
{
    [Table("domainstatus")]
    public class PsqlDomainStatusDto : BasePsqlDto
    {
        public int domainid { get; set; }
        public bool error { get; set; }
        public bool nowhois { get; set; }
        public bool registrarlock { get; set; }
        public bool ok { get; set; }
        public bool serverhold { get; set; }
        public bool redemptionperiod { get; set; }
        public bool addperiod { get; set; }
        public bool autorenewperiod { get; set; }
        public bool inactive { get; set; }
        public bool pendingcreate { get; set; }
        public bool pendingdelete { get; set; }
        public bool pendingrenew { get; set; }
        public bool pendingrestore { get; set; }
        public bool pendingtransfer { get; set; }
        public bool pendingupdate { get; set; }
        public bool renewperiod { get; set; }
        public bool serverdeleteprohibited { get; set; }
        public bool serverrenewprohibited { get; set; }
        public bool servertransferprohibited { get; set; }
        public bool serverupdateprohibited { get; set; }
        public bool transferperiod { get; set; }
        public bool clientdeleteprohibited { get; set; }
        public bool clienthold { get; set; }
        public bool clientrenewprohibited { get; set; }
        public bool clienttransferprohibited { get; set; }
        public bool clientupdateprohibited { get; set; }
    }
}
