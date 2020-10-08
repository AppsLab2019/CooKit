using CooKit.Mobile.Models;

namespace CooKit.Mobile.Contexts.Entities
{
    public class RecipePictogramPair
    {
        public int RecipeId { get; set; }
        public RecipeEntity Recipe { get; set; }

        public int PictogramId { get; set; }
        public Pictogram Pictogram { get; set; }

        public RecipePictogramPair()
        {
        }

        public RecipePictogramPair(RecipeEntity recipe, Pictogram pictogram)
        {
            Recipe = recipe;
            Pictogram = pictogram;

            RecipeId = recipe.Id;
            PictogramId = pictogram.Id;
        }
    }
}
