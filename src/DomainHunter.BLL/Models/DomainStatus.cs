using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    /// <summary>
    /// check also https://www.icann.org/resources/pages/epp-status-codes-2014-06-16-en
    /// </summary>
    /*
BY REGISTRY:
addPeriod
autoRenewPeriod
inactive
ok
pendingCreate
pendingDelete
pendingRenew
pendingRestore
pendingTransfer
pendingUpdate
redemptionPeriod
renewPeriod
serverDeleteProhibited
serverHold
serverRenewProhibited
serverTransferProhibited
serverUpdateProhibited
transferPeriod

BY REGISTRAR:
clientDeleteProhibited
clientHold
clientRenewProhibited
clientTransferProhibited
clientUpdateProhibited
     */
    public class DomainStatus : BaseModel
    {
        //BY REGISTRY
        public bool Error    { get; set; }
        public bool NoWhois { get; set; }
        public bool RegistrarLock { get; set; }
        public bool Ok { get; set; }
        public bool ServerHold { get; set; }
        public bool RedemptionPeriod { get; set; }
        public bool AddPeriod { get; set; }
        public bool AutoRenewPeriod { get; set; }
        public bool Inactive { get; set; }
        public bool PendingCreate { get; set; }
        public bool PendingDelete { get; set; }
        public bool PendingRenew { get; set; }
        public bool PendingRestore { get; set; }
        public bool PendingTransfer { get; set; }
        public bool PendingUpdate { get; set; }
        public bool RenewPeriod { get; set; }
        public bool ServerDeleteProhibited { get; set; }
        public bool ServerRenewProhibited { get; set; }
        public bool ServerTransferProhibited { get; set; }
        public bool ServerUpdateProhibited { get; set; }
        public bool TransferPeriod { get; set; }

        //BY REGISTRAR
        public bool ClientDeleteProhibited { get; set; }
        public bool ClientHold { get; set; }
        public bool ClientRenewProhibited { get; set; }
        public bool ClientTransferProhibited { get; set; }
        public bool ClientUpdateProhibited { get; set; }
    }
}
