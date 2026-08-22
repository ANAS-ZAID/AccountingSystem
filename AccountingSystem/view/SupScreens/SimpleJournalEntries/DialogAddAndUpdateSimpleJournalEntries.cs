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
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.SimpleJournalEntries
{
    public partial class DialogAddAndUpdateSimpleJournalEntries : Form
    {
        SimpleJournalEntriesController controller;
        public DialogAddAndUpdateSimpleJournalEntries(SimpleJournalEntriesController controller)
        {
            InitializeComponent();
            this.controller = controller;
          

        }
        private void DialogAddAndUpdateSimpleJournalEntries_Load(object sender, EventArgs e)
        {
            debitAccount.DataSource = controller.supAccounts;
            creditAccount.DataSource = controller.supAccounts;
            currency.DataSource = controller.allCurrency;
            debitAccount.TextOnly();
            amount.PriceOnly();
            exchangeRate.PriceOnly();
            amountMainCurrency.PriceOnly();
            currency.TextOnly();
            creditAccount.TextOnly();
            if(controller.prosessesType==ProsessesType.add)
            {
                controller.temp.Currency = controller.allCurrency.FirstOrDefault(c => c.currencyType == "رئيسية");
                exchangeRate.Text = controller.temp.Currency.exchangeRate.ToString();
                date.Value = DateTime.Now;
            }
           
            reSetCombobox();
            if (controller.prosessesType == ProsessesType.update)
                fillField();

           
        }
        void reSetCombobox()
        {
            debitAccount.SelectedItem = controller.temp.AccountDebit;
            creditAccount.SelectedItem = controller.temp.AccountCredit;
            currency.SelectedItem = controller.temp.Currency;

        }

        void fillField()
        {
            reSetCombobox();
            amount.Text = controller.temp.amount?.ToString();
            description.Text = controller.temp.description;
            date.Value = controller.temp.date.Value;
          
            exchangeRate.Text = controller.journalEntrycCreditAccount.ExchangeRate.ToString();
            displayOrHideExchangeRate();
            creditAccount_SelectionChangeCommitted(null, null);
            debitAccount_SelectionChangeCommitted(null, null);
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
                //  exchangeRate.Text=controller.temp.Currency.exchangeRate.ToString();

            }
            exchangeRate.Text = controller.temp.Currency.exchangeRate.ToString();
            amountMainCurrency.Text = (Convert.ToDecimal(String.IsNullOrEmpty(amount.Text) ? "0" : amount.Text) * Convert.ToDecimal(String.IsNullOrEmpty(exchangeRate.Text) ? "0" : exchangeRate.Text)).ToString();
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
            this.Close();
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
        {
            controller.clearTempData();
            description.Clear();
            exchangeRate.Clear();
            amountMainCurrency.Clear();
            date.Value = DateTime.Now;
            reSetCombobox();


        }

        private void DialogAddAndUpdateSimpleJournalEntries_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }

        private void debitAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {decimal balance = controller.selectedDebitAccount(debitAccount.SelectedItem);
            if (currency.SelectedItem != null)
            {
                
                balanceAccount.Text = (balance >= 0 ? "دائن : " : "مدين : ") + (balance < 0 ? balance * -1 : balance);
            }
        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
            if (debitAccount.SelectedItem != null)
            {
                debitAccount_SelectionChangeCommitted(null, null);
            }
            if (creditAccount.SelectedItem != null)
            {
                creditAccount_SelectionChangeCommitted(null, null);
            }
            displayOrHideExchangeRate();
        }

        private void creditAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            decimal balance = controller.selectedCreditAccount(creditAccount.SelectedItem);
            if (currency.SelectedItem != null)
            {
                
                balanceCashier.Text = (balance >= 0 ? "دائن : " : "مدين : ") + (balance < 0 ? balance * -1 : balance);
            }
        }
        private void exchangeRate_TextChanged(object sender, EventArgs e)
        {
            amountMainCurrency.Text = (Convert.ToDecimal(String.IsNullOrEmpty(amount.Text) ? "0" : amount.Text) * Convert.ToDecimal(String.IsNullOrEmpty(exchangeRate.Text) ? "0" : exchangeRate.Text)).ToString();
        }

        private void amount_TextChanged(object sender, EventArgs e)
        {
            amountMainCurrency.Text = (Convert.ToDecimal(String.IsNullOrEmpty(amount.Text) ? "0" : amount.Text) * Convert.ToDecimal(String.IsNullOrEmpty(exchangeRate.Text) ? "0" : exchangeRate.Text)).ToString();
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedDate(date.Value);
        }
    }
}
