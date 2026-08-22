using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetMovementOfItems:DataSetInvoicesAndStores
    {

        public string account { get; set; }
        public string invoiceType { get; set; }
        public int invoiceNumber{ get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime date { get; set; }
        public decimal balanceQuantity { get; set; }
        public string description { get; set; }
    }
}
