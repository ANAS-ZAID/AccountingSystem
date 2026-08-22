using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetBills:DataSetInvoicesAndStores
    {
        public decimal quantity { get; set; }

        public decimal unitPrice { get; set; }

        public string description { get; set; }
    }

}
