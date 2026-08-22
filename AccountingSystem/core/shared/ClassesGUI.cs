using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core.DAG;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shell;
using System.Xml.Linq;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.view.SupScreens.ClassifyManagament;

namespace AccountingSystem.core.shared
{
    public class ClassesGUI
    {

    } 
    public class CompoundJournalEntriesWidget 
    {
        static int countnNewRow = 0;
        Guna2Panel tablePanels;
        List<ChartOfAccount> supAccounts;
        Dictionary<int, ChartOfAccount> selectedAccounts;
    //    List<ChartOfAccount> temAccounts;
        ToolTip toolTip;
        TransactionType transactionType;
        public MyData debitTotal;
        public MyData creditTotal;
        public MyData difference;
        public  CompoundJournalEntriesWidget(List<ChartOfAccount> accounts, ToolTip toolTip, TransactionType transactionType)
        {
            creditTotal = new MyData { MyProperty = "" };
            debitTotal = new MyData { MyProperty = "" };
            difference = new MyData { MyProperty = "" };
            selectedAccounts = new Dictionary<int, ChartOfAccount>();
            supAccounts = accounts;
            this.toolTip = toolTip;
            tablePanels = new Guna2Panel();
            tablePanels.AutoScroll = true;
            tablePanels.BackColor = System.Drawing.Color.Transparent;
            tablePanels.Dock = System.Windows.Forms.DockStyle.Fill;
            tablePanels.Location = new System.Drawing.Point(0, 201);
            tablePanels.Name = "bodyPanel";
            tablePanels.Size = new System.Drawing.Size(857, 605);
            tablePanels.TabIndex = 59;
            this.transactionType = transactionType;
        }
        public  Guna2Panel returnNewTablePanels()
        { 
            return tablePanels;
        }
        public void newRow()
        {
            AddNewRow();
        }
        //public void buildeTablePanels(int countRow)
        //{
        //    for (int i = countRow - 1; i >= 0; i--)
        //    {
        //      //  AddNewRow();
        //    }
        //for (int i = journalEntries.Count - 1; i >= 0; i--)
        //{
        //    Guna2Panel panel =(Guna2Panel) tablePanels.Controls[i];
        //    panel.Controls[1].Text = journalEntries[i].credit.ToString();
        //    panel.Controls[2].Text = journalEntries[i].debit.ToString();
        //    panel.Controls[0].Text = journalEntries[i].description.Replace("قيد مركب ", "").Replace(" / ملاحضات / ", "");
        //    KryptonComboBox comboBox = (KryptonComboBox)panel.Controls[3];
        //     comboBox.SelectedItem = journalEntries[i].ChartOfAccount;
        //    selectedAccounts.Add(int.Parse(panel.Name), journalEntries[i].ChartOfAccount);
        //}

        //}
        public void fillTablePanels(List<JournalEntry> journalEntries)
        {
            foreach (var journalEntry in journalEntries)
            {
                AddNewRow(journalEntry);
            }
            CreditOrDeditTextBox_TextChanged(null, null);
        }
        private void AddNewRow(JournalEntry journalEntry=null)
        {///for  first itm is null
            ChartOfAccount[] tempList = new ChartOfAccount[supAccounts.Count + 1];
            supAccounts.CopyTo(tempList);
            ChartOfAccount temp = tempList[0];
            tempList[0] = new ChartOfAccount() { name = "", id = 0, accountGroupId = 0 };
            tempList[supAccounts.Count] = temp;
            ////
            Guna2Panel newPanel = new Guna2Panel();
            tablePanels.Controls.Add(newPanel);
            newPanel.Size = new Size(735, 55);
            newPanel.BackColor = Color.Transparent;
            Size size = new Size(120, 30);
            Size ComboBoxSize = new Size(250, 30);
            newPanel.Name = (countnNewRow).ToString();
            Guna2TextBox descriptionTextBox =BuildControls.buildTextBox("البيان", "description", size, new Point(35, 5));
            Guna2TextBox debitTextBox = BuildControls.buildTextBox("مدين", "debit", size, new Point(160, 5));
            Guna2TextBox creditTextBox = BuildControls.buildTextBox("دائن", "credit", size, new Point(285, 5));
            KryptonComboBox comboBox = BuildControls.buildComboBox("الحساب" + countnNewRow, "account", ComboBoxSize, new Point(413, 5), tempList, eventHandler: comboBox_SelectionChangeCommitted);
            newPanel.Controls.Add(descriptionTextBox);
            newPanel.Controls.Add(debitTextBox);
            newPanel.Controls.Add(creditTextBox);
            newPanel.Controls.Add(comboBox);
            
            comboBox.KeyPress += ValidatingData.eventTextBoxTextOnly;
            debitTextBox.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            creditTextBox.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            debitTextBox.TextChanged += CreditOrDeditTextBox_TextChanged;
            creditTextBox.TextChanged += CreditOrDeditTextBox_TextChanged;
            Guna2Button removeButton = BuildControls.buildButton("b", "s", new Point(675, 7), Properties.Resources.FluentMdl2Cancel, Properties.Resources.FluentMdl2Cancel__1_);
            removeButton.Click += buttonRemoveRowClick;
            toolTip.SetToolTip(removeButton, "حذف");
            newPanel.Controls.Add(removeButton);
            sortTablePanels();
            if (journalEntry != null)
            {
                debitTextBox.Text = journalEntry.debit.ToString();
                creditTextBox.Text = journalEntry.credit.ToString();
                descriptionTextBox.Text = journalEntry.description.Replace(transactionType.ToString(), "").Replace(" / ملاحضات / ", "");
                comboBox.SelectedItem = journalEntry.Account;
                selectedAccounts.Add(int.Parse(newPanel.Name), journalEntry.Account);

            }
            countnNewRow++;
        }

        private void CreditOrDeditTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal credit = 0;
            decimal debit = 0;

            if (selectedAccounts.Any())
            {
                foreach (Guna2Panel panel in tablePanels.Controls)
                {
                    debit += (!String.IsNullOrEmpty(panel.Controls[1].Text) ? Convert.ToDecimal(panel.Controls[1].Text) : 0);
                    credit += (!String.IsNullOrEmpty(panel.Controls[2].Text) ? Convert.ToDecimal(panel.Controls[2].Text) : 0);
                }
                creditTotal.MyProperty = credit.ToString();
                debitTotal.MyProperty = debit.ToString();
                difference.MyProperty = (credit - debit).ToString();
            }
        }

        private void DebitTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Guna2Panel pearant = (Guna2Panel)comboBox.Parent;
           
