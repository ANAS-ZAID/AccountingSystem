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
    public class WarehouseQuantitiesController: ReportItemsAndStoresController
    {
            public List<DataSetItemQuantities> tempData;
            public WarehouseQuantitiesController()
            {
                dBContext = new AccountingDbContext();
                dataSource = new BindingSource();
                tempData = new List<DataSetItemQuantities> { };
                dataSource.DataSource = typeof(DataSetItemQuantities);
            }

            public bool search()
            {
                tempData = new List<DataSetItemQuantities>();
                try
                {

                var items = itemsWithAllData.Where(x => (selectedItem == null || x.id == selectedItem.id) && (group == null || x.ClassifyGroupId == group.id)).ToList();
                if (selectedItem == null || selectedItem.Childrens.Any())
                    items = items.GetAllItemsWithChildren().Where(x => (group == null || x.ClassifyGroupId == group.id)).ToList();
                foreach (var item in items )
                    {
                      
                        foreach (var measurementItem in item.MeasurementsItems)
                        {
                      
                        //   if (withoutCompoundItems &&! measurementItem.CompositeItems.Any())
                        foreach (var store in dBContext.Stores.ToList().Where(s => (store == null || s.id == store.id)))
                        {
                            AppDialogAleart.showAleartNoPermissions("nameAr=" + measurementItem.item.nameAr + ",store=" + store.name);
                            var saleBeforStartDate = store.Sales.Where(sale => sale.date.Before(startDate));
                            var saleBetweenDates = store.Sales.Where(sale => sale.date.Between(startDate, endDate));
                            var purchases = store.Purchases.Where(p => p.IsPurchases());
                            var purchasesBeforStartDate = purchases.Where(purchase => purchase.date.Before(startDate));
                            var purchasesBetweenDates = purchases.Where(purchase => purchase.date.Between(startDate, endDate));
     

                                TotalQuantityAndPriceMeasurementItem previesQuantityAndPrice = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
                                if (startDate != null)
                                {
                                    previesQuantityAndPrice=getQuantityItem(saleBeforStartDate,purchasesBeforStartDate,measurementItem.id); 
                                }
                               
                                TotalQuantityAndPriceMeasurementItem quantityAndPriceBetweenDates = getQuantityItem(saleBetweenDates, purchasesBetweenDates, measurementItem.id);
                                DataSetItemQuantities itemQuantities = new DataSetItemQuantities()
                                {
                                    name = item.nameAr,
                                    store=store.name,
                                    number = item.ClassifyNumber.Value,
                                    unitName = measurementItem.Unit.name,
                                    purchasePrice = quantityAndPriceBetweenDates.purchased.price,
                                    purchaseQuantity = quantityAndPriceBetweenDates.purchased.quantity,
                                    salePrice = quantityAndPriceBetweenDates.salsed.price,
                                    saleQuantity = quantityAndPriceBetweenDates.salsed.quantity,
                                    purchasePricePerPill=measurementItem.purchasePrice.Format(),
                                    previousBalancePrice = previesQuantityAndPrice.finalPriceBalance(),
                                    previousBalanceQuantity = previesQuantityAndPrice.finalQuantityBalance(),
                                    balancePrice = quantityAndPriceBetweenDates.finalPriceBalance() + previesQuantityAndPrice.finalPriceBalance(),
                                    balanceQuantity = quantityAndPriceBetweenDates.finalQuantityBalance() + previesQuantityAndPrice.finalQuantityBalance(),
                                };

                                if (withoutZeroItems)
                                {
                                    if (quantityAndPriceBetweenDates.salsed.quantity > 0 || quantityAndPriceBetweenDates.purchased.quantity > 0)
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
              

                if (tempData.Any())
                        tempData.Add(new DataSetItemQuantities()
                        {
                            name = "الإجمالي",
                            number = 0,
                            unitName = "#",
                            store="#",
                            purchasePricePerPill="#",
                            purchasePrice = tempData.Sum(item => item.purchasePrice),
                            purchaseQuantity = tempData.Sum(item => item.purchaseQuantity),
                            salePrice = tempData.Sum(item => item.salePrice),
                            saleQuantity = tempData.Sum(item => item.saleQuantity),
                            balancePrice = tempData.Sum(item => item.balancePrice),
                            balanceQuantity = tempData.Sum(item => item.balanceQuantity),
                            previousBalancePrice = tempData.Sum(item => item.previousBalancePrice),
                            previousBalanceQuantity = tempData.Sum(item => item.previousBalanceQuantity),
                            numberColor = 1
                        });
                    dataSource.DataSource = tempData;
                }
                catch(DbEntityValidationException e) 
                {
                AppDialogAleart.showEntityValidationErrors(e);
                    AppDialogAleart.showAleartError();
                }
                return tempData.Any();
            }

        private TotalQuantityAndPriceMeasurementItem getQuantityItem(IEnumerable<Sale> sale, IEnumerable<Purchase> purchases, int id)
        {
            TotalQuantityAndPriceMeasurementItem quantityAndPrice = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
            quantityAndPrice.salsed = sale.QuantityAndPriceMeasurementItemById(id);
            quantityAndPrice.purchased = purchases.QuantityAndPriceMeasurementItemById(id);
            quantityAndPrice.purchased.pasicPricePerPill = dBContext.MeasurementsItems?.Find(id)?.purchasePrice??0;
      return quantityAndPrice;
        }
    }
}
