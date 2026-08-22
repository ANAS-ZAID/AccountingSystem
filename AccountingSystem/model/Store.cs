using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.model
{
    public class Store:BaseEntity
    {
        private static Store instance;
        public static Store getInstance()
        {
            if (instance == null)
                instance = new Store();
            return instance;

        }
    }
}
