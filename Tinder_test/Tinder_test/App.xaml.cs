using System;
using Tinder_test.InfoPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tinder_test {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new StartPage());
            //MainPage = new PersonalityInfoPage();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
