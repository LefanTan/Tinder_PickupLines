using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tinder_test.Dependencies;
using Tinder_test.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tinder_test.InfoPages {

    //remove code duplication between similar button selection classes
    public class SelectorBasePage : ContentPage {

        public SelectorBasePage() {
        }

        protected virtual void Appear(List<Button> buttons, List<string> _selected, string propertyName) {
            List<string> userList = new List<string>();
            var file = DependencyService.Get<IFileService>().ReadFile(FileNames.User_Data, Environment.SpecialFolder.Personal);
            var lineObj = JsonConvert.DeserializeObject<PickupLine>(file);

            if (lineObj != null && !string.IsNullOrWhiteSpace((string)lineObj[propertyName]))
                userList = ((string)lineObj[propertyName]).Split(',').ToList();

            foreach (Button but in buttons) {
                if (userList.Contains(but.Text)) {
                    //load save data
                    _selected.Add(but.Text);
                    but.BackgroundColor = new Color(0, 0, 0, 0.2);
                }
            }
        }

        protected virtual void AssignTextsToButtons(string[] texts, List<Button> buttons) {
            int stringlistIndex = 0;

            for (int i = 0; i < buttons.Count(); i++) {
                var but = buttons[i];
                but.HeightRequest = but.Width;

                if (stringlistIndex < texts.Count()) {
                    but.Text = texts[stringlistIndex];
                    stringlistIndex++;
                } else {
                    but.Text = "Extra Space";
                }

            }
        }

        protected virtual void SelectorButtonChangeEvent(Button but, List<string> selectedList) {
            var butText = but.Text;
            Color pressed = new Color(0, 0, 0, 0.2);
            Color unPressed = new Color(0, 0, 0, 0);

            if (selectedList.Contains(butText)) {
                selectedList.Remove(butText);
                but.BackgroundColor = unPressed;
            } else {
                selectedList.Add(butText);
                but.BackgroundColor = pressed;
            }
        }

        protected virtual void UpdateUserData(List<string> _selected, string propertyName) {
            //user complete input, updates user_data.json
            var file = DependencyService.Get<IFileService>().ReadFile(FileNames.User_Data, Environment.SpecialFolder.Personal);
            var lineObj = JsonConvert.DeserializeObject<PickupLine>(file);

            string newInfo = string.Join(",", _selected);

            if (lineObj == null) {
                var newLine = new PickupLine();
                newLine[propertyName] = newInfo;

                lineObj = newLine;
            } else {
                lineObj[propertyName] = newInfo;
            }

            var content = JsonConvert.SerializeObject(lineObj);
            DependencyService.Get<IFileService>().SaveFile(FileNames.User_Data, content);
        }

        /**
         * Is this a good algorithm to search? Seems expensive to me, try to implement a better and more efficient one in the future
         **/
        protected virtual void GridSearchImplementation(List<Button> buttonsFromGrid, string searchText, Grid gridToSearch) {
            List<Button> matchedButtons = new List<Button>();

            //get buttons that matched the entry text
            foreach (Button but in buttonsFromGrid) {
                string lowered = but.Text.ToLower();
                if (lowered.Contains(searchText.ToLower()))
                    matchedButtons.Add(but);
            }

            int amount = gridToSearch.Children.Count();

            //removes all button
            for (int i = 0; i < amount; i++) {
                if (i < gridToSearch.Children.Count()) {
                    var item = gridToSearch.Children.ElementAt(i);
                    if (item.GetType() == typeof(Button)) {
                        //only affect buttons
                        gridToSearch.Children.RemoveAt(i);
                        i = 0;
                    }
                }
            }

            int rowAmount = gridToSearch.RowDefinitions.Count();
            int columnAmount = gridToSearch.ColumnDefinitions.Count();

            //add matched buttons on to grid, but only those with "Auto" rows as rowdefinition type
            for (int i = 0; i < rowAmount; i++) {
                for (int j = 0; j < columnAmount; j++) {
                    if (gridToSearch.RowDefinitions[i].Height.GridUnitType == GridUnitType.Auto && matchedButtons.Count != 0) {
                        var butToAdd = matchedButtons[0];
                        matchedButtons.RemoveAt(0);
                        //no idea why this works when I switch j and i, can't find the documentation for the add method lol
                        gridToSearch.Children.Add(butToAdd, j, i);
                    }
                }
            }

        }

    }
}