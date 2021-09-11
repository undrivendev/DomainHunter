using System;

namespace DomainHunter.BLL.Common
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);

        object Map(object source, Type sourceType, Type destinationType);

        object Map(object source, object destination, Type sourceType, Type destinationType);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}