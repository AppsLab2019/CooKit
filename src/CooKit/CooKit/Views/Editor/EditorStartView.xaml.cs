using CooKit.ViewModels.Editor;

namespace CooKit.Views.Editor
{
    public partial class EditorStartView
    {
        public EditorStartView() => 
            InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is EditorStartViewModel vm)
                vm.RefreshCommand.Execute(null);
        }
    }
}