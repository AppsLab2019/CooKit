using Android.App;
using Android.OS;

namespace CooKit.Mobile.Droid
{
    [Activity(Label = "CooKit", Icon = "@mipmap/icon", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }

        // this prevents app exit while main activity is starting
        public override void OnBackPressed()
        {
        }
    }
}