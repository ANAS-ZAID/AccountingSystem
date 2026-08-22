using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Media3D;
using AccountingSystem.controller.Screen;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.view.ReportPages;
using AccountingSystem.view.SupScreens.ClassifyManagament;

namespace AccountingSystem.view.SupScreens.InventoryTransferManagament
{
    public partial class DialogAddAndUpdateInventoryTransfer : Form
    {
        InventoryTransferController controller;
    
        
        public DialogAddAndUpdateInventoryTransfer(InventoryTransferController controller)
        {
            InitializeComponent();
            this.controller = controller;
            controller.detailTable.Columns = ColumnsDetailTable;
            controller.detailTable.operationFields = new AppTableOperationField() { fields = new List<string>() { "الكميه", "سعر الوحده" } };
            customTable1.TextBoxTotal.TextChanged += total_TextChanged;
            controller.startHADSDP();

        }
        public  List<AppColumn> ColumnsDetailTable
        {
          get {
                int height = 40;
                return new List<AppColumn>()
             {
          new AppColumn(){caption="رقم الصنف",ValueType=typeof(int),ReadOnly=true ,SizeF=new System.Drawing.SizeF(0.1f,height),flex=true},
         new AppColumn(){caption="الصنف",Type=typeof(Guna2ComboBox),CombBox=new AppTableCombBox(){DisplayMember="nameAr",DataSource=controller.copySupItems ,eventHandler=item_Selection},ValueType=typeof(string), SizeF = new System.Drawing.SizeF(0.15f, height),flex=true},
         new AppColumn(){caption="الوحده",Type=typeof(Guna2ComboBox),CombBox=new AppTableCombBox(){DataSource=controller.copyUnits},ValueType=typeof(string), SizeF = new System.Drawing.SizeF(.11f, height), flex = true},
         new AppColumn(){caption="الكميه",ValueType=typeof(decimal),DefaultValue=1,AutoFocus=true ,SizeF = new System.Drawing.SizeF(.1f, height),flex=true},
         new AppColumn(){caption="سعر الوحده",ValueType=typeof(decimal),SizeF=new System.Drawing.SizeF(.1f,height),flex=true},
         new AppColumn(){caption="الإجمالي",ValueType=typeof(decimal),ReadOnly=true,SizeF=new System.Drawing.SizeF(0.14f,height), flex = true},
         new AppColumn(){caption="ملاحضات",ValueType=typeof(string),SizeF=new System.Drawing.SizeF(.14f,height),flex=true},
            };
            }
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
                    unitPrice.Text = measurement.sellingPrice.Format();
                    AppCell cellUnit = customTable1.Cell(unit);
                    cellUnit.CombBox.Tag = measurement;
                    cellUnit.id = measurement.id.ToString();
                }
            }
            else
            {
                 customTable1.CellSister(comboBox, "الكميه").Text="1";
                 unitPrice.Text ="0";


            }
            //customTable1.CellSister(comboBox, "الكميه").Text = "1";
        }
        void displayOrHideExchangeRate()
        {
            if (!controller.CurrentCurrencyIsMain)
            {
                exchangeRate.Visible = true;
            }
            else
                exchangeRate.Visible = false;
            exchangeRate.Text = controller.ExchangeRate;
        }

            private void DialogAddAndUpdateInventoryTransfer_Load(object sender, EventArgs e)
        {
            int height = Height;
            var style = new AppTableStyle()
            {
                flex=true,
                BtnsTable=new BtnsTable() { AddBtn=new BtnTable() { Show=true } , DeleteBtn = new BtnTable() { Show = true } },
            };
            customTable1.build(controller.dataSourceDetail, style);
            if (controller.IsAdd)
                customTable1.AddNewRowToTable();
            invoiceSearchScreen1.buildeListBtnGroup(btnCardItem_Click);
            fromStore.DataSource = controller.storeList;
            toStore.DataSource = controller.storeList;
            currency.DataSource=controller.currencies;
            fromStore.TextOnly();
            toStore.TextOnly();
            currency.TextOnly();
            voucherNumber.NumberOnly();
             total.PriceOnly();
            //if (controller.IsUpdate)
            //    fillData();
               voucherNumber.Text=controller.VoucherNumber;
          
            setComboBox();
            controller.endADSDP();
            
            timer1.Start();
        }

        private void fillData()
        {
           voucherNumber.Text=controller.temp.number.ToString();
        }

        private void setComboBox()
        {
            displayOrHideExchangeRate();
            fromStore.SelectedItem = controller.temp.FromStore;
            toStore.SelectedItem = controller.temp.ToStore;
            currency.SelectedItem = controller.temp.Currency;
            date.Value=controller.temp.date.Value;
           
        }

        private void btnCardItem_Click(object sender, EventArgs e)
        {

            customTable1.buildRow(controller.newRow(invoiceSearchScreen1.selectedItem));
            //AppDialogAleart.showAlertGetType(controller.newRow(invoiceSearchScreen1.selectedItem),50);
        }
      

        

       

        private void row1_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[1].Width = (int)(row.Width * 0.170);
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.160);
            row.Controls[2].Width = (int)(row.Width * 0.210);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.205);
            row.Controls[3].Width = (int)(row.Width * 0.150);
            row.Controls[3].Controls[0].Width = (int)(row.Width * 0.145);
            int w = 0;
            for (int i = 0; i < row.Controls.Count; i++)
            {
                w+= row.Controls[i].Width;
            }
            row.Controls[0].Width = (row.Width - w)/2;
        }

        private void row2_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[1].Width = (int)(row.Width * 0.170);
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.160);
         
            row.Controls[1].Controls[0].Controls[0].Location =new Point((row.Controls[1].Controls[0].Width - row.Controls[1].Controls[0].Controls[0].Width)/2, -2);
            row.Controls[2].Width = (int)(row.Width * 0.210);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.205);
            row.Controls[3].Width = (int)(row.Width * 0.150);
            //row.Controls[3].Controls[0].Width = (int)(row.Width * 0.145);
            int w = 0;
            for (int i = 0; i < row.Controls.Count; i++)
            {
                w += row.Controls[i].Width;
            }
            row.Controls[0].Width = (row.Width - w) / 2;

        }

        private void footer_SizeChanged(object sender, EventArgs e)
        {
            Control row = sender as Control;
            row.Controls[1].Width = (int)(row.Width * 0.170);
            row.Controls[1].Controls[0].Width = (int)(row.Width * 0.160);
            row.Controls[2].Width = (int)(row.Width * 0.210);
            row.Controls[2].Controls[0].Width = (int)(row.Width * 0.205);
            int w = 0;
            for (int i = 0; i < row.Controls.Count; i++)
            {
                w += row.Controls[i].Width;
            }
            row.Controls[0].Width = (row.Width - w) / 2;
        }
        int lenRows = 0;

       

        private void body_SizeChanged_1(object sender, EventArgs e)
        {
           
            Control control = sender as Control;
                control.Controls[0].Width = (int)(control.Width * 0.340);
                control.Controls[0].Height = (int)(control.Height * 0.995);
            control.Controls[1].Width = (int)(control.Width * 0.660);
            control.Controls[1].Height = (int)(control.Height * 0.995);

        }
        bool u=false;
        private void timer1_Tick(object sender, EventArgs e)
        {if (!u)
            {
                WindowState = FormWindowState.Maximized;
                u = true;
                timer1.Stop();
            }
        }
        private void total_TextChanged(object sender, EventArgs e)
        {
            total.Text=((sender as Control)?.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.dataProcessing(voucherNumber.Text, exchangeRate.Text, customTable1.newData))
            {
                if (controller.prosessesType == ProsessesType.add)
                {
                    voucherNumber.Text=controller.VoucherNumber;
                      customTable1.Clear();
                }
                else
                    Close();
             
            }
        }

        private void fromStore_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectFromStore(fromStore.SelectedItem);
        }

        private void currency_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCurrency(currency.SelectedItem);
            displayOrHideExchangeRate();
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            controller.selectDate(date.Value);
        }

        private void toStore_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectToStore(toStore.SelectedItem);
        }
    }
}
