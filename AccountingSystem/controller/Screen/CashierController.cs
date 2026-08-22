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
using System.Security.Principal;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using System.Diagnostics;
using Bunifu.UI.WinForms.Helpers.Transitions;
using AccountingSystem.model;
using System.Data.Entity.Validation;


namespace AccountingSystem.controller
{
    public class CashierController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم الصندوق", "رقم الحساب ", "الحساب الأب" };
        public BindingSource dataSource;
        AccountingDbContext dBContext;
        public dynamic allCashier;
        public List<ChartOfAccount> mainAccounts { get { return dBContext.ChartOfAccounts.Where(a => a.type == "رئيسي").ToList(); } }
        public Cashier tempCashier;
        //public Cashier olde;
        public ProsessesType prosessesType { get; set; }
        public CashierController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            lodeData();
        }
        private void lodeData()
        {
          clearTempData();
            try
            { 
                allCashier =
                   dBContext.Cashiers.AsNoTracking().OrderByDescending(a=>a.id).Include(c => c.Account).Include(c => c.Account.perantAccount).
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
        public void clearTempData()
        {
            tempCashier = new Cashier ();
            tempCashier.Account = new ChartOfAccount();
            tempCashier.Account.perantAccount = null;
        }
        void fillDataGridView()
        {

            var dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            foreach (var Cashier in allCashier)
            {
                dataTable.Rows.Add(Cashier.id, Cashier.name,
                Cashier.accountNumber, Cashier.parentName);
            }
            dataSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {

                tempCashier = dBContext.Cashiers.Include(a=>a.Account).Include(a=>a.Account.perantAccount).FirstOrDefault(a=>a.id==id);
                if (tempCashier == null)
                    throw new Exception();
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search( string name,string accountNumber)
        {
            if (LoginData.permissions["cashier"].viewPermission.Value)
            {
                try
                {
                    allCashier = dBContext.Cashiers.AsNoTracking().Include(c => c.Account).Include(c => c.Account.perantAccount).Where(c => DbFunctions.Like(c.name, "%" + name + "%") && DbFunctions.Like((c.Account.accountNumber != 0 ? c.Account.accountNumber.ToString() : ""), "%" + accountNumber + "%")).
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
        public bool add( string name,string accountNumber)
        {

            bool status = false;
           
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(tempCashier.Account.perantAccount, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[2]))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
            try
            {
                var anyItem = dBContext.Cashiers.FirstOrDefault(a => a.name == name);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد صندوق سابق بنفس الأسم"); return status; }
                var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber);
                if (anyAccount != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == name);
                if (anyAccount != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الأسم"); return status; }
                ChartOfAccount newAccount=new ChartOfAccount() { accountNumber = newAccountNumber, name = name,parentId=tempCashier.Account.perantAccount.id, rankk=tempCashier.Account.perantAccount.rankk+1,type="فرعي",natureOfAccount=SharedData.balanceSheet, accountLocation = SharedData.accountLocations["cashiers"]};
                newAccount=  dBContext.ChartOfAccounts.Add(newAccount);
                dBContext.SaveChanges();
               // int newId = dBContext.ChartOfAccounts.FirstOrDefault(a => a.number == accountNumber).id;
                //AppDialogAleart.showAleartConfirmation(newAccount.accountNumber + "number"+ newAccount.id );
                Cashier newItem = new Cashier() { name = name, accountId= newAccount.id };
                
                dBContext.Cashiers.Add(newItem);
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

        public bool update( string name,string accountNumber)
        {

            bool status = false;

            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;

            
            if (!ValidatingData.validatingData(tempCashier.Account.perantAccount, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, columnsNamesInAR[2]))
                return false;
            int newAccountNumber = int.Parse(accountNumber);
           using(var transaction=dBContext.Database.BeginTransaction())
            {
                try
                {

                    var anyItem = dBContext.Cashiers.FirstOrDefault(i => i.name == name && i.id != tempCashier.id);
                    if (anyItem != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد صندوق بهذا الأسم"); return status; }
                    var anyAccount = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == newAccountNumber && tempCashier.accountId != a.id);
                    if (anyAccount != null)
                    { AppDialogAleart.showAleartPreExistingData("يوجد حساب سابق بنفس الرقم"); return status; }
                    // tempCashier.Account.name = name;
                    tempCashier.Account.parentId = tempCashier.Account.perantAccount.id;
                    tempCashier.Account.rankk = tempCashier.Account.perantAccount.rankk + 1;
                    tempCashier.Account.accountNumber = newAccountNumber;
                    tempCashier.name = name;
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
            string statusProcess="";
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
                            int accountId =Convert.ToInt32(tempCashier.accountId);
                            statusProcess = dBContext.ChartOfAccounts.deleteAccount(accountId);
                            if (statusProcess != "true")
                            { throw new Exception(); }
                            dBContext.Cashiers.Remove(tempCashier);
                             
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
                        if(statusProcess!="true"&& !String.IsNullOrEmpty(statusProcess))
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
