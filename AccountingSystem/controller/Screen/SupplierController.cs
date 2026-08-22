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
using AccountingSystem.model;

namespace AccountingSystem.controller
{
    public class SupplierController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "الأسم","رقم الحساب", "رقم الهاتف ", "العنوان ", "الحساب الأب" };
        public BindingSource dataSource;
        public dynamic allData;
        AccountingDbContext dBContext;
        public List<ChartOfAccount> mainAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "رئيسي").ToList(); } }

        public Supplier temp;

        public ProsessesType prosessesType { get; set; }
        public SupplierController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            temp = new Supplier();
            allData = new List<Supplier>();
            lodeData();
        }
        public void clearTempData()
        {
            temp = new Supplier();
            temp.Account = new ChartOfAccount();
            temp.Account.AccountsGroup = null;
            temp.Account.perantAccount = null;
        }
        public void lodeData()
        {

            clearTempData();

            try
            {
                allData = dBContext.Suppliers.Include(c => c.Account).Include(c => c.Account.perantAccount).Select(i => new {
                    id = i.id,
                    name = i.name,
                    accountNumber= i.Account.accountNumber,
                    phoneNumber = i.phoneNumber,
                    address = i.address,
                    perantAccountName = i.Account.perantAccount.name,
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
            foreach (var Supplier in allData)
            {
                dataTable.Rows.Add(Supplier.id, Supplier.name, Supplier.accountNumber,
                Supplier.phoneNumber, Supplier.address, Supplier.perantAccountName
                );
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                temp = new Supplier();
                temp = dBContext.Suppliers.Include(c => c.Account).Include(c => c.Account.perantAccount).FirstOrDefault(e => e.id == id);

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
        public void search(string name, string phoneNumber, string accountNumber)
        {
            if (LoginData.permissions["supplier"].viewPermission.Value)
            {

                try
                {
                    allData = dBContext.Suppliers.AsNoTracking().OrderByDescending(a => a.id).Include(c => c.Account).Include(c => c.Account.perantAccount).
                        Where(
                            a => DbFunctions.Like(a.name, "%" + name + "%")
                                 && DbFunctions.Like(a.phoneNumber, "%" + phoneNumber + "%")
                                 && DbFunctions.Like(a.Account.accountNumber.ToString(), "%" + accountNumber + "%")

                             ).
                        Select(i => new
                        {
                            id = i.id,
                            name = i.name,
                            accountNumber = i.Account.accountNumber,
                            phoneNumber = i.phoneNumber,
                            address = i.address,
                            perantAccountName = i.Account.perantAccount.name,
                        }).ToList();

                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
        //private bool add()
        //{

        //}
        public bool add(string name, string accountNumber, string phoneNumber, string address)
        {

            bool status = false;

            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(temp.Account?.perantAccount ?? null, columnsNamesInAR[5], false))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[2]))
                return false;
            if (!ValidatingData.validatingData(phoneNumber, columnsNamesInAR[3]))
                return false;

            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {
                    var anyItem = dBContext.Suppliers.FirstOrDefault(e => e.name == name);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد مورد سابق بهذا الأسم"); return status; }

                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                    anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == name);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الأسم"); return status; }
                    ChartOfAccount newAccount = new ChartOfAccount() { accountNumber = newAccountNumber, name = name, parentId = temp.Account.perantAccount.id, rankk = temp.Account.perantAccount.rankk + 1, accountGroupId = temp.Account.AccountsGroup?.id, type = "فرعي", natureOfAccount = SharedData.balanceSheet, accountLocation = SharedData.accountLocations["custmoresAndSuplires"] };
                    newAccount = dBContext.ChartOfAccounts.Add(newAccount);
                    dBContext.SaveChanges();
                    Supplier newItem = new Supplier() { name = name, phoneNumber = phoneNumber, accountId = newAccount.id, address = address};
                    newItem = dBContext.Suppliers.Add(newItem);
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

        public bool update(string name, string accountNumber, string phoneNumber, string address)
        {

            bool status = false;



            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(temp.Account?.perantAccount ?? null, columnsNamesInAR[5], false))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[2]))
                return false;
            if (!ValidatingData.validatingData(phoneNumber, columnsNamesInAR[3]))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {


                    var anyItem = dBContext.Suppliers.FirstOrDefault(i => i.name == name && temp.id != i.id);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد مورد سابق بهذا الأسم"); return status; }

                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber && temp.accountId != a.id);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }

                    temp.Account.parentId = temp.Account.perantAccount.id;
                    temp.Account.rankk = temp.Account.perantAccount.rankk + 1;
                    temp.Account.accountNumber = newAccountNumber;
                    temp.name = name;
                    temp.phoneNumber = phoneNumber;
                    temp.address = address;
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
                            int accountId = temp.accountId;
                            statusProcess = dBContext.ChartOfAccounts.deleteAccount(accountId);
                            if (statusProcess != "true")
                            { throw new Exception(); }
                            dBContext.Suppliers.Remove(temp);
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
