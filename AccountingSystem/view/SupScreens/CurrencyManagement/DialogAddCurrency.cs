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
using AccountingSystem.controller;
using AccountingSystem.core.shared;
using AccountingSystem.Model;

namespace AccountingSystem.view.Screens.CurrencyManagement
{
    public partial class DialogAddCurrency : Form
    {
        CurrencyController controller;

        public DialogAddCurrency( CurrencyController controller)
        {
            InitializeComponent();
            this.  controller =controller;
           labelTitel.Text = Functions.getCurrentRoot();
        }
        private void DialogAddCurrency_Load(object sender, EventArgs e)
        {
            currencyCode.DataSource = controller.currencies;
            currencyCode.TextOnly("code");
            currencyName.TextOnly();
            exchangeRate.PriceOnly();
            if (controller.prosessesType == ProsessesType.update)
            {

                fillFeild();
            }
            setCombBox();
        }

        private void setCombBox()
        {
            currencyCode.SelectedItem = controller.temp;
        }
        private void fillFeild()
        {
            currencyName.Text = controller.temp.name;
            exchangeRate.Text = controller.temp.exchangeRate.ToString();
            if(controller.temp.currencyType==btnCurrencyMain.Text)
                FunctionsGUI.changeColorActiveBtn(btnCurrencyMain);
            else
                FunctionsGUI.changeColorActiveBtn(btnCurrencySecondary);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.dataProcessing(currencyName.Text, currencyCode.Text, exchangeRate.Text))
                this.Close();
         }

        private void btnsCurrencyType_Click(object sender, EventArgs e)
        {Guna2Button button=(Guna2Button)sender;
            if (button.Tag==null)
            {
                FunctionsGUI.reChangeColorActiveBtn(btnCurrencyMain);
                FunctionsGUI.reChangeColorActiveBtn(btnCurrencySecondary);
                FunctionsGUI.changeColorActiveBtn(button);
                controller.temp.currencyType = button.Text;
            }
        }
        private void clearFieldAndReferesh()
        {
            controller.clearTempData();
            currencyName.Clear();
            exchangeRate.Clear();
            FunctionsGUI.reChangeColorActiveBtn(btnCurrencyMain);
            FunctionsGUI.reChangeColorActiveBtn(btnCurrencySecondary);
            setCombBox();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }
    }
}
