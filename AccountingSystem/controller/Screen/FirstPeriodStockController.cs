using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;
using Microsoft.EntityFrameworkCore;
using AccountingSystem.core.shared;
using System.Data;
using System.Threading;
using System.Data.Entity.Migrations;
using AccountingSystem.view.Screens.CurrencyManagement;
using AccountingSystem.controller.Screen;
using Guna.UI2.WinForms;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using System.Data.Entity;


namespace AccountingSystem.controller
{
    public class FirstPeriodStockController : DBContextController
    {
        public BindingSource dataSourceInventory;
        //public List<AppColumn> viewColumns;
        public AppTable viewTable;
        BeginningInventory temp;
        Classify selectedItem;
        Store store;
        List<Classify> tempItemsAndInventories;
        public List<Store> stores { get { return dBContext.Stores.OrderBy(x => x.id).ToList(); } }
        public List<Classify> items { get { return dBContext.Classifies.SupItems(); } }
        //List<BeginningInventory> tempInventories;
        public Dictionary<string, string[]> dataColumns { get; set; }
        public List<Classify> itemsWithAllData
        { get { return dBContext.Classifies.AsNoTracking().ToList(); } }

        public FirstPeriodStockController()
        {
            permissions = model.LoginData.permissions["currency"];
            columnsNamesInAR = new List<string> { "الرقم", "رقم الصنف", "الصنف", "الباركود", "الوحده", "المخزن", "الكميه" };
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            dataSourceInventory = new BindingSource();
            dataSourceInventory.DataSource = typeof(AppTable);
            HasHomeScreenDataProcessed = false;
            tempItemsAndInventories = new List<Classify>();
            dataSource.DataSource = typeof(BeginningInventory);
            lodeData();
        }
        override public void clearTempData() { }
        override public bool lodeData(bool shearch = false,dynamic dynamic=null)
        {

            tempData = new List<dynamic>();
            try
            {
                var beginningInventories = dBContext.BeginningInventories?.ToList();
                if (shearch)
                    beginningInventories = beginningInventories?.Where(x => (store == null || x.storeId == store.id) && (selectedItem == null || x.item.id == selectedItem.id))?.ToList();
                tempData.AddRange(beginningInventories?.Select(b => new { id = b.id, barcode = b.MeasurementsItem.barcode, item = b.item.nameAr, store = b.Store.name, number = b.item.ClassifyNumber.Value, unit = b.MeasurementsItem.Unit.name, quantity = b.quantity, })?.ToList());
                fillDataGridView();

            }
            catch
            {

                AppDialogAleart.showAleartError();
            }
            return tempData.Any();
        }
        override protected void fillDataGridView()
        {
            dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            Thread thread = new Thread(fillTableData);
            thread.Start();
        }

        override protected void fillTableData()
        {
            foreach (var item in tempData)
            {
                dataTable.Rows.Add(item.id, item.number, item.item,
                item.barcode, item.unit, item.store, item.quantity);
            }
            dataSource.DataSource = dataTable;
        }
        override public bool find(int id)
        {
            bool status = true;
            try
            {

                temp = dBContext.BeginningInventories.FirstOrDefault(a => a.id == id);
                if (temp == null)
                    throw new Exception();
                tempItemsAndInventories = new List<Classify>() { temp.item };
         

            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
         public bool dataProcessing()
        {
            bool status = true;
            return status;
        }
        public override bool add()
        {
            throw new NotImplementedException();
        }
        public override bool update()
        {
            throw new NotImplementedException();
        }
        override public bool delete(List<int> keys)
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
                      
                            dBContext.BeginningInventories.Remove(temp);

                        }
                        dBContext.SaveChanges();
                        transaction.Commit();
                        AppDialogAleart.showAleartSuccess();
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
            else { AppDialogAleart.showAleart("لحذفها", MessageType.NoDataSpecified); }
            return status;
        }
        protected void fillDataInventory()
        {
            int height = 40;
            viewTable = new AppTable() { Rows=new List<AppRow>()};
          

            viewTable.Columns = new List<AppColumn>()
       {
            new AppColumn(){caption="رقم الصنف",ValueType=typeof(int),ReadOnly=true ,Size=new System.Drawing.Size(150,height)},
                new AppColumn(){caption="أسم الصنف",ValueType=typeof(string),ReadOnly=true, Size = new System.Drawing.Size(200, height)},
                new AppColumn(){caption="الوحده",ValueType=typeof(string),ReadOnly=true, Size = new System.Drawing.Size(150, height)},
                new AppColumn(){caption="سعر الشراء",ValueType=typeof(decimal),Size=new System.Drawing.Size(150,height)},
       };

            foreach (Store store in stores)
            {
                viewTable.Columns.Add(new AppColumn() { id = store.id.ToString(), caption = store.name, ValueType = typeof(decimal), Size = new System.Drawing.Size(150, height) });
            }
            fillTableDataInventory();
            //Thread thread = new Thread(fillTableDataInventory);
            //thread.IsBackground = true;
            //thread.Start();
        }

