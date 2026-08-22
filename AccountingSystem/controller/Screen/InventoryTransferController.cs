using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;

using AccountingSystem.NewModel.EFModel;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using AccountingSystem.view.SupScreens.InventoryTransferManagament;
using AccountingSystem.view.SupScreens.SalesSystem;

namespace AccountingSystem.controller.Screen
{
    public class InventoryTransferController : DBContextController
    {
        public DateTime? startDate;
        public DateTime? endDate;
        public Store fromStore;
        public Store toStore;
        public AppTable detailTable;
        List<InventoryTransferDetail> newTransferDetails;
        public AppTable newDetailTable {  set {
                newTransferDetails = new List<InventoryTransferDetail>();
                for (int i = 0; i < value.Rows.Count; i++)
                {
                    var row = value.Rows[i];
                    var item = (Classify)value[i, "الصنف"].CombBox?.SelectedItem??null;
                    var unit = value[i, "الوحده"];
                    var unitPrice = value[i, "سعر الوحده"].value.ToDecimal();
                    var quantity = value[i, "الكميه"].value.ToDecimal();
                    var description = value[i, "ملاحضات"].value;
                    if(item != null&&item.id!=0)
                    {
                        int measurementItemId = int.Parse(unit.id);
                        var data = new InventoryTransferDetail() { itemId = item.id, measurementItemId = measurementItemId, unitPrice = unitPrice, quantity = quantity, description = description };
                        //if (IsUpdate&& row.id.HasValue)
                        //    data.id = row.id.Value;

                        newTransferDetails.Add(data);
                        addCompositeItems(measurementItemId, quantity);
                    }
                }

            } }
        void addCompositeItems(int measurementItemId, decimal quantity)
        {
            var compositeItems = dBContext.CompositeItems.Where(x => x.componentItemId ==measurementItemId);
            foreach (var compositeItem in compositeItems)
            {
                newTransferDetails.Add(new InventoryTransferDetail() { quantity = compositeItem.quantity * quantity, unitPrice = compositeItem.purchasePrice,
                    measurementItemId = compositeItem.measurementItemId, itemId = compositeItem.ComponentItem.itemId,main=false,
                    type = MeasurementsItemType.مركب.ToString(),
                });
            }
        }
        public BindingSource dataSourceDetail = new BindingSource();
        public List<Store> storeList { get { return dBContext.Stores.ToList(); } }
        public List<Currency> currencies { get { return dBContext.Currencies.ToList(); } }
        public Classify item;
        public InventoryTransferDetail selectedDetail;
        public Dictionary<int,InventoryTransferDetail> selectedDetails;
        public InventoryTransfer temp;
        List<JournalEntry> tempJournalEntries;
        public DateTime? lastDate;
        public   InventoryTransferController()
        {
            permissions = model.LoginData.permissions["inventoryTransfer"];
            columnsNamesInAR = new List<string>() { "الرقم", "رقم السند", "التأريخ", "من مخزن", "الى مخزن", "الإجمالي", "العمله", "وقت الإضافة", "وقت التحديث" };
            transactionType = TransactionType.تحويل_مخزني.ToString();
            detailTable = new AppTable();
            dataSource.DataSource = typeof(InventoryTransfer);
            lodeData();
        }
        public string VoucherNumber
        {
            get
            {
               
                int? newNum = temp.number;

                if (IsAdd)
                {
                    Random random = new Random(100000);
                    b:
                    newNum = random.Next(1, 100000);
                    var any = dBContext.InventoryTransfers.Where(x => x.number == newNum);
                    if (any.Any())
                        goto b;
                }

                return newNum.ToString();
            }
        }
       
