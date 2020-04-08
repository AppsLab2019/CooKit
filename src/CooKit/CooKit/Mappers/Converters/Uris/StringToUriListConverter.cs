using System;
using System.Collections.Generic;
using AutoMapper;

namespace CooKit.Mappers.Converters.Uris
{
    public sealed class StringToUriListConverter : IStringToUriListConverter
    {
        public IList<Uri> Convert(string sourceMember, ResolutionContext context)
        {
            if (sourceMember is null)
                return null;

            throw new NotImplementedException();
        }
    }
}
