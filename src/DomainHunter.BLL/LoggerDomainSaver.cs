using Mds.Common.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    public class LoggerDomainSaver : IDomainSaver
    {
        private readonly ILogger _logger;

        public LoggerDomainSaver(ILogger logger)
        {
            _logger = logger;
        }

        public void SaveDomain(string name)
        {
            _logger.Log(new LogEntry(LoggingEventType.Information, $"Found free domain: {name}"));
        }
    }
}
