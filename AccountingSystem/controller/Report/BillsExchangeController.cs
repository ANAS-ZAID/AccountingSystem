using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.controller
{
    public class BillsExchangeController
    {
        AccountingDbContext dbContext;
        public BindingSource dataSource;
      public  BillsExchangeController()
        {
            dbContext = new AccountingDbContext();
            dataSource=new BindingSource();
            dataSource.DataSource = typeof(DataSetJournalEntry);
        }
       public bool search(string number)
        {
            bool status=true;
            List<DataSetJournalEntry> journalEntries = new List<DataSetJournalEntry>();
            if (String.IsNullOrEmpty(number)) 
            {
                AppDialogAleart.showAleartError(" مطلوب كتابة رقم الفاتوره");
                return false;
            }
            try
            {
                foreach (var entry in dbContext.JournalEntries.ToList().Where(j => j.IsSalesOrPurchases()&&j.transactionId==int.Parse(number)))
                {
                    journalEntries.Add(new DataSetJournalEntry()
                    {
                        id=entry.id,
                        number = entry.transactionId ?? 0,
                        type = entry.transactionType,
                        credit = entry.credit ?? 0,
                        debit = entry.debit ?? 0,
                        currency = entry.Currency.name,
                        account = entry.Account.name,
                        description = entry.description,
                    });
                }
            }
            catch
            {
               
                AppDialogAleart.showAleartError();
                status = false;
            }
            dataSource.DataSource=journalEntries;
            if (status&&!journalEntries.Any())
                AppDialogAleart.showAleartError("لم يتم العثور على بيانات فاتوره بهذا الرقم");
            return journalEntries.Any();
        }
    }
  
}
