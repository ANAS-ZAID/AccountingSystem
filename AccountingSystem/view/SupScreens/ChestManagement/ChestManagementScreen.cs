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
using AccountingSystem.view.SupScreens.AccountingGuide;

namespace AccountingSystem.view.SupScreens.ChestManagement
{
    public partial class ChestManagementScreen : Form
    {  CashierController controller;

        TablePagination tablePagination;
        public ChestManagementScreen()
        {
            InitializeComponent();
       
            tablePagination = new TablePagination();
            controller = new CashierController();
            name.TextOnly();
            accountNumber.NumberOnly();
            rowCount.NumberOnly();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
        private void ChestManagementScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["cashier"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
               
            }
        }
        private void btnShowDialogAddChest_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["cashier"].addPermission.Value)
            {
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateChest dialog = new DialogAddAndUpdateChest(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateChest_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["cashier"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateChest dialog = new DialogAddAndUpdateChest(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewChest_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["cashier"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["cashier"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            
                tablePagination.changePageSize(rowCount.Text);
                controller.search(name.Text,accountNumber.Text);
 
        }

       

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            name.Clear();
            accountNumber.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text,accountNumber.Text);
        }

        
    }
}
