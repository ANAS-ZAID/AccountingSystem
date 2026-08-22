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
using AccountingSystem.view.SupScreens.ClassifyManagament;
using AccountingSystem.view.SupScreens.SupplierManagement;

namespace AccountingSystem.view.SupScreens.ClassifyGroup
{
    public partial class ClassifyGroupScreen : Form
    {
        ClassifyGroupController controller;
        TablePagination tablePagination;
       
        public ClassifyGroupScreen()
        {
            InitializeComponent();

            tablePagination = new TablePagination();
            controller = new ClassifyGroupController();
            groupName.TextOnly();
            rowCount.NumberOnly();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
         
        }
       
        private void ClassifyGroupScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["classifyGruop"].viewPermission.Value)
            {
          
                tablePagination.fillData(controller.gruopsSource);
            }
        }
        

        private void btnShowDialogAddClassify_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["classifyGruop"].addPermission.Value)
            {
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateClassifyGroup dialog = new DialogAddAndUpdateClassifyGroup(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateClassify_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["classifyGruop"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateClassifyGroup dialog = new DialogAddAndUpdateClassifyGroup(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewClassify_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["classifyGruop"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["classifyGruop"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            controller.search(groupName.Text, rowCount.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            groupName.Clear();
            rowCount.Clear();
            controller.search(groupName.Text, rowCount.Text);
        }

        
    }
}
