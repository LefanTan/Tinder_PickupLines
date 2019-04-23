using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Com.EightbitLab.BlurViewBinding;
using Tinder_test;
using Tinder_test.View;
using Tinder_test.Droid;

[assembly: ExportRenderer(typeof(FormsBlurView), typeof(FormsBlurViewRenderer))]
namespace Tinder_test.Droid {
    
    public class FormsBlurViewRenderer : ViewRenderer<FormsBlurView, FrameLayout> {
        // aapt resource value: 0x7f0d004f
        public const int colorOverlay = 2131558479;

        public FormsBlurViewRenderer(Context context) : base(context) {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FormsBlurView> e) {
            base.OnElementChanged(e);

            if (Control == null) {
                var context = Context;
                var activity = context as Activity;

                var rootView = (ViewGroup)activity.Window.DecorView.FindViewById(Android.Resource.Id.Content);
                var windowBackground = activity.Window.DecorView.Background;

                var blurView = new BlurView(context);

                blurView.SetOverlayColor(colorOverlay);

                blurView.SetupWith(rootView)
                   .WindowBackground(windowBackground)
                   .BlurAlgorithm(new RenderScriptBlur(context))
                   .BlurRadius(10f);

                SetNativeControl(blurView);
            }
        }
    }
}