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
    public class EmployeesTypeController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم القسم التوظيفي" };
        public BindingSource dataSource;
        AccountingDbContext dBContext;
        public EmployeesType temp;
        List<EmployeesType> allData;
        public ProsessesType prosessesType { get; set; }
        public EmployeesTypeController()
        {
            dBContext = new AccountingDbContext();
            allData = new List<EmployeesType>();
            dataSource = new BindingSource();
            lodeData();
        }
        private void lodeData()
        {
            try
            {
                allData = dBContext.EmployeesTypes.AsNoTracking().ToList();
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
            foreach (var accountGruop in allData)
            {
                dataTable.Rows.Add(accountGruop.id, accountGruop.name);
            }
            dataSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                temp = new EmployeesType();
                temp = dBContext.EmployeesTypes.Find(id);
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
        public void search(string name)
        {
           
            try
            {
                allData = dBContext.EmployeesTypes.AsNoTracking().Where(a => DbFunctions.Like(a.name, "%" + name + "%")).ToList();
                fillDataGridView();
            }
            catch
            {
                AppDialogAleart.showAleartError();
            }
        }
        public bool add(string name)
        {
            bool status = false;
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            try
            {
                var olde = dBContext.EmployeesTypes.FirstOrDefault(g => g.name == name);
                if (olde != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد قسم سابق بهذا الأسم"); return status; }

                EmployeesType newItem = new EmployeesType() { name = name };
                dBContext.EmployeesTypes.Add(newItem);
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
                MessageBox.Show(temp.id.ToString());
                var item = dBContext.EmployeesTypes.FirstOrDefault(g => g.name == name && g.id != temp.id);
                if (item != null)
                { AppDialogAleart.showAleartPreExistingData("يوجد قسم سابق بهذا الأسم"); return status; }
                temp.name = name;
                dBContext.SaveChanges();
                status = true;
                AppDialogAleart.showAleartSuccess();
                temp = null;
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
                            dBContext.EmployeesTypes.Remove(temp);
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                        }
                        dBContext.SaveChanges();
                        transaction.Commit();
                        temp = null;
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
