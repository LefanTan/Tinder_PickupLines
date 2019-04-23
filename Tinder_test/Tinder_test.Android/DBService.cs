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
using Xamarin.Forms;
using Tinder_test.Dependencies;
using Android.Content.Res;
using Tinder_test.Droid;
using SQLite;
using System.IO;

[assembly: Dependency(typeof(DBService))]
namespace Tinder_test.Droid {

    public class DBService : IDBInterface {

        public SQLiteConnection CreateConnection(string dbName) {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(path, dbName);

            if (!File.Exists(completePath)) {
                using (var binaryReader = new BinaryReader(Android.App.Application.Context.Assets.Open(dbName))) {
                    using(var binaryWriter = new BinaryWriter(new FileStream(completePath, FileMode.Create))) {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0) {
                            binaryWriter.Write(buffer, 0, length);
                        }
                    }
                }
            }

            var conn = new SQLiteConnection(completePath);
            return conn;
        }

    }
}