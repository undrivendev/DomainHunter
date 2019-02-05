using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainHunter.BLL;
using DomainHunter.BLL.Whois;
using DomainHunter.DAL;
using Ladasoft.Common.Base;
using Ladasoft.Common.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace DomainHunter.Service
{
    public class Startup
    {
        private Container _container = new Container();
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            IntegrateSimpleInjector(services);
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            _container.Verify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();


            StartApplication();
        }


        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services

            //COMMON
            _container.Register<ILogger>(() => new SerilogLoggingProxy(Serilog.Log.Logger), Lifestyle.Singleton);
            
            //AUTOMAPPER
            //automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = false;
                AutoMapperConfiguration.Add(cfg);
            });
            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
            _container.Register<IMapper>(() => new AutomapperWrapper(AutoMapper.Mapper.Instance), Lifestyle.Singleton);
            _container.Register<PsqlParameters>(() => new PsqlParameters(_configuration.GetConnectionString("Main")), Lifestyle.Singleton);

            _container.Register<IDomainRepository, PsqlDomainRepository>(Lifestyle.Singleton);
            _container.RegisterDecorator<IDomainRepository, CachedDomainRepository>(Lifestyle.Singleton);
            _container.Register<DomainHunterParameters>(() => new DomainHunterParameters()
            {
                Length = int.Parse(_configuration["DomainLength"]),
                SleepMs = int.Parse(_configuration["DomainSleepMs"]),
                Tld = _configuration["DomainTld"]
            }, Lifestyle.Singleton);
            _container.Register<ServerSelectorOptions>(() => new ServerSelectorOptions()
            {
                Servers = new string[]
                {
                        "whois.verisign-grs.com"
                }
            }, Lifestyle.Singleton);

            _container.Register<IRandomNameGenerator, DefaultRandomNameGenerator>(Lifestyle.Singleton);
            _container.Register<IRandomNumberGenerator, DefaultRandomNumberGenerator>(Lifestyle.Singleton);
            _container.Register<IServerSelector, RandomServerSelector>(Lifestyle.Singleton);
            _container.Register<IWhoisResponseParser, RegexWhoisResponseParser>(Lifestyle.Singleton);
            _container.Register<IWhoisService, DefaultWhoisService>(Lifestyle.Singleton);
            _container.Register<IDomainChecker, WhoisDomainChecker>(Lifestyle.Singleton);

            _container.Register<DomainHunterService>(Lifestyle.Singleton);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            _container.AutoCrossWireAspNetComponents(app);
        }

        private void StartApplication()
        {
            var logger = _container.GetInstance<ILogger>();
            logger.Log("Starting the hunt...");

            var service = _container.GetInstance<DomainHunterService>();
            Task.Run(() => 
            {
                while (true)
                {
                    service.HuntName().Wait();
                }
            });

            //var concurrentTaskNumber = int.Parse(configuration["ConcurrentTaskNumber"]);
            //StartJobConcurrently(concurrentTaskNumber, service).Wait();
        }
    }
}
