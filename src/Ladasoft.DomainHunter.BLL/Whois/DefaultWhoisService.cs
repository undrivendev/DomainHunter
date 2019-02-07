using Ladasoft.Common.Base;
using Ladasoft.Common.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainHunter.BLL.Whois
{
    public class DefaultWhoisService : IWhoisService
    {
        private readonly ILogger _logger;
        private readonly IServerSelector _serverSelector;
        private readonly IWhoisResponseParser _whoisResponseParser;

        public DefaultWhoisService(ILogger logger, IServerSelector serverSelector, IWhoisResponseParser whoisResponseParser)
        {
            _logger = logger;
            _serverSelector = serverSelector;
            _whoisResponseParser = whoisResponseParser;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient?view=netstandard-2.0
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<Result<string>> GetWhoisResponseForDomain(Domain domain)
        {
            var initialServer = _serverSelector.GetServer();

            var result = await GetResponseFromServer(initialServer, domain);
            if (result.Success)
            {
                if (_whoisResponseParser.ParseIsNoMatch(result.Data))
                {
                    return result;
                }
                var finalServer = _whoisResponseParser.ParseRegistrarServerName(result.Data);
                if (finalServer.ToLowerInvariant() != initialServer.ToLowerInvariant())
                {
                    result = await GetResponseFromServer(finalServer, domain);
                    if (result.Success)
                    {
                        return result;
                    }
                }
            }
            return Result.FailedResult<string>(result.Errors.ToArray());
        }

        private async Task<Result<string>> GetResponseFromServer(string server, Domain domain)
        {
            var resultString = "";

            try
            {
                TcpClient client = new TcpClient(server, 43);
                using (var netStream = client.GetStream())
                {
                    //request
                    var requestData = System.Text.Encoding.ASCII.GetBytes($"{domain}\r\n");
                    await netStream.WriteAsync(requestData, 0, requestData.Length);

                    byte[] responseData = new byte[1024];
                    using (MemoryStream memStream = new MemoryStream())
                    {

                        int numBytesRead;
                        while ((numBytesRead = await netStream.ReadAsync(responseData, 0, responseData.Length)) > 0)
                        {
                            memStream.Write(responseData, 0, numBytesRead);
                        }
                        resultString = Encoding.ASCII.GetString(memStream.ToArray(), 0, (int)memStream.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"failed to get domain information from whois server. parameters: server={server}, domain={JsonConvert.SerializeObject(domain)}";
                _logger.Log(new LogEntry(LoggingEventType.Error, errorMessage));
                _logger.Log(ex);
                return Result.FailedResult<string>(new Error(ErrorCode.GENERIC_ERROR, ErrorLevel.Error, errorMessage));
            }

            if (_whoisResponseParser.IsValidResponse(resultString))
            {
                return Result.SuccessResult(resultString);
            }
            else
            {
                return Result.FailedResult<string>(new Error(ErrorCode.GENERIC_ERROR, ErrorLevel.Error, resultString));
            }
        }
    }
}