        protected void fillTableDataInventory()
        {
            if (prosessesType == ProsessesType.add)
            {
                tempItemsAndInventories = items;
            }
            foreach (var item in tempItemsAndInventories)
            {
               
                AppRow row = new AppRow();

                foreach (var measurements in item.MeasurementsItems)
                {
                    row.Cells.Add(new AppCell() {id=item.id.ToString(),name= item.id.ToString(), value= item .ClassifyNumber.ToString(),});
                    row.Cells.Add(new AppCell() {id=item.id.ToString(),name= item.id.ToString(), value= item .nameAr,});
                    row.Cells.Add(new AppCell() {id= measurements.id.ToString(),name= measurements.id.ToString(), value= measurements.Unit?.name, });
                    row.Cells.Add(new AppCell() {value= measurements?.BeginningInventories?.FirstOrDefault()?.unitPrice?.Format(), });
                    foreach (var store in stores)
                    {
                        var inventory = measurements?.BeginningInventories?.FirstOrDefault(x => x.storeId == store.id);
                        row.Cells.Add(new AppCell() { id = store.id.ToString(), name = store.id.ToString(), value = inventory?.quantity?.Format(),caption="كمية " });

                    }
                    viewTable.Rows.Add(row);
                }

            }
            dataSourceInventory.DataSource = viewTable;
        }
        public void selectStore(object value)
        {
            if (HasHomeScreenDataProcessed)
                store = value == null ? null : (Store)value;
        }
        public void selectItem(object value)
        {
            if (HasHomeScreenDataProcessed)
                selectedItem = value == null ? null : (Classify)value;
        }

