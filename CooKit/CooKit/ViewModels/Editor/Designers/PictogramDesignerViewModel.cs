using CooKit.Models;

namespace CooKit.ViewModels.Editor.Designers
{
    public sealed class PictogramDesignerViewModel : BaseSimpleDesignerViewModel<IPictogram, IPictogramBuilder>
    {
        public PictogramDesignerViewModel() : base(App.GetPictogramStore()) { }
    }
}
