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
using AccountingSystem.view.SupScreens.ChestManagement;
using AccountingSystem.view.SupScreens.UnitGuide;

namespace AccountingSystem.view.SupScreens.WarehouseManagement
{
    public partial class WarehouseManagementScreen : Form
    {
      StoreController controller;
       
        TablePagination tablePagination;
        public WarehouseManagementScreen()
        {
            InitializeComponent();
           
            tablePagination = new TablePagination();
            controller = new StoreController();
            name.TextOnly();
            accountNumber. NumberOnly();
            rowCount.NumberOnly();
            
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
        private void WarehouseManagementScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["store"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
            }
        }
        private void btnShowDialogAddWarehouse_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["store"].addPermission.Value)
            {
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateWarehouse dialog = new DialogAddAndUpdateWarehouse(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateWarehouse_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["store"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateWarehouse dialog = new DialogAddAndUpdateWarehouse(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewWarehouse_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["store"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["store"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

                tablePagination.changePageSize(rowCount.Text);
                controller.search(name.Text, accountNumber.Text);
            
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            name.Clear();
            accountNumber.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text, accountNumber.Text);
        }

       
    }
}