        public bool neDataTable(AppTable table)
        {
            bool status = true;
            var itemColumn = table["أسم الصنف"];
            var unitColumn = table["الوحده"];
            var priceColumn = table["سعر الشراء"];
            if (!items.Any() && !stores.Any())
            { AppDialogAleart.showAleartError("لايوجد أي صنف ومخزن في النظام!"); return false; }
            if (!items.Any())
            { AppDialogAleart.showAleartError("لايوجد أي صنف  في النظام!"); return false; }

            if (!stores.Any())
            { AppDialogAleart.showAleartError("لايوجد أي مخزن في النظام!"); return false; }

            DbContextTransaction transaction = dBContext.Database.BeginTransaction();
            try
            {

                for (int i = 3; i < viewTable.Columns.Count; i++)
                {
                    string key = viewTable.Columns[i].id;
                    var store = stores.FirstOrDefault(x => x.id.ToString() == key);
                    if (store != null)
                    {
                        var storeColumn = table[viewTable.Columns[i].caption];
                        for (global::System.Int32 j = 0; j < storeColumn.Cells.Count; j++)
                        {
                            decimal fieldQuantity = (storeColumn.Cells[j].value).ToDecimal();
                            decimal? fieldPrice = priceColumn.Cells[j].value.ToDecimal();
                            if (String.IsNullOrEmpty(priceColumn.Cells[j].value))
                                fieldPrice = null;
                            string fieldItem = itemColumn.Cells[j].id;
                            string fieldMeasurementItem = unitColumn.Cells[j].id;

                            var item = items.FirstOrDefault(x => x.id.ToString() == fieldItem);
                            var beginningInventory = store.BeginningInventories.FirstOrDefault(x => x.MeasurementsItem?.id.ToString() == fieldMeasurementItem && x.item.id.ToString() == fieldItem);
                            if (beginningInventory != null)
                            {
                                beginningInventory.quantity = fieldQuantity;
                                beginningInventory.unitPrice = fieldPrice;
                                if (beginningInventory.updateDate != null)
                                    beginningInventory.updateDate = DateTime.Now;

                            }
                            else
                            {
                                //var measurementItemId = item?.MeasurementsItems.FirstOrDefault(x => x.Unit.name == fieldUnit).id;
                                beginningInventory = new BeginningInventory() { itemId = item.id, measurementItemId = int.Parse(fieldMeasurementItem), storeId = store.id, employeeId = model.LoginData.employee.id, brancheId = model.LoginData.branch.id, unitPrice = fieldPrice, quantity = fieldQuantity };
                                // dBContext.BeginningInventories.Add(beginningInventory);
                            }
                            dBContext.BeginningInventories.AddOrUpdate(beginningInventory);
                            dBContext.SaveChanges();
                        }
                    }
                }
                transaction.Commit();
                AppDialogAleart.showAleartSuccess();
                lodeData();
            }
            catch (DbEntityValidationException ex)
            {
                transaction.Rollback();
                AppDialogAleart.showEntityValidationErrors(ex);
                AppDialogAleart.showAleartError();
                status = false;
            }
            return false;
        }
        public bool fillDataColumns(AppColumn[] dataColumns)
        {
            bool status = true;
            var itemColumn = dataColumns[1];
            var unitColumn = dataColumns[2];
            var priceColumn = dataColumns[3];
            if (!items.Any() && !stores.Any())
            { AppDialogAleart.showAleartError("لايوجد أي صنف ومخزن في النظام!"); return false; }
            if (!items.Any())
            { AppDialogAleart.showAleartError("لايوجد أي صنف  في النظام!"); return false; }

            if (!stores.Any())
            { AppDialogAleart.showAleartError("لايوجد أي مخزن في النظام!"); return false; }

            DbContextTransaction transaction = dBContext.Database.BeginTransaction();
            try
            {

                for (int i = 3; i < viewTable.Columns.Count; i++)
                {
                    string key = viewTable.Columns[i].id;
                    var store = stores.FirstOrDefault(x => x.id.ToString() == key);
                    if (store != null)
                    {
                        var storeColumn = dataColumns[i];
                        for (global::System.Int32 j = 0; j < storeColumn.Cells.Count; j++)
                        {
                            decimal fieldQuantity = (storeColumn.Cells[j].value).ToDecimal();
                            decimal? fieldPrice = priceColumn.Cells[j].value.ToDecimal();
                            if (String.IsNullOrEmpty(priceColumn.Cells[j].value))
                                fieldPrice = null;
                            string fieldItem = itemColumn.Cells[j].id;
                            string fieldMeasurementItem = unitColumn.Cells[j].id;

                            var item = items.FirstOrDefault(x => x.id.ToString() == fieldItem);
                            var beginningInventory = store.BeginningInventories.FirstOrDefault(x => x.MeasurementsItem?.id.ToString() == fieldMeasurementItem && x.item.id.ToString() == fieldItem);
                            if (beginningInventory != null)
                            {
                                beginningInventory.quantity = fieldQuantity;
                                beginningInventory.unitPrice = fieldPrice;
                                if (beginningInventory.updateDate != null)
                                    beginningInventory.updateDate = DateTime.Now;

                            }
                            else
                            {
                                //var measurementItemId = item?.MeasurementsItems.FirstOrDefault(x => x.Unit.name == fieldUnit).id;
                                beginningInventory = new BeginningInventory() { itemId = item.id, measurementItemId = int.Parse(fieldMeasurementItem), storeId = store.id, employeeId = model.LoginData.employee.id, brancheId = model.LoginData.branch.id, unitPrice = fieldPrice, quantity = fieldQuantity };
                                // dBContext.BeginningInventories.Add(beginningInventory);
                            }
                            dBContext.BeginningInventories.AddOrUpdate(beginningInventory);
                            dBContext.SaveChanges();
                        }
                    }
                }
                transaction.Commit();
                AppDialogAleart.showAleartSuccess();
                lodeData();
            }
            catch(DbEntityValidationException ex)
            {
                transaction.Rollback();
                AppDialogAleart.showEntityValidationErrors(ex);
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        override public void showDialogUpdate(int id)
        {
            if (permissions.updatePermission.Value)
            {
                if (id != 0)
                {
                    prosessesType = ProsessesType.update;
                    if (!find(id))
                        return;
                    fillDataInventory();
                    DialogAddOrUpdateFirstPeriodStock dialog = new DialogAddOrUpdateFirstPeriodStock(this);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleart("لتعديلها", MessageType.NoDataSpecified); ;

            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        override public void showDialogAdd()
        {
            if (permissions.addPermission.Value)
            {
                clearTempData();
                prosessesType = ProsessesType.add;
                fillDataInventory();
                DialogAddOrUpdateFirstPeriodStock dialog = new DialogAddOrUpdateFirstPeriodStock(this);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        public override void showDialogView(DataGridViewRow row)
        {
            if (permissions.viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(columnsNamesInAR, row);
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
    }
}
