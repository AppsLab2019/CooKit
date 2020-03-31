using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using CooKit.Services;
using CooKit.Services.Impl;
using CooKit.Services.Impl.ImageLoaders;
using CooKit.Services.SQLite;
using SQLite;
using XF.Material.Forms;

namespace CooKit
{
    public partial class App
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            InitializeComponent();
            Material.Init(this, "Material.Configuration");
        }

        protected override async void OnStart()
        {
            var imageStore = new ImageStoreImpl();
            imageStore.RegisterLoader(new FileImageLoader());
            imageStore.RegisterLoader(new UriImageLoader());

            var dbConnection = await OpenDbConnection();

            var ingredientStore = await ISQLiteIngredientStoreBuilder
                .CreateDefault()
                .Connection.Set(dbConnection)
                .ImageStore.Set(imageStore)
                .BuildAsync();

            var pictogramStore = await ISQLitePictogramStoreBuilder
                .CreateDefault()
                .Connection.Set(dbConnection)
                .ImageStore.Set(imageStore)
                .BuildAsync();

            var recipeStepStore = await ISQLiteStepStoreBuilder
                .CreateDefault()
                .Connection.Set(dbConnection)
                .ImageStore.Set(imageStore)
                .BuildAsync();

            var recipeStore = await ISQLiteRecipeStoreBuilder
                .CreateDefault()
                .Connection.Set(dbConnection)
                .ImageStore.Set(imageStore)
                .IngredientStore.Set(ingredientStore)
                .PictogramStore.Set(pictogramStore)
                .StepStore.Set(recipeStepStore)
                .BuildAsync();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(imageStore).As<IImageStore>();
            builder.RegisterInstance(pictogramStore).As<IPictogramStore>();
            builder.RegisterInstance(ingredientStore).As<IIngredientStore>();
            builder.RegisterInstance(recipeStepStore).As<IRecipeStepStore>();
            builder.RegisterInstance(recipeStore).As<IRecipeStore>();

            Container = builder.Build();
            MainPage = new AppShell();
        }

        private async Task<SQLiteAsyncConnection> OpenDbConnection()
        {
            var path = GetDefaultDbPath();

            if (!File.Exists(path))
                await ExtractDb(path);

            return new SQLiteAsyncConnection(path);
        }

        internal static string GetDefaultDbPath()
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
    }
}
