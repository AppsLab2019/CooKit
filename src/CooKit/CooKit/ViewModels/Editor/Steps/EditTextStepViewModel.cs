﻿using CooKit.Models.Editor.Steps;

namespace CooKit.ViewModels.Editor.Steps
{
    public sealed class EditTextStepViewModel : BaseEditStepViewModel<IEditorTextStep>
    {
        protected override IEditorTextStep CreateStep()
        {
            return new EditorTextStep { Text = "Example Text" };
        }

        protected override IEditorTextStep CloneStep(IEditorTextStep step)
        {
            return step is null ? null : new EditorTextStep { Text = step.Text };
        }

        protected override void ProjectStep(IEditorTextStep from, IEditorTextStep target)
        {
            if (from is null || target is null)
                return;

            target.Text = from.Text;
        }
    }
}
