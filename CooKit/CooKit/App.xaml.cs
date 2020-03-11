using System.Threading.Tasks;
using CooKit.Services;
using CooKit.Services.Impl;
using CooKit.Services.Impl.ImageLoaders;
using CooKit.Services.Impl.Json;

namespace CooKit
{
    public partial class App
    {
        public IRecipeStore RecipeStore { get; private set; }
        public IImageStore ImageStore { get; private set; }

        public App() => 
            InitializeComponent();

        protected override async void OnStart()
        {
            ImageStore = new ImageStoreImpl();
            ImageStore.RegisterLoader("FileImageLoader", new FileImageLoader());
            ImageStore.RegisterLoader("UriImageLoader", new UriImageLoader());

            var recipeStoreBuilder = new JsonRecipeStoreBuilder()
                .ImageStore.Set(ImageStore)
                .JsonStore.Set(new MockJsonStore());

            RecipeStore = await Task.Run(recipeStoreBuilder.Build);

            MainPage = new AppShell();
        }
    }
}
