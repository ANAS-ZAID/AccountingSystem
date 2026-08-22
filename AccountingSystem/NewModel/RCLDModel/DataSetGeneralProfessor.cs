using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetGeneralProfessor
    {
       
            public decimal periodMovementDebit { get; set; }
            public decimal periodMovementCredit { get; set; }
            public decimal openingBalanceDebit { get; set; }
            public decimal openingBalanceCredit { get; set; }
            public decimal previousBalance { get; set; }
            public decimal finalBalance { get; set; }
            public string currency { get; set; }
            public string account { get; set; }
            public int numberColor { get; set; }
       
    }
}
