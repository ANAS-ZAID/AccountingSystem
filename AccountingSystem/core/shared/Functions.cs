
using Guna.UI2.WinForms;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Forms;
using AccountingSystem.controller.Screen;
using AccountingSystem.core.Functions;
using AccountingSystem.model;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.core.shared
{

    public static class AppDBFunctions
    {

        //public static string description(this List<SaleDetail> saleDetails)
        //{ string description = ". \n تفاصيل الفاتورة/";
        //    using (AccountingDbContext dBContext = new AccountingDbContext())
        //    {

        //        int detailNumber = 1;
        //        foreach (var saleDetail in saleDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()))
        //        {
        //            var m = dBContext.MeasurementsItems.FirstOrDefault(x => x.id == saleDetail.measurementItemId);
        //            string price = ((saleDetail.unitPrice ?? 0) - (saleDetail.descountPrice ?? 0) * (saleDetail.unitPrice ?? 0)).Format();
        //            description += $"{detailNumber}" + "\n الصنف: " + m.item.nameAr + ". الوحده: " + m.Unit.name + ". سعر الوحده: " + saleDetail.UnitPrice() + ". الكميه: " + (saleDetail.quantity ?? 0) + ". الإجمالي: " + saleDetail.TotalPrice() + "; \n";
        //            detailNumber++;

        //        }
        //    }
        //    return description;
        //}\
        public static bool isSale(this InvoiceType invoiceType,string type) =>invoiceType.ToString().Replace("_"," ")== (type.Contains("_") ? type.Replace("_"," ") : type);
        public static List<Purchase> Search(this List<Purchase> purchases, Purchase tempSearch, DateTime? startDate, DateTime? endDate, string additional = null) => purchases.Where(x => (String.IsNullOrEmpty(additional) || x.number.ToString().Contains(additional)) && (x.date.Between(startDate, endDate)
                  && (tempSearch.Supplier == null || x.supplierId == tempSearch.Supplier.id) && (tempSearch.Store == null || x.storeId == tempSearch.Store.id) && (tempSearch.Cashier == null || x.cashierId == tempSearch.Cashier.id) && (tempSearch.Currency == null || x.currencyId == tempSearch.Currency.id) && (tempSearch.Employee == null || x.employeeId == tempSearch.Employee.id)
                  && (String.IsNullOrEmpty(tempSearch.paymentType) || x.paymentType.Contains(tempSearch.paymentType)))).ToList();
        public static List<JournalEntry> AddJournalEntry(this Purchase purchase, List<PurchaseDetail> newSaleDetails, int transactionNumber, bool isReturned)
        {
            List<JournalEntry> tempJournalEntries = new List<JournalEntry>();
            decimal total = newSaleDetails.TotalPrice();
            AddJournalEntry();

            void AddJournalEntry()
            {
                //tempJournalEntries = new List<JournalEntry>();



                AddSalesEntries();

                if (purchase.amountPaid > 0)
                {
                    AddPaymentEntries();
                }
            }
            void AddSalesEntries()
            {
                string purchaseDescription = GetPurchaseDescription();
                string storeDescription = GetStoreDescription();

                tempJournalEntries.Add(CreateJournalEntry(purchase.Store.accountId.Value, isReturned ? 0 : total, isReturned ? total : 0, storeDescription));
                tempJournalEntries.Add(CreateJournalEntry(purchase.Supplier.accountId, isReturned ?total  :0 , isReturned ? 0 : total, purchaseDescription));
            }
            string GetPurchaseDescription()
            {
                return $"{(isReturned ? "عليكم فاتورة مردود مشتريات رقم:" : "لكم فاتورة مشتريات رقم:")}{transactionNumber}";
            }
            string GetStoreDescription()
            {
                return $"{(isReturned ? "لكم فاتورة مردود مشتريات رقم:" : "عليكم فاتورة مشتريات رقم:")}{transactionNumber} من المورد/ {purchase.Supplier.name}";
            }
            void AddPaymentEntries()
            {
                string paymentType = purchase.amountPaid != total ? " مقدم " : " مسدد ";
                string paymentDescription = $" مبلغ {paymentType} من  {(isReturned ? "فاتورة مردود مشتريات رقم: " : "فاتورة مشتريات رقم: ")}{transactionNumber}";
                string supplierPaymentDescription = (isReturned ?  " لكم ":" عليكم " ) + paymentDescription;
                string cashierPaymentDescription = (isReturned ?  " عليكم ":" لكم " ) + paymentDescription + " للمورد " + purchase.Supplier.name;

                tempJournalEntries.Add(CreateJournalEntry(purchase.Supplier.accountId, isReturned ? 0 : (purchase.amountPaid ?? 0), isReturned ? (purchase.amountPaid ?? 0) : 0, supplierPaymentDescription));
                tempJournalEntries.Add(CreateJournalEntry(purchase.Cashier.accountId.Value, isReturned ? (purchase.amountPaid ?? 0) :0 , (isReturned ?  0: (purchase.amountPaid ?? 0)), cashierPaymentDescription));
            }
            JournalEntry CreateJournalEntry(int accountId, decimal debit, decimal credit, string description)
            {
                return new JournalEntry
                {
                    transactionId = transactionNumber,
                    accountId = accountId,
                    currencyId = purchase.currencyId,
                    ExchangeRate = purchase.exchangeRate,
                    transactionType = (isReturned ? TransactionType.مرتجع_مشتريات : TransactionType.فاتورة_مشتريات).ToString(),
                    transactionDate = purchase.date,
                    debit = debit,
                    credit = credit,
                    description = description
                };
            }
            

            return tempJournalEntries;
        }
        public static Purchase Copy(this Purchase purchase, List<PurchaseDetail> details) => new Purchase() { supplierId = purchase.Supplier.id, brancheId = purchase.brancheId, employeeId = purchase.employeeId, number = purchase.number, storeId = purchase.Store.id, cashierId = purchase.Cashier.id, currencyId = purchase.Currency.id, paymentType = purchase.paymentType, priceType = purchase.priceType, exchangeRate = purchase.exchangeRate, date = purchase.date, type = purchase.type, enteryDate = purchase.enteryDate, description = purchase.description, amountPaid = purchase.amountPaid, PurchaseDetails = details };
        public static IEnumerable<PurchaseDetail> Primary(this ICollection<PurchaseDetail> details) => details.Where(x => x.type != MeasurementsItemType.مركب.ToString());
        public static Sale Copy(this Sale sale,List<SaleDetail>details )=> new Sale() { customerId = sale.Customer.id, brancheId = sale.brancheId, employeeId = sale.employeeId, number = sale.number, storeId = sale.Store.id, cashierId = sale.Cashier.id, currencyId = sale.Currency.id, paymentType = sale.paymentType, priceType = sale.priceType, orderTime = sale.orderTime, orderType = sale.orderType, exchangeRate = sale.exchangeRate, date = sale.date, type = sale.type, descountPrice = sale.descountPrice, enteryDate = sale.enteryDate, description = sale.description, amountPaid = sale.amountPaid, SaleDetails = details };
        public static IEnumerable<SaleDetail> Primary(this ICollection<SaleDetail> details) => details.Where(x => x.type != MeasurementsItemType.مركب.ToString());
        public static void AppRemove(this DbSet<JournalEntry> journalEntries,int number,string transactionType,string add=null)
        {
           var entries = journalEntries.Where(x => x.transactionId == number && x.transactionType == transactionType && (add == null || x.transactionType == add));
              journalEntries.RemoveRange(entries);
        }
       public static List<JournalEntry> AddJournalEntry(this Sale tempSale, List<SaleDetail> newSaleDetails, int transactionNumber, decimal exchangeRate, bool isReturned)
        {
            List<JournalEntry> tempJournalEntries = new List<JournalEntry>();
            decimal total = newSaleDetails.TotalPrice();
            decimal totalAfterDiscount = CalculateTotalAfterDiscount();

            AddJournalEntry();

            void AddJournalEntry()
            {
                //tempJournalEntries = new List<JournalEntry>();



                AddSalesEntries();

                if (tempSale.amountPaid > 0)
                {
                    AddPaymentEntries();
                }
            }
            void AddSalesEntries()
            {
                string salesDescription = GetSalesDescription();
                string storeDescription = GetStoreDescription();

                tempJournalEntries.Add(CreateJournalEntry(tempSale.Store.accountId.Value, isReturned ?  totalAfterDiscount:0 , isReturned ? 0 : totalAfterDiscount, storeDescription));
                tempJournalEntries.Add(CreateJournalEntry(tempSale.Customer.accountId.Value, isReturned ? 0 : totalAfterDiscount, isReturned ? totalAfterDiscount : 0, salesDescription));
            }
            string GetSalesDescription()
            {
                return $"{(isReturned ? "لكم فاتورة مردود مبيعات رقم:" : "عليكم فاتورة مبيعات رقم:")}{transactionNumber}";
            }
            string GetStoreDescription()
            {
                return $"{(isReturned ? "عليكم فاتورة مردود مبيعات رقم:" : "لكم فاتورة مبيعات رقم:")}{transactionNumber} من العميل/ {tempSale.Customer.name}";
            }
            void AddPaymentEntries()
            {
                string paymentType = tempSale.amountPaid != CalculateTotalAfterDiscount() ? " مقدم " : " مسدد ";
                string paymentDescription = $" مبلغ {paymentType} من  {(isReturned ? "فاتورة مردود مبيعات رقم: " : "فاتورة مبيعات رقم: ")}{transactionNumber}";
                string customerPaymentDescription = (isReturned ? " عليكم " : " لكم ") + paymentDescription;
                string cashierPaymentDescription = (isReturned ? " لكم " : " عليكم ") + paymentDescription + " للعميل " + tempSale.Customer.name;

                tempJournalEntries.Add(CreateJournalEntry(tempSale.Customer.accountId.Value, isReturned ? (tempSale.amountPaid ?? 0) :0 , isReturned ? 0:  (tempSale.amountPaid ?? 0), customerPaymentDescription));
                tempJournalEntries.Add(CreateJournalEntry(tempSale.Cashier.accountId.Value, isReturned ? 0 : (tempSale.amountPaid ?? 0), (isReturned ? (tempSale.amountPaid ?? 0) : 0), cashierPaymentDescription));
            }
            JournalEntry CreateJournalEntry(int accountId, decimal debit, decimal credit, string description)
            {
                return new JournalEntry
                {
                    transactionId = transactionNumber,
                    accountId = accountId,
                    currencyId = tempSale.currencyId,
                    ExchangeRate = exchangeRate,
                    transactionType = (isReturned ? TransactionType.مرتجع_مبيعات : TransactionType.فاتورة_مبيعات).ToString(),
                    transactionDate = tempSale.date,
                    debit = debit,
                    credit = credit,
                    description = description
                };
            }
            decimal CalculateTotalAfterDiscount()
            {
                return total - (tempSale.descountPrice ?? 0);
            }

            return tempJournalEntries;
        }
        public static List<Sale> Search(this List<Sale> sales,Sale tempSearch,DateTime? startDate, DateTime? endDate, string additional=null) =>sales. Where(x => (String.IsNullOrEmpty(additional) || x.number.ToString().Contains(additional)) && (x.date.Between(startDate, endDate)
                        && (tempSearch.Customer == null || x.customerId == tempSearch.Customer.id) && (tempSearch.Store == null || x.storeId == tempSearch.Store.id) && (tempSearch.Cashier == null || x.cashierId == tempSearch.Cashier.id) && (tempSearch.Currency == null || x.currencyId == tempSearch.Currency.id) && (tempSearch.Employee == null || x.employeeId == tempSearch.Employee.id)
                        && (String.IsNullOrEmpty(tempSearch.paymentType) ||x.paymentType.Contains(tempSearch.paymentType)) && (String.IsNullOrEmpty(tempSearch.orderType) ||  x.orderType.Contains(tempSearch.orderType)))).ToList();
        public static IQueryable<Sale> Original(this DbSet<Sale> sales, bool AsNoTracking = false) => AsNoTracking ? sales.AsNoTracking().Where(sale => sale.type.StartsWith(TransactionType.فاتورة_مبيعات.ToString())) : sales.Where(sale => sale.type.StartsWith(TransactionType.فاتورة_مبيعات.ToString()));
        public static IEnumerable<Purchase> Original(this IEnumerable<Purchase> purchases, string invoiceType = null) => purchases.Where(purchase => purchase.type.StartsWith(TransactionType.فاتورة_مشتريات.ToString()) && (invoiceType == null || invoiceType == InvoiceType.مشتريات.ToString()));
        public static IEnumerable<Purchase> Returned(this IEnumerable<Purchase> purchases, string invoiceType = null) => purchases.Where(purchase => purchase.type.StartsWith(TransactionType.مرتجع_مشتريات.ToString()) && (invoiceType == null || invoiceType == InvoiceType.مرتجع_مشتريات.ToString()));
        public static IEnumerable<Sale> Original(this IEnumerable<Sale> sales,string invoiceType=null) =>sales.Where(sale => sale.type.StartsWith(TransactionType.فاتورة_مبيعات.ToString()) && (invoiceType == null || invoiceType == InvoiceType.مبيعات.ToString()));
        public static IEnumerable<Sale> Returned(this IEnumerable<Sale> sales, string invoiceType = null) =>sales.Where(sale => sale.type.StartsWith(TransactionType.مرتجع_مبيعات.ToString()) && (invoiceType == null || invoiceType == InvoiceType.مرتجع_مبيعات.ToString()));
        public static IQueryable<Purchase> Original(this DbSet<Purchase> purchases, bool AsNoTracking = false) => AsNoTracking ? purchases.AsNoTracking().Where(purchase => purchase.type.StartsWith(TransactionType.فاتورة_مشتريات.ToString())) : purchases.Where(purchase => purchase.type.StartsWith(TransactionType.فاتورة_مشتريات.ToString()));
        public static IQueryable<Sale> Returned(this DbSet<Sale> sales, bool AsNoTracking = false) => AsNoTracking ? sales.AsNoTracking().Where(sale => sale.type.StartsWith(TransactionType.مرتجع_مبيعات.ToString())).OrderByDescending(x => x.id) : sales.Where(sale => sale.type.StartsWith(TransactionType.مرتجع_مبيعات.ToString()));
        public static IQueryable<Purchase> Returned(this DbSet<Purchase> purchases, bool AsNoTracking = false) => AsNoTracking ? purchases.AsNoTracking().Where(purchase => purchase.type.StartsWith(TransactionType.مرتجع_مشتريات.ToString())) : purchases.Where(purchase => purchase.type.StartsWith(TransactionType.مرتجع_مشتريات.ToString()));
        public static bool IsReturned(this Sale sale, string invoiceType = null) => sale.type == TransactionType.مرتجع_مبيعات.ToString() && (invoiceType == null || invoiceType == InvoiceType.مرتجع_مبيعات.ToString());
        public static bool IsReturned(this Purchase purchase, string invoiceType = null) => purchase.type == TransactionType.مرتجع_مشتريات.ToString() && (invoiceType == null || invoiceType == InvoiceType.مرتجع_مشتريات.ToString());
        public static decimal Total(this List<InventoryTransferDetail> transferDetails) => transferDetails?.Where(x => x.main)?.Sum(x =>x.Total())??0;
        public static decimal Total(this InventoryTransferDetail transferDetail) => (transferDetail.quantity??0) * (transferDetail.unitPrice??0);
        public static bool isMain(this Currency currency) => currency.currencyType == "رئيسية";
        public static NewModel.EFModel.Unit[] UnitsWithEmpty(this AccountingDbContext dBContext)
        {
            var element = dBContext.Units.AsNoTracking().ToList();
            element.Insert(0, new NewModel.EFModel.Unit() { id = 0, name = "" });
            NewModel.EFModel. Unit[] copy = new NewModel.EFModel.Unit[element.Count];
            element.CopyTo(copy, 0);
            return copy;
        }
        public static Classify[] SupItemsWithEmpty(this AccountingDbContext dBContext)
        {
            var supItems = dBContext.Classifies.AsNoTracking().ToList().Where(x=>x.IsSupItem()).ToList();
            supItems.Insert(0, new Classify() { id = 0, nameAr = "" });
            Classify[] copy = new Classify[supItems.Count];
            supItems.CopyTo(copy, 0);

            return copy;
        }

        public static Classify[] Swap(this Classify[] items, Classify item,int index)
        {
            //Classify[] copyItems=new Classify[items.Length];
            //items.CopyTo(copyItems, 0);

            if (index < items.Length)
            {
                Classify old = items[index];
                var i = items.FirstOrDefault(x => x.id == item.id);
                int newIndex = items.IndexOf(i);
                items[index] = item;
                    items[newIndex] = old;

                if (index != 1)
                {
                    var emptyItem = items.FirstOrDefault(x => x.id == 0);
                    int indexEmptyItem = items.IndexOf(emptyItem);
                    old = items[1];
                    items[1] = emptyItem;
                    items[indexEmptyItem]= old;
                }
            }
            return items;
        }
        public static bool IsComboBox(this Type type) => type == typeof(Guna2ComboBox);
        public static bool IsTextBox(this Type type)=>type==typeof(Guna2TextBox);
       public static int LastIndex(this List<object> list)=>list.Count()-1;
       public static decimal TotalPrice(this InventoryTransfer transfer) => transfer.InventoryTransferDetails.Where(x=>x.main).ToList().Total();
        public static decimal Total(this ICollection<InventoryTransferDetail> details) =>details?.Sum(x=>(x.unitPrice??0) *(x.quantity??0))??0;
        public static bool BetweenOrNull(this DateTime? date, DateTime? startDate, DateTime? endDate) => ((startDate == null || date.Value.Date >= startDate.Value.Date)
                                                                           && (endDate == null || date.Value.Date <= endDate.Value.Date));
        public static bool IsSupItem(this  Classify item) => item.type=="فرعي";
        public static List<Classify> SupItems(this  DbSet<Classify> items) => items.ToArray().Where(x=>x.IsSupItem()).ToList();
        public static bool IsFirstPeriodStock(this Purchase purchase) => (purchase.type.StartsWith( TransactionType.مخزون_اول_فتره.ToString()));
        public static bool IsPurchases(this Purchase purchase, string invoiceType = null) => (purchase.type==null||purchase.type.StartsWith( TransactionType.فاتورة_مشتريات.ToString()) && (invoiceType == null || invoiceType == InvoiceType.مشتريات.ToString()));
        public static bool IsSales(this Sale sale, string invoiceType = null) => ((sale.type ==null|| sale.type.StartsWith(TransactionType.فاتورة_مبيعات.ToString())) && (invoiceType == null || invoiceType == InvoiceType.مبيعات.ToString()));

        public static decimal Total(this DataSetAccountStatementWithNumberHours data) => data.priceHour * data.quantity;
        public static decimal Total(this IEnumerable<DataSetAccountStatementWithNumberHours> list) => list?.Sum(x => x.priceHour * x.quantity)??0;
        public static decimal Total(this List<DataSetAccountStatementWithNumberHours> list) => list?.Sum(x => x.priceHour * x.quantity)??0;
        public static decimal AveragePriceHour(this IEnumerable<DataSetAccountStatementWithNumberHours> list) => (list?.Sum(x => x.quantity) ?? 0)>0?((list?.Total() ?? 0) / (list?.Sum(x => x.quantity) ?? 0)):0;
        public static bool Between(this DateTime? date, DateTime? startDate, DateTime? endDate) =>((startDate == null || date.Value.Date >= startDate.Value.Date)
                                                                            && (endDate == null || date.Value.Date <= endDate.Value.Date));
        public static bool Before(this DateTime? date, DateTime? startDate) => startDate != null && date.Value.Date < startDate.Value.Date;
        public static bool IsSale(this DataSetAccountStatementWithNumberHours dataSetAccountStatement)=>(dataSetAccountStatement.transactionType==TransactionType.فاتورة_مبيعات.ToString());
        public static decimal Balance(this IEnumerable<JournalEntry> journalEntries) => (journalEntries?.Sum(j => (j?.credit ?? 0)) ?? 0) - (journalEntries?.Sum(j => (j?.debit ?? 0)) ?? 0);
        public static decimal Balance(this JournalEntry journalEntry) => (journalEntry?.credit ?? 0) - (journalEntry?.debit ?? 0);
        public static decimal Balance(this DataSetAccountStatementWithNumberHours data) => (data?.credit ?? 0) - (data?.debit ?? 0);
        public static decimal Balance(this IEnumerable<DataSetAccountStatementWithNumberHours> list) => (list?.Sum(j => (j?.credit ?? 0)) ?? 0) - (list?.Sum(j => (j?.debit ?? 0)) ?? 0);
       public static bool IsPurchases(this DataSetAccountStatementWithNumberHours dataSetAccountStatement)=>(dataSetAccountStatement.transactionType==TransactionType.فاتورة_مشتريات.ToString());
        public static bool IsSalesOrPurchases(this DataSetAccountStatementWithNumberHours dataSetAccountStatement)=>(dataSetAccountStatement.IsPurchases()||dataSetAccountStatement.IsSale());
        public static bool IsNotSalesOrPurchases(this DataSetAccountStatementWithNumberHours dataSetAccountStatement)=>!(dataSetAccountStatement.IsPurchases()||dataSetAccountStatement.IsSale());
        
        public static bool IsSale(this JournalEntry journalEntry)=>(journalEntry.transactionType==TransactionType.فاتورة_مبيعات.ToString());
        public static bool IsPurchases(this JournalEntry journalEntry)=>(journalEntry.transactionType==TransactionType.فاتورة_مشتريات.ToString());
        public static bool IsSalesOrPurchases(this JournalEntry journalEntry)=>(journalEntry.IsSale()|| journalEntry.IsPurchases());
        public static bool IsNotSalesOrPurchases(this JournalEntry journalEntry) =>!(journalEntry.IsSale() || journalEntry.IsPurchases());
        public static FinalInvoiceData FinalInvoiceSaleData(int saleNumber)
        {
            using (AccountingDbContext dBContext = new AccountingDbContext())
            {
                var sale = dBContext.Sales?.FirstOrDefault(s => s.number == saleNumber);
                var saleDetails = sale?.SaleDetails?.Where(detail => detail.type != MeasurementsItemType.مركب.ToString());
                decimal totalQuantity = saleDetails?.Sum(x => (x.quantity ?? 0)) ?? 0;
                decimal totalPrice = saleDetails?.Sum(x => ((x.unitPrice ?? 0) - (x.descountPrice ?? 0) * (x.unitPrice ?? 0)) * x.quantity ?? 0) ?? 0;


                return new FinalInvoiceData()
                {
                    total = totalPrice - (sale?.descountPrice ?? 0)
                  ,
                    amountPaid = sale?.amountPaid ?? 0,
                    quantityHours = totalQuantity
                };
            }
        }
        public static FinalInvoiceData FinalInvoicePurchaseData(int saleNumber)
        {
            using (AccountingDbContext dBContext = new AccountingDbContext())
            {
                var purchase = dBContext.Purchases?.FirstOrDefault(s => s.number == saleNumber);
                var purchaseDetails = purchase?.PurchaseDetails?.Where(detail => detail.type != MeasurementsItemType.مركب.ToString());
                decimal totalQuantity = purchaseDetails?.Sum(x => (x.quantity ?? 0)) ?? 0;
                decimal totalPrice = purchaseDetails?.Sum(x => (x.unitPrice ?? 0) * (x.quantity ?? 0)) ?? 0;


                return new FinalInvoiceData()
                {
                    total =totalPrice,
                    amountPaid = purchase?.amountPaid ?? 0,
                    quantityHours = 0
                };
            }
        }
        public static void Description()
        {
            using (AccountingDbContext dBContext = new AccountingDbContext())
            {
                var sales = dBContext.Sales;
                //string description = ". \n تفاصيل الفاتورة/";

                //int detailNumber = 1;
                //foreach (var saleDetail in sale.SaleDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()))
                //{
                //    var m = dBContext.MeasurementsItems.FirstOrDefault(x => x.id == saleDetail.measurementItemId);
                //    string price = ((saleDetail.unitPrice ?? 0) - (saleDetail.descountPrice ?? 0) * (saleDetail.unitPrice ?? 0)).Format();
                //    description += $"{detailNumber}" + "\n الصنف: " + m.item.nameAr + ". الوحده: " + m.Unit.name + ". سعر الوحده: " + saleDetail.UnitPrice() + ". الكميه: " + (saleDetail.quantity ?? 0) + ". الإجمالي: " + saleDetail.TotalPrice() + "; \n";
                //    detailNumber++;

                //}

                foreach (var sale in sales)
                {
                    var journalEntries = dBContext.JournalEntries.Where(j => j.transactionId == sale.number && j.transactionType == sale.type);
                    foreach (var journalEntry in journalEntries)
                    {
                        // journalEntry.description += description;
                        journalEntry.description = journalEntry.description.Substring(0, (journalEntry.description.LastIndexOf("تفاصيل الفات") - 1 > 0 ? journalEntry.description.LastIndexOf("تفاصيل الفات") - 1 : journalEntry.description.Length));
                    }
                }
                dBContext.SaveChanges();
            }


        }
        public static decimal UnitPrice(this SaleDetail saleDetail)
        {
            string f = (((saleDetail.unitPrice ?? 0) - (saleDetail.descountPrice ?? 0) * (saleDetail.unitPrice ?? 0)) ).ToString("F2");
            return decimal.Parse(f);
        }
        public static decimal DescountPrice(this SaleDetail saleDetail) 
        {
            string f = ((saleDetail.descountPrice ?? 0 * saleDetail.unitPrice ?? 0) * saleDetail.quantity ?? 0).ToString("F2");
            return decimal.Parse(f) ;
        }public static decimal TotalPrice(this SaleDetail saleDetail) 
        {
            string f = (((saleDetail.unitPrice ?? 0) - (saleDetail.descountPrice ?? 0) * (saleDetail.unitPrice ?? 0)) * (saleDetail.quantity ?? 0)).ToString("F2");
            return decimal.Parse(f);
        }
        public static decimal TotalPrice(this List<SaleDetail> saleDetails) 
        {
            return saleDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()).Sum(x => ((x.unitPrice??0) - (x.descountPrice??0) * (x.unitPrice??0)) * (x.quantity ?? 0)) ;
        }
        public static decimal TotalPrice(this PurchaseDetail purchaseDetail)
        { string f = ((purchaseDetail.unitPrice ?? 0) * (purchaseDetail.quantity ?? 0)).ToString("F2");
            return decimal.Parse(f);
        }
        public static decimal TotalPrice(this InventoryTransferDetail detail)
        {
            string f = ((detail.unitPrice ?? 0) * (detail.quantity ?? 0)).ToString("F2");
            return decimal.Parse(f);
        }
        public static decimal TotalPrice(this List<PurchaseDetail> purchaseDetails) 
        {
            return purchaseDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()).Sum(x => (x.unitPrice??0) * (x.quantity??0)) ;
        }
        
        public static decimal TotalPrice(this ICollection<SaleDetail> saleDetails) 
        {
            return saleDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()).Sum(x => ((x.unitPrice ?? 0) - (x.descountPrice ?? 0) * (x.unitPrice ?? 0)) * (x.quantity??0)) ;
        }
        //public static QuantityAndPrice QuantityAndPriceMeasurementItem(this IEnumerable<SaleDetail> saleDetails) 
        //{
        //    int totalQuantitySaled =saleDetails?.Sum(x => (int)x.quantity) ?? 0;
        //    decimal totalPriceQuantitySalsed =saleDetails?.Sum(x => (x.unitPrice - x.descountPrice * x.unitPrice) * x.quantity) ?? 0;
        //    //FinalMeasurementItemDetails finalMeasurementItemDetails = new FinalMeasurementItemDetails() { purchased = new Detail() { price = totalPriceQuantityPurchased, quantity = totalQantityPurchased }, salsed = new Detail() { price = totalPriceQuantitySalsed, quantity = totalQuantitySaled } };
        //    return new QuantityAndPrice() { price = totalPriceQuantitySalsed, quantity = totalQuantitySaled };
        //}
        public static QuantityAndPrice QuantityAndPriceMeasurementItemById(this IEnumerable<Sale> sales, int id)
        {
            var saleDetails = sales?.SelectMany(sale => sale?.SaleDetails?.Where(detail => detail.measurementItemId == id));
            int totalQuantitySaled = saleDetails?.Sum(x => (int)(x.quantity??0)) ?? 0;
            decimal totalPriceQuantitySalsed = saleDetails?.Sum(x => ((x.unitPrice ?? 0) - (x.descountPrice ?? 0) * (x.unitPrice ?? 0)) * (x.quantity ?? 0)) ?? 0;
    
            return new QuantityAndPrice() { price = totalPriceQuantitySalsed, quantity = totalQuantitySaled };
        }
        public static QuantityAndPrice QuantityAndPriceMeasurementItemById(this IEnumerable<Purchase> purchases, int id)
        {
            var purchaseDetails = purchases?.SelectMany(purchase => purchase?.PurchaseDetails?.Where(detail => detail.measurementItemId == id));
            int totalQuantity = purchaseDetails?.Sum(x => (int)(x.quantity??0) ) ?? 0;
            decimal totalPriceQuantity = purchaseDetails?.Sum(x =>  (x.unitPrice ?? 0) * (x.quantity ?? 0)) ?? 0;
    
            return new QuantityAndPrice() { price = totalPriceQuantity, quantity = totalQuantity };
        } 
        public static QuantityAndPrice QuantityAndPriceMeasurementItemByIdWithNotCompositeItem(this IEnumerable<Purchase> purchases, int id)
        {
            var purchaseDetails = purchases?.SelectMany(purchase => purchase?.PurchaseDetails?.Where(detail => detail.measurementItemId == id&& detail.type!=MeasurementsItemType.مركب.ToString()));
            int totalQuantity = purchaseDetails?.Sum(x => (int)(x.quantity??0) ) ?? 0;
            decimal totalPriceQuantity = purchaseDetails?.Sum(x => (x.unitPrice?? 0)  * (x.quantity ?? 0)) ?? 0;
           // foreach (var item in purchaseDetails)
           // {
           //     AppDialogAleart.showAleartNoPermissions(" item.quantity=" + item.quantity + ",; item.unitPrice=" + item.unitPrice);
           // }
           //AppDialogAleart.showAleartNoPermissions("totalPriceQuantity=" + totalPriceQuantity+ ",; totalQuantity="+ totalQuantity);
            return new QuantityAndPrice() { price = totalPriceQuantity, quantity = totalQuantity };
        }
        public static QuantityAndPrice QuantityAndPriceMeasurementItemByIdWithNotCompositeItem(this List<SaleDetail> saleDetails, int id)
        {
           // var saleDetails = sales?.SelectMany(sale => sale?.SaleDetails?.Where(detail => detail.measurementItemId == id && detail.type != MeasurementsItemType.مركب.ToString()));
            int totalQuantitySaled = saleDetails?.Where(detail => detail.measurementItemId == id && detail.type != MeasurementsItemType.مركب.ToString()).Sum(x => (int)(x.quantity ?? 0)) ?? 0;
            decimal totalPriceQuantitySalsed = saleDetails?.Where(detail => detail.measurementItemId == id && detail.type != MeasurementsItemType.مركب.ToString()).Sum(x => ((x.unitPrice ?? 0) - (x.descountPrice ?? 0) * (x.unitPrice ?? 0)) * (x.quantity ?? 0)) ?? 0;

            return new QuantityAndPrice() { price = totalPriceQuantitySalsed, quantity = totalQuantitySaled };
        }
        //public static QuantityAndPrice QuantityAndPriceMeasurementItem(this IEnumerable<PurchaseDetail> purchaseDetails, int measurementItemId) 
        //{
        //    int totalQuantityPurchased = purchaseDetails?.Sum(x => (int)x.quantity) ?? 0;
        //    decimal totalPriceQuantityPurchased = purchaseDetails?.Sum(x => x.unitPrice * x.quantity) ?? 0;
        //    //FinalMeasurementItemDetails finalMeasurementItemDetails = new FinalMeasurementItemDetails() { purchased = new Detail() { price = totalPriceQuantityPurchased, quantity = totalQantityPurchased }, salsed = new Detail() { price = totalPriceQuantitySalsed, quantity = totalQuantitySaled } };
        //    return new QuantityAndPrice() { price = totalPriceQuantityPurchased, quantity = totalQuantityPurchased };
        //}
        public static decimal TotalPrice(this ICollection<PurchaseDetail> purchaseDetails) 
        {
            return purchaseDetails.Where(x => x.type != MeasurementsItemType.مركب.ToString()).Sum(x => (x.unitPrice ?? 0) * x.quantity) ?? 0;
        } 
        public static int availableQuantity(int measurementItemId) 
        {AccountingDbContext dbContext = new AccountingDbContext();
            int sumPurchase = Convert.ToInt32(dbContext.PurchaseDetails.Where(x => x.measurementItemId == measurementItemId && x.type != MeasurementsItemType.مركب.ToString())?.Sum(x => x.quantity)??0);
            int sumSale = Convert.ToInt32(dbContext.SaleDetails.Where(x => x.measurementItemId == measurementItemId && x.type != MeasurementsItemType.مركب.ToString())?.Sum(x => x.quantity) ?? 0);
          
            return sumPurchase-sumSale;
        }
        //public static FinalMeasurementItemDetails Quantity(this MeasurementsItem measurementsItem) 
        //{   AccountingDbContext dbContext = new AccountingDbContext();
           
        //    int totalQantityPurchased = measurementsItem?.PurchaseDetails?.Sum(x => (int)x.quantity) ?? 0;
        //    int totalQuantitySaled = measurementsItem?.SaleDetails?.Sum(x =>(int) x.quantity) ?? 0;
        //    decimal totalPriceQuantityPurchased = measurementsItem?.SaleDetails?.Sum(x => (x.unitPrice - x.descountPrice * x.unitPrice) * x.quantity) ?? 0;
        //    decimal totalPriceQuantitySalsed = measurementsItem?.PurchaseDetails?.Sum(detail => detail.quantity * detail.unitPrice)??0;


        //   // FinalMeasurementItemDetails finalMeasurementItemDetails = new FinalMeasurementItemDetails() { purchased=new Detail() {price=totalPriceQuantityPurchased,quantity=totalQantityPurchased } , salsed=new Detail() { price=totalPriceQuantitySalsed,quantity=totalQuantitySaled} };
        //    return finalMeasurementItemDetails ;
        //}
        public static decimal ToDecimal(this string text)
        {
         return   (!String.IsNullOrEmpty(text) ? Convert.ToDecimal(text) : 0);
        }
        //public static string ToDecimal(this string text)
        //{
        //    decimal v=text.ToDecimal()
        //    return (string) ;
        //}
        public static DialogResult verifyNewBalanceNotNegative(decimal balance, decimal amount)
        {
            decimal newBalance = balance - amount;
            DialogResult result = DialogResult.OK;
            if (newBalance < 0)
            {
                result = AppDialogAleart.showAleartConfirmation("أصبح رصيد الحساب الذي تريد التحويل منه سالبا \n هل تريد اكمال العمليه؟");
            }
            return result;
        }
        public static int getNewAccountNumByParentId(int parentId)
        {
            int newNumber;
            AccountingDbContext dBContext = new AccountingDbContext();
            var items = dBContext.ChartOfAccounts.Include(a => a.Childrens).FirstOrDefault(a => a.id == parentId);
            if (items.Childrens.Count > 0)
                newNumber = Convert.ToInt32(items.Childrens.OrderByDescending(a => a.id).First().accountNumber) + 1;
            else newNumber = Convert.ToInt32(items.accountNumber) * 1000 + 1;
            return newNumber;
        }public static int getNewItemNumByParentId(int parentId)
        {
            int newNumber;
            AccountingDbContext dBContext = new AccountingDbContext();
            var items = dBContext.Classifies.Include(a => a.Childrens).FirstOrDefault(a => a.id == parentId);
            if (items.Childrens.Count > 0)
                newNumber = Convert.ToInt32(items.Childrens.OrderByDescending(a => a.id).First().ClassifyNumber) + 1;
            else newNumber = Convert.ToInt32(items.ClassifyNumber) * 1000 + 1;
            return newNumber;
        }
        public static string deleteAccount(this DbSet<ChartOfAccount> chartOfAccounts, int id,bool isFromChartOfAccounts=false)
        {

            ChartOfAccount account = new ChartOfAccount();

            try
            {
                account = chartOfAccounts.Include(a => a.perantAccount).
                    Include(a => a.Employees).
                    Include(a => a.Cashiers).Include(a => a.Customers).Include(a => a.Stores).
              Include(a => a.Childrens).Include(a => a.JournalEntries).Include(a => a.SimpleEntriesCredit)
              .Include(a => a.SimpleEntriesDebit).Include(a => a.Trades).Include(a => a.Vouchers).FirstOrDefault(a => a.id == id);
               
                if (account == null)
                    return "غير موجود";

                if (isFromChartOfAccounts)
                {
                    if (account.Employees.Count > 0)
                        return SharedData.erorrDeleteAccount("موظف");
                    if (account.Cashiers.Count > 0)
                        return SharedData.erorrDeleteAccount("بصندوق");
                    if (account.Customers.Count > 0)
                        return SharedData.erorrDeleteAccount("بعميل");
                    if (account.Stores.Count > 0)
                        return SharedData.erorrDeleteAccount("بمخزن");
                }
                if (account.Childrens.Count() > 0)
                    return SharedData.erorrDeleteAccount("بحسابات أخرى");
                if (account.JournalEntries.Count() > 0)
                    return SharedData.erorrDeleteAccount("بعمليات ماليه");
                if (account.SimpleEntriesCredit.Count() > 0)
                    return SharedData.erorrDeleteAccount("بعمليات ماليه");
                if (account.SimpleEntriesDebit.Count() > 0)
                    return SharedData.erorrDeleteAccount("بعمليات ماليه");
                if (account.Trades.Count() > 0)
                    return SharedData.erorrDeleteAccount("بعمليات ماليه");
                if (account.Vouchers.Count() > 0)
                    return SharedData.erorrDeleteAccount("بعمليات ماليه");

                chartOfAccounts.Remove(account);
            }
            catch
            {
                //AppDialogAleart.showAleartError();

            }
            return "true";
        }
        
        public static bool addPermission(this AccountingDbContext dBContext,ICollection<Permission> permissionsEmployee, int employeeId)
        {
               

            bool status =true;
            try
            {
                foreach (var permission in permissionsEmployee)
                {
                    if (dBContext.Permissions.Any())
                    {
                        var per = dBContext.Permissions.ToList().Last();
                        if (per != null)
                        {
                            permission.id = per.id + 1;
                         
                        }
                        else
                        {
                            permission.id = 1;
                        }

                        //AppDialogAleart.showAleartError("Property: " + " Error: " + dBContext.Employees.Find(employeeId)?.name);
                    }
                    else
                    {
                        permission.id = 1;
                    }
               
                    permission.employeeId = employeeId;
               //AppDialogAleart.showAleartError("Property: " + " Error: " +permission.employeeId);
                    
                    dBContext.Permissions.Add(permission);
                    dBContext.SaveChanges();
               //  AppDialogAleart.showAleartError("Property: " + " Error: "+dBContext.Employees.Find(employeeId)?.name );
                }

            }
            catch 
            {
                
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
      public  static void updatePermissions(this ICollection<Permission> permissions, List<Permission> newPermissions)
        {
            for (int i = 0; i < permissions.Count; i++)
            {
                permissions.ElementAt(i).addPermission = newPermissions[i].addPermission;
                permissions.ElementAt(i).updatePermission = newPermissions[i].updatePermission;
                permissions.ElementAt(i).deletePermission = newPermissions[i].deletePermission;
                permissions.ElementAt(i).viewPermission = newPermissions[i].viewPermission;
            }
        } 
        //public  static void updateJournalEntries(this DbSet<JournalEntry> journalEntries1 ,List<JournalEntry> journalEntries, List<JournalEntry> newJournalEntries)
        //{
        //    int difference=journalEntries.Count()-newJournalEntries.Count();
        //    int Key = 0;

        //        for (int i = 0; i < difference; i++)
        //        {
        //        journalEntries1.Remove(journalEntries[i]);
        //        journalEntries.RemoveAt(i);
        //        }

        //    while (Key<journalEntries.Count)
        //    {
        //        journalEntries[Key].accountId= newJournalEntries[Key].accountId;
        //        journalEntries[Key].currencyId= newJournalEntries[Key].currencyId;
        //        journalEntries[Key].ExchangeRate= newJournalEntries[Key].ExchangeRate;
        //        journalEntries[Key].debit= newJournalEntries[Key].debit;
        //        journalEntries[Key].credit= newJournalEntries[Key].credit;
        //        journalEntries[Key].transactionDate= newJournalEntries[Key].transactionDate;
        //        journalEntries[Key].description= newJournalEntries[Key].description;
        //        Key++;
        //    }
        //    while (Key<newJournalEntries.Count)
        //    {
        //        journalEntries1.Add(newJournalEntries[Key++]);
        //    }

        //}

        public  enum ProcessStatus
        {
            success,
            found,
            foundName,
            foundNumber,
            failure,
            notFound,
            linkedEmployee,
            linkedCashier,
            linkedCustomer,
            linkedStore,
            linkedAccounts,
            linkedFinancialOperations,

        }
    }
    internal class Functions
    {
        public static System.Drawing.Image choseImage()
        {
            System.Drawing.Image image=null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Files|*.png;*.jpg;*.jpeg";//"PDF files (*.pdf)|*.pdf"
            if (DialogResult.OK == dialog.ShowDialog())
            {
                if (!String.IsNullOrEmpty(dialog.FileName))
                {
                    image = System.Drawing.Image.FromFile(dialog.FileName);

                }

            }
            return image;
        }   
        public static System.Drawing.Image saveImage(string path)
        {
          System.Drawing.Image image = choseImage();

            path = SharedData.pathImages+ path;
            if (image != null)
            {
                if (!Directory.Exists(SharedData.pathImages))
                    Directory.CreateDirectory(SharedData.pathImages);
               // if(File.Exists(path))

               image.Save(path);
            }
           
            return image;
        }
        public static System.Drawing.Image readImage(string path)
        {
            System.Drawing.Image image;
            path = SharedData.pathImages+ path; 
            if (File.Exists( path))
                image = System.Drawing.Image.FromFile(path);
            else
                image = null;
            return image;
        }
        static public string collectingData(List<string> columns, System.Data.DataRow values)
        {
            string data = "";
            for (int i = 0; i < columns.Count; i++)
            {
                data += columns[i] + " / " + values[i] + " ; ";
            }
            return data;
        }
        public static DataTable getPagedDataTable(DataTable dataTable, int pageNumber, int pageSize)
        {
           // dataTable.TableName = dataTable.Rows.Count.ToString();
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
            }

            // التحقق من أن جدول البيانات ليس فارغًا
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return dataTable; // أو يمكنك إرجاع جدول بيانات فارغ جديد
            }

            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, dataTable.Rows.Count);

            // استخدام Select لانتقاء الصفوف المطلوبة
            var selectedRows = dataTable.Rows.Cast<DataRow>()
                                            .Skip(startIndex)
                                            .Take(endIndex - startIndex);

            // إنشاء جدول بيانات جديد لاحتواء الصفوف المحددة
            DataTable pagedDataTable = dataTable.Clone();
            for (int i = 0; i < 100; i++)
            foreach (var row in selectedRows)
            {
                pagedDataTable.Rows.Add(row.ItemArray);
            }
            if (pagedDataTable.Rows.Count < 1 && pageNumber - 1 > 0)
                pagedDataTable = getPagedDataTable(dataTable, pageNumber - 1, pageSize);
            return pagedDataTable;
        }

        public static DataTable getPagedDataTable(object dataSource, int pageNumber, int pageSize)
        {
            DataTable dataTable = null;

            if (dataSource is DataGridView dataGridView)
            {
                // إذا كان المصدر DataGridView
                dataTable = ((DataTable)dataGridView.DataSource).Copy();
            }
            else if (dataSource is BindingSource bindingSource)
            {
                // إذا كان المصدر BindingSource
                dataTable= bindingSource.Cast<DataRow>().CopyToDataTable();

            }

            if (dataTable == null)
            {
                throw new ArgumentException("Invalid data source. Must be a DataGridView or BindingSource.");
            }
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
            }

            // التحقق من أن جدول البيانات ليس فارغًا
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return dataTable; // أو يمكنك إرجاع جدول بيانات فارغ جديد
            }

            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, dataTable.Rows.Count);

            // استخدام Select لانتقاء الصفوف المطلوبة
            var selectedRows = dataTable.Rows.Cast<DataRow>()
                                            .Skip(startIndex)
                                            .Take(endIndex - startIndex);

            // إنشاء جدول بيانات جديد لاحتواء الصفوف المحددة
            DataTable pagedDataTable = dataTable.Clone();
            foreach (var row in selectedRows)
            {
                pagedDataTable.Rows.Add(row.ItemArray);
            }
            if (pagedDataTable.Rows.Count < 1 && pageNumber - 1 > 0)
                pagedDataTable = getPagedDataTable(dataTable, pageNumber - 1, pageSize);
            return pagedDataTable;
        }
        static public string getCurrentRoot()
        {
            string root="";
            //List<string> listviewRoot = Program.getListviewRoot();
            //for (int i = 0; i < listviewRoot.Count(); i++)
            //{
            //    root += listviewRoot[i] + ">";
            //}
            return root;
        }
        
        //public static DataTable SearchDataTable(DataTable dataTable, List<String> conditions)
        //        {
        //            // تحديد حجم القسم (يمكن تعديله حسب الحاجة)
        //            int partitionSize = 1000;

        //            // تقسيم DataTable إلى أقسام
        //            var partitions = dataTable.AsEnumerable()
        //                                                   .Select((row, Key) => new { Index = Key, Row = row })
        //                                                   .GroupBy(x => x.Index / partitionSize)
        //                                                   .Select(g => g.Select(x => x.Row).CopyToDataTable());

        //            // البحث في كل قسم على حدة بشكل متوازٍ مع استخدام الكاش
        //            var results = partitions.AsParallel()
        //                                    .SelectMany(partition =>
        //                                    {
        //                                        // هنا نستخدم الدالة الأصلية مع إضافة الكاش
        //                                        var query = partition.AsEnumerable();
        //                                        // ... نفس كود الدالة الأصلية مع استخدام الكاش ...
        //                                        return query.CopyToDataTable();
        //                                    })
        //                                    .CopyToDataTable();

        //            return results;
        //        }
        //    }



        public static DataTable SearchDataTable(DataTable dataTable, List<SearchCondition> conditions)
        {
            DataTable data = dataTable.Clone();
            var query = dataTable.AsEnumerable();
            var fainalData = dataTable.AsEnumerable();
            DataTable mergedTable = dataTable.Clone();
            foreach (var condition in conditions)
            {
               
                query = query.Where(row =>
                {
                    var value = row.Field<object>(condition.ColumnName);
                    switch (condition.DataType)
                    {
                        case DataType.String:
                            if (condition.ComparisonType == ComparisonType.Contains)
                                return value.ToString().Contains(condition.SearchValue.ToString());
                            else if (condition.ComparisonType == ComparisonType.Equals)
                                return value.ToString() == condition.SearchValue.ToString();
                            break;
                        case DataType.Int:
                          
                            return Convert.ToInt32(value) == Convert.ToInt32(condition.SearchValue);
                        case DataType.DateTime:
                            return Convert.ToDateTime(value) == Convert.ToDateTime(condition.SearchValue);
                        // ... حالات أخرى لأنواع البيانات
                        default:
                            return false;
                    }
                    return false;
                });
              
            }
            if (query.Count()>0)
            {
                data = query.CopyToDataTable();

            }
            data.TableName = data.Rows.Count.ToString();
           
            return data;
        }
        //public static DataTable SearchDataTable(DataTable dataTable, List<SearchCondition> conditions)

        //{

        //    var query = dataTable.AsEnumerable();



        //    foreach (var condition in conditions)

        //    {

        //        query = query.Where(row =>

        //        {

        //            var value = row.Field<object>(condition.ColumnName);

        //            string cacheKey = condition.ColumnName + "_" + condition.DataType;



        //            if (!condition.cachedValues.TryGetValue(cacheKey, out object cachedValue))

        //            {

        //                // إذا لم تكن القيمة موجودة في الكاش، نقوم بالتحويل ونخزنها

        //                switch (condition.DataType)

        //                {

        //                    case DataType.String:

        //                        cachedValue = value.ToString();

        //                        break;

        //                    case DataType.Int:

        //                        cachedValue = Convert.ToInt32(value);

        //                        break;

        //                    case DataType.DateTime:

        //                        cachedValue = Convert.ToDateTime(value);

        //                        break;

        //                    // ... حالات أخرى لأنواع البيانات

        //                    default:

        //                        return false;

        //                }

        //                condition.cachedValues[cacheKey] = cachedValue;

        //            }

        //            else

        //            {

        //                value = cachedValue;

        //            }



        //            // مقارنة القيم بناءً على نوع المقارنة

        //            switch (condition.ComparisonType)

        //            {

        //                case ComparisonType.Contains:

        //                    return value.ToString().Contains(condition.SearchValue.ToString());

        //                case ComparisonType.Equals:

        //                    return value.Equals(condition.SearchValue);

        //                // ... حالات أخرى لأنواع المقارنة

        //                default:

        //                    return false;

        //            }

        //        });

        //    }
        //    if(query.CopyToDataTable().Rows.Count > 0)                 
        //    return query.CopyToDataTable();
        //    else return dataTable = new DataTable();
        //}
//         catch (DbEntityValidationException ex)
// {
//     foreach (var validationError in ex.EntityValidationErrors)
//     {
//         foreach (var error in validationError.ValidationErrors)

//         {
//             AppDialogAleart.showAleartError("Property: " + error.PropertyName + " Error: " + error.ErrorMessage);
//         }
//}
//transaction.Rollback();
//AppDialogAleart.showAleartError(ex.Message);
//status = false;
// }

    }


}
