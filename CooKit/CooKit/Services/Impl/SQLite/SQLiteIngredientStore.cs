using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;
using CooKit.Models.Impl.SQLite;
using SQLite;

namespace CooKit.Services.Impl.SQLite
{
    internal sealed class SQLiteIngredientStore : SQLiteStoreBase<IIngredient, IIngredientBuilder, SQLiteIngredient, SQLiteIngredientInfo>, IIngredientStore
    {
        private readonly IImageStore _imageStore;

        internal SQLiteIngredientStore(SQLiteAsyncConnection connection, IImageStore imageStore)
            : base(connection) => _imageStore = imageStore;

        public override IIngredientBuilder CreateBuilder() =>
            new StoreCallbackIngredientBuilder(this);

        protected internal override async Task<SQLiteIngredient> CreateObjectFromBuilder(IIngredientBuilder builder)
        {
            var info = new SQLiteIngredientInfo
            {
                Id = builder.Id.Value,
                Name = builder.Name.Value,

                ImageLoader = builder.ImageLoader.Value,
                ImageSource = builder.ImageSource.Value
            };

            return new SQLiteIngredient(info)
            {
                Icon = await _imageStore.LoadImageAsync(info.ImageLoader, info.ImageSource)
            };
        }

        protected internal override async Task<SQLiteIngredient> CreateObjectFromInfo(SQLiteIngredientInfo info) =>
            new SQLiteIngredient(info)
            {
                Icon = await _imageStore.LoadImageAsync(info.ImageLoader, info.ImageSource)
            };
    }
}
