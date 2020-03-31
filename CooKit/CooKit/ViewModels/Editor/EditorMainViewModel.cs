﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using CooKit.Services;
using CooKit.Views.Editor;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : BaseViewModel
    {
        public ICommand ManageIngredientsCommand { get; }
        public ICommand ManagePictogramsCommand { get; }
        public ICommand ManageRecipesCommand { get; }

        public ICommand ExtractDbCommand { get; }
        public ICommand PurgeStepsCommand { get; }

        public EditorMainViewModel()
        {
            ManageIngredientsCommand = new Command(() => PushAsync(new IngredientManagementView()));
            ManagePictogramsCommand = new Command(() => PushAsync(new PictogramManagementView()));
            ManageRecipesCommand = new Command(() => PushAsync(new RecipeManagementView()));

            ExtractDbCommand = new Command(HandleExtractDb);
            PurgeStepsCommand = new Command(HandlePurgeSteps);
        }

        public async void HandleExtractDb()
        {
            if (!await RequestPermission<Permissions.StorageRead>())
                return;

            if (!await RequestPermission<Permissions.StorageWrite>())
                return;

            try
            {
                File.Copy(App.GetDefaultDbPath(), @"/storage/emulated/0/CooKit.db3", true);
            }
            catch (Exception e)
            {
                await DisplayAlert("Exception", $"There was an exception: {e}", "Cancel");
                return;
            }

            await DisplayAlert("Success", "Database successfully extracted!", "Ok");
        }

        private async Task<bool> RequestPermission<T>() where T : Permissions.BasePermission, new()
        {
            var status = await Permissions.CheckStatusAsync<T>();

            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<T>();

            if (status == PermissionStatus.Granted) 
                return true;

            await DisplayAlert("Error", "Storage permissions needed!", "Cancel");
            return false;
        }

        public async void HandlePurgeSteps()
        {
            var result = await DisplayAlert("Confirmation", "Are you sure you want to purge all unused recipe steps?",
                "Yes", "No");

            if (!result)
                return;

            var container = App.Container;

            var loadedRecipes = container.Resolve<IRecipeStore>().LoadedObjects;
            var stepStore = container.Resolve<IRecipeStepStore>();

            var loadedSteps = stepStore
                .LoadedObjects
                .ToList();

            foreach (var recipe in loadedRecipes)
                foreach (var step in recipe.Steps)
                    loadedSteps.Remove(step);

            var tasks = loadedSteps
                .Select(step => stepStore.RemoveAsync(step.Id))
                .ToArray();

            await Task.WhenAll(tasks);
            await DisplayAlert("Success", $"Successfully removed {tasks.Length} steps!", "Ok");
        }
    }
}
