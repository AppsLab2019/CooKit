using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace CooKit.Mappers.Converters.Guids
{
    public sealed class GuidListToStringConverter : IGuidListToStringConverter
    {
        private const char Separator = ';';

        public string Convert(IList<Guid> sourceMember, ResolutionContext context)
        {
            if (sourceMember is null)
                return null;

            return sourceMember.Count switch
            {
                0 => string.Empty,
                1 => sourceMember[0].ToString(),

                _ => sourceMember.Select(id => id.ToString())
                    .Aggregate((id1, id2) => $"{id1}{Separator}{id2}")
            };
        }
    }
}
