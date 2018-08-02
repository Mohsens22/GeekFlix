using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekFlixServer
{
    public static class DBContext
    {
        public static Realm Instance;
        public static void InitializeLocal() => Instance = Realm.GetInstance(@"D:\Output\DB\DBMovieDB.realm");
    }
}
