using CooKit.Models.Ingredients;

namespace CooKit.Models.Editor.Ingredients
{
    public sealed class EditorIngredient : BaseEditorModel, IEditorIngredient
    {
        public IIngredientTemplate Template
        {
            get => _template;
            set => OnPropertyChanged(ref _template, value);
        }

        public string Note
        {
            get => _note;
            set => OnPropertyChanged(ref _note, value);
        }

        public float Quantity
        {
            get => _quantity;
            set => OnPropertyChanged(ref _quantity, value);
        }

        private IIngredientTemplate _template;
        private string _note;
        private float _quantity;
    }
}
