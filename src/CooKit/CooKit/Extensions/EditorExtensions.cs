using System;
using System.Linq;
using CooKit.Models.Editor.Ingredients;
using CooKit.Models.Editor.Recipes;
using CooKit.Models.Editor.Steps;
using CooKit.Models.Ingredients;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;

namespace CooKit.Extensions
{
    public static class EditorExtensions
    {
        public static IEditorIngredient ToEditorIngredient(this IIngredient ingredient)
        {
            if (ingredient is null)
                throw new ArgumentNullException(nameof(ingredient));

            return new EditorIngredient
            {
                Template = ingredient.Template,
                Note = ingredient.Note,
                Quantity = ingredient.Quantity
            };
        }

        public static IEditorStep ToEditorStep(this IStep step)
        {
            if (step is null)
                throw new ArgumentNullException(nameof(step));

            return step switch
            {
                ITextStep textStep => ToEditorStep(textStep),
                IImageStep imageStep => ToEditorStep(imageStep),

                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static IEditorTextStep ToEditorStep(this ITextStep step)
        {
            if (step is null)
                throw new ArgumentNullException(nameof(step));

            return new EditorTextStep { Text = step.Text };
        }

        public static IEditorImageStep ToEditorStep(this IImageStep step)
        {
            if (step is null)
                throw new ArgumentNullException(nameof(step));

            return new EditorImageStep { Image = step.Image };
        }

        public static IEditorRecipe ToEditorRecipe(this IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            var images = recipe.Images?.ToObservableCollection();
            var pictograms = recipe.Pictograms?.ToObservableCollection();

            var ingredients = recipe.Ingredients?
                .Select(ToEditorIngredient)
                .ToObservableCollection();

            var steps = recipe.Steps?
                .Select(ToEditorStep)
                .ToObservableCollection();

            return new EditorRecipe
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                EstimatedTime = recipe.EstimatedTime,

                PreviewImage = recipe.PreviewImage,
                ObservableImages = images,

                ObservableIngredients = ingredients,
                ObservablePictograms = pictograms,
                ObservableSteps = steps
            };
        }
    }
}
