using CooKit.ViewModels.Editor;

namespace CooKit.Views.Editor
{
    public partial class EditorMainView
    {
        public EditorMainView() => 
            InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is EditorMainViewModel vm)
                vm.InitCommand.Execute(null);
        }
    }
}