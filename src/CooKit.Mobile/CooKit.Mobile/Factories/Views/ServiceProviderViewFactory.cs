using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace CooKit.Mobile.Factories.Views
{
    public class ServiceProviderViewFactory : IViewFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderViewFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public View CreateView(Type viewType)
        {
            if (viewType == null)
                throw new ArgumentNullException(nameof(viewType));

            Debug.Assert(typeof(View).IsAssignableFrom(viewType));

            return _serviceProvider.GetService(viewType) as View;
        }
    }
}
