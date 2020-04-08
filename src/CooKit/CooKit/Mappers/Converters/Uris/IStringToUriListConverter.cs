using System;
using System.Collections.Generic;
using AutoMapper;

namespace CooKit.Mappers.Converters.Uris
{
    public interface IStringToUriListConverter : IValueConverter<string, IList<Uri>>
    {
    }
}
