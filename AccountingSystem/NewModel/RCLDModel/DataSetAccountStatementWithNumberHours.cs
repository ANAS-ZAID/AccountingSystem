using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetAccountStatementWithNumberHours: DataSetAccountStatement
    {
        
        public string titel {  get; set; }
        public decimal quantity {  get; set; }
        public decimal priceHour {  get; set; }
        public decimal total {  get; set; }


    }
}
