namespace CooKit.Models.Units
{
    public sealed class UnitInfo : IUnitInfo
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public float Multiplier { get; set; }
        public UnitCategory Category { get; set; }

        public UnitInfo()
        {
        }

        public UnitInfo(string name, string abbreviation, float multiplier, UnitCategory category)
        {
            Name = name;
            Abbreviation = abbreviation;
            Multiplier = multiplier;
            Category = category;
        }
    }
}
