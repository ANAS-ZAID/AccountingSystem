using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AccountingSystem.core.Classes;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.view.Screens.CurrencyManagement;
using AccountingSystem.view.SupScreens.ClassifyManagament;


namespace AccountingSystem.controller
{
    public class CurrencyController
    {
      
        public List<string> columnsNamesAr = new List<string> { "الرقم", "أسم العمله", "رمز العمله", "سعر التحويل", "نوع العمله" };
        string messageErrorType = "يمكن ان يكون في النظام عمله رئيسية واحه فقط";
        public ProsessesType prosessesType { get; set; }
        public Currency temp;
        public List<Currency> currencies { get { return dBContext.Currencies.ToList(); } }
        public BindingSource dataSource;
        AccountingDbContext dBContext;
        dynamic allData;

        public CurrencyController()
        {
            dBContext = new AccountingDbContext();

            dataSource = new BindingSource();
            lodeData();
        }
        public void clearTempData()
        {
            temp=new Currency();
        }
        public void lodeData()
        {
            clearTempData();
           allData=new List<dynamic>();
            try
            {
                allData = dBContext.Currencies.AsNoTracking().ToList().Select(c => new {
                    id = c.id,
                    name = c.name,
                    code = c.code,
                    exchangeRate = c.exchangeRate,
                    currencyType = c.currencyType,
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
            foreach (string name in columnsNamesAr)
            {
                dataTable.Columns.Add(name);
            }
            foreach (var item in allData)
            { 
                dataTable.Rows.Add(item.id, item.name,
                item.code, item.exchangeRate, item.currencyType);
            }
            dataSource.DataSource = dataTable;
        }
        public bool find(int id)
        {
            bool status = true;
            try
            {
                clearTempData();
                temp = dBContext.Currencies.FirstOrDefault(a => a.id == id);

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
            if (model.LoginData.permissions["currency"].viewPermission.Value)
            {
                try
                {
                    allData = dBContext.Currencies.AsNoTracking().
                        Where(
                            a => DbFunctions.Like(a.name, "%" + name + "%")).
                        Select(c => new
                        {
                            id = c.id,
                            name = c.name,
                            code = c.code,
                            exchangeRate = c.exchangeRate,
                            currencyType = c.currencyType,
                        }).ToList();
                    fillDataGridView();
                }
                catch
                {
                    AppDialogAleart.showAleartError();
                }
            }
        }
        public bool dataProcessing(string name, string code, string exchangeRate)
        {
            ///التحقق من البيانات
            if (!ValidatingData.validatingData(name, columnsNamesAr[1]))
                return false;
            if (!ValidatingData.validatingData(code, columnsNamesAr[2], false))
                return false;
            if (!ValidatingData.validatingData(exchangeRate, columnsNamesAr[3]))
                return false;     
            if (!ValidatingData.validatingData(temp.currencyType, columnsNamesAr[4]))
                return false;
            //اسناد البيانات
            temp.name = name;
            temp.code = code;
            temp.exchangeRate = decimal.Parse(exchangeRate);
            //التعامل مع البيانات بناء على الواجهه التي أرسل المستخدم  منها البيانات
            if (prosessesType == ProsessesType.add)
                return add();
            else
                return update();
        }
        public bool add()
        {

            bool status = false;

            try
            {
                var anyItem = dBContext.Currencies.FirstOrDefault(a => a.name == temp.name);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }
                anyItem = dBContext.Currencies.FirstOrDefault(a => a.currencyType == "رئيسية" );
                if (anyItem != null&&temp.currencyType == "رئيسية")
                { AppDialogAleart.showAleartErrorData(messageErrorType); return status; }
                dBContext.Currencies.Add(temp);
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


                var anyItem = dBContext.Currencies.FirstOrDefault(a => a.name == temp.name&&a.id!=temp.id);
                if (anyItem != null)
                { AppDialogAleart.showAleartPreExistingData(); return status; }
                anyItem = dBContext.Currencies.FirstOrDefault(a => a.currencyType == "رئيسية" && a.id != temp.id);
                if (anyItem != null && temp.currencyType == "رئيسية")
                { AppDialogAleart.showAleartErrorData(messageErrorType); return status; }
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

        public bool delete(List<int> keys)
        {
            bool status = true;

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
                            if (temp.JournalEntries.Any())
                            { AppDialogAleart.showAleartError($"لايمكنك حذف العمله {temp.name} لانها مرتبطه بعمليات ماليه"); return false; }
                            dBContext.Currencies.Remove(temp);
                            AppDialogAleart.showAleartSuccess();
                        }
                        dBContext.SaveChanges();
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
            else { AppDialogAleart.showAleart("لحذفها",MessageType.NoDataSpecified); }
             return status;
        }
        public void showDialogUpdate(int id)
        {
            if (model.LoginData.permissions["currency"].updatePermission.Value)
            {
                if (id != 0)
                {
                    prosessesType = ProsessesType.update;
                    find(id);
                    DialogAddCurrency dialog = new DialogAddCurrency(this);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleart("لتعديلها", MessageType.NoDataSpecified); ;

            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        public void showDialogAdd()
        {
            if (model.LoginData.permissions["currency"].addPermission.Value)
            {
                clearTempData();
                prosessesType = ProsessesType.add;
                DialogAddCurrency dialog = new DialogAddCurrency(this);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        public void showDialogView(DataGridViewRow row)
        {
            if (model.LoginData.permissions["currency"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(columnsNamesAr, row);
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
    }
}