            int numberPerant=int.Parse(pearant.Name);
            ChartOfAccount account = (ChartOfAccount)comboBox.SelectedItem;
          // AppDialogAleart.showAleartNoPermissions(pearant.Controls[0].Text+ pearant.Controls[1].Text+ pearant.Controls[2].Name+ pearant.Controls[3].Name);
            if (account.id!=0)
            {
                if (!selectedAccounts.ContainsKey(numberPerant))
                    selectedAccounts.Add(numberPerant, account);
                if (selectedAccounts.ContainsKey(numberPerant))
                    selectedAccounts[numberPerant]=account;
            }
            else if (selectedAccounts.ContainsKey(numberPerant))
                selectedAccounts.Remove(numberPerant);
            CreditOrDeditTextBox_TextChanged(null,null);
        }
        public List<JournalEntry> getJournalEntrys()
        {
            List<JournalEntry> tempJournalEntries = new List<JournalEntry>();
            foreach (var account in selectedAccounts.AsEnumerable())
            {
                string numberPerant= account.Key.ToString();
                foreach (Guna2Panel panel in tablePanels.Controls)
                {
                    if (panel.Name==numberPerant)
                    {
                       
                        decimal debit = (!String.IsNullOrEmpty(panel.Controls[1].Text) ? Convert.ToDecimal(panel.Controls[1].Text) : 0);
                        decimal credit = (!String.IsNullOrEmpty(panel.Controls[2].Text) ? Convert.ToDecimal(panel.Controls[2].Text) : 0);
                        string description = transactionType.ToString() + (String.IsNullOrEmpty(panel.Controls[0].Text)?"":" / ملاحضات / "+ panel.Controls[0].Text);
                      KryptonComboBox comboBox=(KryptonComboBox)panel.Controls[3];
                        if (credit >0||debit>0)
                        {
                          //  temAccounts.Add(account.Value);
                            comboBox.SelectedItem=account.Value;
                            tempJournalEntries.Add(new JournalEntry() {accountId=account.Value.id,credit=credit,debit=debit, description=description});
                        }
                    }
                }
            }
            return tempJournalEntries;

        }
        private void sortTablePanels()
        {
            int countRecordes = tablePanels.Controls.Count;
            Guna2Button addButton = BuildControls.buildButton("s", "AddButton", new Point(0,7), Properties.Resources.PhPlusCircleDuotone, Properties.Resources.PhPlusCircleDuotone__2_, AddButtonNewRow_Click);
            for (int row = 0; row < countRecordes; row++)
            {
                tablePanels.Controls[row].Dock = DockStyle.Top;
                tablePanels.Controls[row].Name = (row).ToString();
                if (tablePanels.Controls[row].Controls.Count == 6)
                {
                    tablePanels.Controls[row].Controls.RemoveAt(5);
                }
                if (row == countRecordes - 1)
                {

                toolTip.SetToolTip(addButton, "اضافة صف جديد");
                    tablePanels.Controls[row].Controls.Add(addButton);
                }
            }

        }
        private void AddButtonNewRow_Click(object sender, EventArgs e)
        {
            Guna2Button textBox = (Guna2Button)sender;
            Panel parentPanel = (Panel)textBox.Parent;
            int index = tablePanels.Controls.GetChildIndex(parentPanel);
            int countRow = tablePanels.Controls.Count;
            if (int.Parse(parentPanel.Name) == countRow - 1)
            {
                AddNewRow();
                tablePanels.Controls[countRow].Controls[3].Focus();
                sortTablePanels();
            }
        }




        private void buttonRemoveRowClick(object sender, EventArgs e)
        {
            Guna2Button textBox = (Guna2Button)sender;
            Guna2Panel parentPanel = (Guna2Panel)textBox.Parent;
            int i = tablePanels.Controls.GetChildIndex(parentPanel);
            tablePanels.Controls.RemoveAt(i);
            sortTablePanels();
            int numberPerant = int.Parse(parentPanel.Name);

                if (selectedAccounts.ContainsKey(numberPerant))
                selectedAccounts.Remove(numberPerant);
            CreditOrDeditTextBox_TextChanged(null, null);
        }

    }
    public class ItemsWidget
    {
        static int countnNewRow = 0;
        Guna2Panel tablePanels;
   
        //Dictionary<int, MeasurementsItem> selectedMeasurements;
        ItemController itemController;
        ToolTip toolTip;
       
       
        public ItemsWidget(ItemController itemController, ToolTip toolTip)
        {  tablePanels=new Guna2Panel(); 
            
            this.itemController = itemController;
            this.toolTip = toolTip;
            tablePanels.AutoSize = true;
            tablePanels.BorderRadius = 20;
            tablePanels.CustomizableEdges.TopLeft = false;
            tablePanels.CustomizableEdges.TopRight = false;
     //   tablePanels.Dock = System.Windows.Forms.DockStyle.Fill;
            tablePanels.FillColor = System.Drawing.Color.White;
            tablePanels.Location = new System.Drawing.Point(0, 54);
            tablePanels.Name = "table";
            tablePanels.Size = new System.Drawing.Size(960, 0);
            tablePanels.TabIndex = 85;
           //tablePanels.SizeChanged += TablePanels_SizeChanged;

        }

       
        private void TablePanels_SizeChanged(object sender, EventArgs e)
        {Control control = (Control)sender;
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control item = control.Controls[i];
                if (!(item is Label))
                {

                    item.Width = control.Width;
                }
            }
        }

        public Guna2Panel returnNewTablePanels()
        {
            return tablePanels;
        }
        public void newRow()
        {
            AddNewRow();
        }
       
        public void fillTablePanels()
        {    if(itemController.temp.MeasurementsItems!=null) 
            foreach (var measurementsItem in itemController.temp.MeasurementsItems)
            {
                AddNewRow(measurementsItem);
            }
            if (itemController.temp.MeasurementsItems == null||!itemController.temp.MeasurementsItems.Any())
                AddNewRow();
            tablePanels.Width = (tablePanels.Parent as Control).Width - 20;
            // CreditOrDeditTextBox_TextChanged(null, null);
        }
        private void AddNewRow(MeasurementsItem measurementsItem = null)
        {///for  first itm is null
            Unit[] tempList = new Unit[measurementsItem == null?itemController.units.Count + 1: itemController.units.Count];
            itemController.units.CopyTo(tempList);
            if (itemController.units.Any())
            {
                if (measurementsItem == null)
                {
                    if (itemController.units.Any())
                    {
                        Unit temp = tempList[0];

                        tempList[itemController.units.Count] = temp;
                    }
                    tempList[0] = new Unit() { name = "", id = 0 };
                }
                else
                {
                    int index = itemController.units.IndexOf(measurementsItem.Unit);
                    tempList[index] = tempList[0];
                    tempList[0] = measurementsItem.Unit;
                }
            }
            ////
            Guna2Panel newPanel = new Guna2Panel();
            tablePanels.Controls.Add(newPanel);
            newPanel.Size = new Size(1130, 35);
            newPanel.BackColor = Color.Transparent;
            Size size = new Size(100, 54);
            Size ComboBoxSize = new Size(100, 54);
            newPanel.Name = (countnNewRow).ToString();
            newPanel.SizeChanged += Size_Changed;
         
            Guna2TextBox minimumPurchaseAmount = BuildControls.buildTextBox("أقل مبلغ شراء", "minimumPurchaseAmount", size, new Point(35, 5));
            Guna2TextBox reductionPercentage = BuildControls.buildTextBox("تخفيض", "reductionPercentage", size, new Point(160, 5));
            Guna2TextBox wholesalePurchasePrice = BuildControls.buildTextBox(" س. شراء جملة", "wholesalePurchasePrice", size, new Point(285, 5));
            Guna2TextBox purchasePrice = BuildControls.buildTextBox("سعر الشراء", "purchasePrice", size, new Point(285, 5));
            Guna2TextBox wholesalePrice = BuildControls.buildTextBox("س. بيع جملة", "wholesalePrice", size, new Point(35, 5));
            Guna2TextBox sellingPrice = BuildControls.buildTextBox("سعر البيع", "sellingPrice", size, new Point(160, 5));
            KryptonComboBox unit = BuildControls.buildComboBox("الوحده" + countnNewRow, "unit", ComboBoxSize, new Point(413, 5), tempList, eventHandler: comboBox_SelectionChangeCommitted);
            Guna2TextBox barcode = BuildControls.buildTextBox("الباركود", "barcode", size, new Point(285, 5));
            newPanel.Controls.Add(minimumPurchaseAmount);
            newPanel.Controls.Add(reductionPercentage);
            newPanel.Controls.Add(wholesalePurchasePrice);
            newPanel.Controls.Add(purchasePrice);
            newPanel.Controls.Add(wholesalePrice);
            newPanel.Controls.Add(sellingPrice);
            newPanel.Controls.Add(unit);
            newPanel.Controls.Add(barcode);
            minimumPurchaseAmount.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            reductionPercentage.KeyPress += ValidatingData.ReductionPercentage_KeyPress;
            wholesalePurchasePrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            purchasePrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            wholesalePrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            sellingPrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            unit.KeyPress += ValidatingData.eventTextBoxTextOnly;
            barcode.KeyPress += ValidatingData.eventTextBoxphoneNumberOnly;
            Guna2Button selectIngredientBtn = BuildControls.buildButton("s", "ingredient", new Point(0, 12), Properties.Resources.IconParkTwotoneAdd, Properties.Resources.IconParkTwotoneAdd__1_, selectIngredientBtnClick);
            this.toolTip.SetToolTip(selectIngredientBtn, "المكونات");
            Guna2Button removeButton = BuildControls.buildButton("b", "s", new Point(675, 7), Properties.Resources.FluentMdl2Cancel, Properties.Resources.FluentMdl2Cancel__1_);
            removeButton.Click += buttonRemoveRowClick;
            toolTip.SetToolTip(removeButton, "حذف");
            newPanel.Controls.Add(selectIngredientBtn);
            newPanel.Controls.Add(removeButton);
            newPanel.Dock = DockStyle.Top;     
          
            if (measurementsItem != null)
            {
                unit.SelectedItem = measurementsItem.Unit;
                minimumPurchaseAmount.Text=measurementsItem.minimumPurchaseAmount.ToString();
                reductionPercentage.Text=measurementsItem.descountPrice.ToString();
                wholesalePurchasePrice.Text=measurementsItem.WholesalePurchasePrice.ToString();
                purchasePrice.Text=measurementsItem.purchasePrice.ToString();
                wholesalePrice.Text=measurementsItem.WholesalePrice.ToString();
                sellingPrice.Text=measurementsItem.sellingPrice.ToString();
                barcode.Text=measurementsItem.barcode.ToString();
             
                unit.SelectedValue= measurementsItem.UnitId;
                itemController.selectedMeasurements.Add(int.Parse(newPanel.Name), measurementsItem);
            }
            else {
            itemController.selectedMeasurements.Add(int.Parse(newPanel.Name), new MeasurementsItem());
                }
            sortTablePanels();
            countnNewRow++;
            //Size_Changed(newPanel, null);
        }

        void Size_Changed(object sender, EventArgs e)
        {
            Control row = sender as Control;
            int feildWidght = (int)(row.Width * 0.113);
            for (int i = 0; i < 8; i++)
                row.Controls[i].Width = feildWidght;
            row.Controls[8].Width = (int)(row.Width * 0.03);
            row.Controls[9].Width = (int)(row.Width * 0.03);
            if (row.Controls.Count > 10)
                row.Controls[10].Width = (int)(row.Width * 0.03);
               
        }

        private void selectIngredientBtnClick(object sender, EventArgs e)
        {
            Guna2Button button = (Guna2Button)sender;
            Guna2Panel pearant = (Guna2Panel)button.Parent;

            int numberPerant = int.Parse(pearant.Name);

            itemController.showDialogSelecteIngredient(numberPerant, pearant.Controls[7].Text,toolTip);
            if (itemController.sellingPriceTotal.accreditation)
                pearant.Controls[5].Text= itemController.sellingPriceTotal.MyProperty.ToString(); 
            if(itemController.purchasePriceTotal.accreditation)
                pearant.Controls[3].Text= itemController.purchasePriceTotal.MyProperty.ToString();
           itemController.clearTempDataIngredient();
          

        }
      

        void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Guna2Panel pearant = (Guna2Panel)comboBox.Parent;

            int numberPerant = int.Parse(pearant.Name);
            itemController.slectedUnit(comboBox.SelectedItem, numberPerant);
            // CreditOrDeditTextBox_TextChanged(null, null);
        }
        public void getMeasurementsItems()
        {
            List<int> measurementsDelete = new List<int>();

            foreach (var valuePair in itemController.selectedMeasurements)
            {
                string numberPerant = valuePair.Key.ToString();
                foreach (Guna2Panel panel in tablePanels.Controls)
                {
                    if (panel.Name == numberPerant)
                    {
                        if (!String.IsNullOrEmpty(panel.Controls[1].Text))
                            valuePair.Value.descountPrice = Convert.ToDecimal(panel.Controls[1].Text);
                       valuePair.Value.minimumPurchaseAmount = (!String.IsNullOrEmpty(panel.Controls[0].Text) ? Convert.ToDecimal(panel.Controls[0].Text) : 0);
                        valuePair.Value.WholesalePurchasePrice = (!String.IsNullOrEmpty(panel.Controls[2].Text) ? Convert.ToDecimal(panel.Controls[2].Text) : 0);
                        valuePair.Value.purchasePrice = (!String.IsNullOrEmpty(panel.Controls[3].Text) ? Convert.ToDecimal(panel.Controls[3].Text) : 0);
                        valuePair.Value.WholesalePrice = (!String.IsNullOrEmpty(panel.Controls[4].Text) ? Convert.ToDecimal(panel.Controls[4].Text) : 0);
                        valuePair.Value.sellingPrice = (!String.IsNullOrEmpty(panel.Controls[5].Text) ? Convert.ToDecimal(panel.Controls[5].Text) : 0);
                        valuePair.Value.barcode = (!String.IsNullOrEmpty(panel.Controls[7].Text) ? Convert.ToInt32(panel.Controls[7].Text) : -1);
                        KryptonComboBox unit = (KryptonComboBox)panel.Controls[6];
                        if (valuePair.Value.Unit!=null && valuePair.Value.barcode >= 0)
                        {
                            unit.SelectedItem = valuePair.Value.Unit;
                            valuePair.Value.UnitId= valuePair.Value.Unit.id;
                        }
                       
                    }
                }
            }
           
            itemController.fillMeasurementForItem();
        }
        private void sortTablePanels()
        {
            int countRecordes = tablePanels.Controls.Count;
            Guna2Button addButton = BuildControls.buildButton("AddButton", "AddButton", new Point(0, 12), Properties.Resources.MaterialSymbolsAddCircleOutlineRounded__1_, Properties.Resources.MaterialSymbolsAddCircleOutlineRounded, AddButtonNewRow_Click);
            this.toolTip.SetToolTip(addButton, "اضافة صف جديد");
            if (countRecordes > 5)
            {
                addButton.ImageAlign = HorizontalAlignment.Right;
                addButton.ImageOffset = new Point(-5, 0);
            }
            else
            {
                addButton.ImageOffset = new Point(3, 0);
                addButton.ImageAlign = HorizontalAlignment.Center;

            }
            for (int row = 0; row < countRecordes; row++)
            {
                tablePanels.Controls[row].Dock = DockStyle.Top;
        //        tablePanels.Controls[row].Name = (row).ToString();


                for (int cell = 0; cell < tablePanels.Controls[row].Controls.Count; cell++)
                {
                    if (countRecordes > 5)
                    {
                        if (tablePanels.Controls[row].Controls[cell] is Guna2TextBox)
                            tablePanels.Controls[row].Controls[cell].Width = 97;
                    }
                    else
                    {
                        if (tablePanels.Controls[row].Controls[cell] is Guna2TextBox)
                            tablePanels.Controls[row].Controls[cell].Width = 100;
                    }
                        tablePanels.Controls[row].Controls[cell].Dock = DockStyle.Right;
                }
                if (tablePanels.Controls[row].Controls.Count == 11)
                {
                 

                    tablePanels.Controls[row].Controls.RemoveAt(10);
                }
                if (row == countRecordes - 1)
                {
                    addButton.Dock = DockStyle.Left;
                    tablePanels.Controls[row].Controls.Add(addButton);
                }
            }
            //tablePanels.Width = (tablePanels.Parent as Control).Width;
        }
        private void AddButtonNewRow_Click(object sender, EventArgs e)
        {
            Guna2Button button = (Guna2Button)sender;
            Panel mainTabel = (Panel)tablePanels.Parent;
            int countRow = tablePanels.Controls.Count;
            if (countRow >= 5)
            {
                int oldeHeightMainTabel = mainTabel.Height;
                int oldeHeightTabel = tablePanels.Height;
                mainTabel.AutoSize = false;
                tablePanels.AutoSize = false;
                tablePanels.AutoScroll = true;
                mainTabel.Height = oldeHeightMainTabel;
                tablePanels.Height = oldeHeightTabel;

            }
            else
            {
                mainTabel.AutoSize = true;
                tablePanels.AutoSize = true;
                tablePanels.AutoScroll = false;
            }
            AddNewRow();
            tablePanels.Controls[countRow].Controls[3].Focus();
            sortTablePanels();
            //TablePanels_SizeChanged(tablePanels,null);
            tablePanels.Width= (tablePanels.Parent as Control).Width-20;
        }
        private void buttonRemoveRowClick(object sender, EventArgs e)
        {
            Guna2Button textBox = (Guna2Button)sender;
            Guna2Panel parentPanel = (Guna2Panel)textBox.Parent;
            int i = tablePanels.Controls.GetChildIndex(parentPanel);
            tablePanels.Controls.RemoveAt(i);
            sortTablePanels();
            int numberPerant = int.Parse(parentPanel.Name);

            if (itemController. selectedMeasurements.ContainsKey(numberPerant))
                itemController.selectedMeasurements.Remove(numberPerant);
            tablePanels.Width = (tablePanels.Parent as Control).Width - 20;
        }
        public void clearTablePanels()
        {
            tablePanels.Controls.Clear();
            itemController.clearTempData();
            AddNewRow();
        }
    }


    public class IngredientWidget
    {
        static int countnNewRow = 0;
        Guna2Panel tablePanels;
        ItemController itemController;
        ToolTip toolTip;
        public IngredientWidget(ItemController itemController, ToolTip toolTip)
        {
           
            tablePanels = new Guna2Panel();
            
            this.itemController = itemController;
            this.toolTip = toolTip;
            tablePanels.AutoSize = true;
            tablePanels.BorderRadius = 20;
            tablePanels.CustomizableEdges.TopLeft = false;
            tablePanels.CustomizableEdges.TopRight = false;
            tablePanels.Dock = System.Windows.Forms.DockStyle.Bottom;
            tablePanels.FillColor = System.Drawing.Color.White;
            tablePanels.Location = new System.Drawing.Point(0, 54);
            tablePanels.Name = "table";
            tablePanels.Size = new System.Drawing.Size(960, 0);
            tablePanels.TabIndex = 85;

        }
        public Guna2Panel returnNewTablePanels()
        {
            return tablePanels;
        }
        public void newRow()
        {
            AddNewRow();
        }

        public void fillTablePanels()
        {
            foreach (var compositeItem in itemController.compositeItemsForSelectedMeasurement())
            {
                AddNewRow(compositeItem);
            }
            if (!itemController.compositeItemsForSelectedMeasurement().Any())
                AddNewRow();
            // CreditOrDeditTextBox_TextChanged(null, null);
        }
        private void AddNewRow(CompositeItem composite=null)
        {///for  first itm is null
            tablePanels.SuspendLayout();
           Unit[] tempList = new Unit[itemController.units.Count + 1];
            itemController.units.CopyTo(tempList);
            if (itemController.units.Any())
            {
                Unit temp = tempList[0];

                tempList[itemController.units.Count] = temp;
            }
            tempList[0] = new Unit() { name = "", id = 0 };

            Classify[] tempListItem = new Classify[itemController.supItms.Count + 1];
            itemController.supItms.CopyTo(tempListItem);
            if (itemController.supItms.Any())
            {
                Classify temp = tempListItem[0];
                tempListItem[itemController.supItms.Count] = temp;
            }
            tempListItem[0] = new Classify() { nameAr = "", id = 0 };
            Guna2Panel newPanel = new Guna2Panel();
            tablePanels.Controls.Add(newPanel);
            newPanel.Size = new Size(1130, 35);
            newPanel.BackColor = Color.Transparent;
            Size size = new Size(100, 54);
            Size ComboBoxSize = new Size(140, 54);
            newPanel.Name = (countnNewRow).ToString();
          
            Guna2TextBox purchasePrice = BuildControls.buildTextBox("ج.الشراء", "purchasePrice", size, new Point(285, 5));
            Guna2TextBox total = BuildControls.buildTextBox("الإجمالي", "total", size, new Point(35, 5),true);
            Guna2TextBox sellingPrice = BuildControls.buildTextBox("سعر البيع", "sellingPrice", size, new Point(160, 5));
            Guna2TextBox quantity = BuildControls.buildTextBox("الكميه", "quantity", size, new Point(285, 5));
            KryptonComboBox unit = BuildControls.buildComboBox("الوحده", "unit", size, new Point(413, 5), tempList, eventHandler: comboBox_SelectionChangeCommitted);
            KryptonComboBox item = BuildControls.buildComboBox("الصنف", "item", ComboBoxSize, new Point(413, 5), tempListItem, eventHandler: item_SelectionChangeCommitted, displayMember:"nameAr");

            quantity.Text = "1";
            newPanel.Controls.Add(purchasePrice);
            newPanel.Controls.Add(total);
            newPanel.Controls.Add(sellingPrice);
            newPanel.Controls.Add(quantity);
            newPanel.Controls.Add(unit);
            newPanel.Controls.Add(item);
            purchasePrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            total.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            sellingPrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            quantity.KeyPress += ValidatingData.eventTextBoxNumberOnly;
            unit.KeyPress += ValidatingData.eventTextBoxTextOnly;
            item.KeyPress += ValidatingData.eventTextBoxTextOnly;
            Guna2Button removeButton = BuildControls.buildButton("b", "s", new Point(675, 7), Properties.Resources.FluentMdl2Cancel, Properties.Resources.FluentMdl2Cancel__1_);
            removeButton.Click += buttonRemoveRowClick;
            toolTip.SetToolTip(removeButton, "حذف");
            newPanel.Controls.Add(removeButton);
            newPanel.Dock = DockStyle.Top;
            sellingPrice.TextChanged += priceSellingAndPurchaseTextBox_TextChanged;
            purchasePrice.TextChanged += priceSellingAndPurchaseTextBox_TextChanged;
            quantity.TextChanged += priceSellingAndPurchaseTextBox_TextChanged;
            sortTablePanels();
            if (composite != null)
            {
                itemController.selectedCompositeItem.Add(int.Parse(newPanel.Name), composite);
                purchasePrice.Text = composite.purchasePrice.ToString();
                sellingPrice.Text = composite.sellingPrice.ToString();
                total.Text = (composite.sellingPrice * composite.purchasePrice).ToString();
                quantity.Text = composite.quantity.ToString();
                unit.SelectedItem = composite.ComponentItem.Unit;
                item.SelectedItem = composite.ComponentItem.item;

            }
            tablePanels.ResumeLayout(true);
          
            countnNewRow++;
        }

        private void priceSellingAndPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {   Control control = (sender as Control).Parent;
            
            int perantNumber = int.Parse(control.Name);
            decimal sellingPrice = 0;
            decimal purchasePrice = 0;
            if(itemController.selectedCompositeItem.ContainsKey(perantNumber))
            { decimal quantity = Convert.ToDecimal(String.IsNullOrEmpty(control.Controls[3].Text)? "1" : control.Controls[3].Text) ==0?1 : Convert.ToDecimal(control.Controls[3].Text);
                control.Controls[1].Text = (quantity * (String.IsNullOrEmpty(control.Controls[2].Text) || control.Controls[2].Text =="0"? 1: Convert.ToDecimal(control.Controls[2].Text))).ToString();
                foreach (Guna2Panel panel in tablePanels.Controls)
                {
                    purchasePrice += (!String.IsNullOrEmpty(panel.Controls[0].Text) ? Convert.ToDecimal(panel.Controls[0].Text) : 0);
                    sellingPrice += (!String.IsNullOrEmpty(panel.Controls[2].Text) ? Convert.ToDecimal(panel.Controls[2].Text) : 0);
                }
                itemController.sellingPriceTotal.MyProperty = sellingPrice.ToString();
                itemController.purchasePriceTotal.MyProperty = purchasePrice.ToString();
            }
        }
        void item_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Guna2Panel pearant = (Guna2Panel)comboBox.Parent;
            int numberPerant = int.Parse(pearant.Name);
            itemController.selecteCompositeItem((Classify)comboBox.SelectedItem, numberPerant);
            update(pearant);
        }
        void update(Guna2Panel panel)
        {
            int numberPerant = int.Parse(panel.Name);
            {
                panel.Controls[0].Text=itemController.tempCompositeItem.purchasePrice.ToString();
                panel.Controls[1].Text= itemController.tempCompositeItem.purchasePrice.HasValue&& itemController.tempCompositeItem.sellingPrice.HasValue? (itemController.tempCompositeItem.purchasePrice* itemController.tempCompositeItem.sellingPrice).ToString():"";
                panel.Controls[2].Text = itemController.tempCompositeItem.sellingPrice.ToString();
               KryptonComboBox unit=(KryptonComboBox)panel.Controls[4];
               KryptonComboBox item=(KryptonComboBox)panel.Controls[5];
                if (itemController.tempCompositeItem.ComponentItem.Unit != null)
                    unit.DataSource = new List<Unit>() { itemController.tempCompositeItem.ComponentItem.Unit };
                else
                    unit.DataSource = itemController.units;
                unit.SelectedItem=itemController.tempCompositeItem.ComponentItem.Unit;
                item.SelectedItem = itemController.tempCompositeItem.ComponentItem.item;
            }
        }
        void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Guna2Panel pearant = (Guna2Panel)comboBox.Parent;

            int numberPerant = int.Parse(pearant.Name);
            Unit unit = (Unit)comboBox.SelectedItem;
         
        }
        public void getCompositeItems()
        {
            foreach (var valuePair in itemController.selectedCompositeItem)
            {
                string numberPerant = valuePair.Key.ToString();
                foreach (Guna2Panel panel in tablePanels.Controls)
                {
                    if (panel.Name == numberPerant)
                    {
                        valuePair.Value.purchasePrice = (!String.IsNullOrEmpty(panel.Controls[0].Text) ? Convert.ToDecimal(panel.Controls[0].Text) : 0);
                        valuePair.Value.sellingPrice = (!String.IsNullOrEmpty(panel.Controls[2].Text) ? Convert.ToDecimal(panel.Controls[2].Text) : 0);
                        valuePair.Value.quantity = (!String.IsNullOrEmpty(panel.Controls[3].Text) ? Convert.ToDecimal(panel.Controls[3].Text) : 0);
                        KryptonComboBox unit = (KryptonComboBox)panel.Controls[4];
                        KryptonComboBox item = (KryptonComboBox)panel.Controls[5];
                        if ((valuePair.Value.purchasePrice > 0 || valuePair.Value.sellingPrice > 0) && valuePair.Value.quantity >0)
                        {
                            unit.SelectedItem = valuePair.Value.ComponentItem.Unit;
                            item.SelectedItem = valuePair.Value.ComponentItem.CompositeItems;
                            valuePair.Value.componentItemId = valuePair.Value.ComponentItem.itemId;
                        }
                    }
                }
            }
            itemController.fillCompositeItemsForSelectedMeasurement();

        }
        public void clearTablePanels()
        {
            itemController.selectedCompositeItem.Clear();
            tablePanels.Controls.Clear();
            itemController.clearTempDataIngredient();
            AddNewRow();
        }
        private void sortTablePanels()
        {
            int countRecordes = tablePanels.Controls.Count;
            Guna2Button addButton = BuildControls.buildButton("AddButton", "AddButton", new Point(0, 12), Properties.Resources.MaterialSymbolsAddCircleOutlineRounded__1_, Properties.Resources.MaterialSymbolsAddCircleOutlineRounded, AddButtonNewRow_Click);
            this.toolTip.SetToolTip(addButton, "اضافة صف جديد");

            if (countRecordes > 13)
            {
                addButton.ImageAlign = HorizontalAlignment.Right;
                addButton.ImageOffset = new Point(-1, 0);

            }
            else
            {
                addButton.ImageOffset = new Point(-7, 0);
                addButton.ImageAlign = HorizontalAlignment.Left;


            }
            for (int row = 0; row < countRecordes; row++)
            {
                tablePanels.Controls[row].Dock = DockStyle.Top;
                tablePanels.Controls[row].Name = (row).ToString();


                for (int cell = 0; cell < tablePanels.Controls[row].Controls.Count; cell++)
                {
                    tablePanels.Controls[row].Controls[cell].Dock = DockStyle.Right;
                }
                if (tablePanels.Controls[row].Controls.Count == 8)
                {
                    tablePanels.Controls[row].Controls.RemoveAt(7);
                }
                if (row == countRecordes - 1)
                {
                    addButton.Dock = DockStyle.Left;

                    //  this.toolTip1.SetToolTip(addButton, "اضافة صف جديد");
                    tablePanels.Controls[row].Controls.Add(addButton);
                }
            }
        }
        private void AddButtonNewRow_Click(object sender, EventArgs e)
        {
            Guna2Button button = (Guna2Button)sender;
            Panel mainTabel = (Panel)tablePanels.Parent;
            int countRow = tablePanels.Controls.Count;
            if (countRow > 12)
            {
                int oldeHeightMainTabel = mainTabel.Height;
                int oldeHeightTabel = tablePanels.Height;
                mainTabel.AutoSize = false;
                tablePanels.AutoSize = false;
                tablePanels.AutoScroll = true;
                mainTabel.Height = oldeHeightMainTabel;
                tablePanels.Height = oldeHeightTabel;

            }
            else
            {
                mainTabel.AutoSize = true;
                tablePanels.AutoSize = true;
                tablePanels.AutoScroll = false;
            }
            AddNewRow();
            tablePanels.Controls[countRow].Controls[3].Focus();
            sortTablePanels();
        }




        private void buttonRemoveRowClick(object sender, EventArgs e)
        {
            Guna2Button button = (Guna2Button)sender;
            Guna2Panel parentPanel = (Guna2Panel)button.Parent;
            int i = tablePanels.Controls.GetChildIndex(parentPanel);
            int numberPerant = int.Parse(parentPanel.Name);
            if (itemController.selectedCompositeItem.ContainsKey(numberPerant))
            {
                tablePanels.Controls[i].Controls[0].Text = "0";
                tablePanels.Controls[i].Controls[2].Text = "0";
                itemController.selectedCompositeItem.Remove(numberPerant);
            }
            tablePanels.Controls.RemoveAt(i);
            sortTablePanels();
            

         

        }

    }

}
namespace AccountingSystem.view.SupScreens.SalesSystem
{
    partial class DialogAddAndUpdteSalesSystem
    {

