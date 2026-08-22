using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetStocItemsLessThanZero:DataSetInvoicesAndStores
    {
        public string features {  get; set; }
        public int quantity {  get; set; }

    }
}
