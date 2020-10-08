namespace CooKit.Mobile.Models.Ingredients
{
    public class Ingredient : BaseModel
    {
        public string Text
        {
            get => _text;
            set => OnPropertyChanged(ref _text, value);
        }

        private string _text;

        public Ingredient()
        {
        }

        public Ingredient(string text)
        {
            _text = text;
        }
    }
}
