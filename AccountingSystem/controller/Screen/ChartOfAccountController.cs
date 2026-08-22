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
using System.Xml.Linq;

namespace AccountingSystem.controller
{
    public class ChartOfAccountController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم الحساب", "رقم الحساب ", "نوع الحساب ", "طبيعة الحساب ", "مكان الحساب", "الحساب الأب", "المجموعه" };
        public BindingSource chartOfAccountSource;
        AccountingDbContext dBContext;
        dynamic allChartOfAccounts ;
        List<AccountsGroup> accountsGroups;
        List<string> accountLocations= new List<string> { "العملاء والوكلاء", "الموردين", "الموظفين", "الصناديق", "المخازن", };
        public ChartOfAccount tempChartOfAccount;
       
        public ProsessesType prosessesType { get; set; }
        public ChartOfAccountController()
        {
            dBContext = new AccountingDbContext();
            tempChartOfAccount=new ChartOfAccount();
            chartOfAccountSource = new BindingSource();
            lodeData();
        }
        private void lodeData()
        {
            try
            { // { "الرقم", "أسم الحساب", "رقم الحساب ", "نوع الحساب ", "طبيعة الحساب ", "مكان الحساب", "الحساب الأب" }
              //dataTable.Rows.Add(chartOfAccount.number, chartOfAccount.name,
              //chartOfAccount.id, chartOfAccount.type, chartOfAccount.natureOfAccount, 
              //    chartOfAccount.accountLocation, chartOfAccount.parentId);
                 allChartOfAccounts =
                    dBContext.ChartOfAccounts.AsNoTracking().Include(c => c.AccountsGroup).Include(a => a.perantAccount).
                                      Select(c=> new {
                                          accountNumber = c.accountNumber, name = c.name, id = c.id, type = c.type,
                                          natureOfAccount = c.natureOfAccount, accountLocation = c.accountLocation,
                                          parentName = c.perantAccount.name ,
                                          nameGroup = c.AccountsGroup.name,
                                      }).ToList();
                accountsGroups=dBContext.AccountsGroups.ToList();
                fillDataGridView();
            }
            catch (Exception ex)
            {
                AppDialogAleart.showAleartError(ex.Message);
            }
        }
        void fillDataGridView( )
        {
           
            var dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            foreach (var chartOfAccount in allChartOfAccounts)
            {
                // { "المجموعه","الرقم", "أسم الحساب", "رقم الحساب ", "نوع الحساب ", "طبيعة الحساب ", "مكان الحساب", "الحساب الأب" }
            
                    dataTable.Rows.Add(chartOfAccount.id, chartOfAccount.name,
                    chartOfAccount.accountNumber, chartOfAccount.type, chartOfAccount.natureOfAccount, 
                    chartOfAccount.accountLocation, chartOfAccount.parentName, chartOfAccount.nameGroup);
            }
            chartOfAccountSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                tempChartOfAccount = new ChartOfAccount();
                tempChartOfAccount = dBContext.ChartOfAccounts.Find(id);
                if (tempChartOfAccount == null)
                    throw new Exception();

            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search(string accountNumber, string name)
        {
         
            int accountId = 0;

            if(!String.IsNullOrEmpty(accountNumber))
                accountId=int.Parse(accountNumber);
          
            try
            {
                allChartOfAccounts = dBContext.ChartOfAccounts.AsNoTracking().Include(a=>a.AccountsGroup).Include(a => a.perantAccount).Where(a => DbFunctions.Like(a.name, "%" + name + "%") && a.id == accountId && a.parentId == tempChartOfAccount.parentId && a.accountGroupId == tempChartOfAccount.accountGroupId).
                    Select(c => new {
                    id = c.id,
                    name = c.name,
                    accountNumber = c.accountNumber,
                    type = c.type,
                    natureOfAccount = c.natureOfAccount,
                    accountLocation = c.accountLocation,
                    parenName = c.perantAccount.name,
                    nameGroup = c.AccountsGroup.name,
                }).ToList();
                fillDataGridView();
            }
            catch
            {
                AppDialogAleart.showAleartError();
            }
        }
        public bool add(string accountNumber, string name)
        {
            
                bool status = false;
          
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
           // if (!ValidatingData.validatingData(tempChartOfAccount.parentId, columnsNamesInAR[4]))
            //    return false;
            if (!ValidatingData.validatingData(tempChartOfAccount.natureOfAccount, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(tempChartOfAccount.type, columnsNamesInAR[2]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, "الحساب"))
                return false;
            try
            {
                var anyItem = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == name);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }
                anyItem = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == int.Parse(accountNumber));
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }

                ChartOfAccount newItem = new ChartOfAccount() { name = name ,parentId=tempChartOfAccount.parentId,natureOfAccount= tempChartOfAccount.natureOfAccount, type= tempChartOfAccount.type, accountNumber = int.Parse(accountNumber),accountLocation = tempChartOfAccount.accountLocation, accountGroupId= tempChartOfAccount.accountGroupId };
                dBContext.ChartOfAccounts.Add(newItem);
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                tempChartOfAccount = null;
                lodeData();

            }
            catch (Exception ex)
            {
                AppDialogAleart.showAleartError(ex.Message);
                status = false;
            }

            return status;
        }

        public bool update(string accountNumber, string name)
        {
            bool status = false;
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(tempChartOfAccount.natureOfAccount, columnsNamesInAR[3]))
                return false;
            if (!ValidatingData.validatingData(tempChartOfAccount.type, columnsNamesInAR[2]))
                return false;
            if (!ValidatingData.validatingData(accountNumber, "الحساب"))
                return false;
            try
            {
               
                var anyItem = dBContext.ChartOfAccounts.FirstOrDefault(g => g.name == name || g.accountNumber == int.Parse(accountNumber) && g.id != tempChartOfAccount.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }
                tempChartOfAccount.accountNumber = int.Parse(accountNumber);
                tempChartOfAccount.name = name;
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                tempChartOfAccount = null;
                lodeData();


            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
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
                                throw new Exception();
                            var anyItem = dBContext.JournalEntries.FirstOrDefault(j => j.accountId==tempChartOfAccount.id);
                            if (anyItem != null)
                            { AppDialogAleart.showAleartError("لايمكنك حذف الحساب لأنه مرتبط بعمليات ماليه"); return status; }
                            dBContext.ChartOfAccounts.Remove(tempChartOfAccount);
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                        }
                        dBContext.SaveChanges();
                        transaction.Commit();
                        tempChartOfAccount  = null;
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
    }
}
