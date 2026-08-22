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

namespace AccountingSystem.view.SupScreens.CityManagement
{
    public partial class CityManagementScreen : Form
    {
   
        TablePagination tablePagination;
        CityController controller;
        public CityManagementScreen()
        {
            InitializeComponent();
            controller = new CityController();
         
            tablePagination = new TablePagination();
            cityName.TextOnly();
            rowCount.NumberOnly();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
            
        }
        private void CityManagementScreen_Load(object sender, EventArgs e)
        {
            if (model.LoginData.permissions["city"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
     
        private void btnShowDialogAddCity_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
        }

        private void btnShowDialogUpdateCity_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }

        private void btnShowDialogViewCity_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            controller.delete(tablePagination.getKeysSelectedRows());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
         
            tablePagination.changePageSize(rowCount.Text);
            controller.search(cityName.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            cityName.Clear();
            rowCount.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(cityName.Text);
        }

        
    }
}
