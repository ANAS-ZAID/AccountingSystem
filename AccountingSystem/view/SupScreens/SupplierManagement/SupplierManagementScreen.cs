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
using AccountingSystem.Model;


namespace AccountingSystem.view.SupScreens.SupplierManagement
{
    public partial class SupplierManagementScreen : Form
    {
        SupplierController controller;
        TablePagination tablePagination;

        public SupplierManagementScreen()
        {
            InitializeComponent();
          
            tablePagination = new TablePagination();
            controller = new SupplierController();

            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
        private void SupplierManagementScreen_Load(object sender, EventArgs e)
        {
            phoneNumber.PhoneOnly();
            name.TextOnly();
            accountNumber.NumberOnly();
            rowCount.NumberOnly();
            if (LoginData.permissions["supplier"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
            }
        }
        
        private void btnShowDialogAddAndUpdateSupplier_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["supplier"].addPermission.Value)
            {
                controller.clearTempData();
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateSupplier dialog = new DialogAddAndUpdateSupplier(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateSupplier_Click(object sender, EventArgs e)
        {

            if (LoginData.permissions["supplier"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateSupplier dialog = new DialogAddAndUpdateSupplier(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewSupplier_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["supplier"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["supplier"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
                tablePagination.changePageSize(rowCount.Text);
                controller.search(name.Text, phoneNumber.Text, accountNumber.Text);
           
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            name.Clear();
            phoneNumber.Clear();
            accountNumber.Clear();
            controller.clearTempData();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text, phoneNumber.Text,accountNumber.Text);
        }

       
    }
}
