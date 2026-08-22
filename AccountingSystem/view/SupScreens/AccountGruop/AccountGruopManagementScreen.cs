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
using AccountingSystem.view.Screens.BranchManagement;
using AccountingSystem.view.SupScreens.AreaManagement;

namespace AccountingSystem.view.SupScreens.AccountGruop
{
    public partial class AccountGruopManagementScreen : Form
    {
       
        AccountGruopController controller;
        FunctionsGUI functionsGUI;
        TablePagination tablePagination;
        public AccountGruopManagementScreen()
        {
            InitializeComponent();
            functionsGUI = new FunctionsGUI();
            tablePagination = new TablePagination();
            controller = new AccountGruopController();
            groupName.TextOnly();
            rowCount.NumberOnly();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
            
           
        }
        private void AccountGruopManagementScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountGruop"].viewPermission.Value)
            {
                tablePagination.fillData(controller.accountGruopSource);
            }
        }
        private void btnShowDialogAddGroupAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountGruop"].addPermission.Value)
            {
                    controller.prosessesType=ProsessesType.add;
                    DialogAddAndUpdateAccountGruop dialog = new DialogAddAndUpdateAccountGruop(controller);
                    dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateGroupAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountGruop"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateAccountGruop dialog = new DialogAddAndUpdateAccountGruop(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewGroupAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountGruop"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountGruop"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {       
            controller.search(groupName.Text, rowCount.Text);
        }

        private void btnClaerSearch_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["accountGruop"].viewPermission.Value)
            {
                groupName.Clear();
                rowCount.Clear();
                controller.search(groupName.Text, rowCount.Text);
            }
        }
    }
}
