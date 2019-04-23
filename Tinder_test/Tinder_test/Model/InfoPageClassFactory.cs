using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tinder_test.InfoPages;
using Xamarin.Forms;

namespace Tinder_test.Model{

    public class InfoPageClassFactory{

        private Dictionary<Info, Func<ContentPage>> _factory;

        public InfoPageClassFactory() {
            _factory = new Dictionary<Info, Func<ContentPage>> {
                [new Info("Keyword")] = () => new KeywordInfoPage(),
                [new Info("TV Shows")] = () => new TVShowInfoPage(),
                [new Info("Food")] = () => new FoodInfoPage()
            };
        }

        public ContentPage GetInfoClass(Info type) {
            if (_factory.ContainsKey(type)) {
                return _factory[type]();
            }
            throw new NoSuchClassException(type.ToString());
        }

        public List<Info> GetInfoList() {
            if (_factory.Count > 0)
                return new List<Info>(_factory.Keys);
            else
                return new List<Info>();
        }

    }

    [Serializable]
    class NoSuchClassException : Exception {

        public NoSuchClassException() {
        }

        public NoSuchClassException(string message)
            : base(String.Format("No such class: {0}", message)) { }

    }
}


