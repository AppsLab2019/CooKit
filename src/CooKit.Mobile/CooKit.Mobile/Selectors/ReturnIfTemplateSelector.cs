using Xamarin.Forms;

namespace CooKit.Mobile.Selectors
{
    public class ReturnIfTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item is DataTemplate template ? template : null;
        }
    }
}
