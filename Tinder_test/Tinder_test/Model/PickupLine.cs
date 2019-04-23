using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder_test.Model{

    public class PickupLine{
        public static string FileLocation = "Tinder_test.PickupLine.pickup.json"; 

        public string Line { get; set; }
        public string Hobby { get; set; }
        public string Keyword { get; set; }
        public string Shows { get; set; }
        public string Sports { get; set; }
        public string Movies { get; set; }
        public string Food { get; set; }

        public object this[string propertyName] {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

    }

}
