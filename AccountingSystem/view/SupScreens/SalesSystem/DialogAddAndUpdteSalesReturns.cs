using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller.Screen;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AccountingSystem.view.SupScreens.SalesSystem
{
    public partial class DialogAddAndUpdateSalesReturns : Form
    {
        SalesReturnsController controller;
        public DialogAddAndUpdateSalesReturns(SalesReturnsController controller)
        {
            InitializeComponent();
            this.controller = controller;
            controller.detailTable.Columns = ColumnsInvoceDetailTable;
            controller.detailTable.operationFields = new AppTableOperationField() { fields = new List<string>() { "الكميه", "سعر الوحده" } };
            customTable1.TextBoxTotal.TextChanged += total_TextChanged;
            controller.startHADSDP();
        }

        private void DialogAddAndUpdteSalesReturns_Load(object sender, EventArgs e)
        {
            customTable1.build(controller.dataSourceDetail, FunctionsGUI.TableDetailsStyle);
            if (controller.IsAdd)
                customTable1.AddNewRowToTable();
            invoiceSearchScreen1.buildeListBtnGroup(btnCardItem_Click);
           
            store.DataSource = controller.stores;
            currency.DataSource = controller.currencies;
            casheir.DataSource = controller.cashiers;
            customer.DataSource = controller.customers;
            customer.TextOnly();
            sum.PriceOnly();
        
            amountPaid.PriceOnly();
            billDiscount.PriceOnly();
            currency.TextOnly();
            store.TextOnly();
            casheir.TextOnly();
            paymentType.TextOnly();
            orderType.TextOnly();
            invoiceNumber.NumberOnly();
            total.PriceOnly();
            remaining.PriceOnly();
            //if (controller.IsUpdate)
            //    fillData();
            invoiceNumber.Text = controller.Number;

            setComboBox();
            controller.endADSDP();
            timer1.Start();
        }
        private void setComboBox()
        {
            customer.SelectedItem = controller.temp.Customer;
            currency.SelectedItem = controller.temp.Currency;
            store.SelectedItem = controller.temp.Store;
            casheir.SelectedItem = controller.temp.Cashier;
            paymentType.Text = controller.temp.paymentType;
            date.Value = controller.temp.date ?? DateTime.Now;
            if (controller.temp.priceType == PriceType.جمله.ToString())
                retailOrWholesale_Click(wholesale, null);
            if (controller.temp.priceType == PriceType.تجزئه.ToString())
                retailOrWholesale_Click(retail, null);
            orderType.Text = controller.temp.orderType;
            displayOrHideExchangeRate();

        }
        void displayOrHideExchangeRate()
        {
            if (!controller.CurrentCurrencyIsMain)
            {
                guna2Panel11.Visible = true;
                exchangeRate.Width = 120;
                orderType.Width = 115;
            }
            else
            {
                guna2Panel11.Visible = false;
                exchangeRate.Width = 255;
                orderType.Width = 250;


            }
            exchangeRate.Text = controller.ExchangeRate;
            //titel_SizeChanged(titel, null);
        }
        List<AppColumn> ColumnsInvoceDetailTable
        {
            get
            {
                int height = 40;
                return new List<AppColumn>()
                {
                    new AppColumn() { caption = "رقم الصنف", ValueType = typeof(int), ReadOnly = true, SizeF = new System.Drawing.SizeF(0.1f, height), flex = true },
                    new AppColumn() { caption = "الصنف", Type = typeof(Guna2ComboBox), CombBox = new AppTableCombBox() { DisplayMember = "nameAr", DataSource = controller.copySupItems, eventHandler = item_Selection }, ValueType = typeof(string), SizeF = new System.Drawing.SizeF(0.15f, height), flex = true },
                    new AppColumn() { caption = "الوحده", Type = typeof(Guna2ComboBox), CombBox = new AppTableCombBox() { DataSource = controller.copyUnits }, ValueType = typeof(string), SizeF = new System.Drawing.SizeF(.11f, height), flex = true },
                    new AppColumn() { caption = "الكميه", ValueType = typeof(decimal), DefaultValue = 1, AutoFocus = true, SizeF = new System.Drawing.SizeF(.1f, height), flex = true },
                    new AppColumn() { caption = "سعر الوحده", ValueType = typeof(decimal), SizeF = new System.Drawing.SizeF(.1f, height), flex = true },
                    new AppColumn() { caption = "الإجمالي", ValueType = typeof(decimal), ReadOnly = true, SizeF = new System.Drawing.SizeF(0.14f, height), flex = true },
                    new AppColumn() { caption = "ملاحضات", ValueType = typeof(string), SizeF = new System.Drawing.SizeF(.14f, height), flex = true },
                };
            }
        }
        private void retailOrWholesale_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            wholesale.Inactive();
            retail.Inactive();
            btn.Active();
           controller.selectPriceType(btn.Text);
            //foreach (Control recorde in bodyTable.Controls)
            //{
            //    recorde.Controls[6].Text = controller.wholesaleOrRetailPriceSelectedDetail(int.Parse(recorde.Name))?.ToString();

            //}

        }
        private void btnCardItem_Click(object sender, EventArgs e)
        {
            customTable1.buildRow(controller.newRow(invoiceSearchScreen1.selectedItem));
        }
      
        public void item_Selection(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Classify item = (Classify)comboBox.SelectedItem;
            var cellItem = customTable1.Cell(comboBox);
            var unit = customTable1.CellSister(comboBox, "الوحده");
            var unitPrice = customTable1.CellSister(comboBox, "سعر الوحده");

            AppCell cellUnitPrice = customTable1.Cell(unitPrice);
            if (item.id != 0)
            {
                MeasurementsItem measurement = controller.measurementSelectItem(comboBox.SelectedItem);

                if (measurement != null)
                {
                    KryptonComboBox boxUnit = (KryptonComboBox)unit;
                    boxUnit.DataSource = new List<Unit>() { measurement.Unit };
                    unitPrice.Text =(controller.isWholesale ? measurement.WholesalePrice : measurement.sellingPrice).Format();
                    AppCell cellUnit = customTable1.Cell(unit);
                    cellUnit.CombBox.Tag = measurement;
                    cellUnit.id = measurement.id.ToString();
                }
            }
            else
            {
                customTable1.CellSister(comboBox, "الكميه").Text = "1";
                unitPrice.Text = "0";


            }
            //customTable1.CellSister(comboBox, "الكميه").Text = "1";
        }

        bool u = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!u)
            {
                WindowState = FormWindowState.Maximized;
                u = true;
                timer1.Stop();
            }
        }
        private void total_TextChanged(object sender, EventArgs e)
        {
            total.Text = ((sender as Control)?.Text);
            calculatePrices(null,null);
        }

        private void calculatePrices(object sender, EventArgs e)
        {
            sum.Text = (total.Text.ToDecimal() - billDiscount.Text.ToDecimal()).Format();
            remaining.Text = (total.Text.ToDecimal() - amountPaid.Text.ToDecimal() - billDiscount.Text.ToDecimal()).Format();
        }

        private void body_SizeChanged(object sender, EventArgs e)
        {

            Control control = sender as Control;
            control.Controls[0].Width = (int)(control.Width * 0.340);
            control.Controls[0].Height = (int)(control.Height * 0.995);
            control.Controls[1].Width = (int)(control.Width * 0.660);
            control.Controls[1].Height = (int)(control.Height * 0.995);

        }
        private void title_SizeChanged(object sender, EventArgs e)
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

        private void title2_SizeChanged(object sender, EventArgs e)
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

        private void customer_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCustomer(customer.SelectedItem);
        }

        private void currency_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCurrency(currency.SelectedItem);
            displayOrHideExchangeRate();
        }

        private void orderType_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectOrderType(orderType.SelectedItem);
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            controller.selectDate(date.Value);
        }

        private void store_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectStore(store.SelectedItem);
        }

        private void paymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectPaymentType(paymentType.SelectedItem);
        }
        private void casheir_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCashier(casheir.SelectedItem);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.dataProcessing(invoiceNumber.Text, exchangeRate.Text, billDiscount.Text,amountPaid.Text, customTable1.newData))
            {
                if (controller.prosessesType == ProsessesType.add)
                {
                    invoiceNumber.Text = controller.Number;
                    customTable1.Clear();
                    sum.Clear();
                    billDiscount.Clear();
                    amountPaid.Clear();
                    total.Clear();
                    remaining.Clear();
                }
                else
                    Close();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
