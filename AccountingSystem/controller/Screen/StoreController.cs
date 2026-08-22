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



namespace AccountingSystem.controller
{
    public class StoreController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم المخزن", "رقم الحساب ", "الحساب الأب" };
        public BindingSource dataSource;
        AccountingDbContext dBContext;
        public dynamic allData;
        public List<ChartOfAccount> mainAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "رئيسي").ToList(); } }
        public Store temp;
        //public FromStore olde;
        public ProsessesType prosessesType { get; set; }
        public StoreController()
        {
            dBContext = new AccountingDbContext();
            
            dataSource = new BindingSource();
            // olde=new FromStore();
            lodeData();
        }
       public void clearTempData()
        {
            temp = new Store();
            temp.Account = new ChartOfAccount();
            temp.Account.perantAccount = null;
        }
        private void lodeData()
        {
            clearTempData();
            try
            {
                allData =
                   dBContext.Stores.AsNoTracking().OrderByDescending(a => a.id).Include(c => c.Account).Include(c => c.Account.perantAccount).
                                     Select(c => new
                                     {
                                         id = c.id,
                                         name = c.name,
                                         accountNumber = c.Account.accountNumber,
                                         parentName = c.Account.perantAccount.name,
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
            foreach (var Store in allData)
            {
                dataTable.Rows.Add(Store.id, Store.name,
                Store.accountNumber, Store.parentName);
            }
            dataSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {

                temp = dBContext.Stores.Include(a => a.Account).Include(a => a.Account.perantAccount).FirstOrDefault(a => a.id == id);
                if (temp == null)
                    throw new Exception();
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search(string name, string accountNumber)
        {
            if (model. LoginData.permissions["store"].viewPermission.Value)
            {
                try
                {
                    allData = dBContext.Stores.AsNoTracking().Include(c => c.Account).Include(c => c.Account.perantAccount).
                        Where(c => DbFunctions.Like(c.name, "%" + name + "%")
                         && DbFunctions.Like(c.Account.accountNumber.ToString(), "%" + accountNumber + "%")).
                        Select(c => new
                        {
                            id = c.id,
                            name = c.name,
                            accountNumber = c.Account.accountNumber,
                            parentName = c.Account.perantAccount.name,
                        }).ToList();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
        public bool add(string name, string accountNumber,string address)
        {

            bool status = false;

            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(temp.Account?.perantAccount??null, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[2]))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
            try
            {
                var anyItem = dBContext.Stores.FirstOrDefault(a => a.name == name);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد مخزن سابق بهذا الأسم"); return status; }
                var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber);
                if (anyAccount != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == name);
                if (anyAccount != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الأسم"); return status; }
                ChartOfAccount newAccount = new ChartOfAccount() { accountNumber = newAccountNumber, name = name, parentId = temp.Account.perantAccount.id, rankk = temp.Account.perantAccount.rankk + 1, type = "فرعي", natureOfAccount = SharedData.balanceSheet, accountLocation = SharedData.accountLocations["stores"] };
                newAccount = dBContext.ChartOfAccounts.Add(newAccount);
                dBContext.SaveChanges();
                Store newItem = new Store() { name = name, address= address, accountId = newAccount.id };
                dBContext.Stores.Add(newItem);
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                lodeData();

            }
            catch 
            {
                AppDialogAleart.showAleartError();
                status = false;
            }

            return status;
        }

        public bool update(string name, string accountNumber,string address)
        {

            bool status = false;

            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;


            if (!ValidatingData.validatingData(temp.Account.perantAccount, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[2]))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {

                    var anyItem = dBContext.Cashiers.FirstOrDefault(i => i.name == name && i.id != temp.id);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد صندوق سابق بهذا الأسم"); return status; }
                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber && temp.accountId != a.id);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                    temp.Account.parentId = temp.Account.perantAccount.id;
                    temp.Account.rankk = temp.Account.perantAccount.rankk + 1;
                    temp.Account.accountNumber = newAccountNumber;
                    temp.name = name;
                    temp.address = address;
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();

                }
                catch
                {
                    AppDialogAleart.showAleartError();
                    status = false;
                    transaction.Rollback();
                }
            }

            return status;
        }


        public bool delete(List<int> keys)
        {
            bool status = false;
            string statusProcess = "";
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
                            int accountId = Convert.ToInt32(temp.accountId);
                            statusProcess = dBContext.ChartOfAccounts.deleteAccount(accountId);
                            if (statusProcess != "true")
                            { throw new Exception(); }
                            dBContext.Stores.Remove(temp);
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                        }
                        dBContext.SaveChanges();
                        transaction.Commit();

                        lodeData();
                    }
                    catch
                    {
                        transaction.Rollback();
                        if (statusProcess != "true" && !String.IsNullOrEmpty(statusProcess))
                            AppDialogAleart.showAleartError(statusProcess);
                        AppDialogAleart.showAleartError();
                        status = false;
                    }
                }
            }
            else { AppDialogAleart.showAleartError("لم تقم بتحديد اي بيانات للحذف"); }


            return status;
        }
    }
}
