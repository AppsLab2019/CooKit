using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Delegates;
using CooKit.Models.Recipes;
using CooKit.Services.Messages;

namespace CooKit.Services.History
{
    // TODO: implement this
    public sealed class HistoryService : IHistoryService
    {
        private readonly IMessageBroker _broker;
        private readonly LinkedList<IRecipe> _lastRecipes;

        public HistoryService(IMessageBroker broker)
        {
            if (broker is null)
                throw new ArgumentNullException(nameof(broker));

            _broker = broker;
            _lastRecipes = new LinkedList<IRecipe>();
            SubscribeToViewMessage();
        }

        private void SubscribeToViewMessage()
        {
            MessageHandlerParam<IRecipe> handler = OnRecipeViewed;
            _broker.Subscribe(this, handler, "View");
        }

        private Task OnRecipeViewed(object _, string __, IRecipe recipe)
        {
            return Add(recipe);
        }

        public Task Add(IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            _lastRecipes.Remove(recipe);
            _lastRecipes.AddFirst(recipe);
            return RaiseHistoryChanged();
        }

        public Task Remove(IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            _lastRecipes.Remove(recipe);
            return RaiseHistoryChanged();
        }

        private Task RaiseHistoryChanged()
        {
            return _broker.Send(this, "Change");
        }

        public Task<IList<IRecipe>> GetLastVisitedRecipes()
        {
            IList<IRecipe> copiedList = _lastRecipes.ToList();
            return Task.FromResult(copiedList);
        }
    }
}
