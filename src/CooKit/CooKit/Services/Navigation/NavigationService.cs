using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CooKit.Factories;
using CooKit.ViewModels;
using CooKit.ViewModels.Recipes;
using CooKit.Views.Root;
using Xamarin.Forms;

namespace CooKit.Services.Navigation
{
    // TODO: remove Application.Current usage
    public sealed class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> _viewModelToViewDictionary;
        private readonly IPageFactory _pageFactory;

        public NavigationService(IPageFactory factory)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            _pageFactory = factory;
        }

        #region Initialization

        public Task InitializeAsync()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            var viewModels = ScanViewModelTypes(thisAssembly).ToList();

            _viewModelToViewDictionary = AssignViewModelTypeToViewType(viewModels);

            return InitializeRoot(typeof(MainRecipeViewModel));
        }

        private static IEnumerable<Type> ScanViewModelTypes(Assembly assembly)
        {
            var types = assembly?.GetTypes();
            return types?
                .Where(type => type.Name.Contains("ViewModel"))
                .Where(IViewModel.IsValidImplementation);
        }

        private static IDictionary<Type, Type> AssignViewModelTypeToViewType(IEnumerable<Type> viewModels)
        {
            if (viewModels is null)
                throw new ArgumentNullException(nameof(viewModels));

            var dictionary = new Dictionary<Type, Type>();

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

                dictionary.Add(viewModel, view);
            }

            return dictionary;
        }

        private async Task InitializeRoot(Type defaultViewModel)
        {
            var root = CreateRootPage(defaultViewModel);
            var concreteRoot = new RootView(root);

            var rootMaster = concreteRoot.Master;
            await InitializeViewModel(rootMaster, null);
            await InitializeRootViewModel(root, null);

            Application.Current.MainPage = concreteRoot;
        }

        #endregion

        #region Push Methods

        public Task PushAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return PushAsync(typeof(T), parameter, animated);
        }
        
        public Task PushAsync(Type viewModel, object parameter = null, bool animated = true)
        {
            return InternalPushAsync(viewModel, parameter, animated, PushAction);
        }

        private static Task PushAction(INavigation nav, Page page, bool anim)
        {
            return nav.PushAsync(page, anim);
        }

        #endregion

        #region Push Modal Methods

        public Task PushModalAsync<T>(object parameter = null, bool animated = true) where T : IViewModel
        {
            return PushModalAsync(typeof(T), parameter, animated);
        }

        public Task PushModalAsync(Type viewModel, object parameter = null, bool animated = true)
        {
            return InternalPushAsync(viewModel, parameter, animated, PushModalAction);
        }

        private static Task PushModalAction(INavigation nav, Page page, bool anim)
        {
            return nav.PushModalAsync(page, anim);
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

        private Task InternalPushAsync(Type viewModelType, object parameter, bool animated, NavAction navAction)
        {
            AssertApplicationMainPageIsRoot();

            var page = CreatePage(viewModelType);
            var navigation = GetDetailNavigation();

            var initializationTask = InitializeViewModel(page, parameter);
            var pushTask = navAction(navigation, page, animated);

            return Task.WhenAll(initializationTask, pushTask);
        }

        private Task InternalSetRootAsync(Type viewModelType, object parameter)
        {
            AssertApplicationMainPageIsRoot();

            var page = CreateRootPage(viewModelType);
            var initializationTask = InitializeRootViewModel(page, parameter);

            var rootView = GetRootView();
            rootView.Detail = page;

            // TODO: move this somewhere else?
            rootView.IsPresented = false;

            return initializationTask;
        }

        private static Task InitializeViewModel(BindableObject bindable, object parameter)
        {
            if (bindable.BindingContext is IViewModel viewModel)
                return viewModel.InitializeAsync(parameter);

            return Task.CompletedTask;
        }

        private static Task InitializeRootViewModel(BindableObject bindable, object parameter)
        {
            if (!(bindable is NavigationPage root))
                throw new ArgumentException($"{bindable.GetType().Name} is not a valid root page!");

            return InitializeViewModel(root.RootPage, parameter);
        }

        private Page CreatePage(Type viewModel)
        {
            var view = GetViewType(viewModel);
            return _pageFactory.CreatePage(view);
        }

        private Page CreateRootPage(Type viewModel)
        {
            var view = GetViewType(viewModel);
            return _pageFactory.CreateRootPage(view);
        }

        private Type GetViewType(Type viewModel)
        {
            if (viewModel is null)
                throw new ArgumentNullException(nameof(viewModel));

            if (_viewModelToViewDictionary.TryGetValue(viewModel, out var viewType))
                return viewType;

            throw new KeyNotFoundException();
        }

        private delegate Task NavAction(INavigation navigation, Page page, bool animated);

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
