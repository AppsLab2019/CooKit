using System;
using System.Collections.Generic;
using AutoMapper;

namespace CooKit.Mappers.Converters.Guids
{
    public interface IGuidListToStringConverter : IValueConverter<IList<Guid>, string>
    {
    }
}
