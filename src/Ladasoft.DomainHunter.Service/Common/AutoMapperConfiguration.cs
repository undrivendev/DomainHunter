using AutoMapper;
using DomainHunter.BLL;
using DomainHunter.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.Service
{
    public static class AutoMapperConfiguration
    {
        public static void Add(IMapperConfigurationExpression cfg)
        {
            // -###- repository
            cfg.CreateMap<DomainStatus, PsqlDomainStatusDto>()
                .ForMember(dest => dest.domainid, opt => opt.Ignore());
            //reverse
            cfg.CreateMap<PsqlDomainStatusDto, DomainStatus>();

            // -###- repository
            cfg.CreateMap<Domain, PsqlDomainDto>();
            //reverse
            cfg.CreateMap<PsqlDomainDto, Domain>();
        }
    }
}
