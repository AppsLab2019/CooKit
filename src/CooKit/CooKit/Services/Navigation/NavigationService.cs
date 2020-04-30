using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CooKit.ViewModels;
using CooKit.ViewModels.Recipes;
using CooKit.Views.Root;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace CooKit.Services.Navigation
{
    public sealed class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> _viewModelToViewDictionary;

        #region Initialization

        public Task InitializeAsync()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            var viewModels = ScanViewModelTypes(thisAssembly);
            var viewModelViewPairs = AssignViewModelTypeToViewType(viewModels);

            _viewModelToViewDictionary = new Dictionary<Type, Type>(viewModelViewPairs);
            return InternalNavigateToAsync(typeof(RecipeListViewModel), null);
        }

        private static IEnumerable<Type> ScanViewModelTypes(Assembly assembly)
        {
            var types = assembly?.GetTypes();
            return types?
                .Where(type => type.Name.Contains("ViewModel"))
                .Where(IViewModel.IsValidImplementation);
        }

        private static IEnumerable<KeyValuePair<Type, Type>> AssignViewModelTypeToViewType(IEnumerable<Type> viewModels)
        {
            if (viewModels is null)
                yield break;

            foreach (var viewModel in viewModels)
            {
                var viewModelName = viewModel.FullName;
                var viewName = viewModelName?.Replace("ViewModel", "View");

                if (viewName is null)
                    throw new Exception();

                var assemblyName = viewModel.Assembly.FullName;
                var viewTypeString = $"{viewName}, {assemblyName}";

                var view = Type.GetType(viewTypeString);

                if (view is null)
                    throw new Exception();

                yield return new KeyValuePair<Type, Type>(viewModel, view);
            }
        }

        #endregion

        public Task PushAsync(Type viewModel, object parameter, bool animated)
        {
            return InternalNavigateToAsync(viewModel, parameter);
        }

        public Task PushAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return PushAsync(typeof(T), parameter, animated);
        }

        public Task PopAsync()
        {
            if (Application.Current.MainPage is MasterDetailPage root)
                return root.Detail.Navigation.PopAsync();

            return Task.CompletedTask;
        }

        public Task PopToRootAsync()
        {
            if (Application.Current.MainPage is MasterDetailPage root)
                return root.Detail.Navigation.PopToRootAsync();

            return Task.CompletedTask;
        }

        public Task SetRootAsync(Type viewModel, object parameter = null, bool animated = true)
        {
            throw new NotImplementedException();
        }

        public Task SetRootAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return SetRootAsync(typeof(T), parameter, animated);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType);
            var application = Application.Current;

            if (application.MainPage is MasterDetailPage root)
            {
                var navigation = root.Detail.Navigation;
                await navigation.PushAsync(page);
            }
            else
                application.MainPage = new RootView(new MaterialNavigationPage(page));

            if (!(page.BindingContext is IViewModel viewModel))
                return;

            await viewModel.InitializeAsync(parameter);
        }

        private Page CreatePage(Type viewModel)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!_viewModelToViewDictionary.ContainsKey(viewModel))
                throw new KeyNotFoundException();

            var viewType = _viewModelToViewDictionary[viewModel];
            return (Page) Activator.CreateInstance(viewType);
        }
    }
}
