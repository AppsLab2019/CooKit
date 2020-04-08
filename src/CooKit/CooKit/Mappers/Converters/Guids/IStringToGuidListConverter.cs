using System;
using System.Collections.Generic;
using AutoMapper;

namespace CooKit.Mappers.Converters.Guids
{
    public interface IStringToGuidListConverter : IValueConverter<string, IList<Guid>>
    {
    }
}
