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
using System.Globalization;
using AccountingSystem.model;
using AccountingSystem.view.ReportPages;
using Guna.UI2.WinForms.Suite;
using System.Threading;

namespace AccountingSystem.controller
{
    public class VoucherController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "المبلغ", "العمله", "الحساب", "الصندوق", "التأريخ", "البيان", "تأريخ الإضافه" };//, "تأريخ التعديل"
        public BindingSource dataSource;
        DataTable dataTable;
        public dynamic allData;
        AccountingDbContext dBContext;
        public List<ChartOfAccount> supAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "فرعي").Include(i=>i.JournalEntries).ToList(); } }
        public List<Currency> allCurrency { get { return dBContext.Currencies.ToList(); } }
        public List<Cashier> allCashiers { get { return dBContext.Cashiers.Include(i=>i.Account).Include(i => i.Account.JournalEntries).ToList(); } }
        public Voucher temp;
       public JournalEntry journalEntryAccountCredit;
        JournalEntry journalEntryCashierCredit;
        public TransactionType transactionType;
        public DateTime? startDate = null;
        public DateTime? endDate = null;
        public DateTime? lastDate = null;
        public ProsessesType prosessesType { get; set; }
        decimal balanceAccount = 0;
        decimal balanceCashiert = 0;
        public bool HasHomeScreenDataProcessed;
        public bool HasAddAndUpdateScreenDataProcessed;
        public VoucherController(TransactionType transactionType)
        {
            HasHomeScreenDataProcessed = false;
            this.transactionType = transactionType;  
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            temp = new Voucher();
            lodeData();
        }
        public void clearTempData(bool t=false)
        {
            //HasAddAndUpdateScreenDataProcessed = t;
            //temp = new Voucher() { date= (prosessesType == ProsessesType.add && temp != null) ? temp.date : DateTime.Now,Currency= allCurrency.FirstOrDefault(c => c.currencyType == "رئيسية") };
       
            temp = new Voucher() { date=DateTime.Now,Currency= allCurrency.FirstOrDefault(c => c.currencyType == "رئيسية") };
            temp.Account =  null;
            temp.Cashier = null;
            balanceAccount = 0; balanceCashiert = 0;    
        }   
        public void clearTempDataUpdate(bool t=false)
        {
            temp.date = DateTime.Now;
            temp.Account =  null;
            temp.Cashier = null;
            temp.Currency = allCurrency.FirstOrDefault(c => c.currencyType == "رئيسية");
            balanceAccount = 0; balanceCashiert = 0;    
        }
        public void lodeData()
        {

            clearTempData();

            try
            {

                allData = dBContext.Vouchers.AsNoTracking().OrderByDescending(a => a.id)
                    .Where(i=>i.type== transactionType.ToString()).ToList()
                    .Select(e => new
                    {
                        id = e.id,
                        amount = e.amount,
                        currency=e.Currency.name,
                        account = e.Account.name,
                        cashier = e.Cashier.name,
                        date = e.date,
                        description = e.description,
                        entryDate = e.entryDate,
                       
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

             dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
          Thread thread=new Thread(new ThreadStart(fillDataTable));
            thread.Start();
        }

        private void fillDataTable()
        {
            foreach (var Voucher in allData)
            {
                dataTable.Rows.Add(Voucher.id, Voucher.amount,
                Voucher.currency, Voucher.account, Voucher.cashier,
                ((DateTime)Voucher.date).Format(), Voucher.description, ((DateTime)Voucher.entryDate).Format());
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            journalEntryAccountCredit = null;
            journalEntryCashierCredit = null;
            try
            {
                temp = new Voucher();
                temp = dBContext.Vouchers.OrderByDescending(a => a.id).Include(c => c.Currency)
                    .Include(c => c.Cashier).Include(c => c.Cashier.Account).Include(c => c.Account).FirstOrDefault(i => i.id == id&& i.type== transactionType.ToString());

                if (temp == null)
                    throw new Exception();
                journalEntryAccountCredit=dBContext.JournalEntries.FirstOrDefault(i=>i.transactionId==temp.id&&i.accountId== temp.Account.id&&i.transactionType== transactionType.ToString());
                journalEntryCashierCredit= dBContext.JournalEntries.FirstOrDefault(i => i.transactionId == temp.id && i.accountId == temp.Cashier.Account.id && i.transactionType == transactionType.ToString());

                if (journalEntryAccountCredit == null|| journalEntryCashierCredit==null)
                    throw new Exception();
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search( string description)
        {  
            string account = temp.Account == null ? "" : temp.Account.name;
            string currency = temp.Currency == null ? "" : temp.Currency.name;
            string cashier = temp.Cashier == null ? "" : temp.Cashier.name;
     
           
            try
            {
                allData = dBContext.Vouchers.AsNoTracking().OrderByDescending(a => a.id).Include(c => c.Currency)
                    .Include(c => c.Cashier).Include(c => c.Cashier.Account).Include(c => c.Account).
                    Where(
                        v => 
                         DbFunctions.Like(v.description, "%" + description + "%")
                             && DbFunctions.Like(v.Account.name, "%" + account + "%")
                             && DbFunctions.Like(v.Currency.name, "%" + currency + "%")
                             && DbFunctions.Like(v.Cashier.name , "%" + cashier + "%")
                             && v.type== transactionType.ToString()
                         ).
                    Select(e => new
                    {
                        id = e.id,
                        amount = e.amount,
                        currency = e.Currency.name,
                        account = e.Account.name,
                        cashier = e.Cashier.name,
                        date = e.date,
                        description = e.description,
                        entryDate = e.entryDate,
                    }).ToList().
                    Where(v=> (startDate == null || v.date.Value.Date >= startDate.Value.Date) && (endDate == null || v.date.Value.Date <= endDate.Value.Date));

                fillDataGridView();
            }
            catch
            {
               
                AppDialogAleart.showAleartError();
            }
        }
        //private bool add()
        //{

        //}
        public void print(int id)
        {
            dynamic data = new { };
            if(!find(id))
                return;
                data = new { date = temp.date.Value.ToString(SharedData.formatDisplayDate), type = "("+ transactionType.ToString().Replace("_"," ") + ")", number = temp.id.ToString(), user = temp.Employee?.name ?? "user", account = temp.Account.name, accountNumber = (temp.Account?.accountNumber??0).ToString(), amount = (temp.amount??0).ToString(), currency = temp.Currency.name, description=temp.description, currencyCode=temp.Currency?.code ??""};
            (new ViewPrintingVoucher(data)).ShowDialog();
            clearTempData();
        }
        public bool add(string amount,string exchangeRate, string description)
        {

            bool status = false;

            if (!ValidatingData.validatingData(amount, columnsNamesInAR[1]))
                return false;
           
            if (!ValidatingData.validatingData(temp.Currency, columnsNamesInAR[2],false))
                return false;
            if (temp.Currency.currencyType == "ثانوية")
                if (!ValidatingData.validatingData(exchangeRate, "سعر الصرف"))
                    return false;
            if (!ValidatingData.validatingData(temp.Account, columnsNamesInAR[3], false))
                return false;
            if (!ValidatingData.validatingData(temp.Cashier, columnsNamesInAR[4], false))
                return false;
           
           
            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {

                    Voucher newItem = new Voucher() { employeeId = LoginData.employee?.id,brancheId=LoginData.branch?.id, amount =Convert.ToDecimal(amount),currencyId=temp.Currency.id,accountId=temp.Account.id,cashierID=temp.Cashier.id,description=description,date=temp.date,entryDate=DateTime.Now,type=transactionType.ToString() };
                    newItem = dBContext.Vouchers.Add(newItem);
                    dBContext.SaveChanges();
                   
                    string accountDescription = "";
                   string cashierDescription = "";
                   
                    JournalEntry journalEntryAccountCredit = new JournalEntry() { transactionId=newItem.id,accountId= newItem.accountId, currencyId= newItem.currencyId, ExchangeRate=Convert.ToDecimal(exchangeRate),transactionType= transactionType.ToString(),transactionDate= newItem.date};
                 
                    JournalEntry journalEntryCashierCredit = new JournalEntry() { transactionId=newItem.id,accountId= newItem.Cashier.accountId, currencyId= newItem.currencyId, ExchangeRate=Convert.ToDecimal(exchangeRate),transactionType= transactionType.ToString(),transactionDate= newItem.date };
                    switch (transactionType)
                    {
                        case TransactionType.سند_قبض:
                            accountDescription = "سند قيض الى صندوق  / " + temp.Cashier.name + "/ ملاحضات/ " + description;
                            cashierDescription = "سند قيض من حساب  / " + temp.Account.name + "/ ملاحضات/ " + description;
                            journalEntryCashierCredit.debit = newItem.amount;
                            journalEntryAccountCredit.credit = newItem.amount;
                            journalEntryAccountCredit.debit = 0;
                            journalEntryCashierCredit.credit = 0;
                            break;
                        default:
                            if (AppDBFunctions.verifyNewBalanceNotNegative(balanceAccount, Convert.ToDecimal(amount)) != DialogResult.OK)
                                return false;
                            accountDescription = "سند صرف من صندوق  /  " + temp.Cashier.name + "/ ملاحضات/ " + description;
                            cashierDescription = "سند صرف الى حساب  / " + temp.Account.name + "/ ملاحضات/ " + description;
                            journalEntryCashierCredit.credit = newItem.amount;
                            journalEntryAccountCredit.debit = newItem.amount;
                            journalEntryAccountCredit.credit = 0;
                            journalEntryCashierCredit.debit = 0;
                            break;

                    }
                    journalEntryAccountCredit.description = accountDescription;
                    journalEntryCashierCredit.description = cashierDescription;
                    dBContext.JournalEntries.AddRange(new JournalEntry[] { journalEntryAccountCredit, journalEntryCashierCredit });
                    dBContext.SaveChanges();
                    transaction.Commit();
                    lastDate=temp.date;
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

        public bool update(string amount, string exchangeRate, string description)
        {

            bool status = false;


            if (!ValidatingData.validatingData(amount, columnsNamesInAR[1]))
                return false;

            if (!ValidatingData.validatingData(temp.Currency, columnsNamesInAR[2], false))
                return false;
            if (temp.Currency.currencyType == "ثانوية")
                if (!ValidatingData.validatingData(exchangeRate, "سعر الصرف"))
                    return false;
            if (!ValidatingData.validatingData(temp.Account, columnsNamesInAR[3], false))
                return false;
            if (!ValidatingData.validatingData(temp.Cashier , columnsNamesInAR[4], false))
                return false;
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {
                    AppDialogAleart.showAleartNoPermissions(" temp.Currency.name=" + temp.Currency.name);
                    AppDialogAleart.showAleartNoPermissions(" temp.Currency.id=" + temp.Currency.id);
                     temp.amount=Convert.ToDecimal(amount);
                     temp.currencyId = temp.Currency.id; temp.cashierID = temp.Cashier.id;
                     temp.accountId = temp.Account.id;
                    temp.description = description;
                    //dBContext.Vouchers.
                    journalEntryAccountCredit.ExchangeRate=Convert.ToDecimal(exchangeRate);
                    journalEntryAccountCredit.currencyId = temp.currencyId;
                    journalEntryAccountCredit.accountId = temp.accountId;
                   journalEntryAccountCredit.transactionDate=temp.date;
                    journalEntryCashierCredit.ExchangeRate = Convert.ToDecimal(exchangeRate);
                    journalEntryCashierCredit.currencyId = temp.currencyId;
                    journalEntryCashierCredit.accountId = temp.Cashier.accountId;
                    journalEntryCashierCredit.transactionDate = temp.date;
                    string accountDescription="";
                    string cashierDescription="";
                    switch (transactionType)
                    {
                        case TransactionType.سند_قبض:
                            accountDescription = "سند قيض الى صندوق  / " + temp.Cashier.name + "/ ملاحضات/ " + description;
                            cashierDescription = "سند قيض من حساب  / " + temp.Account.name + "/ ملاحضات/ " + description;
                            journalEntryCashierCredit.debit = temp.amount;
                                journalEntryAccountCredit.credit = temp.amount;
                            break;  
                            default:
                            if (AppDBFunctions.verifyNewBalanceNotNegative(balanceAccount, Convert.ToDecimal(amount)) != DialogResult.OK)
                                return false;
                            accountDescription = "سند صرف من صندوق  /  " + temp.Cashier.name + "/ ملاحضات/ " + description;
                            cashierDescription = "سند صرف الى حساب  / " + temp.Account.name + "/ ملاحضات/ " + description;
                            journalEntryCashierCredit.credit = temp.amount;
                            journalEntryAccountCredit.debit = temp.amount;
                            break;
                    }
                    journalEntryAccountCredit.description = accountDescription;
                    journalEntryCashierCredit.description = cashierDescription;
                    // dBContext.JournalEntries.AddRange(new JournalEntry[] { journalEntryAccountCredit, journalEntryCashierCredit });
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();


                }
                catch(DbEntityValidationException e)
                {
                    transaction.Rollback();
                    AppDialogAleart.showEntityValidationErrors(e);
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

                          dBContext.JournalEntries.RemoveRange(new JournalEntry[] { journalEntryAccountCredit, journalEntryCashierCredit });
                            dBContext.SaveChanges();
                          
                            dBContext.Vouchers.Remove(temp);
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
            if (HasAddAndUpdateScreenDataProcessed || HasHomeScreenDataProcessed)
                temp.Currency=(Currency)currency ?? null;
        }
        public string selectedAccount(object account)
        {
            string balance = "";
            if (HasAddAndUpdateScreenDataProcessed || HasHomeScreenDataProcessed)
            {
                temp.Account = (ChartOfAccount)account ?? null;
                decimal sumCredit = temp.Account?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.credit) ?? 0;
                decimal sumDebit = temp.Account?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.debit) ?? 0;
                balanceAccount = sumCredit - sumDebit;
                if (temp.Currency != null)
                    balance = (balanceAccount >= 0 ? "دائن : " : "مدين : ") + (balanceAccount < 0 ? balanceAccount * -1 : balanceAccount).Format();
            }

            return balance;
        }
        public string selectedCashier(object cashier)
        {
            string balance = "";
            if (HasAddAndUpdateScreenDataProcessed || HasHomeScreenDataProcessed)
            {
                temp.Cashier = (Cashier)cashier ?? null;
                decimal sumCredit = temp.Cashier?.Account?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.credit) ?? 0;
                decimal sumDebit = temp.Cashier?.Account?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.debit) ?? 0;
                balanceCashiert = sumCredit - sumDebit;
                if(temp.Currency!=null)
                balance = (balanceCashiert >= 0 ? "دائن : " : "مدين : ") + (balanceCashiert < 0 ? balanceCashiert * -1 : balanceCashiert).Format();
            }
            return balance;
        }
        public void selectedDate(DateTime? date)
        {
            if (HasAddAndUpdateScreenDataProcessed || HasHomeScreenDataProcessed)
                temp.date = date;
        }
        public void selectedStartDate(DateTime? date)
        {
            startDate = date;
        }
        public void selectedEndDate(DateTime? date)
        {
            endDate = date;
        }
    }


}
