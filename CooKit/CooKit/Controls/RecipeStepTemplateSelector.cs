using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public sealed class RecipeStepTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextRecipeStepTemplate { get; set; }
        public DataTemplate UnknownRecipeStepTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is IRecipeStep step))
                return null;

            return step.Type switch
            {
                RecipeStepType.TextOnly => TextRecipeStepTemplate,

                _ => UnknownRecipeStepTemplate
            };
        }
    }
}
