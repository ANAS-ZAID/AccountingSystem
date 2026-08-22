using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.model;
using AccountingSystem.view.SupScreens.AccountingGuide;
using AccountingSystem.view.SupScreens.EmployeeManagement;

namespace AccountingSystem.view.SupScreens.Receipt
{
    public partial class ReceiptScreen : Form
    {
       
        VoucherController controller;
        TablePagination tablePagination;

        public ReceiptScreen(TransactionType financialType)
        {
            InitializeComponent();
            this.Text = financialType.ToString().Replace("_", " ");
            controller = new VoucherController(financialType);
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());

        }
        private void ReceiptScreen_Load(object sender, EventArgs e)
        { 
            if ((LoginData.permissions["catch"].viewPermission.Value &&controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].viewPermission.Value && controller.transactionType == TransactionType.سند_صرف))
            {
                tablePagination.fillData(controller.dataSource);
                accountNumber.DataSource = controller.supAccounts;
                cashier.DataSource = controller.allCashiers;
                currency.DataSource = controller.allCurrency;
                accountNumber.TextOnly();
                cashier.TextOnly();
                currency.TextOnly();
                rowCount.NumberOnly();
                resetCombobox();
            }
        }
        

        private void btnShowDialogAddReceipt_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["catch"].addPermission.Value && controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].addPermission.Value && controller.transactionType == TransactionType.سند_صرف))

            {
                controller.clearTempData();
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateReceipt dialog = new DialogAddAndUpdateReceipt(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateReceipt_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["catch"].updatePermission.Value && controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].updatePermission.Value && controller.transactionType == TransactionType.سند_صرف))

            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateReceipt dialog = new DialogAddAndUpdateReceipt(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewReceipt_Click(object sender, EventArgs e)
        {
           if ((LoginData.permissions["catch"].viewPermission.Value && controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].viewPermission.Value && controller.transactionType == TransactionType.سند_صرف))
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["catch"].deletePermission.Value && controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].deletePermission.Value && controller.transactionType == TransactionType.سند_صرف))
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["catch"].viewPermission.Value && controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].viewPermission.Value && controller.transactionType == TransactionType.سند_صرف))
            {
                tablePagination.changePageSize(rowCount.Text);
                
             controller.search(description.Text);
               

            }
        }
        void resetCombobox()
        {
            controller.clearTempData();
            rowCount.Clear();
            description.Clear();
            accountNumber.SelectedItem = controller.temp.Account;
            cashier.SelectedItem = controller.temp.Cashier;
            currency.SelectedItem = controller.temp.Currency;
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now;
            controller.selectedStartDate(null);
           controller.selectedEndDate(null);

        }
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            if ((LoginData.permissions["catch"].viewPermission.Value && controller.transactionType == TransactionType.سند_قبض) || (LoginData.permissions["expanse"].viewPermission.Value && controller.transactionType == TransactionType.سند_صرف))
            {
                resetCombobox();
                tablePagination.changePageSize(rowCount.Text);
                controller.search(description.Text);
            }
        }

        private void accountNumber_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedAccount(accountNumber.SelectedItem);
        }

        private void cashier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCashier(cashier.SelectedItem);
        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            controller.print(tablePagination.getKeyCurrentSelectedRow());
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
