using Autofac;
using CooKit.Models;
using CooKit.Services;

namespace CooKit.ViewModels.Editor.Designers
{
    public sealed class PictogramDesignerViewModel : BaseSimpleDesignerViewModel<IPictogram, IPictogramBuilder>
    {
        public PictogramDesignerViewModel() : base(App.Container.Resolve<IPictogramStore>()) { }
    }
}
