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
    public class AccountStatementWithNumberHoursController
    {
        public BindingSource dataSource;
        public List<DataSetAccountStatementWithNumberHours> tempData;
        AccountingDbContext dBContext;
        public DataRowCollection dataRowCollection;
        public DataRowCollection dataRowCollectionTotal;
        public string mainCurrencyCode { get { return dBContext.Currencies.FirstOrDefault(x => x.currencyType == "رئيسية")?.code ?? ""; } }
       
        public List<ChartOfAccount> accounts
        {
            get
            {
                var temp = dBContext.ChartOfAccounts.AsNoTracking().Where(x => x.type == "فرعي").ToList();
                temp.InsertRange(0,new List<ChartOfAccount>() {
                    new ChartOfAccount() { name = "كافة الحسابات", id = 0 },
                    new ChartOfAccount() { name = "العملاء", id = -1 },
                     new ChartOfAccount() { name = "الصناديق", id = -2 },
                      new ChartOfAccount() { name = "المخازن", id = -3 },
                       new ChartOfAccount() { name = "الموردين", id = -4 },
                });
                return temp;
            }
        }
        public List<ChartOfAccount> accountsCustomers
        { get{return dBContext.Customers.Select(x=>x.Account).ToList();}}
        public List<ChartOfAccount> accountsCashirs
        {get{return dBContext.Cashiers.Select(x=>x.Account).ToList();}} 
        public List<ChartOfAccount> accountsStores
        { get{return dBContext.Stores.Select(x=>x.Account).ToList();}}
        public List<ChartOfAccount> accountsSuppliers
        { get{return dBContext.Suppliers.Select(x=>x.Account).ToList();}}
        public List<Currency> currencies
        {
            get
            {
                var temp = dBContext.Currencies.AsNoTracking().ToList();
                temp.Insert(0, new Currency() { name = "كافة العملات", id = 0 });
                return temp;
            }
        }
        public List<AccountsGroup> groups
        {
            get
            {
                var temp = dBContext.AccountsGroups.AsNoTracking().ToList();
                temp.Insert(0, new AccountsGroup() { name = "كافة المجموعات", id = 0 });
                return temp;
            }
        }
        public DateTime? startDate = null;
        public DateTime? endDate = null;
        public ChartOfAccount account;
        public Currency currency;
        public AccountsGroup group;
        public bool total = false;
        public bool noOpeningBalance = false;
        public bool isReporteView = false;
        public bool HasDataProcessed = false;
       public bool mainAccount=false;
        public AccountStatementWithNumberHoursController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            tempData = new List<DataSetAccountStatementWithNumberHours> { };
            dataSource.DataSource = typeof(DataSetAccountStatement);
        }
        public ChartOfAccount currentAccount;
        public Currency currentCurrency;
        public bool search()
        {
            tempData = new List<DataSetAccountStatementWithNumberHours> { };
            //string accountName = account != null && account.id != 0 ? account.name : "";
            //string currencyName = currency != null && currency.id != 0 ? currency?.name : "";
            //string groupName = group != null && group.id != 0 ? group.name : "";
            string transactionType = TransactionType.رصيد_إفتتاحي.ToString();
            //   account = new ChartOfAccount() { name = "كافة الحسابات", id = 0 };
            if (account != null)
            {
                try
                {

                    if (account != null)
                    {
                        List<ChartOfAccount> ofAccounts = new List<ChartOfAccount>();
                         mainAccount=false;
                    
                        switch (account.id)
                        {
                            
                          
                            case -1:
                                ofAccounts = accountsCustomers;
                                mainAccount = true;
                                break;
                            case -2:
                                ofAccounts = accountsCashirs;
                                mainAccount = true;
                                break;
                            case -3:
                                ofAccounts = accountsStores;
                                mainAccount = true;
                                break;
                            case -4:
                                ofAccounts = accountsSuppliers;
                                mainAccount = true;
                                break;
                            default:
                                if(account.id== 0)
                                mainAccount = true;
                                ofAccounts = accounts;
                                break;
                        }
                        var dataAccounts = ofAccounts.
                       Where(x => x.type == "فرعي" 
                       && (!mainAccount ? x.id == account.id : true)  
                       &&(group==null|| group.id ==0|| x.accountGroupId==group.id)
                          ).ToList();

                        foreach (var account in dataAccounts)
                        {
                            currentAccount = account;
                            if (account.JournalEntries.Any())
                            {
                                var journal = account.JournalEntries.Where(
                                x => (currency == null || currency.id == 0 || x.currencyId == currency.id)
                                      && (noOpeningBalance ? !x.transactionType.Contains(transactionType) : true)).ToList();
                               
                                foreach (var group in journal.GroupBy(x => x.currencyId).OrderBy(x => x.Key))
                                {
                                    currentCurrency = currencies.FirstOrDefault(c => c.id == group.Key);
                                    var journalPeriod = group.Where(j => j.transactionDate.Between(startDate, endDate));
                                    var journalEntriesPreviousPeriod = group.Where(j => j.transactionDate.Before(startDate));
                               
                                    DataSetAccountStatementWithNumberHours totalPreviousPeriod = getTotalDataJournalEntriesPreviousPeriod(journalEntriesPreviousPeriod);
                                    List<DataSetAccountStatementWithNumberHours> dataPeriod = getDataJournalEntriesPeriod(journalPeriod);
                                    DataSetAccountStatementWithNumberHours totalPeriod = getTotalDataJournalEntriesPeriod(dataPeriod);
                                    DataSetAccountStatementWithNumberHours totalData = getTotalDataJournalEntries(totalPreviousPeriod, totalPeriod);


                                    if (!total)
                                    {
                                        if (startDate != null)
                                            tempData.Add(totalPreviousPeriod);
                                        tempData.AddRange(dataPeriod);
                                       // if (startDate != null)
                                           
                                    }
                                    if (startDate != null||total)
                                        tempData.Add(totalPeriod);
                                    if (!total)
                                        tempData.Add(totalData);

                                    currentCurrency =null;
                                }

                                if (startDate != null && !total)
                                    tempData.AddRange(notFoundCurrenceis(journal));
                            }

                            currentAccount=null;
                        }
                        if (dataAccounts?.Count > 1)
                            fillFinalTotals();
                       
                        dataSource.DataSource = tempData;

                    }
                }
                catch
                {

                    AppDialogAleart.showAleartError();
                }
            }
            return tempData.Any();
        }

        private void fillFinalTotals()
        {
            var data = tempData?.Where(x => x.titel == "رصيد سابق" || x.titel == "اجمالي الفتره" || x.titel == "الإجمالي").GroupBy(x => x.currency);
           
            foreach (var groupCurrency in data)
            {
              
                foreach (var group in groupCurrency.GroupBy(x => x.titel))
                {
                    decimal balance = group.Balance() ;
                    string text=((group.Key == "اجمالي الفتره" && total) ? "الإجمالي" : (group.Key == "رصيد سابق") ? "إجمالي ال" + group.Key : group.Key)+ "#";
                    
                    //AppDialogAleart.showAleartNoPermissions("titel=" + text);
                    var entry = new DataSetAccountStatementWithNumberHours
                    {
                        debit = group.Sum(x => x.debit),
                        credit = group.Sum(x => x.credit),
                        quantity = group.Sum(x => x.quantity),
                        priceHour = group.AveragePriceHour(),
                        total= group.Total(),
                        balanceCredit =balance>0?balance:0,
                        balanceDebit = balance<0?-balance:0,
                        currency =groupCurrency.Key,
                        date = "#",
                        transactionType = "#",
                        description = text,
                        titel = group.Key,
                        account = text,
                        numberColor = group.Key == "رصيد سابق"?3: 2
                    };
                    tempData.Add(entry);
                }

            }
        }

        private List<DataSetAccountStatementWithNumberHours> notFoundCurrenceis(List<JournalEntry> journal)
        {
            List<DataSetAccountStatementWithNumberHours> data= new List<DataSetAccountStatementWithNumberHours>();
            var notFoundCurrenceis = currencies.Where(x => x.id!=0&&(currency == null || x.id == currency.id) && !journal.Where(j => j.currencyId == x.id).Any());
           
            foreach (var currency in notFoundCurrenceis)
            {
                var entry = new DataSetAccountStatementWithNumberHours
                {
                    debit = 0,
                    credit =  0,
                    quantity = 0,
                    priceHour = 0,
                    balanceCredit = 0,
                    balanceDebit =  0,
                    currency = currency.name,
                    date = "_",
                    transactionType = "",
                    description = "رصيد سابق",
                    titel = "رصيد سابق",
                    account = currentAccount.name,
                    numberColor = -1
                };
                data.Add(entry);
            }
                return data;
        }

        private DataSetAccountStatementWithNumberHours getTotalDataJournalEntries(DataSetAccountStatementWithNumberHours totalPreviousPeriod, DataSetAccountStatementWithNumberHours totalPeriod)
        {
            decimal balance = totalPeriod.Balance()+totalPreviousPeriod.Balance();
            decimal total = (totalPeriod?.total ?? 0) + (totalPreviousPeriod?.total ?? 0);
            decimal quantity = (totalPeriod?.quantity ?? 0) + (totalPreviousPeriod?.quantity ?? 0);
            return new
                           DataSetAccountStatementWithNumberHours
            {
                debit = (totalPeriod?.debit??0) + (totalPreviousPeriod?.debit??0),
                credit = (totalPeriod?.credit ?? 0) + (totalPreviousPeriod?.credit ?? 0),
                balanceCredit = balance > 0 ? balance : 0,
                balanceDebit = balance < 0 ? -balance : 0,
                //baseCredit = (totalPeriod?.baseCredit ?? 0) + (totalPreviousPeriod?.baseCredit ?? 0),
                //baseDebit = (totalPeriod?.baseDebit ?? 0) + (totalPreviousPeriod?.baseDebit ?? 0),
                quantity =quantity ,
                priceHour = quantity>0? total / quantity:0,
                total =total ,
                currency = currentCurrency?.name,
                date = "الإجمالي",
                transactionType = "الإجمالي",
                description = "الإجمالي",
                titel = "الإجمالي",
                account =currentAccount?.name,
                numberColor = 1
            };
    }  
 
        private DataSetAccountStatementWithNumberHours getTotalDataJournalEntriesPeriod(List<DataSetAccountStatementWithNumberHours> dataPeriod)
        {
            decimal balance = dataPeriod.Balance();
            return new
                      DataSetAccountStatementWithNumberHours
           {
               debit = dataPeriod?.Sum(x=>(x.debit))??0,
               credit = dataPeriod?.Sum(x => x.credit) ?? 0,
               quantity = dataPeriod?.Sum(x => x.quantity) ?? 0,
                balanceCredit = balance > 0 ? balance : 0,
                balanceDebit = balance < 0 ? -balance : 0,
                priceHour = dataPeriod.AveragePriceHour(),
                total= dataPeriod.Total(),
               currency = currentCurrency?.name,
               date = "اجمالي الفتره",
               transactionType = "اجمالي الفتره",
               description = "اجمالي الفتره",
               titel = "اجمالي الفتره",
               account = currentAccount?.name,
               numberColor = 1
           };
        }

        private List<DataSetAccountStatementWithNumberHours> getDataJournalEntriesPeriod(IEnumerable<JournalEntry> journalEntries)
        {
            List<DataSetAccountStatementWithNumberHours> data=new List<DataSetAccountStatementWithNumberHours>();
            var journalNotSaleAndPurchase = journalEntries.Where(x => x.IsNotSalesOrPurchases());
            var journalSaleAndPurchase = journalEntries.Where(x => x.IsSalesOrPurchases());
            data.AddRange(getDataJournalEntryNotSalesAndPurchases(journalNotSaleAndPurchase));
            data.AddRange(getDataJournalEntryPurchaseAndSale(journalSaleAndPurchase));
            return data;
        }

        private List<DataSetAccountStatementWithNumberHours> getDataJournalEntryNotSalesAndPurchases(IEnumerable<JournalEntry> journalNotSaleAndPurchase)
        {
            List<DataSetAccountStatementWithNumberHours> data = new List<DataSetAccountStatementWithNumberHours>();
            foreach (var journalEntry in journalNotSaleAndPurchase)
            {
                decimal balance = journalEntry.Balance();
                var entry = new DataSetAccountStatementWithNumberHours
                {
                    debit = journalEntry.debit ?? 0,
                    credit = journalEntry.credit ?? 0,
                    quantity = 0,
                    priceHour = 0,
                    balanceCredit =balance>0 ?balance:0,
                    balanceDebit = balance < 0 ? -balance : 0,
                    currency = journalEntry.Currency.name,
                    date = journalEntry.transactionDate.Format(),
                    transactionType = journalEntry.transactionType.Replace("_", " "),
                    description =journalEntry.description,
                    account = journalNotSaleAndPurchase.FirstOrDefault()?.Account.name,
                    numberColor = -1
                };
                data.Add(entry);
            }
            return data;
        }

        private DataSetAccountStatementWithNumberHours getTotalDataJournalEntriesPreviousPeriod(IEnumerable<JournalEntry> journalEntries)
        {
            var journalNotSaleAndPurchase = journalEntries?.Where(x => x.IsNotSalesOrPurchases());
            var journalSaleAndPurchase = journalEntries?.Where(x => x.IsSalesOrPurchases());
         
         
          
            var dataSalesAndPurchases = getDataJournalEntryPurchaseAndSale(journalSaleAndPurchase);
         var   data= dataSalesAndPurchases.Concat(getDataJournalEntryNotSalesAndPurchases(journalNotSaleAndPurchase));
            decimal balance = data.Balance();
            return new DataSetAccountStatementWithNumberHours
            {
                debit =  (data?.Sum(x => x.debit) ?? 0),
                credit = (data?.Sum(x => x.credit) ?? 0),
                //baseCredit =  (data?.Sum(x => x.credit) ?? 0),
                //baseDebit = (data?.Sum(x => x.IsSale()? x.baseDebit:x.debit) ?? 0),
                balanceCredit = balance > 0 ? balance : 0,
                balanceDebit = balance < 0 ? -balance : 0,
                quantity = data?.Sum(x => x.quantity) ?? 0,
                priceHour = data.AveragePriceHour(),
                total = data.Total(),
                currency = currentCurrency?.name,
                date = "_",
                transactionType = "",
                description = "رصيد سابق",
                titel= "رصيد سابق",
                account = currentAccount?.name,
                numberColor = -1
            }; 
        }

        List<DataSetAccountStatementWithNumberHours> getDataJournalEntryPurchaseAndSale(IEnumerable<JournalEntry> journalEntries)
        {
            List<DataSetAccountStatementWithNumberHours> data = new List<DataSetAccountStatementWithNumberHours>();
            var journalWithSale = journalEntries?.Where(x => x.IsSale()).GroupBy(j => j.transactionId);
            var journalWithPurchase = journalEntries?.Where(x => x.IsPurchases()).GroupBy(j => j.transactionId);
             data.AddRange(getDataJournalEntrySale(journalWithSale, "فواتير المبيعات" + (journalWithPurchase.Any() ? " والمشتريات" : "")));
            data.AddRange(getDataJournalEntryPurchase(journalWithPurchase, journalWithSale.Any()?"" : "فواتير المشتريات"));
            return data;
        }
        private List<DataSetAccountStatementWithNumberHours> getDataJournalEntrySale(IEnumerable<IGrouping<int?, JournalEntry>> journalWithSale,string titel)
        {
            List<DataSetAccountStatementWithNumberHours> data=new List<DataSetAccountStatementWithNumberHours>();
           // bool addedTitel=false;
            foreach (var journalEntry in journalWithSale)
            {
                var finalInvoiceData = AppDBFunctions.FinalInvoiceSaleData(journalEntry.Key ?? 0);
                decimal debit = 0;
                decimal credit = 0;
             //   decimal baseDebit = 0;
                if (accountsCustomers.Where(x => x.id == journalEntry.FirstOrDefault()?.accountId).Any())
                {
                    credit = finalInvoiceData.amountPaid;
                    //debit = finalInvoiceData.RemainingAmount();
                    debit = finalInvoiceData.total;
                  //  baseDebit = finalInvoiceData.total;
                }
                else if (accountsCashirs.Where(x => x.id == journalEntry.FirstOrDefault()?.accountId).Any())
                    debit = finalInvoiceData.amountPaid;
                else if (accountsStores.Where(x => x.id == journalEntry.FirstOrDefault()?.accountId).Any())
                    credit = finalInvoiceData.total;
                decimal balance = credit-debit;
                var entry = new DataSetAccountStatementWithNumberHours
                {

                 //   titel = addedTitel?" ": titel,
                    debit = debit,
                    credit = credit,
                    balanceCredit = balance > 0 ? balance : 0,
                    balanceDebit = balance < 0 ? -balance : 0,
                    //  baseDebit = baseDebit,
                    quantity = finalInvoiceData.quantityHours,
                    priceHour = finalInvoiceData.AveragePriceHour(),
                    currency = journalEntry.FirstOrDefault()?.Currency.name,
                    date = journalEntry.FirstOrDefault()?.transactionDate.Format(),
                    transactionType = journalEntry.FirstOrDefault()?.transactionType.Replace("_", " "),
                    description = journalEntry.FirstOrDefault()?.description,
                    account = journalEntry.FirstOrDefault().Account.name,
                    numberColor = -1
                };
                data.Add(entry);
               // addedTitel = true;
            }
            return data;
        }
        private List<DataSetAccountStatementWithNumberHours> getDataJournalEntryPurchase(IEnumerable<IGrouping<int?, JournalEntry>> journalWithPurchase, string titel)
        {
            List<DataSetAccountStatementWithNumberHours> data = new List<DataSetAccountStatementWithNumberHours>();
          //  bool addedTitel = false;
            foreach (var journalEntry in journalWithPurchase)
            {
                var finalInvoiceData = AppDBFunctions.FinalInvoicePurchaseData(journalEntry.Key ?? 0);
                decimal debit = 0;
                decimal credit = 0;
             //   decimal baseCredit = 0;
                if (accountsSuppliers.Where(x => x.id == journalEntry.FirstOrDefault()?.accountId).Any())
                {
                    debit = finalInvoiceData.amountPaid;
                    //credit = finalInvoiceData.RemainingAmount();
                    credit = finalInvoiceData.total;
                   // baseCredit = finalInvoiceData.total;
                }
                else if (accountsCashirs.Where(x => x.id == journalEntry.FirstOrDefault()?.accountId).Any())
                    credit = finalInvoiceData.amountPaid;
                else if (accountsStores.Where(x => x.id == journalEntry.FirstOrDefault()?.accountId).Any())
                    debit = finalInvoiceData.total;
                decimal balance = credit-debit;
                var entry = new DataSetAccountStatementWithNumberHours
                {   //titel= addedTitel ? " " : titel,
                    debit = debit,
                    credit = credit,
                    balanceCredit = balance > 0 ? balance : 0,
                    balanceDebit = balance < 0 ? -balance : 0,
                    quantity = finalInvoiceData.quantityHours,
                    priceHour = finalInvoiceData.AveragePriceHour(),
                  //  baseCredit = baseCredit,
                    currency = journalEntry.FirstOrDefault()?.Currency.name,
                    date = journalEntry.FirstOrDefault()?.transactionDate.Format(),
                    transactionType = journalEntry.FirstOrDefault()?.transactionType.Replace("_", " "),
                    description =journalEntry.FirstOrDefault()?.description,
                    account = journalEntry.FirstOrDefault().Account.name,
                    numberColor = -1
                };
                data.Add(entry);
            //    addedTitel = true;
            }
            return data;
        }
        //public bool search()
        //{
        //    tempData = new List<DataSetAccountStatementWithNumberHours> { };
        //    string accountName = account != null && account.id != 0 ? account.name : "";
        //    string currencyName = currency != null && currency.id != 0 ? currency?.name : "";
        //    string groupName = group != null && group.id != 0 ? group.name : "";
        //    string transactionType = TransactionType.رصيد_إفتتاحي.ToString();
        //    account = new ChartOfAccount() { name = "كافة المجموعات", id = 0 };

        //    try
        //    {

        //        if (account != null)
        //        {
        //            var alii = dBContext.ChartOfAccounts.AsNoTracking().
        //           Where(x => x.type == "فرعي" &&
        //           (account.id != 0 ? x.id == account.id : true) &&
        //           DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%")
        //       ).Include(x => x.JournalEntries).ToList();

        //            foreach (var item in alii)
        //            {

        //                if (item.JournalEntries.Any())
        //                {
        //                    var allJournalEntries = item.JournalEntries.Where(
        //                    x =>
        //                  (group == null || group.id == 0 || x.Account.accountGroupId == group.id) &&
        //                  x.Currency.name.Contains(currencyName)
        //               ).ToList();

        //                    foreach (var journalEntries in allJournalEntries.GroupBy(x => x.transactionType).OrderBy(x => x.Key))
        //                    {
        //                        //foreach (var group in journalEntries.GroupBy(x => x.currencyId).OrderBy(x => x.Key))
        //                        //{
        //                            string currency = journalEntries.FirstOrDefault()?.Currency.name;
        //                            decimal previousDebit = 0;
        //                            decimal previousCredit = 0;
        //                            decimal previousBalance = 0;

        //                            decimal balanceDebit = 0;
        //                            decimal balanceCredit = 0;
        //                            decimal finalBalance = 0;
        //                            //startDate = DateTime.Now;
        //                            //endDate = DateTime.Now;
        //                            QuantityAndPriceHours quantityAndPrice = new QuantityAndPriceHours() { price = 0, quantity = 0 };

        //                            var entriesBetweenDates = journalEntries.Where(entery => (startDate == null || entery.transactionDate.Value.Date >= startDate.Value.Date) && (endDate == null || entery.transactionDate.Value.Date <= endDate.Value.Date));
        //                            if (!total)
        //                            {
        //                                if (startDate != null)
        //                                {
        //                                var previousEntries = journalEntries.ToList().Where(entery => entery.transactionDate.Value.Date < startDate.Value.Date).ToList();
        //                                previousCredit = previousEntries?.Sum(entery => entery.credit ?? 0) ?? 0;
        //                                    previousDebit = previousEntries?.Sum(entery => entery.debit ?? 0) ?? 0;
        //                                    previousBalance = previousCredit - previousDebit;
        //                                    if (journalEntries.Key == TransactionType.فاتورة_مبيعات.ToString())
        //                                    {
        //                                        foreach (var previousEntry in previousEntries)
        //                                        {
        //                                            var QP = AppDBFunctions.QuantityAndPriceHoursWithNotCompositeItem(previousEntry.transactionId ?? 0);
        //                                            quantityAndPrice.price += QP.price;
        //                                            quantityAndPrice.quantity += QP.quantity;
        //                                        }

        //                                    }
        //                                    tempData.Add(new DataSetAccountStatementWithNumberHours()
        //                                    {
        //                                        quantity = quantityAndPrice.quantity,
        //                                        priceHour = quantityAndPrice.Average(),
        //                                        debit = previousDebit,
        //                                        credit = previousCredit,
        //                                        balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
        //                                        balanceCredit = previousBalance > 0 ? previousBalance : 0,
        //                                        currency = currency,
        //                                        date = "_",
        //                                        transactionType = journalEntries.Key,
        //                                        description = "رصيد سابق",
        //                                        account = item.name,
        //                                        numberColor = -1
        //                                    });

        //                                }

        //                                foreach (var journal in entriesBetweenDates.GroupBy(x => x.transactionId))
        //                                {
        //                                    quantityAndPrice = new QuantityAndPriceHours() { price = 0, quantity = 0 };
        //                                    if (journalEntries.Key == TransactionType.فاتورة_مبيعات.ToString())
        //                                    {

        //                                        quantityAndPrice = AppDBFunctions.QuantityAndPriceHoursWithNotCompositeItem(journal.Key ?? 0);
        //                                    }
        //                                    balanceCredit += journal?.Sum(entery => entery.credit ?? 0) ?? 0;
        //                                    balanceDebit += journal?.Sum(entery => entery.debit ?? 0) ?? 0;
        //                                    finalBalance = balanceCredit - balanceDebit;
        //                                    tempData.Add(new DataSetAccountStatementWithNumberHours()
        //                                    {
        //                                        quantity = quantityAndPrice.quantity,
        //                                        priceHour = quantityAndPrice.Average(),
        //                                        debit = journal?.Sum(entery => entery.debit ?? 0) ?? 0,
        //                                        credit = journal?.Sum(entery => entery.credit ?? 0) ?? 0,
        //                                        balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
        //                                        balanceCredit = previousBalance > 0 ? previousBalance : 0,
        //                                        currency = currency,
        //                                        date = journal.FirstOrDefault()?.transactionDate.Format(),
        //                                        transactionType = journalEntries.Key,
        //                                        description = journal.FirstOrDefault()?.description,
        //                                        account = item.name,
        //                                        numberColor = -1
        //                                    });

        //                                }
        //                            }
        //                            else
        //                            {
        //                                balanceCredit = entriesBetweenDates.Sum(x => x.credit ?? 0);
        //                                balanceDebit = entriesBetweenDates.Sum(x => x?.debit ?? 0);
        //                                finalBalance = balanceCredit - balanceDebit;
        //                            }


        //                        }


        //                    }

        //                    if (startDate != null && !total)
        //                    {

        //                        dBContext.Currencies.Where(x => x.name.Contains(currencyName)).Include(x => x.JournalEntries).OrderBy(c => c.id).ToList().ForEach(c =>
        //                        {

        //                            if (!tempData.Where(t => t.currency == c.name && t.account == item.name).Any())
        //                            {
        //                                var allEntries =
        //                           c.JournalEntries.Where(x => x.accountId == item.id && x.transactionDate.Value.Date < startDate.Value.Date);
        //                                decimal previousDebit = allEntries?.Sum(x => x.debit ?? 0) ?? 0;
        //                                decimal previousCredit = allEntries?.Sum(x => x.credit ?? 0) ?? 0;
        //                                decimal previousBalance = previousCredit - previousDebit;
        //                                tempData.Add(new DataSetAccountStatementWithNumberHours
        //                                {
        //                                    debit = previousDebit,
        //                                    credit = previousCredit,
        //                                    balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
        //                                    balanceCredit = previousBalance > 0 ? previousBalance : 0,
        //                                    currency = c.name,
        //                                    date = "_",
        //                                    transactionType = "",
        //                                    description = "رصيد سابق",
        //                                    account = item.name,
        //                                    numberColor = -1
        //                                });


        //                            }

        //                        });
        //                    }

        //                }
        //            }
        //            dataSource.DataSource = tempData;


        //    }
        //    catch (DbEntityValidationException ex)
        //    {

        //        AppDialogAleart.showEntityValidationErrors(ex);
        //    }
        //    return tempData.Any();
        //}

        public void selectedAccount(object value)
        {
            if (HasDataProcessed)
                account = (ChartOfAccount)value ?? null;
        }
        public void selectedCurrency(object value)
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


