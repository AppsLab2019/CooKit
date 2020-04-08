using System;
using System.Collections.Generic;
using AutoMapper;

namespace CooKit.Mappers.Converters.Uris
{
    public interface IUriListToStringConverter : IValueConverter<IList<Uri>, string>
    {
    }
}
