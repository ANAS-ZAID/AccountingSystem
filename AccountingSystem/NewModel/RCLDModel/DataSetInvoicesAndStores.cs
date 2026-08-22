using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.core.Functions;

namespace AccountingSystem.NewModel.RCLDModel
{
  
    abstract public class DataSetInvoicesAndStores
    {
        public int number { get; set; }
        public string name { get; set; }
        public string unitName { get; set; }
        public int numberColor { get; set; }

    }
    public class TotalQuantityAndPriceMeasurementItem
    {

        public QuantityAndPrice purchased = new QuantityAndPrice();
        public QuantityAndPrice salsed = new QuantityAndPrice();
        public decimal finalPriceBalance() =>  purchased.price - (purchased.Average() > 0 ? purchased.Average() :purchased.pasicPricePerPill) * salsed.quantity;
        public decimal finalTotalProfit() {
           // AppDialogAleart.showAleartNoPermissions("purchased.Average()="+ purchased.Average()); AppDialogAleart.showAleartNoPermissions("salsed.quantity=" + salsed.quantity);
            return salsed.price - ((purchased.quantity> 0) ? purchased.Average() : purchased.pasicPricePerPill) * salsed.quantity;}
        public int finalQuantityBalance() => purchased.quantity - salsed.quantity;
    }
    public struct QuantityAndPrice
    {
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal pasicPricePerPill { get; set; }

        //  public decimal totalPrice() => price;
        public decimal Average() => quantity>0? price / quantity:0;
        //* quantity
    }
    public struct FinalInvoiceData
    {
        public decimal quantityHours { get; set; }
        //public decimal priceHour { get; set; }
        public decimal total { get; set; }
        public decimal amountPaid { get; set; }

        //  public decimal totalPrice() => price;
        public decimal AveragePriceHour() => quantityHours > 0? total / quantityHours:0;
        public decimal Total() => quantityHours * AveragePriceHour();
        public decimal RemainingAmount() => total - amountPaid ;
        //* quantity
    }
   
}

