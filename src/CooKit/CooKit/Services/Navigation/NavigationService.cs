using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    // TODO: remove Application.Current usage
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
            return InitializeRoot(typeof(RecipeListViewModel));
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

        private async Task InitializeRoot(Type defaultViewModel)
        {
            var page = CreatePage(defaultViewModel);
            var root = new RootView(new MaterialNavigationPage(page));

            var rootMaster = root.Master;
            await InitializeViewModel(rootMaster, null);
            await InitializeViewModel(page, null);

            Application.Current.MainPage = root;
        }

        #endregion

        #region Push Methods

        public Task PushAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return PushAsync(typeof(T), parameter, animated);
        }

        public Task PushAsync(Type viewModel, object parameter = null, bool animated = true)
        {
            return InternalPushAsync(viewModel, parameter, animated);
        }

        public Task PushModalAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return PushModalAsync(typeof(T), parameter, animated);
        }

        public Task PushModalAsync(Type viewModel, object parameter = null, bool animated = true)
        {
            return InternalPushModalAsync(viewModel, parameter, animated);
        }

        #endregion

        #region Pop Methods

        public Task PopAsync(bool animated = true)
        {
            return GetDetailNavigation().PopAsync(animated);
        }

        public Task PopModalAsync(bool animated = true)
        {
            return GetDetailNavigation().PopModalAsync(animated);
        }

        public Task PopToRootAsync(bool animated = true)
        {
            return GetDetailNavigation().PopToRootAsync(animated);
        }

        #endregion

        public Task SetRootAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return SetRootAsync(typeof(T), parameter, animated);
        }

        public Task SetRootAsync(Type viewModel, object parameter = null, bool animated = true)
        {
            return InternalSetRootAsync(viewModel, parameter);
        }

        // TODO: merge InternalPushAsync and InternalPushModalAsync logic into a single helper function through delegates

        private async Task InternalPushAsync(Type viewModelType, object parameter, bool animated)
        {
            AssertApplicationMainPageIsRoot();

            var page = CreatePage(viewModelType);
            var navigation = GetDetailNavigation();

            await navigation.PushAsync(page, animated);
            await InitializeViewModel(page, parameter);
        }        
        
        private async Task InternalPushModalAsync(Type viewModelType, object parameter, bool animated)
        {
            AssertApplicationMainPageIsRoot();

            var page = CreatePage(viewModelType);
            var navigation = GetDetailNavigation();

            await navigation.PushModalAsync(page, animated);
            await InitializeViewModel(page, parameter);
        }

        private  Task InternalSetRootAsync(Type viewModelType, object parameter)
        {
            AssertApplicationMainPageIsRoot();

            var page = CreatePage(viewModelType);
            var wrappedPage = new MaterialNavigationPage(page);

            var rootView = GetRootView();
            rootView.Detail = wrappedPage;

            // TODO: move this somewhere else?
            rootView.IsPresented = false;

            return InitializeViewModel(page, parameter);
        }

        private static Task InitializeViewModel(Element element, object parameter)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            if (element.BindingContext is IViewModel viewModel)
                return viewModel.InitializeAsync(parameter);

            return Task.CompletedTask;
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

        #region Simple Getters

        private static Application GetApplication()
        {
            return Application.Current;
        }

        private static RootView GetRootView()
        {
            return (RootView) GetApplication().MainPage;
        }

        private static INavigation GetDetailNavigation()
        {
            return GetRootView().Detail.Navigation;
        }

        #endregion

        #region Assertions

        [Conditional("DEBUG")]
        private static void AssertApplicationMainPageIsRoot()
        {
            if (Application.Current.MainPage is RootView)
                return;

            throw new Exception("Application MainPage is not RootView!");
        }

        #endregion
    }
}
