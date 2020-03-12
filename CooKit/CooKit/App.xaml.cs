using System.Runtime.CompilerServices;
using CooKit.Services;
using CooKit.Services.Impl;
using CooKit.Services.Impl.ImageLoaders;
using CooKit.Services.Impl.SQLite;

namespace CooKit
{
    public partial class App
    {
        public IIngredientStore IngredientStore { get; private set; }
        public IPictogramStore PictogramStore { get; private set; }

        public IRecipeStore RecipeStore { get; private set; }
        public IImageStore ImageStore { get; private set; }

        public App() => 
            InitializeComponent();

        protected override async void OnStart()
        {
            ImageStore = new ImageStoreImpl();
            ImageStore.RegisterLoader(new FileImageLoader());
            ImageStore.RegisterLoader(new UriImageLoader());

            RecipeStore = await new SQLiteRecipeStoreBuilder()
                .ImageStore.Set(ImageStore)
                .BuildAsync();

            IngredientStore = (IIngredientStore) RecipeStore;
            PictogramStore = (IPictogramStore) RecipeStore;

            MainPage = new AppShell();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IIngredientStore GetIngredientStore() =>
            ((App) Current).IngredientStore;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IPictogramStore GetPictogramStore() =>
            ((App) Current).PictogramStore;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IRecipeStore GetRecipeStore() =>
            ((App) Current).RecipeStore;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IImageStore GetImageStore() =>
            ((App) Current).ImageStore;
    }
}
