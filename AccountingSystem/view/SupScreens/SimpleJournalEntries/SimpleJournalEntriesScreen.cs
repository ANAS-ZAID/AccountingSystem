using Guna.UI2.WinForms.Suite;
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
using AccountingSystem.model;
using AccountingSystem.view.SupScreens.Receipt;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace AccountingSystem.view.SupScreens.SimpleJournalEntries
{
    public partial class SimpleJournalEntriesScreen : Form
    {
        SimpleJournalEntriesController controller;
        TablePagination tablePagination;
      
        public SimpleJournalEntriesScreen()
        {
            InitializeComponent();
            controller = new SimpleJournalEntriesController();
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
        private void SimpleJournalEntriesScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["simpleJournalEntries"].viewPermission.Value )
            {
                tablePagination.fillData(controller.dataSource);
                debitAccount.DataSource = controller.supAccounts;
                creditAccount.DataSource = controller.supAccounts;
                currency.DataSource = controller.allCurrency;
                debitAccount.TextOnly();
                currency.TextOnly();
                creditAccount.TextOnly();
                rowCount.NumberOnly();
                resetCombobox();
            }
        }
     

        private void btnShowDialogAddSimpleJournalEntrie_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["simpleJournalEntries"].addPermission.Value)

            {
                controller.clearTempData();
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateSimpleJournalEntries dialog = new DialogAddAndUpdateSimpleJournalEntries(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateSimpleJournalEntrie_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["simpleJournalEntries"].updatePermission.Value)

            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateSimpleJournalEntries dialog = new DialogAddAndUpdateSimpleJournalEntries(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewSimpleJournalEntrie_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["simpleJournalEntries"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["simpleJournalEntries"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
                tablePagination.changePageSize(rowCount.Text);
                controller.search(discription.Text);
            
        }
        void resetCombobox()
        {
            controller.clearTempData();
            rowCount.Clear();
            discription.Clear();
            debitAccount.SelectedItem = controller.temp.AccountDebit;
            creditAccount.SelectedItem = controller.temp.AccountCredit;
            currency.SelectedItem = controller.temp.Currency;
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now;
            controller.selectedStartDate(null);
            controller.selectedEndtDate(null);

        }
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            resetCombobox();
                tablePagination.changePageSize(rowCount.Text);
                controller.search(discription.Text);
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedStartDate(startDate.Value);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedEndtDate(endDate.Value);
        }

        private void debitAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedDebitAccount(debitAccount.SelectedItem);
        }

        private void creditAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCreditAccount(creditAccount.SelectedItem);   
        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
        }
    }
}
