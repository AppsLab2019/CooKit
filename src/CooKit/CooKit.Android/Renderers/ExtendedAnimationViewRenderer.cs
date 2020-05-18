using CooKit.Controls;
using CooKit.Droid.Renderers;
using Lottie.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedAnimationView), typeof(ExtendedAnimationViewRenderer))]
namespace CooKit.Droid.Renderers
{
    public sealed class ExtendedAnimationViewRenderer : Lottie.Forms.Droid.AnimationViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        {
            base.OnElementChanged(e);

            Control?.EnableMergePathsForKitKatAndAbove(true);
        }
    }
}