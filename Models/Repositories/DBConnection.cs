using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Repositories
{
    public abstract class DBConnection
    {
        public string ConnectionString { get; }
        public DBConnection(string con)
        {
            ConnectionString = con;
        }
    }
}
