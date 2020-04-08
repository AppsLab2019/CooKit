using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace CooKit.Mappers.Converters.Guids
{
    public sealed class StringToGuidListConverter : IStringToGuidListConverter
    {
        private const char Separator = ';';

        public IList<Guid> Convert(string sourceMember, ResolutionContext context)
        {
            var rawIds = sourceMember?.Split(Separator);
            return rawIds?.Select(Guid.Parse).ToList();
        }
    }
}
