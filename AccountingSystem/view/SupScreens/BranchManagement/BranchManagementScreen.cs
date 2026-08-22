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
using AccountingSystem.view.Screens.CurrencyManagement;

namespace AccountingSystem.view.Screens.BranchManagement
{
    public partial class BranchManagementScreen : Form
    {
       
        TablePagination tablePagination;
        BranchController controller;
        public BranchManagementScreen()
        {
            InitializeComponent();
            controller =new  BranchController();
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
            brancheName.TextOnly();
            rowCount.NumberOnly();
        }
        private void BranchManagementScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["branch"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
            }
        }
        private void btnShowDialogAddBranch_Click(object sender, EventArgs e)
        {
         if (LoginData.permissions["branch"].addPermission.Value)
            { controller.prosessesType=ProsessesType.add;
                DialogAddBramch dialogAddBramch = new DialogAddBramch(controller);
                dialogAddBramch.ShowDialog();
            }
          else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateBranch_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["branch"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType=ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddBramch dialogAddBramch = new DialogAddBramch(controller);
                    dialogAddBramch.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");

            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewBranch_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["branch"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDeleteBranch_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["branch"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.search(brancheName.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {  rowCount.Clear();
           brancheName.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(brancheName.Text);
        }

       
    }
}
