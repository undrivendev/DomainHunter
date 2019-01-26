using Mds.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.Console
{
    public class AutomapperWrapper : IMapper
    {
        private readonly AutoMapper.IMapper _mapper;

        public AutomapperWrapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            => this._mapper.Map<TSource, TDestination>(source);

        public object Map(object source, Type sourceType, Type destinationType)
            => this._mapper.Map(source, sourceType, destinationType);

        public object Map(object source, object destination, Type sourceType, Type destinationType)
            => this._mapper.Map(source, destination, sourceType, destinationType);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
           => this._mapper.Map<TSource, TDestination>(source, destination);
    }
}
