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
using System.Data.Common;
using System.ComponentModel;
using Microsoft.Extensions;
using System.Data.Entity.Validation;
using AccountingSystem.model;

namespace AccountingSystem.controller
{
    public class AccountingGuideController
    {


        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم الحساب", "رقم الحساب ", "نوع الحساب ", "طبيعة الحساب ", "مكان الحساب", "الحساب الأب", "المجموعه" };
        public BindingSource chartOfAccountSource;
        AccountingDbContext dBContext;
        dynamic allChartOfAccounts;
        public List<AccountsGroup> accountsGroups{ get { return dBContext.AccountsGroups.ToList(); } }
        public List<ChartOfAccount> mainAccounts { get { return dBContext.ChartOfAccounts.Where(x=>x.type=="رئيسي").ToList(); } }

        public  List<string> accountLocations = new List<string> { "العملاء والوكلاء", "الموردين", "الموظفين", "الصناديق", "المخازن" };
        public ChartOfAccount tempChartOfAccount;

        public ProsessesType prosessesType { get; set; }
        public AccountingGuideController()
        {
            dBContext = new AccountingDbContext();
            chartOfAccountSource = new BindingSource();
            lodeData();
        }
        public void clearTempData()
        {
            tempChartOfAccount=new ChartOfAccount() { AccountsGroup=null,perantAccount=null};
        }
        public  void lodeData()
        {
           clearTempData();
            try
            { 
                var temp = dBContext.ChartOfAccounts.AsNoTracking().OrderByDescending(x=>x.id).Include(c => c.AccountsGroup).Include(a => a.perantAccount);
                allChartOfAccounts = temp.ToList().Select(c => new {
                                         id = c.id,
                                         name = c.name,
                                         accountNumber = c.accountNumber,
                                         type = c.type,
                                         natureOfAccount = c.natureOfAccount,
                                         accountLocation = c.accountLocation,
                                         parentName = c.perantAccount?.name,
                                         nameGroup = c.AccountsGroup?.name,
                                     }).ToList();
               
                fillDataGridView();
            }
            catch (Exception ex)
            {
                AppDialogAleart.showAleartError(ex.Message);
            }
        }
        void fillDataGridView()
        {

            var dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            foreach (var chartOfAccount in allChartOfAccounts)
            {
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
              clearTempData();
                tempChartOfAccount = dBContext.ChartOfAccounts.Include(a=>a.perantAccount).Include(a=>a.AccountsGroup).FirstOrDefault(a=>a.id==id);

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
        public void search()
        {
            string parent = tempChartOfAccount.perantAccount != null ? tempChartOfAccount.perantAccount.name : "";
            string group = tempChartOfAccount.AccountsGroup != null ? tempChartOfAccount.AccountsGroup.name : "";
            if (LoginData.permissions["accountingGuide"].viewPermission.Value)
            {
                try
                {
                    allChartOfAccounts = dBContext.ChartOfAccounts.AsNoTracking().OrderByDescending(x => x.id).Include(a => a.AccountsGroup).Include(a => a.perantAccount).
                        Where(
                            a =>

                            DbFunctions.Like(a.name, "%" + tempChartOfAccount.name + "%") &&
                            DbFunctions.Like(a.perantAccount != null ? a.perantAccount.name : "", "%" + parent + "%") &&
                            DbFunctions.Like(a.AccountsGroup != null ? a.AccountsGroup.name : "", "%" + group + "%") &&
                                 DbFunctions.Like(a.accountNumber != 0 ? a.accountNumber.ToString() : "", "%" + (tempChartOfAccount.accountNumber != 0 ? tempChartOfAccount.accountNumber.ToString() : "") + "%") &&
                                 DbFunctions.Like(a.parentId.HasValue ? a.parentId.ToString() : "", "%" + tempChartOfAccount.parentId ?? "" + "%") &&
                                 DbFunctions.Like(a.accountGroupId.HasValue ? a.accountGroupId.ToString() : "", "%" + tempChartOfAccount.accountGroupId ?? "" + "%")
                             ).
                        Select(c => new
                        {
                            id = c.id,
                            name = c.name,
                            accountNumber = c.accountNumber,
                            type = c.type,
                            natureOfAccount = c.natureOfAccount,
                            accountLocation = c.accountLocation,
                            parentName = c.perantAccount != null ? c.perantAccount.name : null,
                            nameGroup = c.AccountsGroup != null ? c.AccountsGroup.name : null,
                        }).ToList();
                    clearTempData();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
        public bool dataProcessing(string accountNum, string name)
        {
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;

            if (!ValidatingData.validatingData(tempChartOfAccount.natureOfAccount, columnsNamesInAR[4], false))
                return false;
            if (!ValidatingData.validatingData(tempChartOfAccount.type, columnsNamesInAR[3], false))
                return false;
            if (!ValidatingData.validatingData(accountNum, " رقم الحساب"))
                return false;
            tempChartOfAccount.accountNumber = int.Parse(accountNum);
            tempChartOfAccount.name = name;
            tempChartOfAccount.parentId=tempChartOfAccount.perantAccount?.id;
            tempChartOfAccount.accountGroupId=tempChartOfAccount.AccountsGroup?.id;
            if (tempChartOfAccount.perantAccount != null)
                tempChartOfAccount.rankk = tempChartOfAccount.perantAccount.rankk + 1;
            else tempChartOfAccount.rankk = 1;
            if (prosessesType == ProsessesType.add)
            return    add();
            else return update();
        }
        public bool add()
        {
          
            bool status = false;

           
            try
            {
                var anyItem = dBContext.ChartOfAccounts.FirstOrDefault(a => a.name == tempChartOfAccount.name);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }
                anyItem = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == tempChartOfAccount.accountNumber);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }
                dBContext.ChartOfAccounts.Add(tempChartOfAccount);
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

        public bool update()
        {
            bool status = false;

            try
            {


                var anyItem = dBContext.ChartOfAccounts.FirstOrDefault(g => g.name == tempChartOfAccount.name && g.id != tempChartOfAccount.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد حساب بهذا الأسم"); return status; }
                 anyItem = dBContext.ChartOfAccounts.FirstOrDefault(a => a.accountNumber == tempChartOfAccount.accountNumber && a.id != tempChartOfAccount.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد حساب بهذا الرقم"); return status; }
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                lodeData();


            }
            catch(DbEntityValidationException ex)
            {
                AppDialogAleart.showEntityValidationErrors(ex);
                AppDialogAleart.showAleartError();
                status = false;
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
                                throw new Exception("notFound");

                            statusProcess = dBContext.ChartOfAccounts.deleteAccount(id,true);
                            if (statusProcess != "true")
                            { throw new Exception(); }
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
                        else
                        AppDialogAleart.showAleartError();
                      
                        status = false;
                    }
                }
            }
            else { AppDialogAleart.showAleart("للحذف",MessageType.NoDataSpecified); }


            return status;
        }
        public void selectedGroup(object value)
        {
            tempChartOfAccount.AccountsGroup=(AccountsGroup)value;
        }
        public string selectedParent(object value)
        {
            tempChartOfAccount.perantAccount=(ChartOfAccount)value;
          return  AppDBFunctions.getNewAccountNumByParentId(tempChartOfAccount.perantAccount.id).ToString();
        }     
        public void selectedLocation(object value)
        {
            tempChartOfAccount.accountLocation=(string)value;
        } 
        public void selectedNature(object value)
        {
            tempChartOfAccount.natureOfAccount=(string)value;
        }    
        public void selectedType(object value)
        {
            tempChartOfAccount.type=(string)value;
        }
    }
}
