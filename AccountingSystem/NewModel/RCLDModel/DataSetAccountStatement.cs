using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.NewModel.RCLDModel
{
    public class DataSetAccountStatement
    {
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public decimal balanceDebit { get; set; }
        public decimal balanceCredit { get; set; }
        public string currency { get; set; }
        public string date { get; set; }
        public string transactionType { get; set; }
        public string description { get; set; }
        public string account { get; set; }
        public int numberColor { get; set; }
    }
}
