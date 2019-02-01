using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    /// <summary>
    /// check also https://www.icann.org/resources/pages/epp-status-codes-2014-06-16-en
    /// </summary>
    /*
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
     */
    [Flags]
    public enum DomainStatus
    {
        error = -1,
        nowhois = 0,
        ok = 1,
        serverHold = 2,
        redemptionPeriod = 4,
        addPeriod = 8,
        autoRenewPeriod = 16,
        inactive = 32,
        pendingCreate = 64,
        pendingDelete = 128,
        pendingRenew = 256,
        pendingRestore = 512,
        pendingTransfer = 1024,
        pendingUpdate = 2048,
        renewPeriod = 4096,
        serverDeleteProhibited = 8192,
        serverRenewProhibited = 16384,
        serverTransferProhibited = 32768,
        serverUpdateProhibited = 65536,
        transferPeriod = 131072
    }
}