//    public class AccountStatementControllerWithNumberHours
//    {
//        public BindingSource dataSource;
//        public List<DataSetAccountStatementWithNumberHours> tempData;
//        AccountingDbContext dBContext;
//        public DataRowCollection dataRowCollection;
//        public DataRowCollection dataRowCollectionTotal;
//        public string mainCurrencyCode { get { return dBContext.Currencies.FirstOrDefault(x => x.currencyType == "رئيسية")?.code ?? ""; } }
//        public List<ChartOfAccount> accounts
//        {
//            get
//            {
//                var temp = dBContext.ChartOfAccounts.AsNoTracking().Where(x => x.type == "فرعي").ToList();
//                temp.Insert(0, new ChartOfAccount() { name = "كافة الحسابات", id = 0 });
//                return temp;
//            }
//        }
//        public List<Currency> currencies
//        {
//            get
//            {
//                var temp = dBContext.Currencies.AsNoTracking().ToList();
//                temp.Insert(0, new Currency() { name = "كافة العملات", id = 0 });
//                return temp;
//            }
//        }
//        public List<AccountsGroup> groups
//        {
//            get
//            {
//                var temp = dBContext.AccountsGroups.AsNoTracking().ToList();
//                temp.Insert(0, new AccountsGroup() { name = "كافة المجموعات", id = 0 });
//                return temp;
//            }
//        }
//        public DateTime? startDate = null;
//        public DateTime? endDate = null;
//        public ChartOfAccount account;
//        public Currency currency;
//        public AccountsGroup group;
//        public bool total = false;
//        public bool noOpeningBalance = false;
//        public bool isReporteView = false;
//        public bool HasDataProcessed = false;
//        public AccountStatementControllerWithNumberHours()
//        {
//            dBContext = new AccountingDbContext();
//            dataSource = new BindingSource();
//            tempData = new List<DataSetAccountStatementWithNumberHours> { };
//            dataSource.DataSource = typeof(DataSetAccountStatement);
//        }

