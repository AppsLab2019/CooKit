﻿using CooKit.Models.Ingredients;
using CooKit.Services.Repositories.Ingredients;

namespace CooKit.Services.Stores.Ingredients
{
    public sealed class IngredientTemplateStore : RepositoryStore<IIngredientTemplate>, IIngredientTemplateStore
    {
        public IngredientTemplateStore(IIngredientTemplateRepository repository) : base(repository)
        {
        }
    }
}
