namespace CooKit.Models.Editor.Steps
{
    public sealed class EditorImageStep : BaseEditorModel, IEditorImageStep
    {
        public string Image
        {
            get => _image;
            set => OnPropertyChanged(ref _image, value);
        }

        private string _image;
    }
}
