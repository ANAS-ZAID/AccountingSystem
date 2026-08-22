using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class DialogSelecteIngredient : Form
    {

        IngredientWidget ingredientWidget;

        ItemController itemController;
        public DialogSelecteIngredient(ItemController itemController, ToolTip toolTip)
        {
            InitializeComponent();

            this.itemController = itemController;
            ingredientWidget = new IngredientWidget(itemController, toolTip);
            Guna2Panel panel = ingredientWidget.returnNewTablePanels();
            mainTable.Controls.Add(panel);
           
            sellingPriceTotal.DataBindings.Add("Text", itemController.sellingPriceTotal, "MyProperty", true, DataSourceUpdateMode.OnPropertyChanged);
            purchasePriceTotal.DataBindings.Add("Text", itemController.purchasePriceTotal, "MyProperty", true, DataSourceUpdateMode.OnPropertyChanged);
            titel.Text=$"يمكنك هنا إختيار الأصناف التي يتكون منها الصنف {itemController.nameItemSelectedMeasurementsItem} رقم الصنف {itemController.numberItemSelectedMeasurementsItem} رقم الباركود {itemController.barcodeSelectedMeasurementsItem} ";
         //   this.item = item;
        ingredientWidget.fillTablePanels();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           ingredientWidget.getCompositeItems();
            itemController.purchasePriceTotal.accreditation = accreditationPurchasePrice.Checked;
            itemController.sellingPriceTotal.accreditation = accreditationSellingPrice.Checked;

            Close();
        }

        private void DialogSelecteIngredient_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ingredientWidget.clearTablePanels();
        }
    }
}
