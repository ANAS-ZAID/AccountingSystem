using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Classes;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

using AccountingSystem.view.SupScreens.AreaManagement;


namespace AccountingSystem.controller
{
    public class AreaController
    {  
    
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم المنطقه" ,"المدينه"};
        public BindingSource dataSource;
        public AccountingDbContext dbContext;
        public ProsessesType prosessesType { get; set; }
        public Area temp;
         dynamic allData;
        public List<City> allCity { get { return dbContext.Cities.ToList(); } set { } }
        public AreaController()
        {
            dataSource = new BindingSource();
            dbContext = new AccountingDbContext();
            temp = new Area();
            lodeData();
        }
        public void clearTempData()
        {
            temp = new Area();
            temp.City = null;
        }

        void lodeData()
        {
            clearTempData();
            try
            {
                allData = dbContext.Areas.AsNoTracking().OrderByDescending(x=>x.id).Include(x => x.City).Select(x => new
                {
                    id = x.id,
                    name = x.name,
                    city = x.City.name,
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
                dataTable.Rows.Add(item.id, item.name,item.city);
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                clearTempData() ;
                temp = dbContext.Areas
                    .Include(c => c.City).FirstOrDefault(e => e.id == id);

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
            if (model.LoginData.permissions["area"].viewPermission.Value)
            {
                string cityName = temp.City != null ? temp.City.name : "";
                try
                {
                    allData = dbContext.Areas.AsNoTracking().OrderByDescending(a => a.id).Include(x => x.City)
                        .Where(
                           x => DbFunctions.Like(x.name, "%" + name + "%") &&
                             DbFunctions.Like(x.City.name, "%" + cityName + "%")
                             ).
                      Select(x => new
                      {
                          id = x.id,
                          name = x.name,
                          city = x.City.name,

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
                var anyItem = dbContext.Areas.FirstOrDefault(x => x.name == temp.name);
                if (anyItem != null)
                { AppDialogAleart.showAleartError("توجد منطقه سابقه بهذا الأسم"); return false; }
                dbContext.Areas.Add(temp);
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
                var anyItem = dbContext.Areas.FirstOrDefault(x => x.name == temp.name && x.id != temp.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartError("توجد منطقه سابقه بهذا الأسم"); return false; }
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
            if (!ValidatingData.validatingData(temp.City, columnsNamesInAR[2], false))
                return false;
            //اسناد البيانات
            temp.name = name;
            temp.cityId = temp.City.id;
            //التعامل مع البيانات بناء على الواجهه التي أرسل المستخدم  منها البيانات
            if (prosessesType == ProsessesType.add)
                return add();
            else
                return update();
        }

        public bool delete(List<int> keys)
        {
            bool status = false;
            if (model.LoginData.permissions["area"].deletePermission.Value)
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
                            dbContext.Areas.Remove(temp);
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
        public void selectedCity(object city)
        {   if (city != null)
            temp.City = (City)city;
        }
       
      public  void showDialogUpdate(int id)
        {
            if (model.LoginData.permissions["area"].updatePermission.Value)
            {
                if (id != 0)
                {
                   
                    prosessesType = ProsessesType.update;
                    find(id);
                    DialogAddAndUpdateArea dialogAddBramch = new DialogAddAndUpdateArea(this);
                    dialogAddBramch.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");

            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        public void showDialogAdd() {
            if  (model.LoginData.permissions["area"].addPermission.Value)
            {
                clearTempData();
                prosessesType = ProsessesType.add;
                DialogAddAndUpdateArea dialogAddBramch = new DialogAddAndUpdateArea(this);
                dialogAddBramch.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        public void showDialogView(DataGridViewRow row)
        {
            if (model.LoginData.permissions["area"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(columnsNamesInAR, row);
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
    }
}
