using Guna.UI2.WinForms;
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
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.model;
using AccountingSystem.view.SupScreens.EmployeesTypeMangement;


namespace AccountingSystem.view.SupScreens.EmployeeManagement
{
    public partial class DialogAddAndUpdateEmployee : Form
    {
        EmployeeController controller;
        public DialogAddAndUpdateEmployee(EmployeeController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;


        }
        private void fillFeild()
        {
            accountNumber.Text = controller.tempEmployee.Account.accountNumber.ToString();
            name.Text = controller.tempEmployee.name;
            phoneNumber.Text = controller.tempEmployee.phoneNamber;
            loginName.Text = controller.tempEmployee.loginName;
            loginPassword.Text = controller.tempEmployee.password;
            setComboBox();
          


        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            setComboBox();
     

            
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(name.Text,phoneNumber.Text,loginName.Text,loginPassword.Text, accountNumber.Text, getPermissions()))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(name.Text, phoneNumber.Text, loginName.Text, loginPassword.Text, accountNumber.Text, getPermissions()))
                    return;
            this.Close();

        }
        private void clearFieldAndReferesh()
        {
            controller.clearTempData();
            accountNumber.Clear();
            name.Clear();
            phoneNumber.Clear();
            loginName.Clear();
            loginPassword.Clear();
            setComboBox();
           

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void DialogAddAndUpdateEmployee_Load(object sender, EventArgs e)
        {
            comboPerantAccount.DataSource = controller.mainAccounts;
            comboCashir.DataSource = controller.allCashiers;
            comboType.DataSource = controller.allEmployeeType;
            comboBranch.DataSource = controller.allBranches;
            name.TextOnly();
            comboPerantAccount.TextOnly();
            comboBranch.TextOnly();
            comboCashir.TextOnly();
            comboPerantAccount.TextOnly();
            phoneNumber.PhoneOnly();
            accountNumber.TextOnly();
            comboType.TextOnly();
            setComboBox();

            lodePermissions();
            if (controller.prosessesType == ProsessesType.update)
            {

                fillFeild();
                lodeUpdatePermissions();
            }
           
           

        }

        private void setComboBox()
        {
            comboPerantAccount.SelectedItem = controller.tempEmployee.Account?.perantAccount;
            comboCashir.SelectedItem = controller.tempEmployee?.Cashier;
            comboType.SelectedItem = controller.tempEmployee?.EmployeesType;
            comboBranch.SelectedItem = controller.tempEmployee?.Branch;
        }

        private List<Permission> getPermissions()
        {
           // controller.tempEmployee.Permissions=new List<Permission>();
           List<Permission> permissions = new List<Permission>();
            foreach (var check in panelPermissions.Controls)
            {
                if (check is Guna2GroupBox)
                {
                    Guna2GroupBox groupBox = (Guna2GroupBox)check;
                    Permission permission = new Permission() { tableName = groupBox.Name };
                   // AppDialogAleart.showAleartNoPermissions(permission.tableName);
                    foreach (Guna2CheckBox item in groupBox.Controls)
                    {
                        if (item.Name == "add")
                            permission.addPermission = item.Checked;

                        if (item.Name == "update")
                            permission.updatePermission = item.Checked;
                        if (item.Name == "view")
                            permission.viewPermission = item.Checked;
                        if (item.Name == "delete")
                            permission.deletePermission = item.Checked;
                    }
                permissions.Add(permission);
                  
                }
            }
            return permissions;
        }
        private void lodePermissions()
        {
             panelPermissions.Controls.Clear();
            panelPermissions.Height = 55 * LoginData.permissions.Count;
           
            foreach (var permissionGUI in LoginData.permissionsTables.Values)
            {
                panelPermissions.Controls.Add(BuildControls.buildGroupBoxPermission(permissionGUI));
               // controller.newPermissions.Add(permission);
            }
            selectAllPermissions(false);
            
        }
        void selectAllPermissions(bool isCheck)
        {
            foreach (var item in panelPermissions.Controls)
            {
                if (item is Guna2GroupBox)
                {
                    Guna2GroupBox groupBox = (Guna2GroupBox)item;
                    foreach (Guna2CheckBox check in groupBox.Controls)
                    {
                        check.Checked = isCheck;
                        
                    }
                    groupBox.Visible=true;
                }

            }
        }
        private void lodeUpdatePermissions()
        {
            int i = 0;
            foreach (var item in panelPermissions.Controls)
            {
                if (item is Guna2GroupBox)
                {
                    Guna2GroupBox groupBox = (Guna2GroupBox)item;
                    foreach (Guna2CheckBox check in groupBox.Controls)
                    {
                       
                        if (check.Name == "add")
                            check.Checked = controller.tempEmployee.Permissions.ElementAt(i).addPermission.Value;

                        if (check.Name == "update")
                            check.Checked = controller.tempEmployee.Permissions.ElementAt(i).updatePermission.Value;
                        if (check.Name == "view")
                            check.Checked = controller.tempEmployee.Permissions.ElementAt(i).viewPermission.Value;
                        if (check.Name == "delete")
                            check.Checked = controller.tempEmployee.Permissions.ElementAt(i).deletePermission.Value;
                    }i++;
                }
            }
        } private void lodePermitionAvilabl(bool isAvilabl = true)
        {
            int i = 0;
            List<int> ints = new List<int>();
            foreach (var item in panelPermissions.Controls)
            {bool isC=false;
                if (item is Guna2GroupBox)
                {
                    Guna2GroupBox groupBox = (Guna2GroupBox)item;
                    foreach (Guna2CheckBox check in groupBox.Controls)
                    {
                        if (check.Checked)
                        {
                            isC= true;
                            break;
                        }
                    }
                    if (isAvilabl)
                    {
                        if (!isC)
                            groupBox.Visible = false;
                    }else
                        groupBox.Visible = true;
                }
                i++;
            }
        }

        private void comboPerantAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.tempEmployee.Account.perantAccount=(ChartOfAccount)comboPerantAccount.SelectedItem;
            accountNumber.Text = AppDBFunctions.getNewAccountNumByParentId(controller.tempEmployee.Account.perantAccount.id).ToString();
        }

        private void comboBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.tempEmployee.Branch = (NewModel.EFModel.Branch)comboBranch.SelectedItem;
        }

        private void comboType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.tempEmployee.EmployeesType = (NewModel.EFModel.EmployeesType)comboType.SelectedItem;
        }

        private void comboCashir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.tempEmployee.Cashier = (NewModel.EFModel.Cashier)comboCashir.SelectedItem;
        }

        private void selectAll_CheckedChanged(object sender, EventArgs e)
        {
           
           selectAllPermissions(selectAll.Checked);
        }

        private void permitionAvilabl_CheckedChanged(object sender, EventArgs e)
        {
           if(permitionAvilabl.Checked)
                lodePermitionAvilabl();
           else lodePermitionAvilabl(false);
        }

        private void addEmployeeType_Click(object sender, EventArgs e)
        {
            DialogAddAndUpdateEmployeesType dialog = new DialogAddAndUpdateEmployeesType();
            dialog.ShowDialog();
            comboType.DataSource = controller.allEmployeeType;
            controller.tempEmployee.EmployeesType = (EmployeesType)comboType.SelectedItem;
        }

        private void DialogAddAndUpdateEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }
    }
}
