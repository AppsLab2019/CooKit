using System;
using System.Globalization;
using System.Text;
using CooKit.Models.Ingredients;
using Xamarin.Forms;

namespace CooKit.Converters
{
    public sealed class IngredientToFormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            if (!(value is IIngredient ingredient))
                return value;

            var builder = new StringBuilder(ingredient.Template.Name);

            if (!string.IsNullOrEmpty(ingredient.Note))
            {
                builder.Append(" (");
                builder.Append(ingredient.Note);
                builder.Append(')');
            }

            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotSupportedException();
    }
}
