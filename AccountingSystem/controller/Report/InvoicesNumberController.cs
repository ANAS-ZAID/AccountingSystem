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
    public class InvoicesNumberController:ReportItemsAndStoresController
    {
        public List<DataSetInvoicesNumber> tempData;
        public InvoicesNumberController()
        {
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            tempData = new List<DataSetInvoicesNumber> { };
            dataSource.DataSource = typeof(DataSetInvoicesNumber);
        }

        public bool search()
        {
            tempData = new List<DataSetInvoicesNumber>();
            try
            {
                
                var sales = this.sales.Where(sale => (store == null || sale.storeId == store.id)&&sale.date.Between(startDate,endDate)&&account==null);
                var purchases = this.purchases.Where(purchase => (store == null || purchase.storeId == store.id)&&purchase.date.Between(startDate, endDate)&&(supplier==null||purchase.supplierId==supplier.id));

                foreach (var purchase in purchases)
                {
                    tempData.Add(new DataSetInvoicesNumber()
                    {
                        number = purchase.number.Value,
                        type = purchase.type.Replace("_", " "),
                        date = purchase.date.Format(),
                        name = purchase.Supplier.name,
                        store = purchase.Store.name,
                    });
                }
                foreach (var sale in sales)
                {
                    tempData.Add(new DataSetInvoicesNumber()
                    {
                        number = sale.number.Value,
                        type = sale.type.Replace("_", " "),
                        date = sale.date.Format(),
                        name = sale.Customer.name,
                        store = sale.Store.name,
                    });
                }
         
               dataSource.DataSource = tempData?.OrderByDescending(x => Convert.ChangeType(x.date, typeof(DateTime)))?.ToList();
            }
            catch 
            {
               
  
                AppDialogAleart.showAleartError();
            }
            return tempData.Any();
        }
    }
    public class DataSetInvoicesNumber : DataSetInvoicesAndStores
    {
        public string type { get; set; }
        public string date { get; set; }
        public string store { get; set; }

    }
}
