using System;
using System.Reflection;
using Autofac;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static void Initialize(IContainer container)
        {
            if (container is null)
                throw new ArgumentNullException(nameof(container));

            _container = container;
        }

        public static void OnAutoWireVMChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
                return;

            var viewType = view.GetType();
            var viewName = viewType.FullName?.Replace(".Views.", ".ViewModels.");

            if (viewName is null)
                throw new Exception();

            var assembly = viewType.GetTypeInfo().Assembly.FullName;
            var vmName = $"{viewName}Model, {assembly}";
            var vmType = Type.GetType(vmName);

            if (vmType is null)
                throw new Exception();

            var viewModel = _container.Resolve(vmType);
            view.BindingContext = viewModel;
        }

        #region AutoWireVM BindableProperty

        public static BindableProperty AutoWireVMProperty = BindableProperty.CreateAttached(
            "AutoWireVM", typeof(bool), typeof(ViewModelLocator), false, propertyChanged: OnAutoWireVMChanged);

        public static bool GetAutoWireVM(BindableObject bindable)
        {
            return (bool) bindable.GetValue(AutoWireVMProperty);
        }

        public static void SetAutoWireVM(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireVMProperty, value);
        }

        #endregion
    }
}
