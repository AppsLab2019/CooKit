using System;
using Xamarin.Forms;

namespace CooKit.Mobile.Providers.Page.MasterDetail
{
    public class ServiceProviderMasterDetailPageProvider : IMasterDetailPageProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderMasterDetailPageProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MasterDetailPage GetMasterDetailPage()
        {
            return _serviceProvider.GetService(typeof(MasterDetailPage)) as MasterDetailPage;
        }
    }
}
