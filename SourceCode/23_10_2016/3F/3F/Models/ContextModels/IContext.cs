using _3F.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3F.Models
{
    public class IContext
    {
        private MongoDatabase database;

        public IContext()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            database = server.GetDatabase(Utils.Utils.DATABASE_NAME);
        }
        public void MyFunc() { }

    }
}