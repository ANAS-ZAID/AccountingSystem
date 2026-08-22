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
using System.Threading;


namespace AccountingSystem.controller
{
    public class CustmoreController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "الأسم", "رقم الهاتف ", "العنوان ", "الحساب الأب","المجموعه", "المدينه", "المنطقه"};
        public BindingSource dataSource;
        DataTable dataTable;
        public dynamic allData;
        AccountingDbContext dBContext;
        public List<ChartOfAccount> mainAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "رئيسي").ToList(); } }
       public List<Area> allAreas { get { return dBContext.Areas.ToList(); } }
       public List<City> allCity{ get { return dBContext.Cities.ToList(); } }
       public List<AccountsGroup> allAccountGroups{ get { return dBContext.AccountsGroups.ToList(); } }
 
        public Customer temp;

        public ProsessesType prosessesType { get; set; }
        public CustmoreController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            temp = new Customer();
            allData = new List<Customer>();
            lodeData();
        }
        public void clearTempData()
        {
            temp = new Customer();
            temp.Account =new ChartOfAccount();
            temp.Area = null;
            temp.City = null;
          temp.Account.AccountsGroup = null;
           temp.Account.perantAccount = null;
        }
        public void lodeData()
        {

            clearTempData();

            try
            {
                allData = dBContext.Customers.Select(e => new {
                    id = e.id,
                    name = e.name,
                    phoneNamber = e.phoneNamber,
                    address = e.address,
                    perantAccountName = e.Account.perantAccount.name,
                    groupName = e.Account.AccountsGroup.name,
                    areaName = e.Area.name,
                    cityName = e.City.name,
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
            Thread thread = new Thread(new ThreadStart(fillDataTable));
            thread.Start();
        }

        private void fillDataTable()
        {
            foreach (var Customer in allData)
            {
                dataTable.Rows.Add(Customer.id, Customer.name,
                Customer.phoneNamber, Customer.address, Customer.perantAccountName,
                Customer.groupName, Customer.areaName, Customer.cityName
                );
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                temp = new Customer();
                temp = dBContext.Customers.FirstOrDefault(e => e.id == id);

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
        public void search(string name, string phoneNamber)
        {
            if (model.LoginData.permissions["custmore"].viewPermission.Value)
            {
                string areaName = temp.Area == null ? "" : temp.Area.name;
                string cityName = temp.City == null ? "" : temp.City.name;
                string accountGroup = temp.Account.AccountsGroup == null ? "" : temp.Account.AccountsGroup.name;

                try
                {
                    allData = dBContext.Customers.AsNoTracking().OrderByDescending(a => a.id).
                        Where(
                            a => DbFunctions.Like(a.name, "%" + name + "%")
                                 && DbFunctions.Like(a.phoneNamber, "%" + phoneNamber + "%")
                                 && DbFunctions.Like(a.Area != null ? a.Area.name : "", "%" + areaName + "%")
                                 && DbFunctions.Like(a.Account.AccountsGroup != null ? a.Account.AccountsGroup.name : "", "%" + accountGroup + "%")
                                 && DbFunctions.Like(a.City != null ? a.City.name : "", $"%" + cityName + "%")
                             ).
                        Select(e => new
                        {
                            id = e.id,
                            name = e.name,
                            phoneNamber = e.phoneNamber,
                            address = e.address,
                            perantAccountName = e.Account.perantAccount.name,
                            groupName = e.Account.AccountsGroup.name,
                            areaName = e.Area.name,
                            cityName = e.City.name,
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
        public bool add(string name, string accountNumber, string phoneNamber, string address)
        {

            bool status = false;

            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(temp.Account?.perantAccount ?? null, columnsNamesInAR[4], false))
                return false;
            if (!ValidatingData.validatingData(accountNumber, "رقم الحساب"))
                    return false;
            if (!ValidatingData.validatingData(phoneNamber, columnsNamesInAR[2]))
                return false;
            
            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {
                    var anyItem = dBContext.Customers.FirstOrDefault(e => e.name == name);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد عميل سابق بهذا الأسم"); return status; }
                   
                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                    anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == name);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الأسم"); return status; }
                    ChartOfAccount newAccount = new ChartOfAccount() { accountNumber = newAccountNumber, name = name, parentId = temp.Account.perantAccount.id, rankk = temp.Account.perantAccount.rankk + 1,accountGroupId=temp.Account.AccountsGroup?.id, type = "فرعي", natureOfAccount = SharedData.balanceSheet, accountLocation = SharedData.accountLocations["custmoresAndSuplires"] };
                    newAccount = dBContext.ChartOfAccounts.Add(newAccount);
                    dBContext.SaveChanges();

                    Customer newItem = new Customer() { name = name, phoneNamber = phoneNamber, accountId = newAccount.id, address=address,areaId = temp.Area?.id,cityId=temp.City?.id };
                    newItem = dBContext.Customers.Add(newItem);
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

        public bool update(string name, string accountNumber, string phoneNamber, string address)
        {

            bool status = false;


            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(temp.Account?.perantAccount ?? null, columnsNamesInAR[4], false))
                return false;
                if (!ValidatingData.validatingData(accountNumber, "رقم الحساب"))
                return false;
                    if (!ValidatingData.validatingData(phoneNamber, columnsNamesInAR[2]))
                        return false;
            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {


                    var anyItem = dBContext.Customers.FirstOrDefault(i =>i.name == name&&temp.id!=i.id);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد عميل سابق بهذا الأسم"); return status; }
                   
                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber && temp.accountId != a.id);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                    
                    temp.Account.parentId = temp.Account.perantAccount.id;
                    temp.Account.rankk = temp.Account.perantAccount.rankk + 1;
                    temp.Account.accountNumber= newAccountNumber;
                    temp.name = name;
                    temp.phoneNamber = phoneNamber;
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
                            int accountId = temp.accountId.Value;
                            statusProcess = dBContext.ChartOfAccounts.deleteAccount(accountId);
                            if (statusProcess != "true")
                            { throw new Exception(); }
                            dBContext.Customers.Remove(temp);
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
        public void selectedCity(object value)
        {
            temp.City = (City)value;
        }
        public void selectedArea(object value)
        {
            temp.Area = (Area)value;
        } 
        public void selectedAccountsGroup(object value)
        {
            temp.Account.AccountsGroup = (AccountsGroup)value;
        }
    }
}
