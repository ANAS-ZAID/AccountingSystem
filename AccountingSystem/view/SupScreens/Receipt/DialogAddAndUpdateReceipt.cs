using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.Receipt
{
    public partial class DialogAddAndUpdateReceipt : Form
    {
        VoucherController controller;
        public DialogAddAndUpdateReceipt(VoucherController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
            controller.HasHomeScreenDataProcessed=false;
            controller.lastDate = null;

        }
        private void DialogAddAndUpdateReceipt_Load(object sender, EventArgs e)
        {     controller.HasAddAndUpdateScreenDataProcessed=false;
            account.DataSource = controller.supAccounts;
            cashier.DataSource = controller.allCashiers;
            currency.DataSource = controller.allCurrency;
            account.TextOnly();
            amount.PriceOnly();
            amountMainCurrency.PriceOnly();
            exchangeRate.PriceOnly();
            currency.TextOnly();
            cashier.TextOnly();
            if (controller.prosessesType == ProsessesType.add)
            {
                exchangeRate.Text = controller.temp.Currency.exchangeRate.Format();
            }
         
            reSetCombobox();
            controller.HasAddAndUpdateScreenDataProcessed = true;
            if (controller.prosessesType == ProsessesType.update)
            {
                fillField();

            }
            
        }
        void reSetCombobox()
        {
            account.SelectedItem = controller.temp.Account;
            cashier.SelectedItem = controller.temp.Cashier;
            currency.SelectedItem = controller.temp.Currency;
            date.Value = controller.temp.date.Value;
        }
        void fillField()
        {
          
            reSetCombobox();
            amount.Text = controller.temp.amount.Format();
            description.Text = controller.temp.description;
            
            exchangeRate.Text = controller.journalEntryAccountCredit.ExchangeRate.Format();
            account_SelectionChangeCommitted(null, null);
            cashier_SelectionChangeCommitted(null, null);
            displayOrHideExchangeRate();
        }
        void displayOrHideExchangeRate()
        {
            if (controller.temp.Currency.currencyType != "رئيسية")
            {
                if (panelMainField.Tag == null)
                {
                    panelMainField.Top += 65;
                    panelMainField.Tag = "open";
                }
              
               
            }
            else
            {
                if (panelMainField.Tag != null)
                {
                    panelMainField.Top -= 65;
                    exchangeRate.Clear();
                    amountMainCurrency.Clear();
                    panelMainField.Tag = null;
                }
          
             
            }
            exchangeRate.Text = controller.temp.Currency.exchangeRate.Format();
            amountMainCurrency.Text = (amount.Text .ToDecimal()*exchangeRate.Text.ToDecimal()).Format();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(amount.Text, exchangeRate.Text, description.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(amount.Text, exchangeRate.Text, description.Text))
                    return;
            if (controller.prosessesType == ProsessesType.add)
            {
                DialogAddAndUpdateReceipt_Load(null, null);
                clearFieldAndReferesh();
                date.Value=controller.lastDate.Value;
            }
            else
            {this.Close();
               
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void clearFieldAndReferesh()
        { if(controller.prosessesType==ProsessesType.add)
            controller.clearTempData();
        else
                controller.clearTempDataUpdate();
            description.Clear();
            exchangeRate.Clear();
            amount.Clear();
            amountMainCurrency.Clear();
            balanceAccount.Text=String.Empty;
            balanceCashier.Text=String.Empty;
            reSetCombobox();
            displayOrHideExchangeRate();

        }

        private void DialogAddAndUpdateReceipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
            if (account.SelectedItem!=null)
            {
                account_SelectionChangeCommitted(null, null);
            }
            if (cashier.SelectedItem!=null)
            {
               cashier_SelectionChangeCommitted(null,null);
            }
            displayOrHideExchangeRate();

        }

        private void account_SelectionChangeCommitted(object sender, EventArgs e)
        {
                //if (currency.SelectedItem != null&& account.SelectedItem!=null)
                balanceAccount.Text = controller.selectedAccount(account.SelectedItem);
        }

        private void cashier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
                //if (currency.SelectedItem != null&& cashier.SelectedItem!=null)
                balanceCashier.Text = controller.selectedCashier(cashier.SelectedItem);
          

        }

        private void CalAmountMainCurrency(object sender, EventArgs e)
        {
            amountMainCurrency.Text =( amount.Text.ToDecimal()* exchangeRate.Text.ToDecimal()).Format();
        }
        private void date_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedDate(date.Value);
        }
    }
}
