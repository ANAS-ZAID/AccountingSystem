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
    public class SimpleJournalEntriesController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "المبلغ", "العمله", " من حساب", "الى حساب", "التأريخ", "البيان", "تأريخ الإضافه","تأريخ التحديث" };//, "تأريخ التعديل"
        public BindingSource dataSource;
        public dynamic allData;
        AccountingDbContext dBContext;
        public List<ChartOfAccount> supAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "فرعي").Include(i => i.JournalEntries).ToList(); } }
        public List<Currency> allCurrency { get { return dBContext.Currencies.ToList(); } }
      
        public SimpleEntry temp;
        public JournalEntry journalEntrycCreditAccount;
        JournalEntry journalEntryDebitAccount;
        public TransactionType transactionType;
        DateTime? startDate;
        DateTime? endDate;
        public ProsessesType prosessesType { get; set; }
        decimal balanceCrditAccount = 0;
        decimal balanceDebitAccount = 0;
        public SimpleJournalEntriesController()
        {
            this.transactionType =TransactionType.قيد_بسيط;
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            temp = new SimpleEntry();
            lodeData();
        }
        public void clearTempData()
        {
            temp = new SimpleEntry();
            temp.AccountCredit = null;
            temp.Currency = null;
            temp.AccountDebit = null;
            //   temp.JournalEntries = null;
            journalEntrycCreditAccount = null;
            journalEntryDebitAccount = null;
        }
        public void lodeData()
        {

            clearTempData();

            try
            {

                allData = dBContext.SimpleEntries.AsNoTracking().OrderByDescending(a => a.id).Include(c => c.Currency)
                    .Include(c => c.AccountDebit).Include(c => c.AccountCredit).ToList()
                    .Select(e => new
                    {
                        id = e.id,
                        amount = e.amount,
                        currency = e.Currency.name,
                        accountDebit = e.AccountDebit.name,
                        accountCredit = e.AccountCredit.name,
                        date = e.date,
                        description = e.description,
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
                dataTable.Rows.Add(SimpleEntry.id, SimpleEntry.amount,
                SimpleEntry.currency, SimpleEntry.accountDebit, SimpleEntry.accountCredit,
                SimpleEntry.date?.ToString(SharedData.formatDisplayDate), SimpleEntry.description, SimpleEntry.entryDate?.ToString(SharedData.formatDisplayDate), SimpleEntry.updateDate?.ToString(SharedData.formatDisplayDate));
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                temp = new SimpleEntry();
                temp = dBContext.SimpleEntries.OrderByDescending(a => a.id).Include(c => c.Currency)
                    .Include(c => c.AccountDebit).Include(c => c.AccountCredit).FirstOrDefault(i => i.id == id );

                if (temp == null)
                    throw new Exception();
                journalEntrycCreditAccount = dBContext.JournalEntries.FirstOrDefault(i => i.transactionId == temp.id && i.accountId == temp.AccountCredit.id && i.transactionType == transactionType.ToString());
                journalEntryDebitAccount = dBContext.JournalEntries.FirstOrDefault(i => i.transactionId == temp.id && i.accountId == temp.AccountDebit.id && i.transactionType == transactionType.ToString());
          

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
            if (LoginData.permissions["simpleJournalEntries"].viewPermission.Value)
            {
                string debitAccount = temp.AccountDebit == null ? "" : temp.AccountDebit.name;
                string currency = temp.Currency == null ? "" : temp.Currency.name;
                string creditAccount = temp.AccountCredit == null ? "" : temp.AccountCredit.name;




                try
                {
                    allData = dBContext.SimpleEntries.AsNoTracking().OrderByDescending(a => a.id).Include(c => c.Currency)
                        .Include(c => c.AccountDebit).Include(c => c.AccountCredit).
                        Where(v => DbFunctions.Like(v.description, "%" + description + "%")
                                 && DbFunctions.Like(v.AccountDebit.name, "%" + debitAccount + "%")
                                 && DbFunctions.Like(v.Currency.name, "%" + currency + "%")
                                 && DbFunctions.Like(v.AccountCredit.name, "%" + creditAccount + "%")

                             ).
                        Select(e => new
                        {
                            id = e.id,
                            amount = e.amount,
                            currency = e.Currency.name,
                            accountDebit = e.AccountDebit.name,
                            accountCredit = e.AccountCredit.name,
                            date = e.date,
                            description = e.description,
                            entryDate = e.entryDate,
                            updateDate=e.updateDate,
                        }).ToList().Where(v => (startDate == null || v.date.Value.Date >= startDate.Value.Date) && (endDate == null || v.date.Value.Date <= endDate.Value.Date));

                    fillDataGridView();
                }
                catch
                {

                    AppDialogAleart.showAleartError();
                }
            }
        }

        public bool add(string amount, string exchangeRate, string description)
        {
            //AppDialogAleart.showAleartConfirmation(temp.Currency.id+" name="+temp.Currency.name+"type="+temp.Currency.currencyType);

            bool status = false;

            if (!ValidatingData.validatingData(amount, columnsNamesInAR[1]))
                return false;

            if (!ValidatingData.validatingData(temp.Currency, columnsNamesInAR[2], false))
                return false;
            if (temp.Currency.currencyType == "ثانوية")
                if (!ValidatingData.validatingData(exchangeRate, "سعر الصرف"))
                    return false;
            if (!ValidatingData.validatingData(temp.AccountDebit, columnsNamesInAR[3], false))
                return false;
            if (!ValidatingData.validatingData(temp.AccountCredit, columnsNamesInAR[4], false))
                return false;


            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {
                   if (AppDBFunctions.verifyNewBalanceNotNegative(balanceDebitAccount, Convert.ToDecimal(amount))!=DialogResult.OK)
                        return false;
                    SimpleEntry newItem = new SimpleEntry() { brancheId = LoginData.branch?.id , employeeId=LoginData.employee?.id,amount = Convert.ToDecimal(amount), currencyId = temp.Currency.id, debitAccount = temp.AccountDebit.id, creditAccount = temp.AccountCredit.id, description = description, date = temp.date, entryDate = DateTime.Now };
                    newItem = dBContext.SimpleEntries.Add(newItem);
                    dBContext.SaveChanges();

                    string debitDescription = "تحويل الى حساب  / " + temp.AccountCredit.name + "/ ملاحضات/ " + description;
                    string creditDescription = "تحويل من حساب  / " + temp.AccountDebit.name + "/ ملاحضات/ " + description;

                    JournalEntry journalEntrycCreditAccount = new JournalEntry() {credit= newItem.amount,debit=0, transactionId = newItem.id, accountId = newItem.AccountCredit.id, currencyId = newItem.currencyId, ExchangeRate = Convert.ToDecimal(exchangeRate), transactionType = transactionType.ToString(), transactionDate = newItem.date ,description=creditDescription};

                    JournalEntry journalEntryDebitAccount = new JournalEntry() { credit =0, debit = newItem.amount, transactionId = newItem.id, accountId = newItem.AccountDebit.id, currencyId = newItem.currencyId, ExchangeRate = Convert.ToDecimal(exchangeRate), transactionType = transactionType.ToString(), transactionDate = newItem.date,description=debitDescription };
                    dBContext.JournalEntries.AddRange(new JournalEntry[] { journalEntrycCreditAccount, journalEntryDebitAccount });
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();

                }
                catch //(DbEntityValidationException ex)
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
            if (!ValidatingData.validatingData(temp.AccountDebit, columnsNamesInAR[3], false))
                return false;
            if (!ValidatingData.validatingData(temp.AccountCredit, columnsNamesInAR[4], false))
                return false;
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {
                    if (AppDBFunctions.verifyNewBalanceNotNegative(balanceDebitAccount, Convert.ToDecimal(amount)) != DialogResult.OK)
                        return false;
                    string debitDescription = "تحويل الى حساب  / " + temp.AccountCredit.name + "/ ملاحضات/ " + description;
                    string creditDescription = "تحويل من حساب  / " + temp.AccountDebit.name + "/ ملاحضات/ " + description;
                    temp.amount = Convert.ToDecimal(amount);
                    temp.currencyId = temp.Currency.id; temp.AccountCredit.id = temp.AccountCredit.id;
                    temp.AccountDebit.id = temp.AccountDebit.id;
                    temp.description = description;
                   temp.updateDate= DateTime.Now;
                    journalEntrycCreditAccount.ExchangeRate = Convert.ToDecimal(exchangeRate);
                    journalEntrycCreditAccount.currencyId = temp.currencyId;
                    journalEntrycCreditAccount.accountId = temp.AccountCredit.id;
                   journalEntrycCreditAccount.transactionDate=temp.date;
                   journalEntrycCreditAccount.credit=temp.amount;
                    journalEntryDebitAccount.ExchangeRate = Convert.ToDecimal(exchangeRate);
                    journalEntryDebitAccount.currencyId = temp.currencyId;
                    journalEntryDebitAccount.accountId = temp.AccountDebit.id;
                    journalEntryDebitAccount.transactionDate = temp.date;
                    journalEntryDebitAccount.debit = temp.amount;
                    journalEntrycCreditAccount.description = debitDescription;
                    journalEntryDebitAccount.description = creditDescription;
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

                            dBContext.JournalEntries.RemoveRange(new JournalEntry[] { journalEntrycCreditAccount, journalEntryDebitAccount });
                            dBContext.SaveChanges();
                            dBContext.SimpleEntries.Remove(temp);
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
        public decimal selectedCreditAccount(object account)
        {

            temp.AccountCredit = (ChartOfAccount)account ?? null;
            decimal sumCredit = temp.AccountCredit?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.credit) ?? 0;
            decimal sumDebit = temp.AccountCredit?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.debit) ?? 0;
            balanceCrditAccount = sumCredit - sumDebit;
            return balanceCrditAccount;
        }
        public decimal selectedDebitAccount(object cashier)
        {
            temp.AccountDebit = (ChartOfAccount)cashier ?? null;
            decimal sumCredit = temp.AccountDebit?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.credit) ?? 0;
            decimal sumDebit = temp.AccountDebit?.JournalEntries?.Where(i => i.currencyId == temp.Currency?.id)?.Sum(J => J.debit) ?? 0;
            balanceDebitAccount = sumCredit - sumDebit;
            return balanceDebitAccount ;
        }
        public void selectedDate(DateTime? date)
        {
            temp.date = date;
        }       public void selectedStartDate(DateTime? date)
        {
            startDate = date;
        } 
        public void selectedEndtDate(DateTime? date)
        {
            endDate = date;
        }


}
}
