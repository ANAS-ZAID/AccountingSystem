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
    public class UnitGuideController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم الوحده" };
        public BindingSource unitsSource;
        AccountingDbContext dBContext;
        public Unit tempUnit;
        List<Unit> allUnits;
        public ProsessesType prosessesType { get; set; }
        public UnitGuideController()
        {
            dBContext = new AccountingDbContext();
            allUnits = new List<Unit>();
            unitsSource = new BindingSource();
            lodeData();
        }
        private void lodeData()
        {
            try
            {
                allUnits = dBContext.Units.AsNoTracking().ToList();
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
            foreach (var accountGruop in allUnits)
            {
                dataTable.Rows.Add(accountGruop.id, accountGruop.name);
            }
            unitsSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                tempUnit = new Unit();
                tempUnit = dBContext.Units.Find(id);
                if (tempUnit == null)
                    throw new Exception();

            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search(string name)
        {
            if (LoginData.permissions["uintGuide"].viewPermission.Value)
            {
                try
                {
                    allUnits = dBContext.Units.AsNoTracking().Where(a => DbFunctions.Like(a.name, "%" + name + "%")).ToList();
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
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            try
            {
                var oldeGroup = dBContext.Units.FirstOrDefault(g => g.name == name);
                if (oldeGroup != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }

                Unit newGroup = new Unit() { name = name };
                dBContext.Units.Add(newGroup);
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                lodeData();

            }
            catch (Exception ex)
            {
                AppDialogAleart.showAleartError(ex.Message);
                status = false;
            }

            return status;
        }

        public bool update(string name)
        {
            bool status = false;
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            try
            {
              
                var group = dBContext.Units.FirstOrDefault(g => g.name == name && g.id != tempUnit.id);
                if (group != null)
                    throw new Exception();
                tempUnit.name = name;
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                tempUnit = null;
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
                            dBContext.Units.Remove(tempUnit);
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                        }
                        dBContext.SaveChanges();
                        transaction.Commit();
                        tempUnit = null;
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