//        public bool search()
//        {
//            tempData = new List<DataSetAccountStatementWithNumberHours> { };
//            string accountName = account != null && account.id != 0 ? account.name : "";
//            string currencyName = currency != null && currency.id != 0 ? currency?.name : "";
//            string groupName = group != null && group.id != 0 ? group.name : "";
//            string transactionType = TransactionType.رصيد_إفتتاحي.ToString();
//            account = new ChartOfAccount() { name = "كافة المجموعات", id = 0 };

//            try
//            {

//                if (account != null)
//                {
//                    var alii = dBContext.ChartOfAccounts.AsNoTracking().
//                   Where(x => x.type == "فرعي" &&
//                   (account.id != 0 ? x.id == account.id : true) &&
//                   DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%")
//               ).Include(x => x.JournalEntries).ToList();

//                    foreach (var item in alii)
//                    {

//                        if (item.JournalEntries.Any())
//                        {
//                            var allJournalEntries = item.JournalEntries.Where(
//                            x =>
//                          (group == null || group.id == 0 || x.Account.accountGroupId == group.id) &&
//                          x.Currency.name.Contains(currencyName)
//                       ).ToList();

//                            foreach (var journalEntries in allJournalEntries.GroupBy(x => x.transactionType).OrderBy(x => x.Key))
//                            {
//                                foreach (var group in journalEntries.GroupBy(x => x.currencyId).OrderBy(x => x.Key))
//                                {
//                                    string currency = dBContext.Currencies.Find(group.Key)?.name;
//                                    decimal previousDebit = 0;
//                                    decimal previousCredit = 0;
//                                    decimal previousBalance = 0;

