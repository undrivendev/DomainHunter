using AutoMapper;
using DomainHunter.BLL;
using DomainHunter.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.Console
{
    public static class AutoMapperConfiguration
    {
        public static void Add(IMapperConfigurationExpression cfg)
        {
            // -###- repository
            cfg.CreateMap<Domain, PsqlDomainDto>();
            //reverse
            cfg.CreateMap<PsqlDomainDto, Domain>();
        }
    }
}
