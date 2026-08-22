using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.controller
{
    public class AccountStatementController
    {
        public BindingSource dataSource;
        public  List<DataSetAccountStatement>  tempData;
        AccountingDbContext dBContext;
        public  DataRowCollection dataRowCollection;
        public  DataRowCollection dataRowCollectionTotal;
        public string mainCurrencyCode { get { return dBContext.Currencies.FirstOrDefault(x => x.currencyType == "رئيسية")?.code ?? ""; } }
        public List<ChartOfAccount> accounts { get {
                var temp = dBContext.ChartOfAccounts.AsNoTracking().Where(x=>x.type=="فرعي").ToList();
                temp.Insert(0, new ChartOfAccount() { name = "كافة الحسابات", id = 0 });
                return temp;
            } } 
        public List<Currency> currencies { get {
                var temp = dBContext.Currencies.AsNoTracking().ToList();
                temp.Insert(0, new Currency() { name = "كافة العملات", id = 0 });
                return temp;
            } } 
        public List<AccountsGroup> groups { get {
                var temp = dBContext.AccountsGroups.AsNoTracking().ToList();
                temp.Insert(0, new AccountsGroup() { name = "كافة المجموعات", id = 0 });
                return temp;
            } }
       public DateTime? startDate = null;
      public  DateTime? endDate = null;
        public ChartOfAccount account;
        public Currency currency;
        public AccountsGroup group;
        public bool total=false;
        public bool noOpeningBalance = false;
        public bool isReporteView = false;
        public bool HasDataProcessed = false;
        public AccountStatementController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            tempData=new List<DataSetAccountStatement> {  };
            dataSource.DataSource = typeof(DataSetAccountStatement);
        }
 
        public bool search()
        {
                tempData= new List<DataSetAccountStatement> { };
                string accountName = account != null && account.id != 0 ? account.name : "";
                string currencyName = currency != null && currency.id != 0 ? currency?.name : "";
                string groupName = group != null && group.id != 0 ? group.name : "";
                string transactionType = TransactionType.رصيد_إفتتاحي.ToString();
           

                try
                {
             
                if (account != null)
                {
                    var alii = dBContext.ChartOfAccounts.AsNoTracking().
                   Where(x => x.type=="فرعي"&&
                   (account.id != 0 ? x.id == account.id : true) &&
                   DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%")
               ).Include(x => x.JournalEntries).ToList();
                   
                    foreach (var item in alii)
                    {
                 
                        if (item.JournalEntries.Any())
                        {
                            var j = item.JournalEntries.Where(
                            x =>
                           (startDate == null || x.transactionDate.Value.Date >= startDate.Value.Date) &&
                            (endDate == null || x.transactionDate.Value.Date <= endDate.Value.Date) &&
                          (group == null || group.id == 0 || x.Account.accountGroupId == group.id) &&
                          x.Currency.name.Contains(currencyName)
                          && (noOpeningBalance ? !x.transactionType.Contains(transactionType) : true)
                      ).ToList();
                            foreach (var group in j.GroupBy(x => x.currencyId).OrderBy(x => x.Key))
                            {
                                string currency = dBContext.Currencies.Find(group.Key)?.name;
                                decimal previousDebit = 0;
                                decimal previousCredit = 0;
                                decimal previousBalance = 0;

                                decimal balanceDebit = 0;
                                decimal balanceCredit = 0;
                                decimal finalBalance = 0;
                                if (!total)
                                {
                                    if (startDate != null)
                                    {
                                        var previousEntries = dBContext.JournalEntries.ToList().Where(entery => entery.currencyId == group.Key && entery.accountId == item.id && entery.transactionDate.Value.Date < startDate.Value.Date).ToList();
                                        previousCredit = previousEntries?.Sum(entery => entery.credit ?? 0) ?? 0;
                                        previousDebit = previousEntries?.Sum(entery => entery.debit ?? 0) ?? 0;
                                        previousBalance = previousCredit - previousDebit;
                                        tempData.Add(new DataSetAccountStatement
                                        {
                                            debit = previousDebit,
                                            credit = previousCredit,
                                            balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
                                            balanceCredit = previousBalance > 0 ? previousBalance : 0,
                                            currency = currency,
                                            date = "_",
                                            transactionType = "",
                                            description = "رصيد سابق",
                                            account = item.name,
                                            numberColor = -1
                                        });

                                    }

                                    foreach (var journalEntry in group)
                                    {
                                     
                                        balanceCredit += journalEntry.credit ?? 0;
                                        balanceDebit += journalEntry.debit ?? 0;
                                        finalBalance = balanceCredit - balanceDebit + previousBalance;
                                        tempData.Add(new DataSetAccountStatement { debit = journalEntry.debit ?? 0, credit = journalEntry.credit ?? 0, balanceDebit = finalBalance < 0 ? finalBalance * -1 : 0, balanceCredit = finalBalance > 0 ? finalBalance : 0, currency = journalEntry.Currency.name, date = journalEntry.transactionDate?.ToString(SharedData.formatDisplayDate), transactionType = journalEntry.transactionType.Replace("_", " "), description = journalEntry.description, account = item.name, numberColor = -1 });

                                    }
                                }
                                else
                                {
                                    balanceCredit = group.Sum(x => x.credit ?? 0);
                                    balanceDebit = group.Sum(x => x?.debit ?? 0);
                                    finalBalance = balanceCredit - balanceDebit;
                                }

                                if (!total)
                                {
                                    if (startDate != null && !total)
                                        tempData.Add(new
                              DataSetAccountStatement
                                        {
                                            debit = balanceDebit,
                                            credit = balanceCredit,
                                            balanceDebit = (balanceCredit - balanceDebit) < 0 ? (balanceCredit - balanceDebit) * -1 : 0,
                                            balanceCredit = (balanceCredit - balanceDebit) > 0 ? (balanceCredit - balanceDebit) : 0,
                                            currency = currency,
                                            date = "اجمالي الفتره",
                                            transactionType = "اجمالي الفتره",
                                            description = "اجمالي الفتره",
                                            account = item.name,
                                            numberColor = 1
                                        });
                                    tempData.Add(new
                              DataSetAccountStatement
                                    {
                                        debit = balanceDebit + previousDebit,
                                        credit = balanceCredit + previousCredit,
                                        balanceDebit = finalBalance < 0 ? finalBalance * -1 : 0,
                                        balanceCredit = finalBalance > 0 ? finalBalance : 0,
                                        currency = currency,
                                        date = "الإجمالي",
                                        transactionType = "الإجمالي",
                                        description = "الإجمالي",
                                        account = item.name,
                                        numberColor = 1
                                    });
                                }


                                tempData.Add(new
                              DataSetAccountStatement
                                {
                                    debit = finalBalance < 0 ? finalBalance * -1 : 0,
                                    credit = finalBalance > 0 ? finalBalance : 0,
                                    balanceDebit = finalBalance < 0 ? finalBalance * -1 : 0,
                                    balanceCredit = finalBalance > 0 ? finalBalance : 0,
                                    currency = dBContext.Currencies.Find(group.Key)?.name,
                                    date = "الرصيد",
                                    transactionType = "الرصيد",
                                    description = "الرصيد",
                                    account = item.name,
                                    numberColor = 2
                                });
                            }


                        }
                      
                        if (startDate!= null&&!total)
                        {
                          
                            dBContext.Currencies.Where(x => x.name.Contains(currencyName)).Include(x => x.JournalEntries).OrderBy(c => c.id).ToList().ForEach(c =>
                            {
                             
                                if (!tempData.Where(t => t.currency == c.name&& t.account==item.name).Any())
                                {
                                    var allEntries =
                               c.JournalEntries.Where(x => x.accountId == item.id && x.transactionDate.Value.Date < startDate.Value.Date);
                                    decimal previousDebit = allEntries?.Sum(x => x.debit ?? 0) ?? 0;
                                    decimal previousCredit = allEntries?.Sum(x => x.credit ?? 0) ?? 0;
                                    decimal previousBalance = previousCredit - previousDebit;
                                    tempData.Add(new DataSetAccountStatement
                                    {
                                        debit = previousDebit,
                                        credit = previousCredit,
                                        balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
                                        balanceCredit = previousBalance > 0 ? previousBalance : 0,
                                        currency = c.name,
                                        date = "_",
                                        transactionType = "",
                                        description = "رصيد سابق",
                                        account = item.name,
                                        numberColor = -1
                                    });


                                }

                            });

                        }
                    }
                    dataSource.DataSource = tempData;

                }
                }
                catch  
                {
                   
                   AppDialogAleart.showAleartError();
                }
           return tempData.Any();
        } 
        
        public void selectedAccount(object value)
        {   if(HasDataProcessed)
           account = (ChartOfAccount)value ?? null;
        }public void selectedCurrency(object value)
        {
            if (HasDataProcessed)
                currency = (Currency)value ?? null;
        }
        public void selectedGroup(object value)
        {
            if (HasDataProcessed)
                group = (AccountsGroup)value ?? null;
        }   
        public void selectedStartDate(DateTime? date)
        {
            if (HasDataProcessed)
                startDate = date;
        }
        public void selectedEndDate(DateTime? date)
        {
            if (HasDataProcessed)
                endDate = date;
        } 
        public void selectedTotal(bool value)
        {
            if (HasDataProcessed)
                total = value;
        }
        public void selectedNoOpeningBalance(bool value)
        {
            if (HasDataProcessed)
                noOpeningBalance = value;
        }

    }

}
