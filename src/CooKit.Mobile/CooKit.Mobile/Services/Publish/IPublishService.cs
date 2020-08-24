using System;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Services.Publish
{
    public interface IPublishService
    {
        Task PublishRecipeAsync(Recipe recipe, IProgress<string> progress);
    }
}
