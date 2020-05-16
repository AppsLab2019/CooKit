using System;
using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.Selectors
{
    public sealed class StepTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item switch
            {
                ITextStep _ => TextTemplate,
                IImageStep _ => ImageTemplate,

                _ => throw new ArgumentException(nameof(item))
            };
        }
    }
}
