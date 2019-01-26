using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainHunter.BLL.Whois
{
    public class DefaultWhoisService : IWhoisService
    {


        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient?view=netstandard-2.0
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<string> GetWhoisResponseForDomain(Domain domain)
        {
            var initialServer = "whois.verisign-grs.com";

            var result = await GetResponseFromServer(initialServer, domain);
            var finalServer = ParseForServerName(result);

            if (finalServer.ToLowerInvariant() != initialServer.ToLowerInvariant())
            {
                result = await GetResponseFromServer(finalServer, domain);
            }

            return result;
        }

        private string ParseForServerName(string whoisString)
            => Regex.Match(whoisString, @"(?<=Registrar WHOIS Server: ).+\r").Value.Trim();
        

        private async Task<string> GetResponseFromServer(string server, Domain domain)
        {
            var result = "";
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
                    while ((numBytesRead = netStream.Read(responseData, 0, responseData.Length)) > 0)
                    {
                        memStream.Write(responseData, 0, numBytesRead);
                    }
                    result = Encoding.ASCII.GetString(memStream.ToArray(), 0, (int)memStream.Length);
                }
            }
            return result;
        }
    }
}
