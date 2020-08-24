using System;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Services.Publish
{
    public class MockPublishService : IPublishService
    {
        public async Task PublishRecipeAsync(Recipe recipe, IProgress<string> progress)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe));

            for (var i = 0; i < 3; i++)
            {
                progress?.Report($"Uploading image {i + 1}.");
                await Task.Delay(2000);
            }

            progress?.Report("Publishing recipe!");
            await Task.Delay(1000);
        }
    }
}
