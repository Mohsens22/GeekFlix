using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class DBContext
    {
        public static Realm Instance;
        public static Realm LocalInstance;
        public static void InitializeLocal()
        {
            Instance = Realm.GetInstance(@"e:\Output\DB\DBMovieDB.realm");
            LocalInstance = Realm.GetInstance(@"MovieDB.realm");
        }
    }
}
