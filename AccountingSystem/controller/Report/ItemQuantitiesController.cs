using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class ItemQuantitiesController: ReportItemsAndStoresController
    {
       public List<DataSetItemQuantities> tempData;
        public ItemQuantitiesController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
           tempData = new List<DataSetItemQuantities> { };
            dataSource.DataSource = typeof(DataSetItemQuantities);
        }

        public bool search()
        {
            tempData=new List<DataSetItemQuantities>();
            try { 
          //  Supplier supplier = account != null ? suppliers.FirstOrDefault(a => a.Account.id == account.id) : null;
            var sales = this.sales.Where(sale => (store == null || sale.storeId == store.id)&& account==null);
            var purchases = this.purchases.Where(purchase => (store == null || purchase.storeId == store.id) && (account == null || purchase.Supplier.accountId == account.id));
            var saleBeforStartDate = sales.Where(sale => (startDate == null || sale.date.Value.Date < startDate.Value.Date));
            var saleBetweenDates = sales.Where(sale => (startDate == null || sale.date.Value.Date >= startDate.Value.Date) && (endDate == null || sale.date.Value.Date <= endDate.Value.Date));
            var purchasesBeforStartDate = purchases.Where(purchase => (startDate == null || purchase.date.Value.Date < startDate.Value.Date));
            var purchasesBetweenDates = purchases.Where(purchase => (startDate == null || purchase.date.Value.Date >= startDate.Value.Date) && (endDate == null || purchase.date.Value.Date <= endDate.Value.Date));
            
            foreach (var item in itemsWithAllData.Where(x => (selectedItem == null || x.id == selectedItem.id || (selectedItem.type == "رئيسي" && x.parentId == selectedItem.id)) && (group == null || x.ClassifyGroupId == group.id)))
            {
                    
                foreach (var measurementItem in item.MeasurementsItems)
                {
                      
                     //   if (withoutCompoundItems &&! measurementItem.CompositeItems.Any())
                        {
                            TotalQuantityAndPriceMeasurementItem previesQuantityAndPrice = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
                            if (startDate != null)
                            {
                                previesQuantityAndPrice.salsed = saleBeforStartDate.QuantityAndPriceMeasurementItemById(measurementItem.id);
                                previesQuantityAndPrice.purchased = purchasesBeforStartDate.QuantityAndPriceMeasurementItemById(measurementItem.id);
                                previesQuantityAndPrice.purchased.pasicPricePerPill = measurementItem.purchasePrice ?? 0;
                            }
                            TotalQuantityAndPriceMeasurementItem quantityAndPriceBetweenDates = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
                            quantityAndPriceBetweenDates.salsed = saleBetweenDates.QuantityAndPriceMeasurementItemById(measurementItem.id);
                            quantityAndPriceBetweenDates.purchased = purchasesBetweenDates.QuantityAndPriceMeasurementItemById(measurementItem.id);
                            quantityAndPriceBetweenDates.purchased.pasicPricePerPill = measurementItem.purchasePrice??0;
                            DataSetItemQuantities itemQuantities = new DataSetItemQuantities()
                            {
                                name = item.nameAr+",الباركود : "+measurementItem.barcode,
                                number = item.ClassifyNumber.Value,
                                unitName = measurementItem.Unit.name,
                                purchasePrice = quantityAndPriceBetweenDates.purchased.price,
                                purchaseQuantity = quantityAndPriceBetweenDates.purchased.quantity,
                                salePrice = quantityAndPriceBetweenDates.salsed.price,
                                saleQuantity = quantityAndPriceBetweenDates.salsed.quantity,
                                previousBalancePrice =  previesQuantityAndPrice.finalPriceBalance(),
                                previousBalanceQuantity = previesQuantityAndPrice.finalQuantityBalance(),
                                balancePrice = quantityAndPriceBetweenDates.finalPriceBalance() + previesQuantityAndPrice.finalPriceBalance(),
                                balanceQuantity = quantityAndPriceBetweenDates.finalQuantityBalance() + previesQuantityAndPrice.finalQuantityBalance(),
                            };

                            if(withoutZeroItems)
                            {
                                if(quantityAndPriceBetweenDates.salsed.quantity>0|| quantityAndPriceBetweenDates.purchased.quantity > 0)
                                tempData.Add(itemQuantities);
                            }
                            else if (withoutCompoundItems)
                            {
                                if (!measurementItem.CompositeItems.Any())
                                tempData.Add(itemQuantities);
                            }
                            else
                            {
                                tempData.Add(itemQuantities);
                            }
                        }
                }
                
             }
                    if(tempData.Any()) 
                    tempData.Add(new DataSetItemQuantities()
                    {
                        name = "الإجمالي",
                        number = 0,
                        unitName = "#",
                        purchasePrice = tempData.Sum(item=>item.purchasePrice),
                        purchaseQuantity = tempData.Sum(item => item.purchaseQuantity),
                        salePrice = tempData.Sum(item => item.salePrice),
                        saleQuantity = tempData.Sum(item => item.saleQuantity),
                        balancePrice = tempData.Sum(item => item.balancePrice),
                        balanceQuantity = tempData.Sum(item => item.balanceQuantity),   
                        previousBalancePrice = tempData.Sum(item => item.previousBalancePrice),
                        previousBalanceQuantity = tempData.Sum(item => item.previousBalanceQuantity),
                        numberColor=1
                    });
                dataSource.DataSource = tempData;
            }
            catch
            {
               
                AppDialogAleart.showAleartError();
            }
            return tempData.Any();
        }
    }
}