        protected void fillDetailTable()
        {
            bool status = true;
            detailTable.Rows = new List<AppRow>();
            foreach (var detail in temp?.InventoryTransferDetails?.Where(x=>x.main))
            {
                if (!newRow(detail)) { status= false; break; }
            }
            if (status)
                dataSourceDetail.DataSource = detailTable;
            else AppDialogAleart.showAleartError();
        }
         bool newRow(InventoryTransferDetail detail)
        {

            if (detail.item == null)
                detail.item=dBContext.Classifies.FirstOrDefault(x=>x.id==detail.itemId);
            if (detail.MeasurementsItem == null)
                detail.MeasurementsItem = dBContext.MeasurementsItems.FirstOrDefault(x => x.id == detail.measurementItemId);
            if (detail.item == null|| detail.MeasurementsItem == null)
                return false;
           var items = new Classify[1];
            if(detail.item.IsSupItem())
           items = copySupItems.Swap(detail.item, 0);
            else
                items=copySupItems;


            AppRow row = new AppRow()
            {
                id=detail.id,
                Cells = new List<AppCell>()
            {
            new AppCell() {value = detail.item?.ClassifyNumber?.ToString(), },
            new AppCell() {id=detail.item?.id.ToString(), CombBox=new AppTableCombBox(){ DataSource=items,SelectedItem=detail.item} },
            new AppCell() {id=detail.MeasurementsItem.id.ToString(),CombBox=new AppTableCombBox(){ DataSource=new Unit[1]{ detail.MeasurementsItem.Unit},SelectedItem=detail.MeasurementsItem.Unit,Tag=detail.MeasurementsItem} },
            new AppCell() {value=detail.quantity.Format() },
            new AppCell() {value=detail.unitPrice.Format() },
            new AppCell() {value=detail.unitPrice.Format() },
            new AppCell() {value=detail.description},
            }
            };
         detailTable.Rows.Add(row);
            return true;
        }
        public Classify[] copySupItems { get => dBContext.SupItemsWithEmpty(); }
        public Unit[] copyUnits{get=>dBContext.UnitsWithEmpty();}
    

