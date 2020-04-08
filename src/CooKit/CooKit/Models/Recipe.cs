using System;
using System.Collections.Generic;

namespace CooKit.Models
{
    public sealed class Recipe : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Uri PreviewImage { get; set; }
        //public IList<Uri> Images { get; set; }

        public IList<Guid> IngredientIds { get; set; }
        public IList<Guid> PictogramIds { get; set; }
    }
}
