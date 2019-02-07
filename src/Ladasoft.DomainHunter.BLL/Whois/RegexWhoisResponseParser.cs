using Ladasoft.Common.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DomainHunter.BLL.Whois
{
    public class RegexWhoisResponseParser : IWhoisResponseParser
    {
        private readonly ILogger _logger;

        public RegexWhoisResponseParser(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsValidResponse(string input)
        {
            return ParseIsNoMatch(input) || input.Substring(input.IndexOf(input.First(c => Regex.IsMatch(c.ToString(), @"\S"))), 11).ToLowerInvariant() == "domain name";
        }

        public bool ParseIsNoMatch(string input)
        {
            return input.Substring(input.IndexOf(input.First(c => Regex.IsMatch(c.ToString(), @"\S"))), 12).ToLowerInvariant() == "no match for";
        }

        public DateTime ParseRegistrarExpirationDate(string input)
        {
            var dateString = new Regex(@"(?<=Expiration.*:).+", RegexOptions.IgnoreCase).Match(input).Value.Trim();
            DateTime parsedDateTime;
            //2019-12-24-T19:36:59Z
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd-THH:mm:ssZ", CultureInfo.InvariantCulture,
                           DateTimeStyles.None,
                           out parsedDateTime))
            { 
                return parsedDateTime;
            }
            return DateTime.Parse(dateString);
        }

        public string ParseRegistrarServerName(string input)
        {
            return new Regex(@"(?<=WHOIS Server.*:).+", RegexOptions.IgnoreCase).Match(input).Value.Trim();
        }

        public DomainStatus GetDomainStatus(string input)
        {
            var allStringStatuses = new Regex(@"(?<=Status.*:).+?(?= (http|-))", RegexOptions.IgnoreCase).Matches(input);
            DomainStatus finalStatus = new DomainStatus();
            foreach (var currentStatus in allStringStatuses)
            {
                var trimmedStatus = currentStatus.ToString().Trim().ToLowerInvariant().Replace("*", "");
                switch (trimmedStatus)
                {
                    case "registrar-lock":
                        finalStatus.RegistrarLock = true;
                        break;
                    case "ok":
                        finalStatus.Ok = true;
                        break;
                    case "serverhold":
                        finalStatus.ServerHold = true;
                        break;
                    case "redemptionperiod":
                        finalStatus.RedemptionPeriod = true;
                        break;
                    case "addperiod":
                        finalStatus.AddPeriod = true;
                        break;
                    case "autorenewperiod":
                        finalStatus.AutoRenewPeriod = true;
                        break;
                    case "inactive":
                        finalStatus.Inactive = true;
                        break;
                    case "pendingcreate":
                        finalStatus.PendingCreate = true;
                        break;
                    case "pendingdelete":
                        finalStatus.PendingDelete = true;
                        break;
                    case "pendingrenew":
                        finalStatus.PendingRenew = true;
                        break;
                    case "pendingrestore":
                        finalStatus.PendingRestore = true;
                        break;
                    case "pendingtransfer":
                        finalStatus.PendingTransfer = true;
                        break;
                    case "pendingupdate":
                        finalStatus.PendingUpdate = true;
                        break;
                    case "renewperiod":
                        finalStatus.RenewPeriod = true;
                        break;
                    case "serverdeleteprohibited":
                        finalStatus.ServerDeleteProhibited = true;
                        break;
                    case "serverrenewprohibited":
                        finalStatus.ServerRenewProhibited = true;
                        break;
                    case "servertransferprohibited":
                        finalStatus.ServerTransferProhibited = true;
                        break;
                    case "serverupdateprohibited":
                        finalStatus.ServerUpdateProhibited = true;
                        break;
                    case "transferperiod":
                        finalStatus.TransferPeriod = true;
                        break;
                    case "clientdeleteprohibited":
                        finalStatus.ServerDeleteProhibited = true;
                        break;
                    case "clienthold":
                        finalStatus.ServerRenewProhibited = true;
                        break;
                    case "clientrenewprohibited":
                        finalStatus.ServerTransferProhibited = true;
                        break;
                    case "clienttransferprohibited":
                        finalStatus.ServerUpdateProhibited = true;
                        break;
                    case "clientupdateprohibited":
                        finalStatus.TransferPeriod = true;
                        break;
                    default:
                        _logger.Log(new LogEntry(LoggingEventType.Error, $"unknown domain status: {trimmedStatus}"));
                        finalStatus.Error = true;
                        break;
                }
            }
            return finalStatus;
        }

      
    }
}
