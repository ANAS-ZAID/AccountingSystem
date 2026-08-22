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

namespace AccountingSystem.view.SupScreens.ChestManagement
{
    public partial class DialogAddAndUpdateChest : Form
    {
        CashierController controller;
        
        public DialogAddAndUpdateChest(CashierController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
        }
        private void DialogAddAndUpdateChest_Load(object sender, EventArgs e)
        {
            comboParentId.DataSource = controller.mainAccounts;
            comboParentId.SelectedItem=controller.tempCashier?.Account?.perantAccount;
            name.TextOnly();
            comboParentId.TextOnly();
            accountNumber.NumberOnly();

            if (controller.prosessesType == ProsessesType.update)
            {
               
                fillFeild();
            }

        }
        private void fillFeild()
        {
            accountNumber.Text = controller.tempCashier.Account.accountNumber.ToString();
            name.Text = controller.tempCashier.name;
           comboParentId.SelectedItem = controller.tempCashier?.Account?.perantAccount;
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            comboParentId.SelectedItem = controller.tempCashier?.Account?.perantAccount;
           

            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(name.Text,accountNumber.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(name.Text, accountNumber.Text))
                    return;
            this.Close();
        }
        private void clearFieldAndReferesh()
        {
           
                name.Clear();
                accountNumber.Clear();
           
            comboParentId.SelectedValue = 0;
            controller.tempCashier.Account.perantAccount = null;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void comboParentId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.tempCashier.Account.perantAccount = (ChartOfAccount)comboParentId.SelectedItem;
            accountNumber.Text = AppDBFunctions.getNewAccountNumByParentId(controller.tempCashier.Account.perantAccount.id).ToString();
        }
    }
}
//    public partial class DialogAddAndUpdateChest : Form
//    {
//        CashierController controller;

//        public DialogAddAndUpdateChest(CashierController controller)
//        {
//            InitializeComponent();
//            labelTitel.Text = Functions.getCurrentRoot();
//            this.controller = controller;
//        }
//        private void DialogAddAndUpdateChest_Load(object sender, EventArgs e)
//        {
//            comboParentId.DataSource = controller.mainAccounts;
//            name.TextOnly();
//            comboParentId.TextOnly();
//            accountNumber.NumberOnly();
//            if (controller.prosessesType == ProsessesType.update)
//            {

//                fillFeild();
//            }
//            setCombBox();
//        }
//        void setCombBox()
//        {
//            comboParentId.SelectedItem = controller.tempCashier.Account.perantAccount;
//        }
//        private void fillFeild()
//        {
//            accountNumber.Text = controller.tempCashier.Account.accountNumber.ToString();
//            name.Text = controller.tempCashier.name;
//            setCombBox();

//        }
//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            comboParentId.SelectedItem = controller.tempCashier?.Account?.perantAccount ?? null;


//            if (controller.prosessesType == ProsessesType.add)
//                if (!controller.add(name.Text, accountNumber.Text))
//                    return;
//            if (controller.prosessesType == ProsessesType.update)
//                if (!controller.update(name.Text, accountNumber.Text))
//                    return;
//            this.Close();
//        }
//        private void clearFieldAndReferesh()
//        {

//            name.Clear();
//            accountNumber.Clear();

//            comboParentId.SelectedValue = 0;
//            controller.tempCashier.Account.perantAccount = null;

//        }
//        private void btnClear_Click(object sender, EventArgs e)
//        {
//            clearFieldAndReferesh();
//        }

//        private void btnReferesh_Click(object sender, EventArgs e)
//        {
//            clearFieldAndReferesh();
//        }

//        private void comboParentId_SelectionChangeCommitted(object sender, EventArgs e)
//        {
//            controller.tempCashier.Account.perantAccount = (ChartOfAccount)comboParentId.SelectedItem;
//            accountNumber.Text = AppDBFunctions.getNewAccountNumByParentId(controller.tempCashier.Account.perantAccount.id).ToString();
//        }
//    }
