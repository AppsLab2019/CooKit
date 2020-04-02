using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.Controls
{
    public sealed class StepTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextStepTemplate { get; set; }
        public DataTemplate ImageStepTemplate { get; set; }
        public DataTemplate UnknownStepTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is IStep step))
                return null;

            return step.Type switch
            {
                StepType.Text => TextStepTemplate,
                StepType.Image => ImageStepTemplate,

                _ => UnknownStepTemplate
            };
        }
    }
}
