using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tinder_test.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(UnanimatedPageRenderer))]
namespace Tinder_test.Droid {

    public class UnanimatedPageRenderer : NavigationRenderer {

        public UnanimatedPageRenderer(Context context) : base(context) {

        }

        protected override Task<bool> OnPushAsync(Page view, bool animated) {
            return base.OnPushAsync(view, false);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated) {
            return base.OnPopViewAsync(page, false);
        }

        protected override Task<bool> OnPopToRootAsync(Page page, bool animated) {
            return base.OnPopToRootAsync(page, false);
        }

    }
}