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
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.WarehouseManagement
{
    public partial class DialogAddAndUpdateWarehouse : Form
    {
        StoreController controller;
        public DialogAddAndUpdateWarehouse(StoreController controller)
        {
            InitializeComponent();
           this.controller = controller;
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogAddAndUpdateWarehouse_Load(object sender, EventArgs e)
        {

            perantAccount.DataSource = controller.mainAccounts;
            name.TextOnly();
            accountNumber.NumberOnly();
            perantAccount.TextOnly();
            perantAccount.SelectedItem = controller.temp.Account?.perantAccount ?? null;
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            perantAccount.SelectedItem = controller.temp?.Account?.perantAccount ?? null;

            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(name.Text, accountNumber.Text,address.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(name.Text, accountNumber.Text, address.Text))
                    return;
            this.Close();
        }
      
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }
        private void fillFeild()
        {
            accountNumber.Text = controller.temp.Account.accountNumber.ToString();
            name.Text = controller.temp.name;
            address.Text = controller.temp.address; 
            perantAccount.SelectedItem = controller.temp.Account.perantAccount;

        }
        private void clearFieldAndReferesh()
        {
            controller.clearTempData();
            name.Clear();
            accountNumber.Clear();
            address.Clear();
            perantAccount.SelectedItem =controller.temp.Account?.perantAccount??null;
        }

        private void perantAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.Account.perantAccount = (ChartOfAccount)perantAccount.SelectedItem;
            accountNumber.Text = AppDBFunctions.getNewAccountNumByParentId(controller.temp.Account.perantAccount.id).ToString();
        }

        private void DialogAddAndUpdateWarehouse_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }
    }
}
