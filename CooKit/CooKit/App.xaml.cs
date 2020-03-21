using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CooKit.Services;
using CooKit.Services.Impl;
using CooKit.Services.Impl.ImageLoaders;
using CooKit.Services.Impl.SQLite;
using SQLite;
using XF.Material.Forms;

namespace CooKit
{
    public partial class App
    {
        public IIngredientStore IngredientStore { get; private set; }
        public IPictogramStore PictogramStore { get; private set; }
        public IRecipeStepStore RecipeStepStore { get; private set; }
        public IRecipeStore RecipeStore { get; private set; }
        public IImageStore ImageStore { get; private set; }

        public App()
        {
            InitializeComponent();
            Material.Init(this, "Material.Configuration");
        }

        protected override async void OnStart()
        {
            ImageStore = new ImageStoreImpl();
            ImageStore.RegisterLoader(new FileImageLoader());
            ImageStore.RegisterLoader(new UriImageLoader());

            var dbConnection = await OpenDbConnection();

            IngredientStore = await new SQLiteIngredientStoreBuilder()
                .ImageStore.Set(ImageStore)
                .DatabaseConnection.Set(dbConnection)
                .BuildAsync();

            PictogramStore = await new SQLitePictogramStoreBuilder()
                .ImageStore.Set(ImageStore)
                .DatabaseConnection.Set(dbConnection)
                .BuildAsync();

            RecipeStepStore = await new SQLiteRecipeStepStoreBuilder()
                .ImageStore.Set(ImageStore)
                .DatabaseConnection.Set(dbConnection)
                .BuildAsync();

            RecipeStore = await new SQLiteRecipeStoreBuilder()
                .ImageStore.Set(ImageStore)
                .IngredientStore.Set(IngredientStore)
                .PictogramStore.Set(PictogramStore)
                .RecipeStepStore.Set(RecipeStepStore)
                .DatabaseConnection.Set(dbConnection)
                .BuildAsync();

            MainPage = new AppShell();
        }

        private async Task<SQLiteAsyncConnection> OpenDbConnection()
        {
            var path = GetDefaultDbPath();

            if (!File.Exists(path))
                await ExtractDb(path);

            return new SQLiteAsyncConnection(path);
        }

        private static string GetDefaultDbPath()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folder, "CooKit.db3");
        }

        private async Task ExtractDb(string path)
        {
            if (!Resources.ContainsKey("Resources.DbAssemblyPath"))
                throw new Exception();

            var assemblyResource = (string) Resources["Resources.DbAssemblyPath"];
            await using var stream = GetType().Assembly.GetManifestResourceStream(assemblyResource) 
                                     ?? throw new Exception();

            await using var file = new FileStream(path, FileMode.Create, FileAccess.Write);

            await stream.CopyToAsync(file);
        }

        #region Store Getters
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
        internal static IRecipeStepStore GetRecipeStepStore() =>
            ((App) Current).RecipeStepStore;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IImageStore GetImageStore() =>
            ((App) Current).ImageStore;
        #endregion
    }
}
