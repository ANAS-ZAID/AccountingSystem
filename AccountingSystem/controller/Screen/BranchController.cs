using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;





namespace AccountingSystem.controller
{
    public class BranchController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم الفرع", "أسم المدير","رقم الهاتف", "المدينه ", "المنطقه", " العنوان","المخزن" };
        public BindingSource dataSource;
       public AccountingDbContext dbContext;
        public ProsessesType prosessesType { get; set; }
        public Branch temp;
        public dynamic allBranches;
        public List<City> allCity { get { return dbContext.Cities.ToList(); } set { } }
        public List<Area> allArea { get { return dbContext.Areas.ToList(); } set { } }
        public List<Store> allStore { get { return dbContext.Stores.ToList(); } set { } }

        public BranchController()
        {
            dataSource = new BindingSource();
            dbContext = new AccountingDbContext();
            temp = new Branch();
            lodeData();
        }
      public void clearTempData()
        {
            temp=new Branch();
            temp.Area = null;
            temp.Store = null;
            temp.City = null;
        }
       
    void lodeData()
        {
            clearTempData();
            try
            {
                allBranches = dbContext.Branches.AsNoTracking().Include(x => x.City).Include(x => x.Area).Include(x => x.Store).Select(x => new
                {
                    id = x.id,
                    name = x.name,
                    administratorName = x.administratorName,
                    phoneNumber = x.phoneNumber,
                    city = x.City.name,
                    area = x.Area.name,
                    address = x.address,
                    store = x.Store != null ? x.Store.name : null,
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
            foreach (var employee in allBranches)
            {
                dataTable.Rows.Add(employee.id, employee.name,
                employee.administratorName, employee.phoneNumber, employee.city,
                employee.area, employee.address, employee.store);
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                temp = new Branch();
                temp = dbContext.Branches.Include(c => c.Area)
                    .Include(c => c.City).Include(c => c.Store).FirstOrDefault(e => e.id == id);

               if(temp == null)
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
            if (model.LoginData.permissions["branch"].viewPermission.Value)
            {
                try
                {
                    allBranches = dbContext.Branches.AsNoTracking().OrderByDescending(a => a.id).Include(x => x.City).Include(x => x.Area).Include(x => x.Store)
                        .Where(
                            a => DbFunctions.Like(a.name, "%" + name + "%")
                             ).
                      Select(x => new
                      {
                          id = x.id,
                          name = x.name,
                          administratorName = x.administratorName,
                          phoneNumber = x.phoneNumber,
                          city = x.City.name,
                          area = x.Area.name,
                          address = x.address,
                          store = x.Store != null ? x.Store.name : null,
                      }).ToList();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
         bool add()
        {  bool status=true;
            try
            {
                var anyItem= dbContext.Branches.FirstOrDefault(x=>x.name== temp.name);
                if (anyItem != null)
                {AppDialogAleart.showAleartError("يوجد فرع سابق بهذا الأسم"); return false;}
                 dbContext.Branches.Add(temp); 
                dbContext.SaveChanges();
                AppDialogAleart.showAleartSuccess();
                lodeData();
            }
            catch
            {
                status = false;
                AppDialogAleart.showAleartError() ;
            }
         
            return status;
        } 
        bool update()
        {
           bool status=true;
            try
            {
                var anyItem= dbContext.Branches.FirstOrDefault(x=>x.name==temp.name&& x.id!=temp.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartError("يوجد فرع سابق بهذا الأسم"); return false; }
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
     public   bool dataProcessing(string name, string administratorName, string phoneNumber, string address)
        {
            ///التحقق من البيانات
            if (!ValidatingData.validatingData(name, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(temp.City, columnsNamesInAR[4], false))
                return false;
            if (!ValidatingData.validatingData(temp.Area, columnsNamesInAR[5], false))
                return false;
            //اسناد البيانات
            temp.name = name;
            temp.administratorName = administratorName;
            temp.phoneNumber = phoneNumber;
            temp.address = address;
            temp.cityId = temp.City.id;
            temp.areaId = temp.Area.id;
            temp.storeId = temp.Store?.id;
            //التعامل مع البيانات بناء على الواجهه التي أرسل المستخدم  منها البيانات
            if (prosessesType == ProsessesType.add)
                return add();
            else
           return update();
        }

        public bool delete(List<int> keys)
        {
            bool status = false;
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
                            dbContext.Branches.Remove(temp);
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


            return status;
        }
        public void selectedCity(object city)
        {if(city != null)
            temp.City=(City)city;
        } public void selectedArea(object area)
        {if (area != null)
            temp.Area=(Area)area;
        } 
        public void selectedStorey(object store)
        {if(store != null)
            temp.Store=(Store)store;
        }

      
    }
}
