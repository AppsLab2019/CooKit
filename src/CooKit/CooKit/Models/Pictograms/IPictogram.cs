using System;

namespace CooKit.Models.Pictograms
{
    public interface IPictogram : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }

        Uri Icon { get; set; }
    }
}
