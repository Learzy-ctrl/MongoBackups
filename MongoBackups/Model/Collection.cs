using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoBackups.Model
{
    public class Collection
    {
        public string DatabaseName { get; set; }
        public string Name { get; set; }
        public long Documents { get; set; }
    }
}
