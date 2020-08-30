using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Images;
using CooKit.Mobile.Providers.ResourcePath;
using CooKit.Mobile.Repositories.Recipes;

namespace CooKit.Mobile.Services.Publish
{
    // TODO: add logging
    public class LocalPublishService : IPublishService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IResourcePathProvider _pathProvider;

        public LocalPublishService(IRecipeRepository recipeRepository, IResourcePathProvider pathProvider)
        {
            _recipeRepository = recipeRepository;
            _pathProvider = pathProvider;
        }

        public async Task PublishRecipeAsync(Recipe recipe, IProgress<string> progress)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe));

            var localImages = recipe.Images
                .Append(recipe.PreviewImage)
                .Where(image => image.Type == ImageType.File);

            foreach (var image in localImages)
            {
                progress?.Report($"Copying image {image.Data}.");
                await CopyImageAsync(image);
            }

            if (recipe.Id != 0)
            {
                progress?.Report("Adding updated recipe to database.");
                await _recipeRepository.UpdateRecipeAsync(recipe);
            }
            else
            {
                progress?.Report("Adding recipe to database.");
                await _recipeRepository.AddRecipeAsync(recipe);
            }
        }

        // TODO: clean this up
        private async Task CopyImageAsync(Image image)
        {
            var basePath = _pathProvider.GetResourceFolderPath();
            var extension = Path.GetExtension(image.Data);
            var name = $"{Guid.NewGuid():D}{extension}";
            var path = Path.Combine(basePath, name);
                
            await using var sourceStream = File.Open(image.Data!, FileMode.Open, FileAccess.Read);
            await using var destinationStream = File.Open(path, FileMode.CreateNew, FileAccess.Write);
            await sourceStream.CopyToAsync(destinationStream);

            image.Type = ImageType.LocalResource;
            image.Data = name;
        }
    }
}
