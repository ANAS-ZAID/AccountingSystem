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
using AccountingSystem.view.SupScreens.AccountingGuide;


namespace AccountingSystem.view.Screens.CustmoreMangement
{
    public partial class CustmoreMangementScreen : Form
    {
       CustmoreController controller;
        TablePagination tablePagination;
      
        public CustmoreMangementScreen()
        {
            InitializeComponent();
          
            tablePagination = new TablePagination();
            controller = new CustmoreController();

           
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
        private void CustmoreMangementScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["custmore"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
                area.DataSource = controller.allAreas;
                city.DataSource = controller.allCity;
                accountGroup.DataSource = controller.allAccountGroups;
                phoneNumber.NumberOnly();
                name.TextOnly();
                city.TextOnly();
                area.TextOnly();
                accountGroup.TextOnly();
                rowCount.NumberOnly();
                setCombBox();
            }
        }
        

        private void btnShowDialogAddCustmore_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["custmore"].addPermission.Value)
            {
                controller.clearTempData();
                controller.prosessesType = ProsessesType.add;
                DialogAddCustmore dialog = new DialogAddCustmore(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateCustmore_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["custmore"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddCustmore dialog = new DialogAddCustmore(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewCustmore_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["custmore"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["custmore"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
                tablePagination.changePageSize(rowCount.Text);
                controller.search(name.Text, phoneNumber.Text);
  
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            controller.clearTempData();
            rowCount.Clear();
            name.Clear();
            phoneNumber.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text, phoneNumber.Text);
            setCombBox();
        }

        private void setCombBox()
        {
            city.SelectedItem = controller.temp.City;
            area.SelectedItem = controller.temp.Area;
            accountGroup.SelectedItem = controller.temp.City;
        }

        private void accountGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedAccountsGroup(accountGroup.SelectedItem);
        }

        private void city_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCity(city.SelectedItem);

        }

        private void area_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedArea(area.SelectedItem);

        }
    }
}
