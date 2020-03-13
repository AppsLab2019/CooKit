using System;
using System.Threading.Tasks;

namespace CooKit.Models
{
    public interface IIngredientBuilder
    {
        IBuilderProperty<IIngredientBuilder, Guid> Id { get; }
        IBuilderProperty<IIngredientBuilder, string> Name { get; }

        IBuilderProperty<IIngredientBuilder, string> ImageLoader { get; }
        IBuilderProperty<IIngredientBuilder, string> ImageSource { get; }

        Task<IIngredient> BuildAsync();
    }
}
