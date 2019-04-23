using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinder_test.Dependencies;
using Tinder_test.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Tinder_test.InfoPages{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TVShowInfoPage : SelectorBasePage{

        string[] _shows;
        
        List<string> _selectedShow;
        List<Button> buttonsFromGrid;

        public TVShowInfoPage(){
			InitializeComponent ();

            _selectedShow = new List<string>();
            _shows = new string[] {
                "Big Bang Theory", "The Office" ,"How I Met Your Mother", "Doctor Who", "Game Of Thrones",
                "The Walking Dead", "Community", "Sherlock"
            };
        } 

        protected override void OnAppearing() {
            var buttonViewList = SelectorGrid.Children.Where(x => x.GetType() == typeof(Button));

            buttonsFromGrid = buttonViewList.Select(
                x => (Button)x
                ).ToList();

            base.AssignTextsToButtons(_shows, buttonsFromGrid);
            base.Appear(buttonsFromGrid, _selectedShow, "Shows");
        }

        async private void BackButton_Clicked(object sender, EventArgs e) { 
            await Navigation.PopAsync();
            base.UpdateUserData(_selectedShow, "Shows");
        }   
        
        private void SelectorButton_Clicked(object sender, EventArgs e) {
            var but = (Button)sender;

            base.SelectorButtonChangeEvent(but, _selectedShow);
        }

        
        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e) {
            //get all of the matched buttons
            var text = ((Entry)sender).Text;

            base.GridSearchImplementation(buttonsFromGrid, text, SelectorGrid);
   
        }

        

    }
}