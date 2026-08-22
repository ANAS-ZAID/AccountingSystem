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
    public class StocItemsLessThanZeroController: ReportItemsAndStoresController
    {
            public List<DataSetStocItemsLessThanZero> tempData;
            public StocItemsLessThanZeroController()
            {
                dBContext = new AccountingDbContext();
                dataSource = new BindingSource();
                tempData = new List<DataSetStocItemsLessThanZero> { };
                dataSource.DataSource = typeof(DataSetStocItemsLessThanZero);
            }

            public bool search(int number)
            {
                tempData = new List<DataSetStocItemsLessThanZero>();
                try
                {
                 
                    var sales = this.sales.Where(sale => (store == null || sale.storeId == store.id));
                    var purchases = this.purchases.Where(purchase => (store == null || purchase.storeId == store.id));
                

                    foreach (var item in itemsWithAllData)
                    {

                        foreach (var measurementItem in item.MeasurementsItems)
                        {

                           
                            {
                                TotalQuantityAndPriceMeasurementItem quantityAndPriceBetweenDates = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 }, salsed = new QuantityAndPrice() { price = 0, quantity = 0 } };
                                quantityAndPriceBetweenDates.salsed = sales.QuantityAndPriceMeasurementItemById(measurementItem.id);
                                quantityAndPriceBetweenDates.purchased = purchases.QuantityAndPriceMeasurementItemById(measurementItem.id);
                            int finalQuantityBalance = quantityAndPriceBetweenDates.finalQuantityBalance();
                            if (finalQuantityBalance<=number)
                            tempData.Add(new DataSetStocItemsLessThanZero()
                                {
                                    name = item.nameAr ,
                                    number = item.ClassifyNumber.Value,
                                    unitName = measurementItem.Unit.name,
                                    features= "الباركود : " + measurementItem.barcode,
                                    quantity = finalQuantityBalance
                                });  
                            }
                        }

                    }
                 tempData=tempData?.OrderedByQuantity();
                    dataSource.DataSource = tempData;
                }
                catch (DbEntityValidationException ex)
                {
                    AppDialogAleart.showEntityValidationErrors(ex);
                    AppDialogAleart.showAleartError();
                }
                return tempData.Any();
            }

    }
}
