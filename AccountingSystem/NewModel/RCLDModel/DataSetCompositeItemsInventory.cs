using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetCompositeItemsInventory: DataSetItemQuantities
    {
    public    string titel {  get; set; }
    public    decimal purchasePricePerPill {  get; set; }
    }
}
