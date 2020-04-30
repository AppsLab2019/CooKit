using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CooKit.ViewModels;

namespace CooKit.Services.Navigation
{
    public sealed class NavigationService : INavigationService
    {
        private IContainer _container;
        private IDictionary<Type, Type> _viewModelToViewDictionary;

        public Task InitializeAsync(IContainer container)
        {
            if (container is null)
                throw new ArgumentNullException(nameof(container));

            _container = container;

            var thisAssembly = Assembly.GetExecutingAssembly();
            var viewModels = ScanViewModelTypes(thisAssembly);
            var viewModelViewPairs = AssignViewModelTypeToViewType(viewModels);

            _viewModelToViewDictionary = new Dictionary<Type, Type>(viewModelViewPairs);
            return Task.CompletedTask;
        }

        private static IEnumerable<Type> ScanViewModelTypes(Assembly assembly)
        {
            var types = assembly?.GetTypes();
            return types?
                .Where(type => type.Name.Contains("ViewModel"))
                .Where(IsValidViewModelType);
        }

        private static bool IsValidViewModelType(Type type)
        {
            return !type.IsInterface
                   && type.IsSealed
                   && typeof(IViewModel).IsAssignableFrom(type);
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

        public Task PushAsync<T>() where T : IViewModel
        {
            throw new NotImplementedException();
        }

        public Task PushAsync<T>(object parameter) where T : IViewModel
        {
            throw new NotImplementedException();
        }

        public Task PopAsync()
        {
            throw new NotImplementedException();
        }
    }
}
