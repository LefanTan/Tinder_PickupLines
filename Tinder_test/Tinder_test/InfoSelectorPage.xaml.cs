using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tinder_test.Model;
using Tinder_test.View;
using System.Reflection;

namespace Tinder_test{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoSelectorPage : ContentPage{

        //factory method to create different info page, depending on button clicked
        private InfoPageClassFactory _factory;
        private List<Info> _infoTypeList;

		public InfoSelectorPage (){
			InitializeComponent ();

            _factory = new InfoPageClassFactory();
            _infoTypeList = new List<Info>();

            //info list is entirely the same as the factor's dictionary
            _infoTypeList = _factory.GetInfoList();
		}

        protected override void OnAppearing() {   
            //var scrollView = new ScrollView();
            //Content = scrollView;

            var blur = new FormsBlurScroll();
            Content = blur;

            var grid = new Grid { ColumnSpacing = 5, RowSpacing = 5, Padding = new Thickness(5, 5, 5, 5) };
            var imgBut = new ImageButton { Style = (Style)Application.Current.Resources["TickButton"] };
            imgBut.Clicked += ImgBut_Clicked;

            var abs = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(grid, new Rectangle(0, 0, 1, .9));
            AbsoluteLayout.SetLayoutFlags(grid, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(imgBut, new Rectangle(1, 1, 100, 100));
            AbsoluteLayout.SetLayoutFlags(imgBut, AbsoluteLayoutFlags.PositionProportional);

            abs.Children.Add(grid);
            abs.Children.Add(imgBut);

            blur.Content = abs;

            int rowAmount = (_infoTypeList.Count() % 2 == 0) ? _infoTypeList.Count() / 2 : (_infoTypeList.Count() + 1) / 2;
            int current = 0;

            //create grid of two columns with variable amount of rows
            for (int i = 0; i < 2; i++) {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                for (int j = 0; j < rowAmount; j++) {
                    //System.Diagnostics.Debug.WriteLine(i + " " + j);
                    if (current > _infoTypeList.Count() - 1)
                        continue;

                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                    var newButton = new Button {
                        Text = _infoTypeList.ElementAt(current).InfoName,
                        //FontSize = 45,
                        Style = (Style)Application.Current.Resources["InfoButton"],
                        HeightRequest = Width / 2
                    };
                    newButton.Clicked += OnButtonClicked;

                    grid.Children.Add(new StackLayout {
                        BackgroundColor = new Color(0, 0, 0, 0.3),
                        Children = { newButton }

                    }, j, i);

                    current++;

                }
            }

        }

        private async void ImgBut_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new LinePage());
        }

        private async void OnButtonClicked(object sender, EventArgs e) {
            var button = (Button)sender;
            var stackLayout = (StackLayout)button.Parent;

            //get newclass based on the button's text, since the button text is created based on the variable InfoName
            //lazy to find another/better implemtation for now
            var newClass = _factory.GetInfoClass(_infoTypeList.Find(x=> x.InfoName == button.Text) );

            stackLayout.BackgroundColor = new Color(0, 0, 0, 0.5);
            await Navigation.PushAsync(newClass);
            stackLayout.BackgroundColor = new Color(0, 0, 0, 0.3);
        }

	}
}