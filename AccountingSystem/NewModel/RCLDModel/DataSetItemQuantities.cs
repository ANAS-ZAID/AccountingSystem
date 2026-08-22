using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetItemQuantities : DataSetInvoicesAndStores
    {
        public decimal purchasePrice { get; set; }
        public int purchaseQuantity { get; set; }
        public decimal salePrice { get; set; }
        public int saleQuantity { get; set; }
        public decimal previousBalancePrice { get; set; }
        public int previousBalanceQuantity { get; set; }
        public decimal balancePrice { get; set; }
        public string purchasePricePerPill { get; set; }
        public int balanceQuantity { get; set; }

        public string store {  get; set; }
      
    }
}
