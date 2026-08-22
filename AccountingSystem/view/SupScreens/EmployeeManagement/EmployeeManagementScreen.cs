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
using AccountingSystem.view.SupScreens.EmployeeManagement;

namespace AccountingSystem.view.SupScreens.EmployeesManagement
{
    public partial class EmployeeManagementScreen : Form
    {
        EmployeeController controller;
     
        TablePagination tablePagination;
        public EmployeeManagementScreen()
        {
            InitializeComponent();
            tablePagination = new TablePagination();
            controller = new EmployeeController();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void EmployeeManagementScreen_Load(object sender, EventArgs e)
        {
            if (LoginData.permissions["employee"].viewPermission.Value)
            {
                tablePagination.fillData(controller.dataSource);
                comboBranch.DataSource = controller.allBranches;
                comboType.DataSource = controller.allEmployeeType;
                accountNumber.NumberOnly();
                phoneNumber.PhoneOnly();
                name.TextOnly();
                comboType.TextOnly();
                comboBranch.TextOnly();
                rowCount.NumberOnly();
                setComboBox();
            }
        }

        private void setComboBox()
        {
            comboBranch.SelectedItem = controller.tempEmployee.Branch;
            comboType.SelectedItem=controller.tempEmployee.EmployeesType;
        }

        private void btnShowDialogAddEmployee_Click(object sender, EventArgs e)
        {
           
            if (LoginData.permissions["employee"].addPermission.Value)
            {   controller.clearTempData();
                controller.prosessesType = ProsessesType.add;
                DialogAddAndUpdateEmployee dialog = new DialogAddAndUpdateEmployee(controller);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogUpdateEmployee_Click(object sender, EventArgs e)
        {

            if (LoginData.permissions["employee"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.prosessesType = ProsessesType.update;
                    controller.find(tablePagination.getKeyCurrentSelectedRow());
                    DialogAddAndUpdateEmployee dialog = new DialogAddAndUpdateEmployee(controller);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnShowDialogViewEmployee_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["employee"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(controller.columnsNamesInAR, tablePagination.getCurrentSelectedRow());
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
                tablePagination.changePageSize(rowCount.Text);
                controller.search(name.Text, phoneNumber.Text, accountNumber.Text);
           
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {   controller.clearTempData();
            rowCount.Clear();
            name.Clear();
            accountNumber.Clear();
            phoneNumber.Clear();
            setComboBox();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(name.Text, phoneNumber.Text, accountNumber.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["employee"].deletePermission.Value)
            {
                controller.delete(tablePagination.getKeysSelectedRows());
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnActivateAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["employee"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                  controller.activateOrDeactivateAccount(tablePagination.getKeyCurrentSelectedRow(),true);
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي حساب لتفعيله");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnDeactivateAccount_Click(object sender, EventArgs e)
        {
            if (LoginData.permissions["employee"].updatePermission.Value)
            {
                if (tablePagination.getCurrentSelectedRow() != null)
                {
                    controller.activateOrDeactivateAccount(tablePagination.getKeyCurrentSelectedRow(), false);
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي حساب لإلغاء تفعيله");
            }
            else AppDialogAleart.showAleartNoPermissions();
        }

        private void comboBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedBranch(comboBranch.SelectedItem);
        }

        private void comboType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedType(comboType.SelectedItem);
        }
    }
}
