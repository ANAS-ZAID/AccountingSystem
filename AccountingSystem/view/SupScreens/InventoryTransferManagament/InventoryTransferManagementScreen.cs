using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using AccountingSystem.controller.Screen;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.SupScreens.InventoryTransferManagament
{
    public partial class InventoryTransferManagementScreen : Form
    {
        TablePagination tablePagination;
        InventoryTransferController controller;


        public InventoryTransferManagementScreen()
        {
            InitializeComponent();
            tablePagination = new TablePagination();
            controller=new InventoryTransferController();
            controller.startHSDP();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void InventoryTransferManagementScreen_Load(object sender, EventArgs e)
        {
            if (controller.permissions.viewPermission ?? false)
            {
               
                tablePagination.fillData(controller.dataSource);
               formStore.DataSource = controller.storeList;
               toStore.DataSource = controller.storeList;
                formStore.TextOnly();
                toStore.TextOnly();
                rowCount.NumberOnly();
                setComboBox();

            }
            controller.endHSDP();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData(true,number.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            controller.delete(tablePagination.getKeysSelectedRows());
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectStartDate(startDate.Value);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectEndtDate(endDate.Value);
        }

        private void formStore_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectFromStore(formStore.SelectedItem);
        }

        private void toStore_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectToStore(toStore.SelectedItem);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            number.Clear();
            tablePagination.changePageSize(rowCount.Text);
            setComboBox();
            controller.lodeData();
        }

        private void setComboBox()
        {
            controller.clearHomeTempData();
            formStore.SelectedItem = controller.fromStore;
            toStore.SelectedItem = controller.toStore;
            startDate.Value=DateTime.Now;   
            endDate.Value=DateTime.Now;

        }
    }
}
