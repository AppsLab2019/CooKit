namespace CooKit.Models.Editor.Steps
{
    public sealed class EditorTextStep : BaseEditorModel, IEditorTextStep
    {
        public string Text
        {
            get => _text;
            set => OnPropertyChanged(ref _text, value);
        }

        private string _text;
    }
}
