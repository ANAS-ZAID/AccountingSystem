using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetJournalEntry
    {
        public int id { get; set; }
        public int number { get; set; }

        public string type { get; set; }

        public string account { get; set; }

        public string currency { get; set; }
        public decimal debit { get; set; }

        public decimal credit { get; set; }
        public string description { get; set; }

    }
}
