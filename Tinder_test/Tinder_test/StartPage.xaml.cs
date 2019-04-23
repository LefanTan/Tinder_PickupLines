using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinder_test.Dependencies;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tinder_test{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage{

		public StartPage (){
			InitializeComponent ();
		}

        protected override void OnAppearing() {
            //StartButton.FontSize = 40;
            //reset user data
            DependencyService.Get<IFileService>().SaveFile(FileNames.User_Data, "");
        }

        async private void Button_Clicked(object sender, EventArgs e) {
            var button = (Button)sender;
            button.Opacity = 0.5;

            var secondPage = new InfoSelectorPage();
            await Navigation.PushAsync(secondPage);
            button.Opacity = 1;

        }
    }
}