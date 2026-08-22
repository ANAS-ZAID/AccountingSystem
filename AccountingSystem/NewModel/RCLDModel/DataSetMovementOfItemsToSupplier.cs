using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetMovementOfItemsToSupplier: DataSetInvoicesAndStores
    {
       
             public string supplierName {  get; set; }
            public decimal purchasePrice { get; set; }
            public int purchaseQuantity { get; set; }
           
         

        }
  
}