        internal AppRow newRow(Classify item)
        {
            var measurement = measurementSelectItem(item);
            if (measurement == null)
                return null;
             var items= copySupItems.Swap(item, 0);
            
            AppRow row = new AppRow() { Cells=new List<AppCell>()
            {
            new AppCell() {value = item.ClassifyNumber?.ToString(), },
            new AppCell() {id=item.id.ToString(), CombBox=new AppTableCombBox(){ DataSource=items,SelectedItem=item} },
            new AppCell() {id=measurement.id.ToString(),CombBox=new AppTableCombBox(){ DataSource=new Unit[1]{measurement.Unit},SelectedItem=measurement.Unit,Tag=measurement} },
            new AppCell() {value="1" },
            new AppCell() {value=measurement.sellingPrice.Format() },
            new AppCell() {value=measurement.sellingPrice.Format() },
            new AppCell() {},
            }
            };
            return row;
        }
        public string ExchangeRate
        {
            get { return (IsAdd ? temp.Currency?.exchangeRate: temp.exchangeRate).ToString(); }   
        }
        public bool CurrentCurrencyIsMain
        {
            get { return temp?.Currency?.isMain()??true; }
        }
        public override void clearTempData()
        {
            var currency= dBContext.Currencies?.ToList().FirstOrDefault(x => x.isMain()) ?? null;
            temp = new InventoryTransfer() { date=DateTime.Now,Currency= currency, currencyId=currency?.id};
        }
        public  void clearHomeTempData()
        { 
            startDate=null; endDate=null;
            fromStore=null;
            toStore=null;
        }
            public  bool dataProcessing(string number,string exchangeRate,AppTable data)
        {
            decimal newExchangeRate = 1;
         
            if (!ValidatingData.validatingData(number, columnsNamesInAR[1]))
             return false;
            if (!ValidatingData.validatingData(temp.FromStore, " المخزن الذي سيتم التحويل منه", false))
                return false;
            if (!ValidatingData.validatingData(temp.Currency, "العمله", false))
                return false;
            if (!ValidatingData.validatingData(temp.date,"التأريخ", false))
                return false;
            if (!ValidatingData.validatingData(temp.ToStore, " المخزن الذي سيتم التحويل اليه", false))
                return false;
            if (!CurrentCurrencyIsMain)
            {
                if (!ValidatingData.validatingData(exchangeRate, "سعر التحويل"))
                    return false;
                newExchangeRate = decimal.Parse(exchangeRate);
            }
            else newExchangeRate = temp.Currency.exchangeRate.Value;
            if(temp.fromStoreId==temp.toStoreId)
            {
                AppDialogAleart.showAleartErrorData("لايمكنك التحويل الى نفس المخزن");
                return false;
            }
            newDetailTable = data;
            if (!newTransferDetails.Any())
                {
                    AppDialogAleart.showAleartErrorData("لم يتم تحديد أي صنف ");
                    return false;
                }
            int newNumber =int.Parse(number);
            if (IsAdd)
            { temp.enteryDate= DateTime.Now;lastDate = temp.date; temp.employeeId = EmployId;
                temp.brancheId = BranchId;
            }
            else temp.updateDate = DateTime.Now;
            decimal total = newTransferDetails.Total();
            JournalEntry journalEntryFromStore = new JournalEntry() { transactionId = newNumber, accountId = temp.FromStore.accountId, currencyId = temp.currencyId
                , ExchangeRate = newExchangeRate, transactionType = transactionType, transactionDate = temp.date, credit = total, debit = 0, description = "لكم تحويل مخزني من مخزن:" + temp.ToStore.name };
            JournalEntry journalEntryToStore = new JournalEntry() { transactionId = newNumber, accountId = temp.ToStore.accountId, currencyId = temp.currencyId, ExchangeRate = newExchangeRate, transactionType = transactionType, transactionDate = temp.date, credit = 0, debit = total, description = "عليكم تحويل مخزني من مخزن:" + temp.FromStore.name };
            tempJournalEntries=new List<JournalEntry>() { journalEntryFromStore,journalEntryToStore};
            temp.exchangeRate = newExchangeRate;
           this.newNumber = newNumber;
            if (IsAdd)
                return add();
            if(IsUpdate)
                return update();

            return false;
        }
        public override bool add()
        {
            bool status = false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {
                    var any = dBContext.InventoryTransfers?.FirstOrDefault(x => x.number == newNumber);
                    if (any != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("يوجد سند سابق بهذا الرقم");
                        return false;
                    }
                    dBContext.JournalEntries.AddRange(tempJournalEntries);
                    temp.number = newNumber;
                    //temp.InventoryTransferDetails = null;
                    //temp.id = 0;
                    temp.InventoryTransferDetails = newTransferDetails;
                    dBContext.InventoryTransfers.Add(temp);
                    dBContext.SaveChanges();
                    transaction.Commit();
                    temp=new InventoryTransfer() { fromStoreId=temp.fromStoreId,FromStore=temp.FromStore,toStoreId=temp.toStoreId,ToStore=temp.ToStore,date=temp.date,Currency=temp.Currency,currencyId=temp.currencyId};
                    status = true;

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

            return status;
        }
        int newNumber;
        public override bool update()
        {
            bool status = false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {
                try
                {

                    var any = dBContext.InventoryTransfers?.FirstOrDefault(x => x.number == newNumber && x.id != temp.id);
                    if (any != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("توجد سند سابقه بهذا الرقم");
                        return false;
                    }
                   
                     
                    dBContext.JournalEntries.RemoveRange(dBContext.JournalEntries.Where(x => x.transactionId == temp.number && x.transactionType == transactionType));
                    dBContext.SaveChanges();
                     dBContext.JournalEntries.AddRange(tempJournalEntries);
                    dBContext.SaveChanges();
        
                    //dBContext.SaveChanges();
                    //temp.InventoryTransferDetails.ToList().ForEach(x => {
                    //    var anyS = newDetails.FirstOrDefault(a => a.id == x.id);
                    //    if (anyS == null)
                    //    {
                    //        AppDialogAleart.showAleartNoPermissions(x.item.nameAr);
                    //        dBContext.InventoryTransferDetails.Remove(x);
                    //        dBContext.SaveChanges();
                    //    }
                    //});
                    //temp.InventoryTransferDetails = newDetails;
                    //dBContext.InventoryTransferDetails.AddOrUpdate(temp.InventoryTransferDetails.ToArray());
                    dBContext.InventoryTransferDetails.RemoveRange(temp.InventoryTransferDetails);
                    dBContext.SaveChanges();
                    temp.InventoryTransferDetails = newTransferDetails;
                    temp.number = newNumber;
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
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

            return status;
        }
        public override bool delete(List<int> keys)
        {
            bool status = false;

            if (permissions.deletePermission.Value)
            {

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
                                    throw new Exception("حدث خطأ ما في العمليه ");

                                dBContext.JournalEntries.RemoveRange(dBContext.JournalEntries.Where(x => x.transactionId == temp.number && x.transactionType == transactionType));
                                dBContext.InventoryTransferDetails.RemoveRange(temp.InventoryTransferDetails);
                                dBContext.InventoryTransfers.Remove(temp);

                            }
                            status = true;
                            AppDialogAleart.showAleartSuccess();
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
                else
                {

                    AppDialogAleart.showAleartErrorData("لم تقم بتحديد اي بيانات للحذف");
                }
            }
            else AppDialogAleart.showAleartNoPermissions();

            return status;
        }
        public void selectFromStore(object store)
        {
            if(HasAddAndUpdateScreenDataProcessed)
            {
                temp.FromStore = (Store)store ?? null;
                temp.fromStoreId = temp.FromStore?.id;
            }
            else if(HasHomeScreenDataProcessed)
                fromStore = (Store)store ?? null;

        }
        public void selectToStore(object store)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.ToStore = (Store)store ?? null;
                temp.toStoreId = temp.ToStore?.id;
            }
            else if (HasHomeScreenDataProcessed)
                toStore = (Store)store ?? null;

        }
        public void selectCurrency(object currency)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.Currency = (Currency)currency ?? null;
                temp.currencyId = temp.Currency?.id;
            }
        }
        public void selectDate(DateTime date)
        {
            if (HasAddAndUpdateScreenDataProcessed)
                temp.date = date;
        }
        public void selectStartDate(DateTime? date)
        {
            if (HasHomeScreenDataProcessed)
                startDate = date;
        }
        public void selectEndtDate(DateTime? date)
        {
            if (HasHomeScreenDataProcessed)
                endDate = date;
        }
        public override bool find(int id)
        {
           
            bool status = true;
            try
            {

                    temp = dBContext.InventoryTransfers?.FirstOrDefault(i => i.id == id);
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

        public override bool lodeData(bool shearch = false, dynamic additional = null)
        {
            if (permissions.viewPermission.Value)
            {
                tempData = new List<dynamic>();
                try
                {
                  var allData=dBContext.InventoryTransfers.OrderByDescending(x=>x.id).ToList();
                    if (shearch)
                        allData = allData.Where(x => (String.IsNullOrEmpty(additional) || x.number.ToString().Contains(additional)) && (x.date.Between(startDate, endDate) && (fromStore == null || x.fromStoreId == fromStore.id) && (toStore == null || x.toStoreId == toStore.id))).ToList();
                    tempData = allData.Select(x => new { id=x.id, number=x.number,date = x.date,fromStore= x.FromStore.name,toStore=x.ToStore.name,total=x.TotalPrice(), currency=x.Currency.name,enteryDate = x.enteryDate,updateDate = x.updateDate}).ToList<dynamic>();
                    fillDataGridView();

                }
                catch
                {

                
                    AppDialogAleart.showAleartError();
                }
                
            }
            return tempData?.Any()??false;
        }

        public override void showDialogAdd()
        {
            if (permissions.addPermission.Value)
            {
             
                clearTempData();
                fillDetailTable();
                prosessesType = ProsessesType.add;
                DialogAddAndUpdateInventoryTransfer dialog = new DialogAddAndUpdateInventoryTransfer(this);
                dialog.ShowDialog();
                endHADSDP();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }

        public override void showDialogUpdate(int id)
        {
            if (permissions.updatePermission.Value)
            {
                if (id != 0)
                {

                    prosessesType = ProsessesType.update;
                    if (find(id))
                    {
                        //  Program.homeScereen().Hide();
                        fillDetailTable();
                        DialogAddAndUpdateInventoryTransfer dialog = new DialogAddAndUpdateInventoryTransfer(this);
                        dialog.ShowDialog();
                        endHADSDP();
                    }
                }
                else AppDialogAleart.showAleartErrorData("لم تقم بتحديد أي بيانات لتعديلها");

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

        protected override void fillDataGridView()
        {
            dataTable=new System.Data.DataTable();
            foreach (var item in columnsNamesInAR)
            {
                dataTable.Columns.Add(item);
            }
            Thread thread = new Thread(fillTableData);
            thread.Start();
        }

        protected override void fillTableData()
        {
            foreach (var item in tempData)
            {
                dataTable.Rows.Add(item.id, item.number, item.date,
                item.fromStore, item.toStore, item.total, item.currency, ((DateTime?)item.enteryDate).Format(), ((DateTime?)item.updateDate).Format());//((DateTime)item.enteryDate).Format(),((DateTime)item.updateDate).Format()
            }
            dataSource.DataSource = dataTable;
        }

        internal MeasurementsItem measurementSelectItem(object selectedItem)
        {
            MeasurementsItem measurement = null;
            if (HasAddAndUpdateScreenDataProcessed)
            {
                measurement = DialogSelecteMeasurementsItem.DialogSelectedMeasurementsItem(selectedItem);
              
            }
            return measurement;
        }

       
    }
}