//                                    decimal balanceDebit = 0;
//                                    decimal balanceCredit = 0;
//                                    decimal finalBalance = 0;
//                                    //startDate=DateTime.Now;
//                                    //endDate=DateTime.Now;
//                                    QuantityAndPriceHours quantityAndPrice = new QuantityAndPriceHours() { price = 0, quantity = 0 };
//                                    var previousEntries = group.ToList().Where(entery => entery.transactionDate.Value.Date < startDate.Value.Date).ToList();
//                                    var entriesBetweenDates = group.Where(entery => (startDate == null || entery.transactionDate.Value.Date >= startDate.Value.Date) && (endDate == null || entery.transactionDate.Value.Date <= endDate.Value.Date));
//                                    if (!total)
//                                    {
//                                        if (startDate != null)
//                                        {

//                                            previousCredit = previousEntries?.Sum(entery => entery.credit ?? 0) ?? 0;
//                                            previousDebit = previousEntries?.Sum(entery => entery.debit ?? 0) ?? 0;
//                                            previousBalance = previousCredit - previousDebit;
//                                            if (journalEntries.Key == TransactionType.فاتورة_مبيعات.ToString()) 
//                                            {
//                                                foreach (var previousEntry in previousEntries)
//                                                {
//                                                   var QP= AppDBFunctions.QuantityAndPriceHoursWithNotCompositeItem(previousEntry.transactionId ?? 0);
//                                                    quantityAndPrice.price += QP.price;
//                                                    quantityAndPrice.quantity += QP.quantity;
//                                                }

