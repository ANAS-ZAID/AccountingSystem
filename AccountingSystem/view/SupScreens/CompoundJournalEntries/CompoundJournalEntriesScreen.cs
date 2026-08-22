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
using AccountingSystem.model;


namespace AccountingSystem.view.SupScreens.CompoundJournalEntries
{
    public partial class CompoundJournalEntriesScreen : Form
    {
        CompoundJournalEntriesController controller;
        TablePagination tablePagination;
    
        public CompoundJournalEntriesScreen(TransactionType transactionType)
        {
            InitializeComponent();
            controller = new CompoundJournalEntriesController(transactionType);
            controller.transactionType = transactionType;
            this.Name=transactionType.ToString().Replace("_"," ");
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void btnShowDialogAddCompoundJournalEntrie_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["compoundJournalEntries"].addPermission.Value&&controller.transactionType==TransactionType.قيد_مركب) ||((LoginData.permissions["openingBalances"].addPermission.Value && controller.transactionType == TransactionType.رصيد_إفتتاحي)))

            {
                controller.clearTempData();
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateCompoundJournalEntries dialog = new DialogAddAndUpdateCompoundJournalEntries(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateCompoundJournalEntrie_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["compoundJournalEntries"].updatePermission.Value && controller.transactionType == TransactionType.قيد_مركب) || ((LoginData.permissions["openingBalances"].updatePermission.Value && controller.transactionType == TransactionType.رصيد_إفتتاحي)))
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateCompoundJournalEntries dialog = new DialogAddAndUpdateCompoundJournalEntries(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewCompoundJournalEntrie_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["compoundJournalEntries"].viewPermission.Value && controller.transactionType == TransactionType.قيد_مركب) || ((LoginData.permissions["openingBalances"].viewPermission.Value && controller.transactionType == TransactionType.رصيد_إفتتاحي)))
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["compoundJournalEntries"].deletePermission.Value && controller.transactionType == TransactionType.قيد_مركب) || ((LoginData.permissions["openingBalances"].deletePermission.Value && controller.transactionType == TransactionType.رصيد_إفتتاحي)))
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void CompoundJournalEntriesScreen_Load(object sender, EventArgs e)
        {
            if ((LoginData.permissions["compoundJournalEntries"].viewPermission.Value && controller.transactionType == TransactionType.قيد_مركب) || ((LoginData.permissions["openingBalances"].viewPermission.Value && controller.transactionType == TransactionType.رصيد_إفتتاحي)))
            {
                tablePagination.fillData(controller.dataSource);
                currncy.DataSource = controller.allCurrency;
                currncy.TextOnly();
                rowCount.NumberOnly();
                resetCombobox();
            }
        }
        void resetCombobox()
        {
            controller.clearTempData();
            rowCount.Clear();
            currncy.SelectedItem = controller.temp.Currency;
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
           
                tablePagination.changePageSize(rowCount.Text);
                currncy.SelectedItem = controller.temp.Currency;
                controller.search();

        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
                resetCombobox();
                tablePagination.changePageSize(rowCount.Text);
                controller.search();

        }

        private void currncy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currncy.SelectedItem);  
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedStartDate(startDate.Value);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedEndDate(endDate.Value);
        }
    }
}
