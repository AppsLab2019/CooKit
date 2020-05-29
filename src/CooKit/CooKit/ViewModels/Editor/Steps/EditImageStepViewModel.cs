using CooKit.Models.Editor.Steps;

namespace CooKit.ViewModels.Editor.Steps
{
    public sealed class EditImageStepViewModel : BaseEditStepViewModel<IEditorImageStep>
    {
        protected override IEditorImageStep CreateStep()
        {
            return new EditorImageStep { Image = string.Empty };
        }

        protected override IEditorImageStep CloneStep(IEditorImageStep step)
        {
            return step is null ? null : new EditorImageStep { Image = step.Image };
        }

        protected override void ProjectStep(IEditorImageStep from, IEditorImageStep target)
        {
            if (from is null || target is null)
                return;

            target.Image = from.Image;
        }
    }
}
