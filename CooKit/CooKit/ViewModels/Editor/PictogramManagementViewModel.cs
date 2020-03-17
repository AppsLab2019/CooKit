using CooKit.Models;
using CooKit.Views.Editor.Designers;

namespace CooKit.ViewModels.Editor
{
    public sealed class PictogramManagementViewModel : SharedManagementViewModel<IPictogram, IPictogramBuilder>
    {
        public PictogramManagementViewModel() : 
            base(App.GetPictogramStore(), typeof(PictogramDesignerView)) { }
    }
}
