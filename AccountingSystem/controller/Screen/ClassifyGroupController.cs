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
    public class ClassifyGroupController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم المجموعه" };
        public BindingSource gruopsSource;
        AccountingDbContext dBContext;
        public ClassifyGroup tempGroup;
        List<ClassifyGroup> allGruops;
        public ProsessesType prosessesType { get; set; }
        public ClassifyGroupController()
        {
            dBContext = new AccountingDbContext();
            allGruops = new List<ClassifyGroup>();
            gruopsSource = new BindingSource();
            lodeData();
        }
        private void lodeData()
        {
            try
            {
                allGruops = dBContext.ClassifyGroups.AsNoTracking().ToList();
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
            foreach (var accountGruop in allGruops)
            {
                dataTable.Rows.Add(accountGruop.id, accountGruop.name);
            }
            gruopsSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                tempGroup = new ClassifyGroup();
                tempGroup = dBContext.ClassifyGroups.Find(id);
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
            if (model.LoginData.permissions["classifyGruop"].viewPermission.Value)
            {
                if (!string.IsNullOrEmpty(rowCount))
                    count = int.Parse(rowCount);
                try
                {
                    allGruops = dBContext.ClassifyGroups.AsNoTracking().Where(a => DbFunctions.Like(a.name, "%" + name + "%")).Take(count).ToList();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
        public bool add(string name)
        {
            bool status = false;
            if (!ValidatingData.validatingData(name, "أسم المجموعه "))
                return false;
            try
            {
                var oldeGroup = dBContext.ClassifyGroups.FirstOrDefault(g => g.name == name);
                if (oldeGroup != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }

                ClassifyGroup newGroup = new ClassifyGroup() { name = name };
                dBContext.ClassifyGroups.Add(newGroup);
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

        public bool update(string name)
        {
            bool status = false;
            if (!ValidatingData.validatingData(name, "أسم المجموعه "))
                return false;
            try
            {
            
                var group = dBContext.ClassifyGroups.FirstOrDefault(g => g.name == name && g.id != tempGroup.id);
                if (group != null)
                    throw new Exception();
                tempGroup.name = name;
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
                            dBContext.ClassifyGroups.Remove(tempGroup);
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
            }
            else { AppDialogAleart.showAleartError("لم تقم بتحديد اي بيانات للحذف"); }


            return status;
        }
    }
}
