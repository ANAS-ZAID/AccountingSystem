using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.model;

namespace AccountingSystem.controller
{
    public class CompoundJournalEntriesController
    {

        public List<string> columnsNamesInAR = new List<string> { "الرقم", "العمله", "التأريخ", " إجمالي الدائن", "إجمالي المدين ", "الفارق", "تأريخ الإضافه","تأريخ التعديل" };//, "تأريخ التعديل"
        public BindingSource dataSource;
        public dynamic allData;
        AccountingDbContext dBContext;
        DateTime? startDate; DateTime? endDate;
        public List<ChartOfAccount> supAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "فرعي").Include(i => i.JournalEntries).ToList(); } }
        public List<Currency> allCurrency { get { return dBContext.Currencies.ToList(); } }

        public List<JournalEntry> temJournalEntries;
        public List<Dictionary<int, ChartOfAccount>> tempAccount;
        public CompoundEntry temp;

        public TransactionType transactionType;
        public ProsessesType prosessesType { get; set; }

        public CompoundJournalEntriesController(TransactionType transactionType)
        {
          this.transactionType = transactionType;
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            temp = new CompoundEntry();
            temJournalEntries = new List<JournalEntry>();

              tempAccount = new List<Dictionary<int, ChartOfAccount>>(); 
            lodeData();
        }
        public void clearTempData()
        {
            temp = new CompoundEntry();
            temp.Currency = null;
            temJournalEntries= new List<JournalEntry>();
        }
        public void lodeData()
        {

            clearTempData();

            try
            {

                allData = dBContext.CompoundEntries.AsNoTracking().Where(i=>i.type==transactionType.ToString()).OrderByDescending(a => a.id).Include(c => c.Currency)
                    .Include(c => c.Employee).ToList()
                    .Select(e => new
                    {
                        id = e.id,
                        currency = e.Currency.name,
                        date = e.date,
                        creditTotal = e.creditTotal,
                        debitTotal = e.debitTotal,
                        difference = e.creditTotal- e.debitTotal,
                        entryDate = e.entryDate,
                        updateDate = e.updateDate,

                    }).ToList();

                fillDataGridView();
            }
            catch 
            {
                AppDialogAleart.showAleartError();
            }
        }
        void fillDataGridView()
        {

            var dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            foreach (var SimpleEntry in allData)
            {
                dataTable.Rows.Add(SimpleEntry.id, SimpleEntry.currency,
                SimpleEntry.date.ToString(SharedData.formatDisplayDate), SimpleEntry.creditTotal, SimpleEntry.debitTotal,
                SimpleEntry.difference, SimpleEntry.entryDate?.ToString(SharedData.formatDisplayDate),SimpleEntry.updateDate?.ToString(SharedData.formatDisplayDate));
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                temp = new CompoundEntry() { Currency=null};
                temp = dBContext.CompoundEntries.Where(i => i.type == transactionType.ToString()).OrderByDescending(a => a.id).Include(c => c.Currency)
                    .FirstOrDefault(i => i.id == id);

                if (temp == null)
                    throw new Exception();
              temJournalEntries=dBContext.JournalEntries.Include(a=>a.Account).Where(a => a.transactionId == temp.id&& a.transactionType==transactionType.ToString()).ToList();
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search()
        {
            if ((LoginData.permissions["compoundJournalEntries"].viewPermission.Value && transactionType == TransactionType.قيد_مركب) || ((LoginData.permissions["openingBalances"].viewPermission.Value && transactionType == TransactionType.رصيد_إفتتاحي)))
            {
                string currency = temp.Currency != null ? temp.Currency.name : "";

                try
                {
                    allData = dBContext.CompoundEntries.AsNoTracking().Where(i => i.type == transactionType.ToString()).OrderByDescending(a => a.id).Include(c => c.Currency).
                        Where(
                            v => DbFunctions.Like(v.Currency.name, "%" + currency + "%")
                             ).
                        Select(e => new
                        {
                            id = e.id,
                            currency = e.Currency.name,
                            date = e.date,
                            debitTotal = e.debitTotal,
                            creditTotal = e.creditTotal,
                            difference = e.creditTotal - e.debitTotal,
                            entryDate = e.entryDate,
                            updateDate = e.updateDate,
                        }).ToList().Where(
                            v => ((startDate == null || v.entryDate.Value.Date >= startDate.Value.Date) && (endDate == null || v.entryDate.Value.Date <= endDate.Value.Date)));

                    fillDataGridView();
                }
                catch
                {

                    AppDialogAleart.showAleartError();
                }
            }
        }
     

        public bool add(string creditTotal, string debitTotal, string exchangeRate,List<JournalEntry> journalEntries)
        {
         

            bool status = false;

            if (!ValidatingData.validatingData(temp.Currency, columnsNamesInAR[1],false))
                return false;
            if (temp.Currency.currencyType == "ثانوية")
                if (!ValidatingData.validatingData(exchangeRate, "سعر الصرف"))
                    return false;
            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {

                CompoundEntry newItem = new CompoundEntry() { brancheId = LoginData.branch?.id, employeeId =LoginData.employee?.id, debitTotal=Convert.ToDecimal(debitTotal), creditTotal=Convert.ToDecimal(creditTotal),currencyId=temp.Currency.id ,date=temp.date ,entryDate = DateTime.Now, type = transactionType.ToString() };
                    newItem = dBContext.CompoundEntries.Add(newItem);
                    dBContext.SaveChanges();

                    if (journalEntries.Any())
                    {
                        journalEntries.ForEach(item => { item.transactionId = newItem.id;item.transactionDate = temp.date;item.currencyId=temp.Currency.id ; item.ExchangeRate = Convert.ToDecimal(exchangeRate);item.transactionType = transactionType.ToString(); });

                        dBContext.JournalEntries.AddRange(journalEntries);
                        dBContext.SaveChanges();
                    }
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();

                }
                catch 
                {

                   

                    transaction.Rollback();
                    AppDialogAleart.showAleartError();
                    status = false;

                }
            }

            return status;
        }

        public bool update(string creditTotal, string debitTotal, string exchangeRate, List<JournalEntry> journalEntries)
        {

            bool status = false;


            if (!ValidatingData.validatingData(temp.Currency, columnsNamesInAR[1]))
                return false;
            if (temp.Currency.currencyType == "ثانوية")
                if (!ValidatingData.validatingData(exchangeRate, "سعر الصرف"))
                    return false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {
                 
                    temp.updateDate=DateTime.Now;
                    temp.currencyId = temp.Currency.id;
                    temp.creditTotal=Convert.ToDecimal(creditTotal);
                    temp.debitTotal=Convert.ToDecimal(debitTotal);
                 
                    journalEntries.ForEach(item => { item.transactionType = transactionType.ToString(); item.transactionId = temp.id; item.transactionDate = temp.date; item.currencyId = temp.Currency.id; item.ExchangeRate = Convert.ToDecimal(exchangeRate); });
                    dBContext.JournalEntries.RemoveRange(temJournalEntries);
                    dBContext.SaveChanges();       
                    dBContext.JournalEntries.AddRange(journalEntries);
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();


                }
                catch
                {
                    transaction.Rollback();
                    AppDialogAleart.showAleartError();
                    status = false;
                }
            }

            return status;
        }


        public bool delete(List<int> keys)
        {
            bool status = false;

            if (keys.Count > 0)
            {
                if (AppDialogAleart.showAleartConfirmation("هل أنت متأكد انك ترغب في حذف البيانات المحدده وعددها: " + keys.Count) != DialogResult.OK)
                    return false;
                using (var transaction = dBContext.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var id in keys)
                        {
                            if (!find(id))
                                throw new Exception("حدث خطأ ما في العمليه ");

                            dBContext.JournalEntries.RemoveRange(temJournalEntries);
                            dBContext.SaveChanges();
                            dBContext.CompoundEntries.Remove(temp);
                            dBContext.SaveChanges();
                        }
                        status = true;
                        AppDialogAleart.showAleartSuccess();
                        dBContext.SaveChanges();
                        transaction.Commit();
                        lodeData();
                    }
                    catch
                    {
                        transaction.Rollback();
                        AppDialogAleart.showAleartError();

                        status = false;
                    }
                }
            }
            else { AppDialogAleart.showAleartError("لم تقم بتحديد اي بيانات للحذف"); }

            return status;
        }

        public void selectedCurrency(object currency)
        {
            temp.Currency = (Currency)currency ?? null;
        }      
        public void selectedDate(DateTime? date)
        {
            temp.date = date;
        }public void selectedStartDate(DateTime? date)
        {
            startDate = date;
        }   
        public void selectedEndDate(DateTime? date)
        {
            endDate = date;
        }
        
    }
}
