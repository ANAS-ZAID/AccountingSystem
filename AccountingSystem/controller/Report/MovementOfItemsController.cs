using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.controller
{
    internal class MovementOfItemsController: ReportItemsAndStoresController
    {
            public List<DataSetMovementOfItems> tempData;
            public MovementOfItemsController()
            {
                dBContext = new AccountingDbContext();
                dataSource = new BindingSource();
                tempData = new List<DataSetMovementOfItems> { };
                dataSource.DataSource = typeof(DataSetItemQuantities);
            }

            public bool search()
            {
            bool status=true;
                tempData = new List<DataSetMovementOfItems>();
           // AppDialogAleart.showAleartNoPermissions("selectedItem.nameAr");
               if(selectedItem==null)
            {
                AppDialogAleart.showAleartError("إختر الصنف من فضلك");
                return false;
            }
            try
                {
                   
                    var allSales = this.sales.Where(sale => (store == null || sale.storeId == store.id) && account == null);
                    var allPurchases = this.purchases.Where(purchase => (store == null || purchase.storeId == store.id) && (account == null || purchase.Supplier.accountId == account.id));
                    var saleBetweenDates = allSales.Where(sale => sale.date.Between(startDate, endDate));
                    var purchasesBetweenDates = allPurchases.Where(purchase => purchase.date.Between(startDate, endDate));
                      var inventoryTransfer = dBContext.InventoryTransfers.ToList().Where(trans =>trans.date.Between(startDate,endDate)&& (store == null || trans.fromStoreId == store.id || trans.toStoreId == store.id) && account == null);
                      var allBeginningInventories = dBContext.BeginningInventories.ToList().Where(beginningInventor => (store == null || beginningInventor.storeId == store.id) && account == null);
                  
                // var sales = saleBetweenDates.Original(invoiceType);
                    //  var salesReturned = saleBetweenDates.Returned(invoiceType);

                //var purchases = purchasesBetweenDates.Original(invoiceType);
                //var purchasesReturned = purchasesBetweenDates.Returned(invoiceType);


                //foreach (var item in itemsWithAllData.Find(x => (selectedItem == null || x.id == selectedItem.id)))
                    {

                    var item = itemsWithAllData.Find(x =>  x.id == selectedItem.id);
                        foreach (var measurementItem in item.MeasurementsItems)
                        {

                         var beginningInventories=allBeginningInventories?.Where(x=>x.measurementItemId == measurementItem.id);

                        if(beginningInventories?.Any()??false)
                            tempData.Add(new DataSetMovementOfItems()
                            {
                                number = item.ClassifyNumber ?? 0,
                                name = item.nameAr + ", الباركود : " + measurementItem.barcode,
                                unitName = measurementItem.Unit.name,
                                account = "_",
                                invoiceType = "رصيد إفتتاحي",
                                invoiceNumber = 0,
                                quantity = beginningInventories?.Sum(x => (x.quantity??0)) ?? 0,
                                unitPrice = beginningInventories?.Sum(x => (x.quantity ?? 0)>0?(x.unitPrice??0):0) ?? 0,
                                totalPrice =0,
                                date = DateTime.MinValue,
                                balanceQuantity = tempData.Any() ? tempData.Last().balanceQuantity : 0,
                                description = "رصيد إفتتاحي",
                                numberColor = 1
                            });
                        foreach (var sale in saleBetweenDates)
                            {
                            bool isSales = sale.IsSales(invoiceType);
                            if (isSales||sale.IsReturned(invoiceType))
                            {
                                var saleDetails = sale.SaleDetails.Where(detail => detail.measurementItemId == measurementItem.id);
                                if (saleDetails.Any())
                                {
                                    foreach (var detail in saleDetails)
                                    {
                                        tempData.Add(new
                                           DataSetMovementOfItems()
                                        {
                                            number = item.ClassifyNumber ?? 0,
                                            name = item.nameAr + ", الباركود : " + measurementItem.barcode,
                                            unitName = measurementItem.Unit.name,
                                            account = sale.Customer.Account.name,
                                            invoiceType =(isSales ? InvoiceType.مبيعات : InvoiceType.مرتجع_مبيعات).ToString(),
                                            invoiceNumber = sale.number ?? 0,
                                            quantity = detail.quantity ?? 0,
                                            unitPrice = detail.unitPrice ?? 0,
                                            totalPrice = detail.TotalPrice(),
                                            date = sale.date.Value,
                                            balanceQuantity = 0,
                                            description = detail.description,
                                        });
                                    }
                                }
                            }
                            }
                           
                        foreach (var purchase in purchasesBetweenDates)
                        {
                            bool isPurchase = purchase.IsPurchases(invoiceType);
                            if ( isPurchase||purchase.IsReturned(invoiceType)) 
                            {
                                var purchaseDetails = purchase.PurchaseDetails.Where(detail => detail.measurementItemId == measurementItem.id);
                                if (purchaseDetails.Any())
                                {
                                    foreach (var detail in purchaseDetails)
                                    {

                                        tempData.Add(new DataSetMovementOfItems()
                                        {
                                            number = item.ClassifyNumber ?? 0,
                                            name = item.nameAr + ", الباركود : " + measurementItem.barcode,
                                            unitName = measurementItem.Unit.name,
                                            account = purchase.Supplier.Account.name,
                                            invoiceType =isPurchase? InvoiceType.مشتريات.ToString():InvoiceType.مرتجع_مشتريات.ToString(),
                                            invoiceNumber = purchase.number ?? 0,
                                            quantity = detail.quantity ?? 0,
                                            unitPrice = detail.unitPrice ?? 0,
                                            totalPrice = detail.TotalPrice(),
                                            date = purchase.date.Value,
                                            balanceQuantity = 0,
                                            description = detail.description,
                                        });
                                    }
                                }
                            }

                            
                            }
                        foreach (var transfer in inventoryTransfer)
                        {
                            //bool isPurchase = purchase.IsPurchases(invoiceType);
                            //if (isPurchase || purchase.IsReturned(invoiceType))
                            for (int i = 0; i < 2; i++)
                            {
                                var transferDetails = transfer.InventoryTransferDetails.Where(detail => detail.measurementItemId == measurementItem.id);
                                if (transferDetails.Any())
                                {   
                                    foreach (var detail in transferDetails)
                                    {
                                       
                                        if ( (i == 0&&detail.type!=MeasurementsItemType.مركب.ToString() && (store == null || transfer.fromStoreId == store.id)) || (i == 1 && (store == null || transfer.toStoreId == store.id)))
                                            tempData.Add(new DataSetMovementOfItems()
                                            {
                                                number = item.ClassifyNumber ?? 0,
                                                name = item.nameAr + ", الباركود : " + measurementItem.barcode,
                                                unitName = measurementItem.Unit.name,
                                                account = "",
                                                invoiceType = i == 0 ? InvoiceType.تحويل_صادر.ToString() : InvoiceType.تحويل_وارد.ToString(),
                                                invoiceNumber = transfer.number ?? 0,
                                                quantity = detail.quantity ?? 0,
                                                unitPrice = detail.unitPrice ?? 0,
                                                totalPrice = detail.TotalPrice(),
                                                date = transfer.date.Value,
                                                balanceQuantity = 0,
                                                description = detail.description,
                                            });
                                    }
                                }
                            }
                        }
                            decimal runningSalesQuantity = 0;
                        decimal runningPurchasesQuantity = 0;
                        tempData = tempData.OrderBy(x => x.date).ToList();
                        foreach (var detail in tempData)
                        {
                            if (detail.invoiceType == InvoiceType.مبيعات.ToString()|| detail.invoiceType== InvoiceType.مرتجع_مشتريات.ToString()|| detail.invoiceType == InvoiceType.تحويل_صادر.ToString())
                                runningSalesQuantity += detail.quantity;
                            else
                                runningPurchasesQuantity += detail.quantity;
                            detail.balanceQuantity = runningPurchasesQuantity - runningSalesQuantity;
                            detail.invoiceType = detail.invoiceType.Replace("_", " ");
                        }
                     //   if (tempData.Any())
                            tempData.Add(new DataSetMovementOfItems()
                            {
                                name = "الإجمالي",
                                number = 0,
                                unitName = "#",
                                account = "الإجمالي",
                                invoiceType = "الإجمالي",
                                invoiceNumber =0,
                                quantity = tempData?.Sum(x => x.quantity)??0,
                                unitPrice = tempData?.Sum(x => x.unitPrice) ?? 0,
                                totalPrice = tempData?.Sum(x => x.totalPrice) ?? 0,
                                date =DateTime.MinValue,
                                balanceQuantity = tempData.Any()? tempData.Last().balanceQuantity : 0,
                                description ="#",
                                numberColor = 1
                            });
                         }
                    
                    }

                   
                    dataSource.DataSource = tempData;
                }
                catch 
                {
                   
                    AppDialogAleart.showAleartError();
                status=false;
                }
                return status;
            }

    }
}
