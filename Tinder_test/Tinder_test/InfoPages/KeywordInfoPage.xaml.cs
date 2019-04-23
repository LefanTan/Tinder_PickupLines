using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tinder_test.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Tinder_test.Dependencies;

namespace Tinder_test.InfoPages{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KeywordInfoPage : ContentPage{

        public KeywordInfoPage(){
			InitializeComponent ();
		}

        protected override void OnAppearing() {
            //load saved names, if non, leave as blank
            var json = JsonConvert.DeserializeObject<PickupLine>(DependencyService.Get<IFileService>().ReadFile(FileNames.User_Data, Environment.SpecialFolder.Personal));

            if (json == null) {
                user_entry.Text = "";
            } else {
                user_entry.Text = json.Keyword;
            }
        }

        async private void BackButton_Clicked(object sender, EventArgs e) {
            await Navigation.PopAsync();
        }

        private void Entry_Completed(object sender, EventArgs e) {
            var text = ((Entry)sender).Text;

            PickupLine temp;

            var json = JsonConvert.DeserializeObject<PickupLine>(DependencyService.Get<IFileService>().ReadFile(FileNames.User_Data, Environment.SpecialFolder.Personal));
            // if user_data doesnt contain any data, make new, if it does assign existing Name with new one from the entry cell
            if (json == null) {
                temp = new PickupLine {
                    Keyword = text
                };
            } else {
                json.Keyword = text;
                temp = json;
            }

            //save new Name
            var content = JsonConvert.SerializeObject(temp);
            DependencyService.Get<IFileService>().SaveFile(FileNames.User_Data, content);
            //System.Diagnostics.Debug.WriteLine(DependencyService.Get<IFileService>().ReadFile("user_data.json", Environment.SpecialFolder.Personal));
        }

    }
}