        Guna2TileButton btnActiveGroup;
      static  int countRow;
     
        private void InitializeAppComponent()
        {
            countRow = 0;

            //setCellTitelTable();
                fillTablePanels();
                buildeListBtnGroup();
            TitelSize_Changed(titelTable, null);
            controller.HasAddAndUpdateScreenDataProcessed = true;
        }

        private void setCellTitelTable()
        {
            if (!controller.isSale)
            {
                label10.Visible = false;
                label12.Visible= false;

                foreach (Label cell in titelTable.Controls)
                {
                    cell.Width += 10;
                }
              //  label5.Width = 53;
            }
        }

        public void fillTablePanels()
        {
            if(controller.isSale)
            {
                Thread thread1 = new Thread(fillSaleDetails);
                thread1.Start();
                controller.selectedSaleDetail = null;
            }
            else
            {
                Thread thread2 = new Thread(fillPurchaseDetails);
                thread2.Start();
                controller.selectedPurchaseDetail = null;
            }
        }

        private void fillSaleDetails()
        {
            if (controller.tempSale != null)
                foreach (var detail in controller.tempSale.SaleDetails)
                {
                    if (detail.type!= MeasurementsItemType.مركب.ToString())
                    {
                        controller.addSelectedSaleDetail(countRow, detail);
                        bodyTable.Invoke(new Action(() =>AddNewRowToTable()));
                    }
                }
            if (controller.tempSale.SaleDetails == null || !controller.tempSale.SaleDetails.Any())
                bodyTable.Invoke(new Action(() =>AddNewRowToTable()));
        }
        private void fillPurchaseDetails()
        {
            if (controller.tempPurchase != null)
                foreach (var detail in controller.tempPurchase.PurchaseDetails)
                {
                    if (detail.type != MeasurementsItemType.مركب.ToString()) 
                    {
                       
                        controller.addSelectedPurchaseDetail(countRow, detail);
                        bodyTable.Invoke(new Action(() => AddNewRowToTable()));
                    }
                }
            if (controller.tempPurchase.PurchaseDetails == null || !controller.tempPurchase.PurchaseDetails.Any())
                bodyTable.Invoke(new Action(() =>AddNewRowToTable()));
        }

