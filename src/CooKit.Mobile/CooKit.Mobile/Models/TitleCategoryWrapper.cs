using System.Collections.Generic;

namespace CooKit.Mobile.Models
{
    public class TitleCategoryWrapper : BaseModel
    {
        public string Name => InnerCategory.Name;
        public ICategory InnerCategory { get; set; }
        public IList<Recipe> PreviewRecipes { get; set; }

        public TitleCategoryWrapper()
        {
        }

        public TitleCategoryWrapper(ICategory innerCategory)
        {
            InnerCategory = innerCategory;
        }

        public TitleCategoryWrapper(ICategory innerCategory, IList<Recipe> previewRecipes)
        {
            InnerCategory = innerCategory;
            PreviewRecipes = previewRecipes;
        }
    }
}
