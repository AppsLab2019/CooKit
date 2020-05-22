using System;
using System.Globalization;
using System.Linq;
using System.Text;
using CooKit.Models.Ingredients;
using CooKit.Services.Units;
using Xamarin.Forms;

namespace CooKit.Converters
{
    public sealed class IngredientToFormattedStringConverter : IValueConverter
    {
        public IUnitService UnitService;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            if (!(value is IIngredient ingredient))
                return value;

            var builder = new StringBuilder();

            if (ingredient.Quantity > 0)
            {
                FormatQuantity(ingredient, builder);
                builder.Append(' ');
            }

            builder.Append(ingredient.Template.Name);

            if (!string.IsNullOrEmpty(ingredient.Note))
            {
                builder.Append(" (");
                builder.Append(ingredient.Note);
                builder.Append(')');
            }

            return builder.ToString();
        }

        private void FormatQuantity(IIngredient ingredient, StringBuilder builder)
        {
            var rawQuantity = ingredient.Quantity;
            var category = ingredient.Template.UnitCategory;
            var units = UnitService.GetAvailableUnits(category);

            var valuesWithAbbreviation = units
                .Select(unit => new { Value = unit.Multiplier * rawQuantity, unit.Abbreviation })
                .ToArray();

            var finalPair = valuesWithAbbreviation
                .Where(pair => pair.Value >= 1.0f)
                .OrderBy(pair => pair.Value)
                .FirstOrDefault() 
                            ?? valuesWithAbbreviation
                .Where(pair => pair.Value < 1.0f)
                .OrderByDescending(pair => pair.Value)
                .First();

            builder.Append(finalPair.Value);
            builder.Append(finalPair.Abbreviation);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotSupportedException();
    }
}