        public void AddNewRowToTable()
        {
            buildeRow();
            sortRecordeTable();
            totalCalculation();
            this.ResumeLayout(true);
            bodyTable.ResumeLayout(true);
          
            controller.selectedPurchaseDetail = null;
            controller.selectedSaleDetail = null;

        }
        void buildeRow()
        {
            FlowLayoutPanel newPanel = new FlowLayoutPanel();
            bool isSale = controller.transactionType == TransactionType.فاتورة_مبيعات;
            int width = isSale ? 0 : 20;
            this.SuspendLayout();
            bodyTable.SuspendLayout();
            newPanel.SuspendLayout();

            //newPanel.Dock = DockStyle.Top;
            newPanel.Name = (countRow++).ToString();
            newPanel.Size = new Size(bodyTable.Width-((int)(bodyTable.Width*0.02)), 40);
            newPanel.BackColor = Color.Transparent;
       

            //AppDialogAleart.showAleartNoPermissions("newPanel.Name" + newPanel.Name);

            int height = 30;
            Guna2Button addButton = BuildControls.buildButton("AddButton", "AddButton", new Point(0, 12), Properties.Resources.MaterialSymbolsAddCircleOutlineRounded__1_, Properties.Resources.MaterialSymbolsAddCircleOutlineRounded, AddButton_Click);

            Guna2TextBox note = BuildControls.buildTextBox("ملاحضات", "note", new Size(110 + width, height), new Point(976, 0));

            Guna2TextBox total = BuildControls.buildTextBox("الإجمالي", "total", new Size(100 + width, height), new Point(976, 0), true);

            Guna2TextBox descountPrice = BuildControls.buildTextBox("تخفيض", "descountPrice", new Size(80, height), new Point(976, 0), true, isSale);

            Guna2TextBox unitPrice = BuildControls.buildTextBox("س.الوحده", "unitPrice", new Size(100 + width, height), new Point(976, 0));

            Guna2TextBox availableQuantity = BuildControls.buildTextBox("م.متاحة", "availableQuantity", new Size(80, height), new Point(976, 0), true, isSale);

            Guna2TextBox quantity = BuildControls.buildTextBox("الكميه", "quantity", new Size(80 + width, height), new Point(976, 0));
            KryptonComboBox unit = BuildControls.buildComboBox("الوحده", "unit", new Size(70 + width, height), new Point(413, 5), controller.copyUnits, eventHandler: unit_SelectionChangeCommitted);
            KryptonComboBox item = BuildControls.buildComboBox("الصنف", "item", new Size(120 + width, height), new Point(413, 5), controller.copySupItms, 10F, item_SelectionChangeCommitted, "nameAr", true);
            Guna2TextBox numberItem = BuildControls.buildTextBox("رقم الصنف", "d", new Size(100, height), new Point(976, 0));

            Guna2Button removeButton = BuildControls.buildButton("s", "removeButton", new Point(0, 12), Properties.Resources.MaterialSymbolsCancelOutlineRounded, Properties.Resources.MaterialSymbolsCancelOutlineRounded__1_, removeButtonClick);
            addButton.Visible = false;
            newPanel.Controls.Add(removeButton);
            newPanel.Controls.Add(numberItem);
            newPanel.Controls.Add(item);
            newPanel.Controls.Add(unit);
            newPanel.Controls.Add(quantity);
            newPanel.Controls.Add(availableQuantity);
            newPanel.Controls.Add(unitPrice);
            newPanel.Controls.Add(descountPrice);
            newPanel.Controls.Add(total);
            newPanel.Controls.Add(note);
            newPanel.Controls.Add(addButton);
            quantity.Text = "1";
            unitPrice.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            quantity.KeyPress += ValidatingData.eventTextBoxPriceOnly;
            unit.KeyPress += ValidatingData.eventTextBoxTextOnly;
            item.KeyPress += ValidatingData.eventTextBoxTextOnly;
            numberItem.KeyPress += ValidatingData.eventTextBoxNumberOnly;
            quantity.TextChanged += QuantityOrUnitPriceTextChanged;
            unitPrice.TextChanged += QuantityOrUnitPriceTextChanged;
            if (controller.selectedSaleDetail != null)
            {

                if (controller.selectedSaleDetail.MeasurementsItem != null && controller.selectedSaleDetail.MeasurementsItem.item.type == "فرعي")
                {
                    unit.DataSource = new List<Unit>() { controller.selectedSaleDetail.MeasurementsItem.Unit };
                }
                unitPrice.Text = controller.selectedSaleDetail.unitPrice.ToString();
                numberItem.Text = controller.selectedSaleDetail.item.ClassifyNumber.ToString();
                item.SelectedItem = controller.selectedSaleDetail?.item;
                availableQuantity.Text = controller.availableQuantityForSelectedSaleDetail().ToString();
                total.Text = (controller.selectedSaleDetail.TotalPrice()).Format();
                note.Text = controller.selectedSaleDetail.description;
                quantity.Text = controller.selectedSaleDetail.quantity.ToString();
                descountPrice.Tag = controller.selectedSaleDetail.descountPrice ?? 0;
                descountPrice.Text = controller.selectedSaleDetail.DescountPrice().Format();
            }
            if (controller.selectedPurchaseDetail != null)
            {

                if (controller.selectedPurchaseDetail.MeasurementsItem != null && controller.selectedPurchaseDetail.MeasurementsItem.item.type == "فرعي")
                {

                    unit.DataSource = new List<Unit>() { controller.selectedPurchaseDetail.MeasurementsItem.Unit };
                }
                unitPrice.Text = controller.selectedPurchaseDetail.unitPrice.ToString();
                numberItem.Text = controller.selectedPurchaseDetail.item.ClassifyNumber.ToString();
                item.SelectedItem = controller.selectedPurchaseDetail?.item;
                total.Text = (((unitPrice.Text.ToDecimal()) * controller.selectedPurchaseDetail.quantity) ?? 0).Format();
                note.Text = controller.selectedPurchaseDetail.description;
                quantity.Text = (controller.selectedPurchaseDetail.quantity ?? 0).Format();
            }
            bodyTable.Controls.Add(newPanel);
            Size_Changed(newPanel, null);
            newPanel.SizeChanged += Size_Changed;
            newPanel.ResumeLayout(true);

        }

