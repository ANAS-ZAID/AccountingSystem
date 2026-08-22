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
using AccountingSystem.core.shared;

namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class FirstPeriodStock : Form
    {
        TablePagination tablePagination;
        FirstPeriodStockController controller;
        public FirstPeriodStock()
        {
            InitializeComponent();
            tablePagination = new TablePagination();
            controller = new FirstPeriodStockController();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void FirstPeriodStock_Load(object sender, EventArgs e)
        {
            if (model.LoginData.permissions["currency"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
                store.DataSource = controller.stores;
                item.DataSource = controller.items;
                rowCount.NumberOnly();
                store.TextOnly();
                item.TextOnly("nameAr");
                setComboBox();
                controller.HasHomeScreenDataProcessed = true;
            }
        }

        private void setComboBox()
        {
           store.SelectedItem=null;
           item.SelectedItem=null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
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

        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectStore(store.SelectedItem);
        }

        private void item_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectItem(item.SelectedItem);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData(true);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData();
        }
    }
}
