using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using Tinder_test.Model;
using System.Collections.ObjectModel;

namespace Tinder_test {

    public partial class MainPage : ContentPage {

        // private string file = 
        private ObservableCollection<PickupLine> line = new ObservableCollection<PickupLine>();

        public MainPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Tinder_test.PickupLine.pickup.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream)) {
                text = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<List<PickupLine>>(text);
            line = new ObservableCollection<PickupLine>(data);

            content.ItemsSource = line;
        }


    }
}
