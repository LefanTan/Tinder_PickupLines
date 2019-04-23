using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tinder_test.Dependencies {

    public interface IDBInterface {
        SQLiteConnection CreateConnection(string dbName);
    }
}
