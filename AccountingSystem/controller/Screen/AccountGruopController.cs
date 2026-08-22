using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.model;

namespace AccountingSystem.controller
{
    public class AccountGruopController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم المجموعه" };
        public  BindingSource accountGruopSource;
        AccountingDbContext dBContext;
        public  AccountsGroup tempGroup;
        List<AccountsGroup> accountsGroups;
        public  ProsessesType prosessesType {  get; set; }
        public  AccountGruopController()
        {  
            dBContext = new AccountingDbContext();
            accountsGroups = new List<AccountsGroup>();
            accountGruopSource = new BindingSource();
            lodeData();
        }
        private  void lodeData()
        { 
            try 
            {  
               accountsGroups = dBContext.AccountsGroups.AsNoTracking().ToList();
                fillDataGridView();
            } catch
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
            foreach (var accountGruop in accountsGroups)
            {
                dataTable.Rows.Add(accountGruop.id, accountGruop.name);
            }
            accountGruopSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                tempGroup = new AccountsGroup();
                tempGroup = dBContext.AccountsGroups.Find(id);
                if (tempGroup == null)
                    throw new Exception();
               
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search(string name, string rowCount)
        {
            int count = 30;
            if (LoginData.permissions["accountGruop"].viewPermission.Value)
            {
                if (!string.IsNullOrEmpty(rowCount))
                    count = int.Parse(rowCount);
                try
                {
                    accountsGroups = dBContext.AccountsGroups.AsNoTracking().Where(a => DbFunctions.Like(a.name, "%" + name + "%")).Take(count).ToList();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
        public bool add(string name)
        {  bool status=false;
            if (!ValidatingData.validatingData(name, "أسم المجموعه "))
                return false;
            try 
            {  var oldeGroup = dBContext.AccountsGroups.FirstOrDefault(g => g.name == name);
                if (oldeGroup != null)
                { AppDialogAleart.showAleartPreExistingData();return status; }
               
                AccountsGroup newGroup = new AccountsGroup() { name = name };
                    dBContext.AccountsGroups.Add(newGroup);
                    dBContext.SaveChanges();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                lodeData();

            } catch(Exception ex)
            {
                AppDialogAleart.showAleartError(ex.Message);
                status=false;
            }

            return status;
        }
        
        public bool update( string name)
        {
            bool status = false;
            if (!ValidatingData.validatingData(name, "أسم المجموعه "))
                return false;
            try
            {
               
                var group = dBContext.AccountsGroups.FirstOrDefault(g => g.name == name && g.id!= tempGroup.id);
                if (group != null)
                    throw new Exception();
                tempGroup.name= name;
                dBContext.SaveChanges();
                     status = true;
                AppDialogAleart.showAleartSuccess();
                tempGroup = null;
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
                            dBContext.AccountsGroups.Remove(tempGroup);
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                        }
                        dBContext.SaveChanges();
                        transaction.Commit();
                        tempGroup = null;
                        lodeData();
                    }
                    catch
                    {
                        transaction.Rollback();
                        AppDialogAleart.showAleartError();
                        status = false;
                    }
                }
            }else { AppDialogAleart.showAleartError("لم تقم بتحديد اي بيانات للحذف"); }
      
        
            return status;
        }
    }
}
