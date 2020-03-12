using CooKit.Services;
using CooKit.Services.Impl;
using CooKit.Services.Impl.ImageLoaders;
using CooKit.Services.Impl.SQLite;

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

            RecipeStore = await new SQLiteRecipeStoreBuilder()
                .ImageStore.Set(ImageStore)
                .BuildAsync();

            MainPage = new AppShell();
        }
    }
}
