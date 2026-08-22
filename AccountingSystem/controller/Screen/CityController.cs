using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AccountingSystem.core.Classes;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

using AccountingSystem.view.SupScreens.CityManagement;

namespace AccountingSystem.controller
{
    public class CityController
    {
        AccountingDbContext dbContext;
        public BindingSource dataSource;
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم المدينه" };
        public City temp;
        dynamic allData;
       public ProsessesType prosessesType;
        public CityController()
        {
            dataSource = new BindingSource();
            dbContext = new AccountingDbContext();
            temp = new City();
            lodeData();
        }
        public void clearTempData()
        {
            temp = new City();
        }

        void lodeData()
        {
            clearTempData();
            try
            {
                allData = dbContext.Cities.AsNoTracking().OrderByDescending(x => x.id).Select(x => new
                {
                    id = x.id,
                    name = x.name,
                }).ToList();
            }
            catch
            {
                AppDialogAleart.showAleartError();
            }

            fillDataGridView();
        }

        void fillDataGridView()
        {

            var dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            foreach (var item in allData)
            {
                dataTable.Rows.Add(item.id, item.name);
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                clearTempData();
                temp = dbContext.Cities.FirstOrDefault(e => e.id == id);
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
            if (model.LoginData.permissions["city"].viewPermission.Value)
            {
                try
                {
                    allData = dbContext.Cities.AsNoTracking().OrderByDescending(a => a.id)
                        .Where(
                           x => DbFunctions.Like(x.name, "%" + name + "%") 
                             ).
                      Select(x => new
                      {
                          id = x.id,
                          name = x.name,
                      }).ToList();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        bool add()
        {
            bool status = true;
            try
            {
                var anyItem = dbContext.Cities.FirstOrDefault(x => x.name == temp.name);
                if (anyItem != null)
                { AppDialogAleart.showAleartError("توجد مدينه سابقه بهذا الأسم"); return false; }
                dbContext.Cities.Add(temp);
                dbContext.SaveChanges();
                AppDialogAleart.showAleartSuccess();
                lodeData();
            }
            catch
            {
                status = false;
                AppDialogAleart.showAleartError();
            }

            return status;
        }
        bool update()
        {
            bool status = true;
            try
            {
                var anyItem = dbContext.Cities.FirstOrDefault(x => x.name == temp.name && x.id != temp.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartError("توجد مدينه سابقه بهذا الأسم"); return false; }
                dbContext.SaveChanges();
                AppDialogAleart.showAleartSuccess();
                lodeData();
            }
            catch
            {
                status = false;
                AppDialogAleart.showAleartError();
            }

            return status;
        }
        public bool dataProcessing(string name)
        {
            ///التحقق من البيانات
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            //اسناد البيانات
            temp.name = name;
            
            //التعامل مع البيانات بناء على الواجهه التي أرسل المستخدم  منها البيانات
            if (prosessesType == ProsessesType.add)
                return add();
            else
                return update();
        }

        public bool delete(List<int> keys)
        {
            bool status = false;
            if (model.LoginData.permissions["city"].deletePermission.Value)
            {
                if (keys.Count > 0)
                {
                    if (AppDialogAleart.showAleartConfirmation("هل أنت متأكد انك ترغب في حذف البيانات المحدده وعددها: " + keys.Count) != DialogResult.OK)
                        return false;
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var id in keys)
                            {
                                if (!find(id))
                                    throw new Exception("حدث خطأ ما في العمليه ");
                                dbContext.Cities.Remove(temp);
                                dbContext.SaveChanges();
                            }
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                            transaction.Commit();
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
            }
            else AppDialogAleart.showAleartNoPermissions();


            return status;
        }
     
        public void showDialogUpdate(int id)
        {
            if (model.LoginData.permissions["city"].updatePermission.Value)
            {
                if (id != 0)
                {
                    prosessesType = ProsessesType.update;
                    find(id);
                    DialogAddAndUpdateCity dialogAddBramch = new DialogAddAndUpdateCity(this);
                    dialogAddBramch.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");

            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        public void showDialogAdd()
        {
            if (model.LoginData.permissions["city"].addPermission.Value)
            {
                clearTempData();
                prosessesType = ProsessesType.add;
                DialogAddAndUpdateCity dialogAddBramch = new DialogAddAndUpdateCity(this);
                dialogAddBramch.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        public void showDialogView(DataGridViewRow row)
        {
            if (model.LoginData.permissions["city"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(columnsNamesInAR, row);
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

    }
}