        void Size_Changed(object sender, EventArgs e)
        {


            Control row = sender as Control;

            row.Controls[0].Width = (int)(row.Width * 0.030);
            
            row.Controls[1].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.090 : .110));
            row.Controls[2].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.110 : .140));
            row.Controls[3].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.1 : .120));
            row.Controls[4].Width = (int)(row.Width * 0.080);
            row.Controls[5].Width = (int)(row.Width * 0.080);
            row.Controls[6].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.1 : .120));
            row.Controls[7].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.080 : .1));
            row.Controls[8].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.110 : .140));
            row.Controls[9].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.100: .120));

            if (row.Controls.Count > 10)
                row.Controls[10].Width = (int)(row.Width * 0.030);

        }
        void TitelSize_Changed(object sender, EventArgs e)
        {


            Control row = sender as Control;

            row.Controls[9].Width = (int)(row.Width * 0.050);

            row.Controls[8].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.090 : .110));
            row.Controls[7].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.110 : .140));
            row.Controls[6].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.1 : .120));
            row.Controls[5].Width = (int)(row.Width * 0.1);
            row.Controls[4].Width = (int)(row.Width * 0.1);
            row.Controls[3].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.1 : .120));
            row.Controls[2].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.080 : .1));
            row.Controls[1].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.110 : .140));
            row.Controls[0].Width = (int)(row.Width * (controller?.isSale == null || controller.isSale ? 0.100 : .120));


        }
        private void QuantityOrUnitPriceTextChanged(object sender, EventArgs e)
        {
            Control row = (sender as Control).Parent;
            int rowNumber = int.Parse(row.Name);
          
            if (controller.selectedSaleDetails.ContainsKey(rowNumber)|| controller.selectedPurchaseDetails.ContainsKey(rowNumber))
            {
                decimal descountPrice= row.Controls[7].Tag!=null?(decimal)(row.Controls[7].Tag):0;
                descountPrice = descountPrice * row.Controls[6].Text.ToDecimal();
                //decimal quantity = row.Controls[5].Text.ToDecimal() == 0 ? 1 : row.Controls[5].Text.ToDecimal();
                decimal quantity = row.Controls[4].Text.ToDecimal();
            
                row.Controls[8].Text = ((row.Controls[6].Text.ToDecimal() - descountPrice) * quantity).Format();
                row.Controls[7].Text =(descountPrice* quantity).Format();
              
                totalCalculation();
            }
        }
        void totalCalculation()
        {
            decimal totalPrice = 0;
            decimal totalDiscountItems = 0;
            foreach (Control recorde in bodyTable.Controls)
            {
                totalPrice += recorde.Controls[8].Text.ToDecimal();
                totalDiscountItems += recorde.Controls[7].Text.ToDecimal();
            }
            total.Text = totalPrice.Format(4);
            discountItems.Text = totalDiscountItems.Format();
            calculatePrices();
        }
        void calculatePrices()
        {
            if (controller.selectedSaleDetails.Any()|| controller.selectedPurchaseDetails.Any())
            {
                sum.Text = (total.Text.ToDecimal() - billDiscount.Text.ToDecimal() - discountItems.Text.ToDecimal()).Format();
                remaining.Text = (total.Text.ToDecimal() - amountPaid.Text.ToDecimal() - billDiscount.Text.ToDecimal() - discountItems.Text.ToDecimal()).Format();
            }
        }
        void item_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            if (comboBox.Parent != null)
            {
                Control pearant = (Control)comboBox.Parent;
                controller.selectedItem(comboBox.SelectedItem, int.Parse(pearant.Name));
                updateDetail(pearant);
            }
        }
        void unit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Control pearant = (Control)comboBox.Parent;

            int numberPerant = int.Parse(pearant.Name);
            Unit unit = (Unit)comboBox.SelectedItem;

        }
        public void sortRecordeTable()
        {
            int countRecordes = bodyTable.Controls.Count;
            if (countRecordes > 0)
                bodyTable.Controls[0].Controls[bodyTable.Controls[0].Controls.Count - 1].Visible=true;
        }
        public void getSaleDetail()
        {
            foreach (var valuePair in controller.selectedSaleDetails)
            {
                string numberPerant = valuePair.Key.ToString();
                foreach (Control row in bodyTable.Controls)
                {
                  
                    if (row.Name == numberPerant)
                    {
                         decimal descountPrice= row.Controls[7].Tag!=null?(decimal)(row.Controls[7].Tag):0;

                        valuePair.Value.descountPrice = descountPrice;
                        valuePair.Value.unitPrice = row.Controls[6].Text.ToDecimal();
                        valuePair.Value.quantity = row.Controls[4].Text.ToDecimal();
                        valuePair.Value.description = row.Controls[9].Text;
                        valuePair.Value.itemId = valuePair.Value.item.id;
                        valuePair.Value.measurementItemId = valuePair.Value.MeasurementsItem.id;
                       KryptonComboBox unit = (KryptonComboBox)row.Controls[3];
                        KryptonComboBox item = (KryptonComboBox)row.Controls[2];
                            unit.SelectedItem = valuePair.Value.MeasurementsItem.Unit;
                            item.SelectedItem = valuePair.Value.item;

                    }
                }
            }
         //   itemController.fillCompositeItemsForSelectedMeasurement();

        }
        public void getInvoiceDetail()
        {
            if(controller.isSale)
                getSaleDetail();
            else getPurchaseDetail();
        }
        public void getPurchaseDetail()
        {
            foreach (var valuePair in controller.selectedPurchaseDetails)
            {
                string numberPerant = valuePair.Key.ToString();
                foreach (Control row in bodyTable.Controls)
                {
                    if (row.Name == numberPerant)
                    {

                        valuePair.Value.unitPrice = row.Controls[6].Text.ToDecimal();
                        valuePair.Value.quantity = row.Controls[4].Text.ToDecimal();
                        valuePair.Value.description = row.Controls[9].Text;
                        valuePair.Value.itemId = valuePair.Value.item.id;
                        valuePair.Value.measurementItemId = valuePair.Value.MeasurementsItem.id;
                        KryptonComboBox unit = (KryptonComboBox)row.Controls[3];
                        KryptonComboBox item = (KryptonComboBox)row.Controls[2];
                        unit.SelectedItem = valuePair.Value.MeasurementsItem.Unit;
                        item.SelectedItem = valuePair.Value.item;

                    }
                }
            }
            //   itemController.fillCompositeItemsForSelectedMeasurement();

        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddNewRowToTable();
            sortRecordeTable();

        }
        private void removeButtonClick(object sender, EventArgs e)
        {
            Control row = (Control)((Control)sender).Parent;
            row.Controls[6].Text = "0";
            controller.removeSelectedDetailAt(int.Parse(row.Name));
            bodyTable.Controls.RemoveByKey(row.Name);
            sortRecordeTable();
            totalCalculation();

        }

        public void buildeListBtnGroup()
        {
  
            btnActiveGroup = BuildControls.buildBtnGroupItem(new NewModel.EFModel.ClassifyGroup() { id = 0, name = "الكل" }, btnGroup_Click);
            panelBtnGroup.Controls.Add(btnActiveGroup);
            FunctionsGUI.changeBtnToActiveOrUnActive(btnActiveGroup);
            Thread threadBtnsGroups = new Thread(buildeListBtnsGroups);
            threadBtnsGroups.Start();
            Thread threadBtnsCards = new Thread(buildeListCardItems);
            threadBtnsCards.Start();

        }
        void buildeListBtnsGroups()
        {
            panelBtnGroupsStartInt();
            //for (int i = 0; i < 50; i++)
            {
                foreach (var group in controller.groups)
                {

                    panelBtnGroup.BeginInvoke( new Action(() => panelBtnGroup.Controls.Add(BuildControls.buildBtnGroupItem(group, btnGroup_Click))));
                }
            }
           panelBtnGroupsEndInt();
        }

       
        private void btnGroup_Click(object sender, EventArgs e)
        {
            FunctionsGUI.changeBtnToActiveOrUnActive(btnActiveGroup);
            btnActiveGroup = (Guna2TileButton)sender;
            FunctionsGUI.changeBtnToActiveOrUnActive(btnActiveGroup);
            filterBtnCardItems();


        }
        private void btnCardItem_Click(object sender, EventArgs e)
        {
            Control control=(Control)sender;
            controller.selectedItem((Classify)((control is Guna2Panel) ? control.Tag : control.Parent.Tag), countRow);
            //AppDialogAleart.showAleartNoPermissions(" controller.selectedItem=" + countRow.ToString());
            AddNewRowToTable();
        }
       void updateDetail(Control row)
        {
            if (controller.isSale&& controller.selectedSaleDetail!=null)
                updateSaleDetail(row);
            else if(controller.selectedPurchaseDetail!=null)
                updatePurchaseDetail(row);
        }
        void updatePurchaseDetail(Control row)
        {
           
            row.Controls[6].Text = (controller.selectedPurchaseDetail.unitPrice ?? 0).Format();
                row.Controls[1].Text = controller.selectedPurchaseDetail?.item?.ClassifyNumber?.ToString();
                //row.Controls[1].Text = (row.Controls[3].Text.ToDecimal() * (row.Controls[5].Text.ToDecimal() == 0 ? 1 : row.Controls[5].Text.ToDecimal())).Format();
                row.Controls[8].Text = (row.Controls[6].Text.ToDecimal() * (row.Controls[4].Text.ToDecimal())).Format();
                KryptonComboBox unit = (KryptonComboBox)row.Controls[3];
                KryptonComboBox item = (KryptonComboBox)row.Controls[2];
                unit.DataSource = new List<Unit>() { controller. selectedPurchaseDetail.MeasurementsItem.Unit };
                item.SelectedItem = controller.selectedPurchaseDetail.item;
                controller.selectedPurchaseDetail = null;

        }
        void updateSaleDetail(Control row)
        {
           
         //   row.Controls[4].Tag = controller.selectedSaleDetail.quantity ?? 0;
                row.Controls[7].Text = ((controller.selectedSaleDetail.descountPrice ?? 00) * controller.selectedSaleDetail.unitPrice ?? 00).Format();
                row.Controls[6].Text = (controller.selectedSaleDetail.unitPrice ?? 0).Format();
                row.Controls[5].Text = controller.availableQuantityForSelectedSaleDetail().ToString();
                row.Controls[1].Text = controller.selectedSaleDetail?.item?.ClassifyNumber?.ToString();
                //row.Controls[1].Text = (row.Controls[3].Text.ToDecimal() * (row.Controls[5].Text.ToDecimal() == 0 ? 1 : row.Controls[5].Text.ToDecimal())).Format();
                row.Controls[8].Text = (row.Controls[6].Text.ToDecimal() * ( row.Controls[4].Text.ToDecimal())).Format();
                KryptonComboBox unit = (KryptonComboBox)row.Controls[3];
                KryptonComboBox item = (KryptonComboBox)row.Controls[2];
                unit.DataSource = new List<Unit>() { controller.selectedSaleDetail.MeasurementsItem.Unit };
                item.SelectedItem = controller.selectedSaleDetail.item;
                controller.selectedSaleDetail = null;

        }
        void clearDetail()
        {
            countRow = 0;
            bodyTable.SuspendLayout();
            panelBtnCard.SuspendLayout();
            panelBtnGroup.SuspendLayout();
            total.Clear();
            billDiscount.Clear();
            discountItems.Clear();
            sum.Clear();
            amountPaid.Clear();
            remaining.Clear();
            bodyTable.Controls.Clear();
            panelBtnGroup.Controls.Clear();
            panelBtnCard.Controls.Clear();
            searchItem.Clear();
            
            panelBtnCard.ResumeLayout(true);
            panelBtnGroup.ResumeLayout(true);
            bodyTable.ResumeLayout(true);
        }
        private void searchItem_TextChanged(object sender, EventArgs e)
        {
            filterBtnCardItems();
        }
        public void buildeListCardItems()
        {
            panelBtnCardStartInt();
            //for (int i = 0; i < 50; i++)
            {
                foreach (var item in controller.supItms)
                {
                    panelBtnCard.BeginInvoke(new Action(() =>panelBtnCard.Controls.Add(BuildControls.buildCardItem(item, btnCardItem_Click))));
                }
            }
            panelBtnCardEndInt();

        }
        string name = "";
        int groupId = 0;
        int lenCardItems = 0;
        public void filterBtnCardItems()
        {
           name = searchItem.Text;
           groupId = int.Parse(btnActiveGroup.Name);
            lenCardItems=panelBtnCard.Controls.Count;
            Thread thread = new Thread(filterItems); thread.Start();
            
        }
       
        private void filterItems()
        {
         panelBtnCardStartInt();
            for (int i = 0; i < lenCardItems; i++)
            {
                panelBtnCard.Invoke(new Action(() => { 
                    Control btnCard = panelBtnCard.Controls[i];
                    if (btnCard.Tag != null)
                    {
                        Classify item = (Classify)btnCard.Tag;
                        if (item != null)
                        {
                            if ((item.nameAr.Contains(name) || item.nameEn.Contains(name)) && (groupId == 0 ? true : (item.ClassifyGroupId == groupId)))
                            {
                                btnCard.Visible = true;
                            }
                            else
                            {
                                btnCard.Visible = false;
                            }
                        }
                    }
                }));

            }
            panelBtnCardEndInt();
        }
        private void panelBtnGroupsEndInt()
        {
            panelBtnGroup.Invoke(new Action(() => { 
                panelBtnGroup.ResumeLayout();
                panelBtnGroup.Show();

            }));
        }

        private void panelBtnGroupsStartInt()
        {
            panelBtnGroup.Invoke(new Action(() => { 
                panelBtnGroup.SuspendLayout();
                panelBtnGroup.Hide();

            }));
        }
        void panelBtnCardStartInt()
        {
            panelBtnCard.Invoke(new Action(() => { 
                panelBtnCard.Hide();
                panelBtnCard.SuspendLayout();
            }));


        }
        void panelBtnCardEndInt()
        {
            panelBtnCard.Invoke(new Action(() => {
                panelBtnCard.ResumeLayout();
                panelBtnCard.Show();

            }));
        }

    }
}