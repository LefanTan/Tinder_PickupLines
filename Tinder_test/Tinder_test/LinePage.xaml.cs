using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinder_test.InfoPages;
using Tinder_test.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using Tinder_test.Dependencies;
using SQLite;

namespace Tinder_test{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinePage : ContentPage {

        //pickup line obj that represents the user_data file
        PickupLine lineObj;

        public LinePage() {
            InitializeComponent();
            lineObj = new PickupLine();
        }

        protected override void OnAppearing() {
            //deserialize json file and assign it to an object
            var json = DependencyService.Get<IFileService>().ReadFile(FileNames.User_Data, Environment.SpecialFolder.Personal);
            lineObj = JsonConvert.DeserializeObject<PickupLine>(json);

            //asign Filtered Pickuplines to list
            contentList.ItemsSource = GetFilteredLines();
        }

        //return a list of filtered lines
        private ObservableCollection<PickupLine> GetFilteredLines() {
            var db = DependencyService.Get<IDBInterface>().CreateConnection("PickupDB.db");

            ObservableCollection<PickupLine> lines = new ObservableCollection<PickupLine>(db.Query<PickupLine>("Select Line from PickupLine"));

            //filtering through name, more filter method can be added
            lines = FilterShow(lines, db);
            lines = FilterKeyword(lines, db);

            return lines;
        }


        private ObservableCollection<PickupLine> FilterKeyword(ObservableCollection<PickupLine> lines, SQLiteConnection db) {
            ObservableCollection<PickupLine> result;

            //if lineobj is null(user data is empty) or lineObj's name is empty,
            //just return the original list of lines from the parameter
            if (lineObj != null && !string.IsNullOrWhiteSpace(lineObj.Keyword)) {
                result = new ObservableCollection<PickupLine>(lines.Where(x => x.Line.Contains(lineObj.Keyword) ));
            } else {
                result = lines;
            }
            
            return result;
        }

        /**
         * Filter Shows as follows: lines related will only show if the show is selected
         **/
        private ObservableCollection<PickupLine> FilterShow(ObservableCollection<PickupLine> lines, SQLiteConnection db) {
            ObservableCollection<PickupLine> result;

            //implement personality algorithm
            if(lineObj != null && !string.IsNullOrWhiteSpace(lineObj.Shows)) {
               // string[] userPersonality = lineObj.Shows.Split(',');
                result = new ObservableCollection<PickupLine>( lines.Where(x => lineObj.Shows.Contains(x.Shows.Replace(",","")) ) );
            } else {
                result = lines;
            }

            return result;
        }

    }

}