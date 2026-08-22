using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.controller
{
    public class ReportItemsAndStoresController
    {
        public BindingSource dataSource;
        protected  AccountingDbContext dBContext;
        public List<Classify> itemsWithAllData
        { get { return dBContext.Classifies.AsNoTracking().Include(i => i.MeasurementsItems).ToList().GetAllItemsWithChildren(); } } 
        public List<Classify> allItemsWithChildren
        { get { return dBContext.Classifies.AsNoTracking().Include(i => i.MeasurementsItems).ToList().GetAllItemsWithChildren(); } } 
        public List<Sale> sales
        { get { return dBContext.Sales.AsNoTracking().Include(i=>i.SaleDetails).ToList(); } }    
        public List<Purchase> purchases
        { get { return dBContext.Purchases.AsNoTracking().Include(i=>i.PurchaseDetails).ToList(); } }
        public List<Classify> items
        { get { return dBContext.Classifies.AsNoTracking().ToList(); } }
        public List<ChartOfAccount> accounts
        { get { return dBContext.ChartOfAccounts.AsNoTracking().Where(x=>x.type=="فرعي").ToList(); } }
        public List<Supplier> suppliers
        { get { return dBContext.Suppliers.AsNoTracking().ToList(); } }
        public List<Store> stores
        { get { return dBContext.Stores.AsNoTracking().ToList(); } }
        public List<ClassifyGroup> groups
        { get { return dBContext.ClassifyGroups.AsNoTracking().ToList(); } }
        public DateTime? startDate = null;
        public DateTime? endDate = null;
        public Classify selectedItem;
        public ChartOfAccount account;
        public Supplier supplier;
        public Store store;
        public ClassifyGroup group;
        public bool withoutCompoundItems = false;
        public bool withoutZeroItems=false;
        public List<string> invoiceTypes = new List<string>() { "مبيعات", "مشتريات", "مرتجع مبيعات", "مرتجع مبيعات" };
        public string invoiceType;
        public bool HasDataProcessed=false;
        public void selecteItem(object value)
        {
            if(HasDataProcessed)
            selectedItem = (Classify)value ?? null;
        }
        public void selectedAccount(object value)
        {
            if (HasDataProcessed)
                account = (ChartOfAccount)value ?? null;
        }    
        public void selectedSupplier(object value)
        {
            if (HasDataProcessed)
                supplier = (Supplier)value ?? null;
        }
        public void selectedStore(object value)
        {
            if (HasDataProcessed)
                store = (Store)value ?? null;
        }
        public void selectedGroup(object value)
        {
            if (HasDataProcessed)
                group = (ClassifyGroup)value ?? null;
        }
        public void selectedInvoiceType(object value)
        {
            if (HasDataProcessed)
                invoiceType = (string)value ?? null;
        }
        public void selectedStartDate(DateTime? date)
        {
            if (HasDataProcessed)
                startDate = date;
        }
        public void selectedEndDate(DateTime? date)
        {
            if (HasDataProcessed)
                endDate = date;
        }  
        public void selectedWithoutZeroItems(bool value)
        {
            if (HasDataProcessed)
                withoutZeroItems = value;
        }  
        public void selectedWithoutCompoundItems(bool value)
        {
            if (HasDataProcessed)
                withoutCompoundItems = value;
        }
        //public PurchasedAndSalsedQuantityAndPriceMeasurementItem GetPurchasedAndSalsedQuantityAndPriceMeasurementItem(IEnumerable<Sale> sales,IEnumerable<Purchase> purchases,int id)
        //{
        //    QuantityAndPrice saleQuantityAndPrice = sales.QuantityAndPriceMeasurementItemById(id);
        //    QuantityAndPrice purchaseQuantityAndPrice = purchases.QuantityAndPriceMeasurementItemById(id);
        //}
       

    }
}
