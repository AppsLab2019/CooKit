using System;
using System.Collections.Generic;
using AutoMapper;

namespace CooKit.Mappers.Converters.Uris
{
    public sealed class UriListToStringConverter : IUriListToStringConverter
    {
        public string Convert(IList<Uri> sourceMember, ResolutionContext context)
        {
            if (sourceMember is null)
                return null;

            if (sourceMember.Count == 0)
                return string.Empty;

            throw new NotImplementedException();
        }
    }
}