//                                            }
//                                            tempData.Add(new DataSetAccountStatementWithNumberHours()
//                                            {
//                                                quantity=quantityAndPrice.quantity,
//                                                priceHour=quantityAndPrice.Average(),
//                                                debit = previousDebit,
//                                                credit = previousCredit,
//                                                balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
//                                                balanceCredit = previousBalance > 0 ? previousBalance : 0,
//                                                currency = currency,
//                                                date = "_",
//                                                transactionType = journalEntries.Key,
//                                                description = "رصيد سابق",
//                                                account = item.name,
//                                                numberColor = -1
//                                            });

//                                        }

//                                        foreach (var journal in entriesBetweenDates.GroupBy(x => x.transactionId))
//                                        {
//                                            quantityAndPrice = new QuantityAndPriceHours() { price = 0, quantity = 0 };
//                                            if (journalEntries.Key == TransactionType.فاتورة_مبيعات.ToString())
//                                            {

//                                                quantityAndPrice = AppDBFunctions.QuantityAndPriceHoursWithNotCompositeItem(journal.Key ?? 0);
//                                            }
//                                            balanceCredit += journal?.Sum(entery => entery.credit ?? 0) ?? 0;
//                                            balanceDebit += journal?.Sum(entery => entery.debit ?? 0) ?? 0;
//                                            finalBalance = balanceCredit - balanceDebit;
//                                            tempData.Add( new DataSetAccountStatementWithNumberHours()
//                                            {
//                                                quantity = quantityAndPrice.quantity,
//                                                priceHour = quantityAndPrice.Average(),
//                                                debit = journal?.Sum(entery => entery.debit ?? 0) ??0 ,
//                                                credit = journal?.Sum(entery => entery.credit ?? 0) ?? 0,
//                                                balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
//                                                balanceCredit = previousBalance > 0 ? previousBalance : 0,
//                                                currency = currency,
//                                                date = journal.FirstOrDefault()?.transactionDate.Format(),
//                                                transactionType = journalEntries.Key,
//                                                description = journal.FirstOrDefault()?.description,
//                                                account = item.name,
//                                                numberColor = -1
//                                            });

