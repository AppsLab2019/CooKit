using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CooKit.Services
{
    public interface IJsonStore
    {
        string GetIngredientJson(Guid guid);
        Task<string> GetIngredientJsonAsync(Guid guid);

        string GetPictogramJson(Guid guid);
        Task<string> GetPictogramJsonAsync(Guid guid);

        string GetRecipeJson(Guid guid);
        Task<string> GetRecipeJsonAsync(Guid guid);

        IReadOnlyList<Guid> GetNextRecipeIds();
        Task<IReadOnlyList<Guid>> GetNextRecipeIdsAsync();
    }
}
