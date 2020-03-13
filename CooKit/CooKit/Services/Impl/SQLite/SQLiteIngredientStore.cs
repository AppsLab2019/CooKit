using System;
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

        protected internal override SQLiteIngredient CreateObjectFromBuilder(IIngredientBuilder builder)
        {
            throw new NotImplementedException();
        }

        protected internal override SQLiteIngredient CreateObjectFromInfo(SQLiteIngredientInfo info) =>
            new SQLiteIngredient(info)
            {
                Icon = _imageStore.LoadImage(info.ImageLoader, info.ImageSource)
            };
    }
}
