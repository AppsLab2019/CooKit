using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeIntroduction
    {
        public RecipeIntroduction() => 
            InitializeComponent();
    }

    // TODO: remove this
    public class TemporaryIngredientInfo
    {
        public ImageSource Icon { get; set; }
        public string Name { get; set; }
    }

    public class TemporaryPictogram
    {
        public ImageSource Icon { get; set; }
    }
}