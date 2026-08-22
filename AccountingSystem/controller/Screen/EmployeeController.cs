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
using System.Data.Entity.Validation;
using System.Threading;


namespace AccountingSystem.controller
{
    public class EmployeeController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم الموظف", "رقم الهاتف ", "أسم الدخول ", "رقم الحساب ", "الحساب الأب", "الفرع", "صندوق الموظف", "نوع الموظف", "الحاله", "مجموعة الصلاحيات" };
        public BindingSource dataSource;
        DataTable dataTable;
        public dynamic allEmployees;
        public List<Employee> employeesList;
        AccountingDbContext dBContext;
        public List<ChartOfAccount> mainAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "رئيسي").ToList(); } }
        public List<Branch> allBranches { get { return dBContext.Branches.ToList(); } }
        public List<EmployeesType> allEmployeeType { get { return dBContext.EmployeesTypes.ToList(); }}
        public List<Cashier> allCashiers { get { return dBContext.Cashiers.ToList(); } }
        public List<Permission> allPermissions;
        public Employee tempEmployee;
        public ICollection<Permission> newPermissions;
        public ProsessesType prosessesType { get; set; }
        public EmployeeController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            allPermissions = new List<Permission>();
            newPermissions = new List<Permission>();
            tempEmployee = new Employee();
            allEmployees = new List<Employee>();
            lodeData();
        }
       public void clearTempData()
        {
            tempEmployee = new Employee();
            tempEmployee.Account = new ChartOfAccount();
            tempEmployee.Permissions = new List<Permission>();
            tempEmployee.Branch = null;
            tempEmployee.Cashier = null;
            tempEmployee.EmployeesType = null;
            tempEmployee.Account.perantAccount =null;

        }
        public void lodeData()
        {

            clearTempData();

            try
            {
                employeesList = dBContext.Employees.AsNoTracking().OrderByDescending(a => a.id).ToList();
                allEmployees = employeesList.Select(e => new {
                    id = e.id,
                    name = e.name,
                    phoneNamber = e.phoneNamber,
                    loginName = e.loginName,
                    accountNumber = e.Account.accountNumber,
                    perantAccountName = e.Account.perantAccount?.name,
                    branchName = e.Branch?.name,
                    cashierName = e.Cashier?.name,
                    employeeTypeName = e.EmployeesType?.name,
                    status = e.status.Value?"فعال":"غير فعال",
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
            Thread thread = new Thread(fillDataTable);
            thread.Start();
        }

        private void fillDataTable()
        {
            foreach (var employee in allEmployees)
            {
                dataTable.Rows.Add(employee.id, employee.name,
                employee.phoneNamber, employee.loginName, employee.accountNumber,
                employee.perantAccountName, employee.branchName, employee.cashierName,
                employee.employeeTypeName, employee.status, null);
            }
            dataSource.DataSource = dataTable;
        }

        public void activateOrDeactivateAccount(int id,bool isActivate)
        {
            if (find(id))
            {
                try
                {
                    tempEmployee.status = isActivate;
                    dBContext.SaveChanges();
                    lodeData();
                }
                catch
                {
                }
            } 
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                tempEmployee = new Employee();
                tempEmployee = dBContext.Employees.Include(c => c.Branch)
                    .Include(c => c.Cashier).Include(c => c.Account).Include(c => c.EmployeesType).Include(c => c.Permissions)
                    .Include(a => a.Account.perantAccount).FirstOrDefault(e => e.id == id);

                if (tempEmployee == null)
                    throw new Exception();
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search(string name, string phoneNamber, string accountNumber)
        {
            if (model.LoginData.permissions["employee"].viewPermission.Value)
            {
                string branchName = tempEmployee.Branch == null ? "" : tempEmployee.Branch.name;
                string type = tempEmployee.EmployeesType == null ? "" : tempEmployee.EmployeesType.name;

                try
                {
                    allEmployees = dBContext.Employees.AsNoTracking().OrderByDescending(a => a.id).Include(c => c.Branch)
                        .Include(c => c.Cashier).Include(c => c.Account).Include(c => c.EmployeesType).Include(c => c.Permissions)
                        .Include(a => a.Account.perantAccount).
                        Where(
                            a =>
                                 DbFunctions.Like(a.name, "%" + name + "%")
                                 && DbFunctions.Like(a.phoneNamber, "%" + phoneNamber + "%")
                                 && DbFunctions.Like(a.Account.accountNumber.ToString(), "%" + accountNumber + "%")
                                 && DbFunctions.Like(a.Branch != null ? a.Branch.name : "", "%" + branchName + "%")
                                 && DbFunctions.Like(a.EmployeesType != null ? a.EmployeesType.name : "", $"%" + type + "%")
                             ).
                        Select(e => new
                        {
                            id = e.id,
                            name = e.name,
                            phoneNamber = e.phoneNamber,
                            loginName = e.loginName,
                            accountNumber = e.Account.accountNumber,
                            perantAccountName = e.Account.perantAccount.name,
                            branchName = e.Branch.name,
                            cashierName = e.Cashier != null ? e.Cashier.name : null,
                            employeeTypeName = e.EmployeesType != null ? e.EmployeesType.name : null,
                            status = e.status.Value ? "فعال" : "غير فعال",
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
        public bool add(string name, string phoneNamber, string loginName, string loginPassword, string accountNumber,List<Permission> permissions)
        {

            bool status = false;

            int? cashierId = null;
            if (tempEmployee.Cashier != null)
                cashierId = tempEmployee.Cashier.id;
            int? employeeTypeId = null;
            if (tempEmployee.EmployeesType != null)
                employeeTypeId = tempEmployee.EmployeesType.id;
            //{ "الرقم", "أسم الموظف", "رقم الهاتف ", "أسم الدخول ", "رقم الحساب ",
            //"الحساب الأب", "الفرع", "صندوق الموظف", "نوع الموظف", "الحاله" }

            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;

            if (!ValidatingData.validatingData(phoneNamber, columnsNamesInAR[2]))
                return false;
            if (!ValidatingData.validatingData(loginName, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(loginPassword, "كلمة المرور"))
                return false;
            if (!ValidatingData.validatingData(tempEmployee.Account?.perantAccount??null, columnsNamesInAR[5],false))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[4]))
                return false;
            if (!ValidatingData.validatingData(tempEmployee.Branch??null, columnsNamesInAR[6], false))
                return false; 
            if (!ValidatingData.validatingData(tempEmployee.EmployeesType??null, columnsNamesInAR[8], false))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {
               
                try
                {
                    var anyItem = dBContext.Employees.FirstOrDefault(e => e.name == name);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData(); return status; }
                    anyItem = dBContext.Employees.FirstOrDefault(e => e.loginName == loginName);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد أسم دخول سابق بنفس الأسم"); return status; }
                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                    anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == name);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الأسم"); return status; }
                    ChartOfAccount newAccount = new ChartOfAccount() { accountNumber = newAccountNumber, name = name, parentId = tempEmployee.Account.perantAccount.id, rankk = tempEmployee.Account.perantAccount.rankk + 1, type = "فرعي", natureOfAccount = SharedData.balanceSheet, accountLocation = SharedData.accountLocations["employees"] };
                    newAccount = dBContext.ChartOfAccounts.Add(newAccount);
                    dBContext.SaveChanges();
                    
                    Employee newItem = new Employee() { status = true, name = name, phoneNamber = phoneNamber, loginName = loginName, password = loginPassword, accountId = newAccount.id, brancheId = tempEmployee.Branch?.id, cashierId = tempEmployee.Cashier?.id, employeeTypeId = tempEmployee.EmployeesType?.id };
                    newItem= dBContext.Employees.Add(newItem);
                    dBContext.SaveChanges();

                    if (!dBContext.addPermission(permissions, newItem.id))
                        throw new Exception();
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

        public bool update(string name, string phoneNamber, string loginName, string loginPassword, string accountNumber,List<Permission> permissions)
        {

            bool status = false;

           
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;

            if (!ValidatingData.validatingData(phoneNamber, columnsNamesInAR[2], false))
                return false;
            if (!ValidatingData.validatingData(loginName, columnsNamesInAR[3], false))
                return false;
            if (!ValidatingData.validatingData(loginPassword, "كلمة المرور", false))
                return false;
            if (!ValidatingData.validatingData(tempEmployee.Account.perantAccount, columnsNamesInAR[5]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[4]))
                return false;
            if (!ValidatingData.validatingData(tempEmployee.Branch, columnsNamesInAR[6]))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {


                    var anyItem = dBContext.Employees.FirstOrDefault(e => e.name == name && e.id != tempEmployee.id);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد موظف سابق بهذا الأسم"); return status; }
                    anyItem = dBContext.Employees.FirstOrDefault(e => e.loginName == loginName && e.id != tempEmployee.id);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد أسم دخول سابق بنفس الأسم"); return status; }
                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(e => e.accountNumber == newAccountNumber && e.id != tempEmployee.Account.id);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }

                    tempEmployee.Account.parentId = tempEmployee.Account.perantAccount.id;
                    tempEmployee.Account.rankk = tempEmployee.Account.perantAccount.rankk + 1;
                    tempEmployee.Account.accountNumber= newAccountNumber;
                    tempEmployee.name = name;
                    tempEmployee.phoneNamber = phoneNamber;
                    tempEmployee.loginName = loginName;
                    tempEmployee.password = loginPassword;
                    tempEmployee.Permissions.updatePermissions(permissions);
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();


                }
                catch(DbEntityValidationException  e )
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

                            dBContext.Permissions.RemoveRange(tempEmployee.Permissions);
                            dBContext.SaveChanges();
                            int accountId = tempEmployee.accountId;
                            statusProcess = dBContext.ChartOfAccounts.deleteAccount(accountId);
                            if (statusProcess != "true")
                            { throw new Exception(); }
                            dBContext.Employees.Remove(tempEmployee);
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
        public void selectedType(object value)
        {
            tempEmployee.EmployeesType=(EmployeesType)value;
        }
        public void selectedBranch(object value)
        {
            tempEmployee.Branch=(Branch)value;
        }
    }
}