//                                        }
//                                    }
//                                    else
//                                    {
//                                        balanceCredit = entriesBetweenDates.Sum(x => x.credit ?? 0);
//                                        balanceDebit = entriesBetweenDates.Sum(x => x?.debit ?? 0);
//                                        finalBalance = balanceCredit - balanceDebit;
//                                    }

//                                    if (!total)
//                                    {
//                                        if (startDate != null && !total)
//                                            tempData.Add(new
//                                  DataSetAccountStatementWithNumberHours
//                                            {
//                                                debit = balanceDebit,
//                                                credit = balanceCredit,
//                                                balanceDebit = (balanceCredit - balanceDebit) < 0 ? (balanceCredit - balanceDebit) * -1 : 0,
//                                                balanceCredit = (balanceCredit - balanceDebit) > 0 ? (balanceCredit - balanceDebit) : 0,
//                                                currency = currency,
//                                                date = "اجمالي الفتره",
//                                                transactionType = "اجمالي الفتره",
//                                                description = "اجمالي الفتره",
//                                                account = item.name,
//                                                numberColor = 1
//                                            });
//                                        tempData.Add(new
//                                  DataSetAccountStatementWithNumberHours
//                                        {
//                                            debit = balanceDebit + previousDebit,
//                                            credit = balanceCredit + previousCredit,
//                                            balanceDebit = finalBalance < 0 ? finalBalance * -1 : 0,
//                                            balanceCredit = finalBalance > 0 ? finalBalance : 0,
//                                            currency = currency,
//                                            date = "الإجمالي",
//                                            transactionType = "الإجمالي",
//                                            description = "الإجمالي",
//                                            account = item.name,
//                                            numberColor = 1
//                                        });
//                                    }


