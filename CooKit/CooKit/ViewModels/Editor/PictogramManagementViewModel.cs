using Autofac;
using CooKit.Models;
using CooKit.Services;
using CooKit.Views.Editor.Designers;

namespace CooKit.ViewModels.Editor
{
    public sealed class PictogramManagementViewModel : SharedManagementViewModel<IPictogram, IPictogramBuilder>
    {
        public PictogramManagementViewModel() : 
            base(App.Container.Resolve<IPictogramStore>(), typeof(PictogramDesignerView)) { }
    }
}
