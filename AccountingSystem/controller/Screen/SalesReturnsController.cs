

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;
using AccountingSystem.view.ReportPages;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using AccountingSystem.view.SupScreens.InventoryTransferManagament;
using AccountingSystem.view.SupScreens.SalesSystem;

namespace AccountingSystem.controller.Screen
{
    public class SalesReturnsController
    : DBContextController
    {
       
        public DateTime? startDate;
        public DateTime? endDate;
        public AppTable detailTable;
        List<SaleDetail> newDetails;


        public BindingSource dataSourceDetail = new BindingSource();
        public List<Currency> currencies{ get { return dBContext.Currencies.ToList(); }}
        public List<Employee> employees { get { return dBContext.Employees.ToList(); }}
        public List<Customer> customers{ get { return dBContext.Customers.ToList(); }}
        public List<Store> stores{ get { return dBContext.Stores.ToList(); }}
        public List<Cashier> cashiers{ get { return dBContext.Cashiers.ToList(); }}
        public Sale temp;
        public Sale tempSearch;
        List<JournalEntry> tempJournalEntries;
        public DateTime? lastDate;
        int newNumber;
        public bool isWholesale => temp.priceType == PriceType.جمله.ToString();

                public SalesReturnsController( bool IsAddReturn = false)
        {
            permissions = model.LoginData.permissions["salesReturns"];
            columnsNamesInAR = new List<string>() { "الرقم", "رقم الفاتوره ", "نوع الدفع", "التأريخ", "المخزن", "الصندوق", "العميل", "المبلغ", "العمله", "المبلغ المدفوع", "المتبقي", "الخصم", "نوع السعر", "تأريخ الإضافه" };
            transactionType = TransactionType.مرتجع_مبيعات.ToString();
            detailTable = new AppTable();
            dataSource.DataSource = typeof(Sale);
            this.IsAddReturn= IsAddReturn;
            if (!IsAddReturn)
            lodeData();
        }
        public string Number
        {
            get
            {

                int? newNum = temp.number;

                if (IsAdd||IsAddReturn)
                {
                    Random random = new Random(100000);
                    b:
                    newNum = random.Next(1, 100000);
                    var any = dBContext.Sales.Where(x => x.number == newNum);
                    var purchases = dBContext.Purchases.Where(x => x.number == newNum);
                    if (any.Any()|| purchases.Any())
                        goto b;
                }

                return newNum.ToString();
            }
        }

        protected void fillDetailTable()
        {
            bool status = true;
            detailTable.Rows = new List<AppRow>();
            foreach (var detail in temp?.SaleDetails.Primary())
            {
                if (!newRow(detail)) { status = false; break; }
            }
            if (status)
                dataSourceDetail.DataSource = detailTable;
            else AppDialogAleart.showAleartError();
        }
        bool newRow(SaleDetail detail)
        {

            if (detail.item == null)
                detail.item = dBContext.Classifies.FirstOrDefault(x => x.id == detail.itemId);
            if (detail.MeasurementsItem == null)
                detail.MeasurementsItem = dBContext.MeasurementsItems.FirstOrDefault(x => x.id == detail.measurementItemId);
            if (detail.item == null || detail.MeasurementsItem == null)
                return false;
            var items = new Classify[1];
            if (detail.item.IsSupItem())
                items = copySupItems.Swap(detail.item, 0);
            else
                items = copySupItems;


            AppRow row = new AppRow()
            {
                id = detail.id,
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
        public Unit[] copyUnits { get => dBContext.UnitsWithEmpty(); }


        internal AppRow newRow(Classify item)
        {
            var measurement = measurementSelectItem(item);
            if (measurement == null)
                return null;
            var items = copySupItems.Swap(item, 0);

            AppRow row = new AppRow()
            {
                Cells = new List<AppCell>()
            {
            new AppCell() {value = item.ClassifyNumber?.ToString(), },
            new AppCell() {id=item.id.ToString(), CombBox=new AppTableCombBox(){ DataSource=items,SelectedItem=item} },
            new AppCell() {id=measurement.id.ToString(),CombBox=new AppTableCombBox(){ DataSource=new Unit[1]{measurement.Unit},SelectedItem=measurement.Unit,Tag=measurement} },
            new AppCell() {value="1" },
            new AppCell() {value=(isWholesale?measurement.WholesalePrice:measurement.sellingPrice).Format() },
            new AppCell() {value=(isWholesale?measurement.WholesalePrice:measurement.sellingPrice).Format() },
            new AppCell() {},
            }
            };
            return row;
        }
        public string ExchangeRate
        {
            get { return IsAdd? temp.Currency?.exchangeRate?.ToString():temp.exchangeRate.ToString(); }
        }
        public bool CurrentCurrencyIsMain
        {
            get { return temp?.Currency?.isMain() ?? true; }
        }
        public override void clearTempData()
        {
            var currency = dBContext.Currencies?.ToList().FirstOrDefault(x => x.isMain()) ?? null;
            temp = new Sale() { date = DateTime.Now, Currency = currency, currencyId = currency?.id, Store = stores.FirstOrDefault(), Cashier = cashiers.FirstOrDefault(),
                paymentType = PaymentType.نقد.ToString(),
                priceType = PriceType.تجزئه.ToString(),
                orderType = "محلي",
            };
        }
        public void clearHomeTempData()
        {
            startDate = null; endDate = null;
            tempSearch = new Sale() { Cashier = null, Customer = null, Store = null, Employee = null, Currency = null };
        }
          bool IsAddReturn=false;
        public bool dataProcessing(string number, string exchangeRate, string discount, string amountPaid, AppTable data)
        {
            decimal newExchangeRate = 1;

            if (!ValidatingData.validatingData(number, "رقم الفاتوره"))
                return false;
          
                if (!ValidatingData.validatingData(temp.Store, "المخزن", false))
                    return false;
          
                if (!ValidatingData.validatingData(temp.Customer, "العميل", false))
                    return false;
              if (!ValidatingData.validatingData(temp.Cashier, "الصندوق", false))
                 return false;
            if (!ValidatingData.validatingData(temp.Currency, "العمله", false))
                return false;
            if (!ValidatingData.validatingData(temp.paymentType, "نوع الدفع", false))
                return false;
            if (!ValidatingData.validatingData(temp.orderType, "وقت الطلب", false))
                return false;
            if (!CurrentCurrencyIsMain)
            {
                if (!ValidatingData.validatingData(exchangeRate, "سعر التحويل"))
                    return false;
            } else
                    newExchangeRate = temp.Currency.exchangeRate.Value;

            newDetailTable = data;
            if (!newDetails.Any())
            {
                AppDialogAleart.showAleartErrorData("لم يتم تحديد أي صنف ");
                return false;
            }
             newNumber = int.Parse(number);
            if (IsAdd)
            { temp.enteryDate = DateTime.Now; lastDate = temp.date; }
            temp.descountPrice = discount.ToDecimal();
            temp.amountPaid = amountPaid.ToDecimal();
         
            temp.exchangeRate = newExchangeRate;
            temp.currencyId = temp.Currency.id;
            temp.customerId = temp.Customer.id;
            temp.cashierId = temp.Cashier.id;
            temp.storeId = temp.Store.id;
            temp.type = transactionType;
            if (IsAdd || IsAddReturn)
            {
                temp.employeeId = EmployId;
                temp.brancheId = BranchId;
            }
            tempJournalEntries = temp.AddJournalEntry(newDetails, newNumber, newExchangeRate, true);
            if (IsAddReturn)
                return addReturn();
            if (IsAdd)
                return add();
            if (IsUpdate)
                return update();
           

            return false;
        }

        private bool addReturn()
        {
            bool status = false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {
                    var any = dBContext.Sales?.FirstOrDefault(x => x.number == newNumber);
                    if (any != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("توجد فاتوره سابقه بهذا الرقم");
                        return false;
                    }
                     any = dBContext.Sales?.FirstOrDefault(x => x.originalInvoiceId == temp.id);
                    if (any != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("لقد تم ارجاع الفاتورة هذه مسبقاً");
                        return false;
                    }
                    dBContext.JournalEntries.AddRange(tempJournalEntries);
                    Sale sale = temp.Copy(newDetails);
                    sale.originalInvoiceId = temp.id;
                    sale.number = newNumber;
                    dBContext.Sales.Add(sale);
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
              

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

        public AppTable newDetailTable
        {
            set
            {
                newDetails = new List<SaleDetail>();
                for (int i = 0; i < value.Rows.Count; i++)
                {
                    var row = value.Rows[i];
                    var item = (Classify)value[i, "الصنف"].CombBox?.SelectedItem ?? null;
                    var unit = value[i, "الوحده"];
                    var unitPrice = value[i, "سعر الوحده"].value.ToDecimal();
                    var quantity = value[i, "الكميه"].value.ToDecimal();
                    var description = value[i, "ملاحضات"].value;
                    if (item != null && item.id != 0)
                    {
                        int measurementItemId = int.Parse(unit.id);
                        var data = new SaleDetail() { itemId = item.id, measurementItemId = measurementItemId, unitPrice = unitPrice, quantity = quantity, description = description };
                        //if (IsUpdate&& row.id.HasValue)
                        //    data.id = row.id.Value;

                        newDetails.Add(data);
                        addCompositeItems(measurementItemId, quantity);
                    }
                }

            }
        }
        void addCompositeItems(int measurementItemId, decimal quantity)
        {
            var compositeItems = dBContext.CompositeItems.Where(x => x.componentItemId == measurementItemId);
            foreach (var compositeItem in compositeItems)
            {
                newDetails.Add(new SaleDetail()
                {
                    quantity = compositeItem.quantity * quantity,
                    unitPrice = compositeItem.purchasePrice,
                    measurementItemId = compositeItem.measurementItemId,
                    itemId = compositeItem.ComponentItem.itemId,
                    type=MeasurementsItemType.مركب.ToString(),
                });
            }
        }
        public override bool add()
        {
            bool status = false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {
                    var any = dBContext.Sales?.FirstOrDefault(x => x.number == newNumber);
                    if (any != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("توجد فاتوره سابقه بهذا الرقم");
                        return false;
                    }
                    dBContext.JournalEntries.AddRange(tempJournalEntries);
                    Sale sale = temp.Copy(newDetails);
                     sale.number = newNumber;
                    dBContext.Sales.Add(sale);
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    //AppDialogAleart.showAleartNoPermissions(tempSale.date.Value.ToString());
                
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

        public override bool update()
        {
            bool status = false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {

                    var any = dBContext.Sales?.FirstOrDefault(x => x.number ==newNumber && x.id != temp.id);
                    if (any != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("توجد فاتوره سابقه بهذا الرقم");
                        return false;
                    }
                    //  AppDialogAleart.showAleartNoPermissions("+jjj"+ dBContext.JournalEntries.Where(x => x.transactionId == tempSale.number && x._transactionType == _transactionType.ToString()).Count());
                    dBContext.JournalEntries.RemoveRange(dBContext.JournalEntries.Where(x => x.transactionId == temp.number && (x.transactionType == transactionType || x.transactionType == TransactionType.قيد_تمتيك.ToString())));
                    dBContext.SaveChanges();
                    dBContext.JournalEntries.AddRange(tempJournalEntries);
                    dBContext.SaveChanges();
                    dBContext.SaleDetails.RemoveRange(temp.SaleDetails);
                    dBContext.SaveChanges();
                    temp.SaleDetails = newDetails;
                    temp.number = newNumber;
                    dBContext.SaleDetails.AddOrUpdate(temp.SaleDetails.ToArray());
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData(true);

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

                                dBContext.JournalEntries.AppRemove(temp.number.Value, transactionType);
                                dBContext.SaleDetails.RemoveRange(temp.SaleDetails);
                                dBContext.Sales.Remove(temp);

                            }
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                            dBContext.SaveChanges();
                            transaction.Commit();
                            lodeData(true);
                        }
                        catch
                        {
                            transaction.Rollback();
                            AppDialogAleart.showAleartError();
                            status = false;
                        }
                    }
                }
                else { AppDialogAleart.showAleartErrorData("لم تقم بتحديد اي بيانات للحذف"); }
            }
            else AppDialogAleart.showAleartNoPermissions();

            return status;
        }
        public void selectStore(object store)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.Store = (Store)store ?? null;
                temp.storeId = temp.Store?.id;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.Store = (Store)store ?? null;
                tempSearch.storeId = tempSearch.Store?.id;
            }

        }
        public void selectCustomer(object customer)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.Customer = (Customer)customer ?? null;
                temp.customerId = temp.Customer?.id;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.Customer = (Customer)customer ?? null;
                tempSearch.customerId = tempSearch.Customer?.id;
            }

        }
        public void selectCurrency(object currency)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.Currency = (Currency)currency ?? null;
                temp.currencyId = temp.Currency?.id;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.Currency = (Currency)currency ?? null;
                tempSearch.currencyId = tempSearch.Currency?.id;
            }
        }
        public void selectCashier(object cashier)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.Cashier = (Cashier)cashier ?? null;
                temp.cashierId = temp.Cashier?.id;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.Cashier = (Cashier)cashier ?? null;
                tempSearch.cashierId = tempSearch.Cashier?.id;
            }
        }
        public void selectPaymentType(object value)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.paymentType = (string)value ?? null;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.paymentType = (string)value ?? null;
            }
        }
        public void selectOrderType(object value)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.orderType = (string)value ?? null;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.orderType = (string)value ?? null;
            }
        }
        public void selectPriceType(string value)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.priceType = (string)value ?? null;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.priceType= (string)value ?? null;
            }
        }
        public void selectEmployee(object value)
        {
            if (HasAddAndUpdateScreenDataProcessed)
            {
                temp.Employee = (Employee)value ?? null;
                temp.employeeId = temp.Employee?.id;
            }
            else if (HasHomeScreenDataProcessed)
            {
                tempSearch.Employee = (Employee)value ?? null;
                tempSearch.employeeId = tempSearch.Employee?.id;
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
        public void selectEndDate(DateTime? date)
        {
            if (HasHomeScreenDataProcessed)
                endDate = date;
        }
        public override bool find(int id)
        {

            bool status = true;
            try
            {

                temp = dBContext.Sales.Returned(IsAddReturn)?.FirstOrDefault(i => i.id == id);
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
            if (permissions.viewPermission ?? false)
            {
                tempData = new List<dynamic>();
                try
                {

                    string number = "";
                    if (additional != null)
                        number = Convert.ToString(additional);
                    var allData = dBContext.Sales.Returned(true).ToList();
                    if (shearch)
                        allData = allData.Search(tempSearch,startDate,endDate,number);
                    tempData = allData.Select(e => new
                    {
                        id = e.id,
                        number = e.number,
                        paymentType = e.paymentType,
                        date = e.date,
                        store = e.Store.name,
                        cashier = e.Cashier.name,
                        customer = e.Customer.name,
                        sumPrice = e.SaleDetails.ToList().TotalPrice() - e.descountPrice,
                        currency = e.Currency.name,
                        amountPaid = e.amountPaid,
                        remaining = e.SaleDetails.ToList().TotalPrice() - e.descountPrice - e.amountPaid,
                        descountPrice = e.descountPrice,
                        priceType = e.priceType,
                        enteryDate = e.enteryDate,
                    }).ToList<dynamic>();
                    fillDataGridView();

                }
                catch
                {

                    
                    AppDialogAleart.showAleartError();
                }

            }
            return tempData?.Any() ?? false;
        }

        public override void showDialogAdd()
        {
            if (permissions.addPermission.Value)
            {

                clearTempData();
                fillDetailTable();
                prosessesType = ProsessesType.add;
                DialogAddAndUpdateSalesReturns dialog = new DialogAddAndUpdateSalesReturns(this);
                dialog.ShowDialog();
                endHADSDP();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }

        public override void showDialogUpdate(int id)
        {
            if ((IsAddReturn? permissions.addPermission.Value: permissions.updatePermission.Value))
            {
                if (id != 0)
                {

                    prosessesType = ProsessesType.update;
                    if (find(id))
                    {
                        //  Program.homeScereen().Hide();
                        fillDetailTable();
                        DialogAddAndUpdateSalesReturns dialog = new DialogAddAndUpdateSalesReturns(this);
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
            dataTable = new System.Data.DataTable();
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
                dataTable.Rows.Add(item.id, item.number,
              item.paymentType, ((DateTime)item.date).Format(), item.store,
              item.cashier, item.customer, ((decimal)item.sumPrice).Format(), item.currency, ((decimal)item.amountPaid).Format(), ((decimal)item.remaining).Format(), ((decimal)item.descountPrice).Format(), item.priceType, ((DateTime)item.enteryDate).Format());
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

        public void print(int id)
        {
            if (!find(id))
                return;

            List<DataSetBills> detail = new List<DataSetBills>();
            dynamic bill = new { };
           
                detail = temp.SaleDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()).Select(d => new DataSetBills() { name = d.item.nameAr, unitName = d.MeasurementsItem.Unit.name, quantity = d.quantity ?? 0, unitPrice = d.TotalPrice(), description = d.description }).ToList();
                bill = new
                {
                    name = "العميل:" + temp.Customer.name,
                    number = temp.number.Value.ToString(),
                    date = temp.date.Value.ToString(SharedData.formatDisplayDate),
                    store = temp.Store.name,
                    type = "مردود مبيعات(" + temp.paymentType + ")",
                    currencyName = temp.Currency.name,
                    currencyCode = temp.Currency.code,
                    amountPaid = (temp.amountPaid ?? 0).ToString(),
                    total = (temp.SaleDetails.ToList().TotalPrice() - (temp.descountPrice ?? 0)).ToString(),
                    user = temp.Employee.name
                };
    
              (new ViewPrintingBills(detail, bill)).ShowDialog();
           
        }
    }
}