//                                    tempData.Add(new
//                                  DataSetAccountStatementWithNumberHours
//                                    {
//                                        debit = finalBalance < 0 ? finalBalance * -1 : 0,
//                                        credit = finalBalance > 0 ? finalBalance : 0,
//                                        balanceDebit = finalBalance < 0 ? finalBalance * -1 : 0,
//                                        balanceCredit = finalBalance > 0 ? finalBalance : 0,
//                                        currency = dBContext.Currencies.Find(group.Key)?.name,
//                                        date = "الرصيد",
//                                        transactionType = "الرصيد",
//                                        description = "الرصيد",
//                                        account = item.name,
//                                        numberColor = 2
//                                    });
//                                }


//                            }

//                            if (startDate != null && !total)
//                            {

//                                dBContext.Currencies.Where(x => x.name.Contains(currencyName)).Include(x => x.JournalEntries).OrderBy(c => c.id).ToList().ForEach(c =>
//                                {

//                                    if (!tempData.Where(t => t.currency == c.name && t.account == item.name).Any())
//                                    {
//                                        var allEntries =
//                                   c.JournalEntries.Where(x => x.accountId == item.id && x.transactionDate.Value.Date < startDate.Value.Date);
//                                        decimal previousDebit = allEntries?.Sum(x => x.debit ?? 0) ?? 0;
//                                        decimal previousCredit = allEntries?.Sum(x => x.credit ?? 0) ?? 0;
//                                        decimal previousBalance = previousCredit - previousDebit;
//                                        tempData.Add(new DataSetAccountStatementWithNumberHours
//                                        {
//                                            debit = previousDebit,
//                                            credit = previousCredit,
//                                            balanceDebit = previousBalance < 0 ? previousBalance * -1 : 0,
//                                            balanceCredit = previousBalance > 0 ? previousBalance : 0,
//                                            currency = c.name,
//                                            date = "_",
//                                            transactionType = "",
//                                            description = "رصيد سابق",
//                                            account = item.name,
//                                            numberColor = -1
//                                        });


//                                    }

//                                });
//                            }

//                        }
//                    }
//                    dataSource.DataSource = tempData;

//                }
//            }
//            catch(DbEntityValidationException ex)
//            {

//                AppDialogAleart.showEntityValidationErrors(ex);
//            }
//            return tempData.Any();
//        }

//        public void selectedAccount(object value)
//        {
//            if (HasDataProcessed)
//                account = (ChartOfAccount)value ?? null;
//        }
//        public void selectedCurrency(object value)
//        {
//            if (HasDataProcessed)
//                currency = (Currency)value ?? null;
//        }
//        public void selectedGroup(object value)
//        {
//            if (HasDataProcessed)
//                group = (AccountsGroup)value ?? null;
//        }
//        public void selectedStartDate(DateTime? date)
//        {
//            if (HasDataProcessed)
//                startDate = date;
//        }
//        public void selectedEndDate(DateTime? date)
//        {
//            if (HasDataProcessed)
//                endDate = date;
//        }
//        public void selectedTotal(bool value)
//        {
//            if (HasDataProcessed)
//                total = value;
//        }
//        public void selectedNoOpeningBalance(bool value)
//        {
//            if (HasDataProcessed)
//                noOpeningBalance = value;
//        }

//    }

//}