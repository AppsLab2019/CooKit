﻿using Foundation;
using UIKit;

namespace CooKit.Mobile.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            XF.Material.iOS.Material.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            var serviceProvider = Bootstrap.CreateServiceProvider();
            var application = (Xamarin.Forms.Application) serviceProvider.GetService(typeof(Xamarin.Forms.Application));
            LoadApplication(application);

            return base.FinishedLaunching(app, options);
        }
    }
}
