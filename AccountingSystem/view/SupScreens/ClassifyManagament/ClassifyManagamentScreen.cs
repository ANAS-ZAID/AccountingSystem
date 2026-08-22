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
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class ClassifyManagamentScreen : Form
    {
        ItemController controller;
        TablePagination tablePagination;
        bool isTreeViewScereen;
        public ClassifyManagamentScreen(bool isTreeViewScereen)
        {
            InitializeComponent();
            controller = new ItemController();
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
            this.isTreeViewScereen = isTreeViewScereen;
        }
        void ShowTreeViewItems()
        {
            (new TreeViewItems()).ShowDialog();
        }
        private void ClassifyManagamentScreen_Load(object sender, EventArgs e)
        {
            if (model.LoginData.permissions["item"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
                if (isTreeViewScereen)
                    ShowTreeViewItems();
                perant.DataSource = controller.mainItms;
                group.DataSource = controller.groups;
                nameAr.TextOnly();
                nameEn.TextOnly();
                perant.TextOnly("nameAr");
                group.TextOnly();
                type.TextOnly();
                itemNumber.NumberOnly();
                rowCount.NumberOnly();
                barcode.NumberOnly();
                setComboBox();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        private void btnShowDialogAddClassify_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
            perant.DataSource = controller.mainItms;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            controller.delete(tablePagination.getKeysSelectedRows());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(nameAr.Text, nameEn.Text, itemNumber.Text, barcode.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            controller.clearTempData();
            rowCount.Clear();
            nameAr.Clear();
            nameEn.Clear();
            itemNumber.Clear();
            barcode.Clear();
            setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(nameAr.Text,nameEn.Text,itemNumber.Text, barcode.Text);
        }

       
        void setComboBox()
        {
            perant.SelectedItem = controller.temp.perantItem;
            group.SelectedItem = controller.temp.ClassifyGroup;
            type.Text=controller.temp.type;
        }

        private void btnShowDialogUpdateSalse_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }

        private void btnShowDialogViewSalse_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void perant_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedPerantItem(perant.SelectedItem); 
        }

        private void group_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedClassifyGroup(group.SelectedItem);
        }

        private void type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.type=(string)type.SelectedItem;
        }
    }
}
