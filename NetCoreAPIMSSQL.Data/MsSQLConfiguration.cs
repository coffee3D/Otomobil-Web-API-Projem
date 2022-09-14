using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMSSQL.Data
{
    public class MsSQLConfiguration
    {
        public string ConnectionString { get; set; }
        public MsSQLConfiguration(string connectionString) => ConnectionString = connectionString;
        
    }
}
