using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
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
    public class GeneralProfessorController
    {
        public BindingSource dataSource;
        public List<DataSetGeneralProfessor> tempData;
        AccountLocations accountLocations;
        // public Dictionary<int,DataSetGeneralProfessor> tempTotalData;
        AccountingDbContext dBContext;
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
      public  DateTime? startDate = null;
        DateTime? endDate = null;
        public Currency currency;
        public AccountsGroup group;

        public GeneralProfessorController(AccountLocations accountLocations)
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            tempData = new List<DataSetGeneralProfessor> { };
            dataSource.DataSource = typeof(DataSetGeneralProfessor);
            this.accountLocations = accountLocations;
        }

        public bool search()
        {
            tempData = new List<DataSetGeneralProfessor> { };
            string currencyName = currency != null && currency.id != 0 ? currency?.name : "";
            string groupName = group != null && group.id != 0 ? group.name : "";
           
          
            List<Currency> currencyListWithEntries = dBContext.Currencies.AsNoTracking().Where(c =>
                        DbFunctions.Like(c.name, "%" + currencyName + "%")).OrderBy(c => c.id).Include(c=>c.JournalEntries).ToList();
            try
            {
                IQueryable<ChartOfAccount> allAccounts = null;
                switch (accountLocations)
                {
                    case AccountLocations.الكل:
                        allAccounts = dBContext.ChartOfAccounts.AsNoTracking().Where(x =>x.type=="فرعي"&&
                        DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%"));
                        break;
                    case AccountLocations.الصناديق:
                         allAccounts = dBContext.Cashiers.Include(x=>x.Account).Select(x=>x.Account).
                            Where(x => DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%"));
                        break; 
                    case AccountLocations.الموردين:
                         allAccounts = dBContext.Suppliers.Include(x=>x.Account).Select(x=>x.Account).
                            Where(x => DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%"));
                        break; 
                    case AccountLocations.المخازن:
                         allAccounts = dBContext.Stores.Include(x=>x.Account).Select(x=>x.Account).
                            Where(x => DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%"));
                        break; 
                    case AccountLocations.الموظفين:
                         allAccounts = dBContext.Employees.Include(x=>x.Account).Include(x=>x.Account.JournalEntries).Select(x=>x.Account).
                            Where(x => DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%"));
                        break;   
                    case AccountLocations.العملاء:
                         allAccounts = dBContext.Customers.Include(x=>x.Account).Include(x=>x.Account.JournalEntries).Select(x=>x.Account).
                            Where(x => DbFunctions.Like(x.AccountsGroup != null ? x.AccountsGroup.name : "", "%" + groupName + "%"));
                        break;

                }
                if(allAccounts!=null|| allAccounts.Any())
                foreach (var account in  allAccounts.OrderBy(a=>a.id).ToList())
                {
                    foreach (var currency in currencyListWithEntries)
                    {
                        var accountEntries = currency.JournalEntries?.Where(entery => entery.accountId == account.id)?.ToList();
                        var noOpeningBalanceEntries = accountEntries?.Where(entery => entery.transactionType != TransactionType.رصيد_إفتتاحي.ToString());
                        var openingBalanceEntries = accountEntries?.Where(x => x.transactionType == TransactionType.رصيد_إفتتاحي.ToString());
                        var periodMovementEntries = noOpeningBalanceEntries?.Where(entery => (startDate == null || entery.transactionDate.Value.Date >= startDate.Value.Date) && (endDate == null || entery.transactionDate.Value.Date <= endDate.Value.Date));
                        decimal periodMovementCredit = periodMovementEntries?.Sum(x => x.credit ?? 0) ?? 0;
                        decimal periodMovementDebit = periodMovementEntries?.Sum(x => x.debit ?? 0) ?? 0;
                        decimal previousBalance = 0;
                        if (startDate != null)
                        {
                            var beforeStartDateEntries = noOpeningBalanceEntries.Where(entery => entery.transactionDate.Value.Date < startDate.Value.Date);
                            previousBalance = (beforeStartDateEntries?.Sum(x => x.credit ?? 0) ?? 0) - beforeStartDateEntries?.Sum(x => x.debit ?? 0) ?? 0;

                        }

                        decimal openingBalanceCredit = openingBalanceEntries?.Sum(x => x.credit ?? 0) ?? 0;
                        decimal openingBalanceDebit = openingBalanceEntries?.Sum(x => x.debit ?? 0) ?? 0;
                        decimal finalBalance = (periodMovementCredit + openingBalanceCredit) - (periodMovementDebit + openingBalanceDebit) + previousBalance;
                        tempData.Add(new
                    DataSetGeneralProfessor
                        {
                            openingBalanceCredit = openingBalanceCredit,
                            openingBalanceDebit = openingBalanceDebit,
                            periodMovementDebit = periodMovementDebit,
                            periodMovementCredit = periodMovementCredit,
                            finalBalance = finalBalance,
                            previousBalance = previousBalance,
                            currency = currency.name,
                            account = account.name,
                            numberColor = 1
                        });

                    }
                    
                }
                foreach (var currency in tempData.GroupBy(x => x.currency))
                    tempData.Add(new
                                DataSetGeneralProfessor
                    {
                        openingBalanceCredit = currency.Sum(x => x.openingBalanceCredit),
                        openingBalanceDebit = currency.Sum(x => x.openingBalanceDebit),
                        periodMovementDebit = currency.Sum(x => x.periodMovementDebit),
                        periodMovementCredit = currency.Sum(x => x.periodMovementCredit),
                        finalBalance = currency.Sum(x => x.finalBalance),
                        previousBalance = currency.Sum(x => x.previousBalance),
                        currency = currency.Key,
                        account = "#الإجمالي",
                        numberColor = 1
                    });
                //        if (item.JournalEntries.Any())
                //        {
                //            var j = item.JournalEntries.Where(
                //            x =>
                //(startDate == null || x.transactionDate.Value.Date >= startDate.Value.Date) &&
                // (endDate == null || x.transactionDate.Value.Date <= endDate.Value.Date) &&
                //          (group == null || group.id == 0 || x.Account.accountGroupId == group.id) &&
                //          x.Currency.name.Contains(currencyName)
                //      ).ToList();
                //            foreach (var group in j.GroupBy(x => x.currencyId).OrderBy(x => x.Key))
                //            {
                //                decimal totalCredit = group.Where(x => x.transactionType != TransactionType.رصيد_إفتتاحي.ToString()).Sum(x => x.credit ?? 0);
                //                decimal totalDebit = group.Where(x => x.transactionType != TransactionType.رصيد_إفتتاحي.ToString()).Sum(x => x.debit ?? 0);
                //                decimal openingBalanceCredit = group.Where(x => x.transactionType == TransactionType.رصيد_إفتتاحي.ToString()).Sum(x => x.credit ?? 0);
                //                decimal openingBalanceDebit = group.Where(x => x.transactionType == TransactionType.رصيد_إفتتاحي.ToString()).Sum(x => x.debit ?? 0);
                //                decimal finalBalance = group.Sum(x => x.credit ?? 0) - group.Sum(x => x?.debit ?? 0);
                //tempData.Add(new
                //DataSetGeneralProfessor
                //{
                //    openingBalanceCredit = openingBalanceCredit,
                //    openingBalanceDebit = openingBalanceDebit,
                //    totalDebit = totalDebit,
                //    totalCredit = totalCredit,
                //    balanceDebit = finalBalance < 0 ? finalBalance * -1 : 0,
                //    balanceCredit = finalBalance > 0 ? finalBalance : 0,
                //    currency = dBContext.Currencies.Find(group.Key)?.name,
                //    account = item.name,
                //    numberColor = 1
                //});
                //            }


                //        }

                //    }
                //foreach (var currency in tempData.GroupBy(x => x.currency))
                //{
                //    var fainlBalance = currency.Sum(x => x.balanceCredit) - currency.Sum(x => x.balanceDebit);
                //    tempData.Add(new
                //                DataSetGeneralProfessor
                //    {
                //        openingBalanceCredit = currency.Sum(x => x.openingBalanceCredit),
                //        openingBalanceDebit = currency.Sum(x => x.openingBalanceDebit),
                //        totalDebit = currency.Sum(x => x.totalDebit),
                //        totalCredit = currency.Sum(x => x.totalCredit),
                //        balanceDebit = fainlBalance < 0 ? fainlBalance * -1 : 0,
                //        balanceCredit = fainlBalance > 0 ? fainlBalance : 0,
                //        currency = currency.Key,
                //        account = "#الإجمالي",
                //        numberColor = 1
                //    });

                //    };
                //}
                //}


                dataSource.DataSource = tempData;
            }
            catch
            {
                AppDialogAleart.showAleartError();
            }
            return tempData.Any();
        }

     
        public void selectedCurrency(object value)
        {
            currency = (Currency)value ?? null;
        }
        public void selectedGroup(object value)
        {
            group = (AccountsGroup)value ?? null;
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

