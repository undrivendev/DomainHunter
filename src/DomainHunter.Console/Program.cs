using DomainHunter.BLL;
using DomainHunter.DAL;
using Mds.Common.Base;
using Mds.Common.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DomainHunter.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();

            Serilog.Log.Logger = new Serilog.LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

            try
            {
                //COMMON
                Mds.Common.Logging.ILogger logger = new SerilogLoggingProxy(Log.Logger);

                //AUTOMAPPER
                //automapper
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMissingTypeMaps = false;
                    AutoMapperConfiguration.Add(cfg);
                });
                AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
                IMapper mapper = new AutomapperWrapper(AutoMapper.Mapper.Instance);

                //REPOSITORY
                PsqlParameters psqlParameters = new PsqlParameters(configuration.GetConnectionString("Main"));
                IDomainRepository domainRepository = new PsqlDomainRepository(psqlParameters, mapper);

                //APP SERVICES
                var huntParameters = new DomainHunterParameters()
                {
                    Length = int.Parse(configuration["DomainLength"]),
                    SleepMs = int.Parse(configuration["DomainSleepMs"]),
                    Tld = configuration["DomainTld"]
                };
                IDomainChecker domainNameChecker = new WhoisDomainNameChecker(logger);
                IRandomNumberGenerator randomNumberGenerator = new DefaultRandomNumberGenerator();
                IRandomNameGenerator randomNameGenerator = new DefaultRandomNameGenerator(randomNumberGenerator);

              
                var service = new DomainHunterService(
                    logger,
                    domainNameChecker,
                    randomNameGenerator,
                    domainRepository,
                    huntParameters);

                logger.Log("Starting the hunt...");
                while (true)
                {
                    service.HuntName().Wait();
                }

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
