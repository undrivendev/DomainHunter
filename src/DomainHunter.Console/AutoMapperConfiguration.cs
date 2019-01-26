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
            cfg.CreateMap<Domain, PsqlDomainDto>()
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => (byte)src.Status));
            //reverse
            cfg.CreateMap<PsqlDomainDto, Domain>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (DomainStatus)src.status));
        }
    }
}
