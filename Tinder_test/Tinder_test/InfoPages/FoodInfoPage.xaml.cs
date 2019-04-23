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

namespace Tinder_test.InfoPages{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodInfoPage : SelectorBasePage{

        string[] _food;
        
        List<string> _selectedFood;
        List<Button> buttonsFromGrid;

        public FoodInfoPage(){
			InitializeComponent ();

            _selectedFood = new List<string>();
            _food = new string[] {
                
            };
        } 

        protected override void OnAppearing() {
            var buttonViewList = SelectorGrid.Children.Where(x => x.GetType() == typeof(Button));

            buttonsFromGrid = buttonViewList.Select(
                x => (Button)x
                ).ToList();

            base.AssignTextsToButtons(_food, buttonsFromGrid);
            base.Appear(buttonsFromGrid, _selectedFood, "Food");
        }

        async private void BackButton_Clicked(object sender, EventArgs e) { 
            await Navigation.PopAsync();
            base.UpdateUserData(_selectedFood, "Food");
        }   
        
        private void SelectorButton_Clicked(object sender, EventArgs e) {
            var but = (Button)sender;

            base.SelectorButtonChangeEvent(but, _selectedFood);
        }

        
        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e) {
            //get all of the matched buttons
            var text = ((Entry)sender).Text;

            base.GridSearchImplementation(buttonsFromGrid, text, SelectorGrid);

        }

        

    }
}