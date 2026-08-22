using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.SalesSystem
{

    public partial class DialogAddAndUpdteSalesSystem : Form
    {  //HomeScereen homeScereen;
       // SalesSystemWidget widget;
       // TransactionType transactionType;

        SalesSystemController controller;
        public DialogAddAndUpdteSalesSystem(SalesSystemController controller)
        {
            controller.HasHomeScreenDataProcessed = false;
            InitializeComponent();
            this.controller = controller;
            controller.lastDate=null;
            //    titel.Text = Functions.getCurrentRoot();
            amountPaid.PriceOnly();
            if (controller.isSale)
            {
                billDiscount.PriceOnly();
            }
           
            currency.TextOnly();
            store.TextOnly();
            casheir.TextOnly();
            paymentType.TextOnly();
            orderType.TextOnly();
            invoiceNumber.NumberOnly();
            total.PriceOnly();
            remaining.PriceOnly();

        }


        private void DialogAddAndUpdteSalesSystem_Load(object sender, EventArgs e)
        {
            invoiceNumber.Text = controller.newInvoiceNumber.ToString();
            if (!controller.isSale)
            {
                customerOrSupplier.DataSource = controller.suppliers;
                customerOrSupplier.CueHint.CueHintText = "المورد";
                orderTime.Visible = false;
                orderType.Visible = false;
                guna2Panel7.Visible = false;
               // sum.Left = 201;//discountItems.Left
            //    discountItems.Left = 381;//billDiscount.Left

            }
            else
            {
                customerOrSupplier.DataSource = controller.customers;
            }
            customerOrSupplier.TextOnly();
            sum.PriceOnly();
            discountItems.PriceOnly();
            store.DataSource = controller.stores;
            currency.DataSource = controller.currencies;
            casheir.DataSource = controller.cashiers;

            setComboBox();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
            InitializeAppComponent();
         


        }
        void displayOrHideExchangeRate()
        {
            if (controller.GetCurrentCurrency()?.currencyType != "رئيسية")
            {
                guna2Panel11.Visible = true;
                guna2Panel8.Width = 120;
                orderType.Width = 115;
            }
            else
            {
                guna2Panel11.Visible = false;
                guna2Panel8.Width = 255;
                orderType.Width = 250;


            }
            exchangeRate.Text = controller.GetCurrentCurrency()?.exchangeRate.ToString();
            titel_SizeChanged(titel,null);
        }
        private void fillFeild()
        {
            if (controller.isSale)
                saleFillFeild();
            else purchaseFillFeild();
            displayOrHideExchangeRate();
            exchangeRate.Text = controller.GetCurrentExchange().ToString();

        }
        private void saleFillFeild()
        {
            invoiceNumber.Text = controller.tempSale.number?.ToString();
            amountPaid.Text = controller.tempSale.amountPaid?.ToString();
            billDiscount.Text = controller.tempSale.descountPrice?.ToString();
            exchangeRate.Text = controller.GetCurrentExchange().ToString();
            date.Value = controller.tempSale.date.Value;

        }
        private void purchaseFillFeild()
        {
            invoiceNumber.Text = controller.tempPurchase.number?.ToString();
            amountPaid.Text = controller.tempPurchase.amountPaid?.ToString();
            date.Value = controller.tempPurchase.date.Value;

        }
        void setComboBox()
        {
            if (controller.isSale)
            {
                //AppDialogAleart.showAleartNoPermissions("tCustomer=" + controller.tempSale.Customer?.name);
                customerOrSupplier.SelectedItem = controller.tempSale.Customer;
                currency.SelectedItem = controller.tempSale.Currency;
                store.SelectedItem = controller.tempSale.Store;
                casheir.SelectedItem = controller.tempSale.Cashier;
                paymentType.Text = controller.tempSale.paymentType;
                date.Value = controller.tempSale.date ?? DateTime.Now;
                if (controller.tempSale.priceType == PriceType.جمله.ToString())
                    retailOrWholesale_Click(wholesale, null);
                if (controller.tempSale.priceType == PriceType.تجزئه.ToString())
                    retailOrWholesale_Click(retail, null);
                orderType.Text = controller.tempSale.orderType;

            }
            else
            {
                customerOrSupplier.SelectedItem = controller.tempPurchase.Supplier;
                currency.SelectedItem = controller.tempPurchase.Currency;
                store.SelectedItem = controller.tempPurchase.Store;
                casheir.SelectedItem = controller.tempPurchase.Cashier;
                paymentType.Text = controller.tempPurchase.paymentType;
                date.Value = controller.tempPurchase.date ?? DateTime.Now;
                if (controller.tempPurchase.priceType == PriceType.جمله.ToString())
                    retailOrWholesale_Click(wholesale, null);
                if (controller.tempPurchase.priceType == PriceType.تجزئه.ToString())
                    retailOrWholesale_Click(retail, null);

            }
        }




        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void DialogAddAndUpdteSalesSystem_FormClosed(object sender, FormClosedEventArgs e)
        {

            controller.clearTempData();
        }

        private void billDiscount_TextChanged(object sender, EventArgs e)
        {
            calculatePrices();
        }

        private void amountPaid_TextChanged(object sender, EventArgs e)
        {
            calculatePrices();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            getInvoiceDetail();
            if (controller.dataProcessing(invoiceNumber.Text, billDiscount.Text, amountPaid.Text, exchangeRate.Text))
            {
                if (controller.prosessesType == ProsessesType.add)
                {
                    controller.clearTempData();
                    clearDetail();
                    DialogAddAndUpdteSalesSystem_Load(null, null);
                    date.Value=controller.lastDate??DateTime.Now;
                }
                else Close();
                // Close();
                // Program.homeScereen().Show();
            }

        }



        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
            displayOrHideExchangeRate();

        }

        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStore(store.SelectedItem);
        }

        private void paymentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedPaymentType(paymentType.SelectedItem);
        }

        private void casheir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCashier(casheir.SelectedItem);
        }

        private void orderType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedoOrderType(orderType.SelectedItem);
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedDate(date.Value);
        }
        private void retailOrWholesale_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            wholesale.Inactive();
            retail.Inactive();
            btn.Active();
            controller.selectedPriceType(btn.Text);
            foreach (Control recorde in bodyTable.Controls)
            {
                recorde.Controls[6].Text = controller.wholesaleOrRetailPriceSelectedDetail(int.Parse(recorde.Name))?.ToString();

            }

        }

        private void customerOrSupplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCustomerOrSupplier(customerOrSupplier.SelectedItem);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

            // Application.Exit();
        }
        private void btnCloseApp_Click_1(object sender, EventArgs e)
        {
            this.Close();
            // Program.homeScereen().Show();
        }

        private void DialogAddAndUpdteSalesSystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.HasAddAndUpdateScreenDataProcessed = false;
            controller.HasHomeScreenDataProcessed = true;
        }

        private void body_SizeChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            control.Controls[0].Width = (int)(control.Width * 0.340);
            control.Controls[0].Height = (int)(control.Height * 0.995);
            control.Controls[1].Width = (int)(control.Width * 0.660);
            control.Controls[1].Height = (int)(control.Height * 0.995);
            //foreach (Control item in control.Controls)
            //{
            //    item.Width=(int)(control.Width*0.5);
            //    item.Height=(int)(control.Height*0.5);
            //}
        }

        private void reightScreen_SizeChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            int childeHeight = 0;
            foreach (Control item in control.Controls)
            {
                item.Width = control.Width;
                if (item.Name != "panelBtnCard")
                    childeHeight += item.Height;
            }
            control.Controls[0].Controls[0].Width = (int)(control.Controls[0].Width * 0.35);
            control.Controls[2].Height = control.Height - childeHeight;

            //   control.Controls[1].Height = (int)(control.Height * 0.9);
        }

        private void leftScreen_SizeChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            int childeHeight = 0;
            int index = -1;
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control item = control.Controls[i];
                item.Width = control.Width-20;
                if (item.Name != "bodyTable")
                    childeHeight += item.Height;
                else
                    index = i;
            }
   
            int w = 0;
            for (int i = 0; i < control.Controls[0].Controls.Count; i++)
            {
                if(!(control.Controls[0].Controls[i] is FlowLayoutPanel))
                w += control.Controls[0].Controls[i].Width;
            }
         
            control.Controls[0].Controls[2].Width= control.Controls[0].Width -w -(int) (control.Controls[0].Width*0.050);


            if (index>=0)
            control.Controls[index].Height =control.Height- childeHeight-20;//- control.Controls[Key].Height
        }

        private void titel_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[0].Width = (int)(row.Width * 0.170);
            row.Controls[0].Controls[0].Width = (int)(row.Width * 0.160);
          
            row.Controls[1].Width = (int)(row.Width * 0.210); 
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.205);
            row.Controls[2].Width = (int)(row.Width * 0.150);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.140);
            row.Controls[3].Width = (int)(row.Width * 0.120);
            row.Controls[3].Controls[0].Width = (int)(row.Width * 0.110);
            row.Controls[4].Width = (int)(row.Width * 0.080);
            row.Controls[4].Controls[0].Width = (int)(row.Width * 0.070);
            row.Controls[5].Width = (int)(row.Width * 0.080);
            row.Controls[5].Controls[0].Width = (int)(row.Width * 0.070);
            row.Controls[6].Width = (int)(row.Width * 0.140);
            row.Controls[6].Controls[0].Width = (int)(row.Width * 0.138);
        }

        private void titel2_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[0].Width = (int)(row.Width * 0.170);
            row.Controls[0].Controls[0].Width = (int)(row.Width * 0.148);
            row.Controls[1].Width = (int)(row.Width * 0.210);
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.208);
            row.Controls[2].Width = (int)(row.Width * 0.150);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.138);
            row.Controls[3].Width = (int)(row.Width * 0.160);
            row.Controls[3].Controls[0].Width = (int)(row.Width * 0.158);
            row.Controls[4].Width = (int)(row.Width * 0.150);
            row.Controls[4].Controls[0].Width = (int)(row.Width * 0.140);
           
        }

        private void footer1_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[0].Width = (int)(row.Width * 0.280);
            row.Controls[0].Controls[0].Width = (int)(row.Width * 0.250);
            row.Controls[1].Width = (int)(row.Width * 0.250);
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.240);
            row.Controls[2].Width = (int)(row.Width * 0.200);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.190);
            row.Controls[3].Width = (int)(row.Width * 0.250);
            row.Controls[3].Controls[0].Width = (int)(row.Width * 0.238);
         
        }

        private void footer_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[0].Width = (int)(row.Width * 0.280);
            row.Controls[0].Controls[0].Width = (int)(row.Width * 0.250);
            row.Controls[1].Width = (int)(row.Width * 0.250);
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.240); 
            row.Controls[2].Width = (int)(row.Width * 0.250);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.200);
        }
        int lenRows = 0;

        private void bodyTable_SizeChanged(object sender, EventArgs e)
        {
            lenRows=bodyTable.Controls.Count;
            Thread thread = new Thread(resiseRow);
            thread.Start();
        }

        private void resiseRow()
        {
            for (int i = 0; i < lenRows; i++)
            {
                bodyTable.Invoke((MethodInvoker)delegate {
                    Control row = bodyTable.Controls[i];

                    row.Width = (int)(bodyTable.Width);

                });

            }
        }

      
    }
}
