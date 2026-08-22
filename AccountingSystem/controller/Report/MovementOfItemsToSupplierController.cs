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
    public class MovementOfItemsToSupplierController: ReportItemsAndStoresController
    {
       
            public List<DataSetMovementOfItemsToSupplier> tempData;
            public MovementOfItemsToSupplierController()
            {
                dBContext = new AccountingDbContext();
                dataSource = new BindingSource();
                tempData = new List<DataSetMovementOfItemsToSupplier> { };
                dataSource.DataSource = typeof(DataSetItemQuantities);
            }

            public bool search()
            {
          //  bool status=tr;
                tempData = new List<DataSetMovementOfItemsToSupplier>();
            if(supplier==null)
            {
                AppDialogAleart.showAleartError("إختر المورد من فضلك");
                return false;
            }
            try
                {
                  
                    var purchases = this.purchases.Where(purchase => (supplier == null || purchase.supplierId == supplier.id));
                    var purchasesBetweenDates = purchases.Where(purchase => (startDate == null || purchase.date.Value.Date >= startDate.Value.Date) && (endDate == null || purchase.date.Value.Date <= endDate.Value.Date));


                    foreach (var item in itemsWithAllData.Where(x => (selectedItem == null || x.id == selectedItem.id )).ToList().GetAllItemsWithChildren())
                    {

                        foreach (var measurementItem in item.MeasurementsItems)
                        {

                            {
                   
                                TotalQuantityAndPriceMeasurementItem quantityAndPriceBetweenDates = new TotalQuantityAndPriceMeasurementItem() { purchased = new QuantityAndPrice() { price = 0, quantity = 0 } };
                                quantityAndPriceBetweenDates.purchased = purchasesBetweenDates.QuantityAndPriceMeasurementItemById(measurementItem.id);
                                quantityAndPriceBetweenDates.purchased.pasicPricePerPill = measurementItem.purchasePrice ?? 0;
                          tempData.Add(new DataSetMovementOfItemsToSupplier()
                                {
                                    supplierName=supplier.name,
                                    name = item.nameAr,
                                    unitName = measurementItem.Unit.name,
                                    purchasePrice = quantityAndPriceBetweenDates.purchased.price,
                                    purchaseQuantity = quantityAndPriceBetweenDates.purchased.quantity,
                                });

                                   
                            }
                        }

                    }
                    if (tempData.Any())
                        tempData.Add(new DataSetMovementOfItemsToSupplier()
                        {
                            name = "الإجمالي",
                            unitName = "#",
                            purchasePrice = tempData.Sum(item => item.purchasePrice),
                            purchaseQuantity = tempData.Sum(item => item.purchaseQuantity),
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

