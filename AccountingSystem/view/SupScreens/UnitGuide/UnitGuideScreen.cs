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
using AccountingSystem.view.SupScreens.ClassifyGroup;

namespace AccountingSystem.view.SupScreens.UnitGuide
{
    public partial class UnitGuideScreen : Form
    {   UnitGuideController controller;
       
        TablePagination tablePagination;
        public UnitGuideScreen()
        {
            InitializeComponent();
          
            tablePagination = new TablePagination();
            controller = new UnitGuideController();
            name.TextOnly();
            rowCount.NumberOnly();
      
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
       private void UnitGuideScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["uintGuide"].viewPermission.Value)
            {
                tablePagination.fillData(controller.unitsSource);
            }
        }

        private void btnShowDialogAddUnitGuide_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["uintGuide"].addPermission.Value)
            {
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateUnitGuide dialog = new DialogAddAndUpdateUnitGuide(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateUnitGuide_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["uintGuide"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateUnitGuide dialog = new DialogAddAndUpdateUnitGuide(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewUnitGuide_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["uintGuide"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["uintGuide"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            name.Clear();
            rowCount.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text);
        }

       
    }
}
