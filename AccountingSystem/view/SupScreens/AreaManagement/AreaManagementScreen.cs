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
using AccountingSystem.Model;
using AccountingSystem.view.SupScreens.CityManagement;

namespace AccountingSystem.view.SupScreens.AreaManagement
{
    public partial class AreaManagementScreen : Form
    {
        FunctionsGUI functionsGUI;
        TablePagination tablePagination;
        AreaController controller;
        public AreaManagementScreen()
        {
            InitializeComponent();
            controller = new  AreaController();
            functionsGUI = new FunctionsGUI();
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
       
        }
        private void AreaManagementScreen_Load(object sender, EventArgs e)
        {
            if (model.LoginData.permissions["area"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
                comboBoxCity.DataSource = controller.allCity;
                areaName.TextOnly();
                comboBoxCity.TextOnly();
                rowCount.NumberOnly();
                setComboBox();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        void setComboBox()
        {
            comboBoxCity.SelectedItem = controller.temp.City;
        }
        private void btnShowDialogAddArea_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
        }

        private void btnShowDialogUpdateArea_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow()); 
        }

        private void btnShowDialogViewArea_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                controller.delete(tablePagination.getKeysSelectedRows());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
             setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(areaName.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            controller.clearTempData();
            rowCount.Clear();
            areaName.Clear();
            setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(areaName.Text);
        }

        private void comboBoxCity_SelectionChangeCommitted(object sender, EventArgs e)
        {      
             controller.selectedCity(comboBoxCity.SelectedItem);
        }

    
    }
}
