using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using Krypton.Toolkit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Markup;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.SupScreens.CompoundJournalEntries
{
    public partial class DialogAddAndUpdateCompoundJournalEntries : Form
    {
     
        CompoundJournalEntriesController controller;
        CompoundJournalEntriesWidget widget;
    
        public DialogAddAndUpdateCompoundJournalEntries(CompoundJournalEntriesController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
            widget = new CompoundJournalEntriesWidget(controller.supAccounts,toolTip1,controller.transactionType);
             Guna2Panel tablePanels = widget.returnNewTablePanels();
            guna2CustomGradientPanel1.Controls.Add(widget.returnNewTablePanels());
            tablePanels.BringToFront();
            creditTotal.DataBindings.Add("Text", widget.creditTotal, "MyProperty", true, DataSourceUpdateMode.OnPropertyChanged);
            debitTotal.DataBindings.Add("Text", widget.debitTotal, "MyProperty", true, DataSourceUpdateMode.OnPropertyChanged);
            difference.DataBindings.Add("Text", widget.difference, "MyProperty", true, DataSourceUpdateMode.OnPropertyChanged);
        

        }

        private void DialogAddAndUpdateCompoundJournalEntries_Load(object sender, EventArgs e)
        {
           
           currency.DataSource = controller.allCurrency;
            currency.TextOnly();
            exchangeRate.NumberOnly();
            if (controller.prosessesType == ProsessesType.add)
            {
                controller.temp.Currency = controller.allCurrency.FirstOrDefault(c => c.currencyType == "رئيسية");
               
                exchangeRate.Text = controller.temp.Currency.exchangeRate.ToString();
                widget.newRow(); reSetCombobox();
            }
           
            if (controller.prosessesType == ProsessesType.update)
                           fillField();

        }

        private void fillField()
        {
             widget.fillTablePanels(controller.temJournalEntries);
             currency.SelectedItem=controller.temp.Currency;
             date.Value=controller.temp.date.Value;
             exchangeRate.Text=controller.temp.Currency.exchangeRate.ToString();
            price_TextChanged(null,null);
            displayOrHideExchangeRate();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(creditTotal.Text, debitTotal.Text,exchangeRate.Text,widget.getJournalEntrys()))
                    return;
             if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(creditTotal.Text, debitTotal.Text, exchangeRate.Text, widget.getJournalEntrys())) 
                    return;
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
           
            widget.fillTablePanels(controller.temJournalEntries);
        }
        void reSetCombobox()
        {
            currency.SelectedItem = controller.temp.Currency;
            date.Value = DateTime.Now;
            //    currency.d = controller.temp.Currency;
        }
        private void clearFieldAndReferesh()
        {
            controller.clearTempData();
            reSetCombobox();


        }
        void displayOrHideExchangeRate()
        {
            if (controller.temp.Currency.currencyType != "رئيسية")
            {
                    panelSeconderyCurrency.Visible=true;   
            }
            else
            {
                    panelSeconderyCurrency.Visible=false;
            }
            exchangeRate.Text = controller.temp.Currency.exchangeRate.ToString();
            price_TextChanged(null,null);
        }
        private void price_TextChanged(object sender, EventArgs e)
        {
            debitSecnderyCurrency.Text = (Convert.ToDecimal(String.IsNullOrEmpty(debitTotal.Text) ? "0" : debitTotal.Text.Replace(".0000","")) * Convert.ToDecimal(String.IsNullOrEmpty(exchangeRate.Text) ? "0" : exchangeRate.Text.Replace(".0000", ""))).ToString();
            cridetSecnderyCurrency.Text = (Convert.ToDecimal(String.IsNullOrEmpty(creditTotal.Text) ? "0" : creditTotal.Text.Replace(".0000", "")) * Convert.ToDecimal(String.IsNullOrEmpty(exchangeRate.Text) ? "0" : exchangeRate.Text.Replace(".0000", ""))).ToString();
            differenceSecnderyCurrency.Text = (Convert.ToDecimal(cridetSecnderyCurrency.Text) - Convert.ToDecimal(debitSecnderyCurrency.Text)).ToString().Replace(".0000", "");
        }
        private void DialogAddAndUpdateCompoundJournalEntries_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
            displayOrHideExchangeRate();
        }
        private void difference_TextChanged(object sender, EventArgs e)
        {
            price_TextChanged(null, null);
        }

        private void exchangeRate_TextChanged(object sender, EventArgs e)
        {
            price_TextChanged(null, null);
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedDate(date.Value);
        }
    }
}
