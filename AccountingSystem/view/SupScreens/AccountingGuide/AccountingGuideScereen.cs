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
using AccountingSystem.view.SupScreens.AccountGruop;
using AccountingSystem.view.SupScreens.AccountingGuide;

namespace AccountingSystem.view.Screens.AccountingGuide
{
    public partial class AccountingGuideScereen : Form
    {
        AccountingGuideController controller;
        FunctionsGUI functionsGUI;
        TablePagination tablePagination;
        bool isTreeViewScereen;
        public AccountingGuideScereen(bool isTreeViewScereen)
        {
            InitializeComponent();

            functionsGUI = new FunctionsGUI();
            tablePagination = new TablePagination();
            controller = new AccountingGuideController();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
            this.isTreeViewScereen = isTreeViewScereen;
        }



        private void btnShowDialogAddAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountingGuide"].addPermission.Value)
            {
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateAccounting dialog = new DialogAddAndUpdateAccounting(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountingGuide"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateAccounting dialog = new DialogAddAndUpdateAccounting(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountingGuide"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        void ShowTreeViewAccounts()
        {
            (new TreeViewAccounts()).ShowDialog();
        }
        private void AccountingGuideScereen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountingGuide"].viewPermission.Value)
            {
                
                tablePagination.fillData(controller.chartOfAccountSource);
                if (isTreeViewScereen)
                    ShowTreeViewAccounts();
                combParentAccount.DataSource = controller.mainAccounts;
                combGroupAccount.DataSource = controller.accountsGroups;
                nameAccount.TextOnly();
                numberAccount.NumberOnly();
                combParentAccount.TextOnly();
                combGroupAccount.TextOnly();
                rowCount.NumberOnly();
                setComboBox();
            }
        }
        void setComboBox()
        {
            combParentAccount.SelectedItem = controller.tempChartOfAccount.perantAccount;
            combGroupAccount.SelectedItem = controller.tempChartOfAccount.AccountsGroup;

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountingGuide"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountingGuide"].viewPermission.Value)
            {
                if (!String.IsNullOrEmpty(numberAccount.Text))
                    controller.tempChartOfAccount.accountNumber = int.Parse(numberAccount.Text);
                else controller.tempChartOfAccount.accountNumber = 0;

                controller.tempChartOfAccount.name = nameAccount.Text;
                tablePagination.changePageSize(rowCount.Text);
                controller.search();
            }

        }

        private void combParentAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedParent(combParentAccount.SelectedItem);
        }

        private void combGroupAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedGroup(combGroupAccount.SelectedItem);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            controller.clearTempData();
            rowCount.Clear();
            nameAccount.Clear();
            numberAccount.Clear();
            setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.search();
        }

    }
}
