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
    public class CompositeItemsInventoryController :ReportItemsAndStoresController
    {
       
            public List<DataSetCompositeItemsInventory> tempData;
            public CompositeItemsInventoryController()
            {
                dBContext = new AccountingDbContext();
                dataSource = new BindingSource();
                tempData = new List<DataSetCompositeItemsInventory> { };
                dataSource.DataSource = typeof(DataSetCompositeItemsInventory);
            }

            public bool search()
            {
                tempData = new List<DataSetCompositeItemsInventory>();
                try
                {
                    //  Supplier supplier = account != null ? suppliers.FirstOrDefault(a => a.Account.id == account.id) : null;
                    var sales = this.sales.Where(sale => (store == null || sale.storeId == store.id) );
                    var purchases = this.purchases.Where(purchase => (store == null || purchase.storeId == store.id) );
                    var saleBeforStartDate = sales.Where(sale => (startDate == null || sale.date.Value.Date < startDate.Value.Date));
                    var saleBetweenDates = sales.Where(sale => (startDate == null || sale.date.Value.Date >= startDate.Value.Date) && (endDate == null || sale.date.Value.Date <= endDate.Value.Date));
                    var purchasesBeforStartDate = purchases.Where(purchase => (startDate == null || purchase.date.Value.Date < startDate.Value.Date));
                    var purchasesBetweenDates = purchases.Where(purchase => (startDate == null || purchase.date.Value.Date >= startDate.Value.Date) && (endDate == null || purchase.date.Value.Date <= endDate.Value.Date));

                    foreach (var item in itemsWithAllData)
                    {

                        foreach (var measurementItem in item.MeasurementsItems)
                        {

                        if (measurementItem.CompositeItems.Any())
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
                            quantityAndPriceBetweenDates.purchased.pasicPricePerPill = measurementItem.purchasePrice ?? 0;
                            tempData.Add(new DataSetCompositeItemsInventory()
                            {
                                number = item.ClassifyNumber.Value,
                                name = item.nameAr,
                                titel = "صنف مركب " + item.nameAr,
                                purchasePricePerPill = measurementItem.purchasePrice ?? 0,
                                unitName = measurementItem.Unit.name,
                                purchasePrice = quantityAndPriceBetweenDates.purchased.price,
                                purchaseQuantity = quantityAndPriceBetweenDates.purchased.quantity,
                                salePrice = quantityAndPriceBetweenDates.salsed.price,
                                saleQuantity = quantityAndPriceBetweenDates.salsed.quantity,
                                previousBalancePrice = previesQuantityAndPrice.finalPriceBalance(),
                                previousBalanceQuantity = previesQuantityAndPrice.finalQuantityBalance(),
                                balancePrice = quantityAndPriceBetweenDates.finalPriceBalance() + previesQuantityAndPrice.finalPriceBalance(),
                                balanceQuantity = quantityAndPriceBetweenDates.finalQuantityBalance() + previesQuantityAndPrice.finalQuantityBalance(),
                                numberColor = 0

                            });
                            bool titelCompositeItems = false;
                            foreach (CompositeItem compositeItem in measurementItem.CompositeItems)
                            {
                                 previesQuantityAndPrice = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
                                if (startDate != null)
                                {
                                    previesQuantityAndPrice.salsed = saleBeforStartDate.QuantityAndPriceMeasurementItemById(compositeItem.ComponentItem.id);
                                    previesQuantityAndPrice.purchased = purchasesBeforStartDate.QuantityAndPriceMeasurementItemById(compositeItem.ComponentItem.id);
                                    previesQuantityAndPrice.purchased.pasicPricePerPill = compositeItem.purchasePrice ?? 0;
                                }
                                quantityAndPriceBetweenDates = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
                                quantityAndPriceBetweenDates.salsed = saleBetweenDates.QuantityAndPriceMeasurementItemById(compositeItem.ComponentItem.id);
                                quantityAndPriceBetweenDates.purchased = purchasesBetweenDates.QuantityAndPriceMeasurementItemById(compositeItem.ComponentItem.id);
                                quantityAndPriceBetweenDates.purchased.pasicPricePerPill = compositeItem.purchasePrice ?? 0;
                                tempData.Add(new DataSetCompositeItemsInventory()
                                {
                                    number = compositeItem.ComponentItem.item.ClassifyNumber.Value,
                                    name = compositeItem.ComponentItem.item.nameAr,
                                    titel = !titelCompositeItems ? ("الاصناف المركبة للصنف:  " + item.nameAr) : " ",
                                    purchasePricePerPill = compositeItem.purchasePrice ?? 0,
                                    unitName = compositeItem.ComponentItem.Unit.name,
                                    purchasePrice = 0, //quantityAndPriceBetweenDates.purchased.price,
                                    purchaseQuantity = 0,//quantityAndPriceBetweenDates.purchased.quantity,
                                    salePrice = 0,// quantityAndPriceBetweenDates.salsed.price,
                                    saleQuantity = 0,// quantityAndPriceBetweenDates.salsed.quantity,
                                    previousBalancePrice = previesQuantityAndPrice.finalPriceBalance(),
                                    previousBalanceQuantity = previesQuantityAndPrice.finalQuantityBalance(),
                                    balancePrice = quantityAndPriceBetweenDates.finalPriceBalance() + previesQuantityAndPrice.finalPriceBalance(),
                                    balanceQuantity = quantityAndPriceBetweenDates.finalQuantityBalance() + previesQuantityAndPrice.finalQuantityBalance(),
                                    numberColor = !titelCompositeItems ? 1 : -1
                                });
                                titelCompositeItems = true;
                            }
                        }
                        }

                    }
                    //if (tempData.Any())
                    //    tempData.Add(new DataSetItemQuantities()
                    //    {
                    //        name = "الإجمالي",
                    //        number = 0,
                    //        unitName = "#",
                    //        purchasePrice = tempData.Sum(item => item.purchasePrice),
                    //        purchaseQuantity = tempData.Sum(item => item.purchaseQuantity),
                    //        salePrice = tempData.Sum(item => item.salePrice),
                    //        saleQuantity = tempData.Sum(item => item.saleQuantity),
                    //        balancePrice = tempData.Sum(item => item.balancePrice),
                    //        balanceQuantity = tempData.Sum(item => item.balanceQuantity),
                    //        previousBalancePrice = tempData.Sum(item => item.previousBalancePrice),
                    //        previousBalanceQuantity = tempData.Sum(item => item.previousBalanceQuantity),
                    //        numberColor = 1
                    //    });
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
