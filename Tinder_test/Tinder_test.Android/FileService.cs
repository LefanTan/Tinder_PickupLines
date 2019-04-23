using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tinder_test.Droid;
using Xamarin.Forms;
using Tinder_test.Dependencies;
using System.IO;
using System.Reflection;
using Android.Content.Res;

[assembly: Dependency(typeof(FileService))]
namespace Tinder_test.Droid { 
    public class FileService : IFileService {

        public string ReadFile(string fileName, System.Environment.SpecialFolder filePath) {
            string fileContent = "";
            var path = System.Environment.GetFolderPath(filePath);
            string completePath = Path.Combine(path, fileName);

            using (var sr = new StreamReader(File.Open(completePath, FileMode.Open))) {
                fileContent = sr.ReadToEnd();
                return fileContent;
            }
        }
        
        public string ReadFileFromAssets(string fileName) {
            string fileContent = "";
            AssetManager assets = Android.App.Application.Context.Assets;

            using (var sr = new StreamReader(assets.Open(fileName))) {
                fileContent = sr.ReadToEnd();
                return fileContent;
            }

        }

        public void SaveFile(string fileName, string content) {
            AssetManager assets = Android.App.Application.Context.Assets;

            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(path, fileName);

            using (var streamWriter = new StreamWriter(completePath)) {
                streamWriter.WriteLine(content);
            }
        }

    }

}