using System;
using CooKit.Mobile.Models.Steps;
using CooKit.Mobile.Views.Steps;
using Xamarin.Forms;

namespace CooKit.Mobile.Selectors.Steps
{
    // TODO: refactor the logic out of this class (registry or provider)
    public class RecipeDetailStepTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _textTemplate;
        private readonly DataTemplate _imageTemplate;

        public RecipeDetailStepTemplateSelector()
        {
            _textTemplate = new DataTemplate(typeof(TextStepView));
            _imageTemplate = new DataTemplate(typeof(ImageStepView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item == null)
                return null;

            return item switch
            {
                TextStep _ => _textTemplate,
                ImageStep _ => _imageTemplate,
                _ => throw new NotImplementedException()
            };
        }
    }
}
