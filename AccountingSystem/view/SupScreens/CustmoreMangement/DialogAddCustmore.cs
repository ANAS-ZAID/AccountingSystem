using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.Screens.CustmoreMangement
{
    public partial class DialogAddCustmore : Form
    {CustmoreController controller;
        public DialogAddCustmore(CustmoreController controller)
        {
            InitializeComponent();
            this.controller = controller;
       
        }
        private void DialogAddCustmore_Load(object sender, EventArgs e)
        {
            perantAccount.DataSource = controller.mainAccounts;
            area.DataSource = controller.allAreas;
            city.DataSource = controller.allCity;
            accountGroup.DataSource = controller.allAccountGroups;
            phoneNumber.PhoneOnly();
            name.TextOnly();
            accountNumber.NumberOnly();
            address.TextOnly();
            city.TextOnly();
            area.TextOnly();
            perantAccount.TextOnly();
            accountGroup.TextOnly();
            if (controller.prosessesType == ProsessesType.update)
            {

                fillFeild();
            }

            selecteIntionlItemCombobosies();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            selecteIntionlItemCombobosies();
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(name.Text, accountNumber.Text, phoneNumber.Text, address.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(name.Text, accountNumber.Text, phoneNumber.Text, address.Text))
                    return;
            this.Close();
        }
        void selecteIntionlItemCombobosies()
        {
            perantAccount.SelectedItem = controller.temp.Account?.perantAccount ;
            city.SelectedItem = controller.temp.City;
            area.SelectedItem = controller.temp.Area ;
            accountGroup.SelectedItem = controller.temp.Account?.AccountsGroup;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }
        private void clearFieldAndReferesh()
        {  controller.clearTempData();
            accountNumber.Clear();
            name.Clear();
            phoneNumber.Clear();
            address.Clear();
            selecteIntionlItemCombobosies();


        }
        private void fillFeild()
        {
            accountNumber.Text = controller.temp.Account.accountNumber.ToString();
            name.Text = controller.temp.name;
            phoneNumber.Text = controller.temp.phoneNamber;
            address.Text = controller.temp.address;
            perantAccount.SelectedItem = controller.temp.Account?.perantAccount;
            area.SelectedItem = controller.temp.Area ;
            city.SelectedItem = controller.temp.City ;
            accountGroup.SelectedItem = controller.temp.Account?.AccountsGroup ;
        }

        private void perantAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.Account.perantAccount = (ChartOfAccount)perantAccount.SelectedItem;
            accountNumber.Text = AppDBFunctions.getNewAccountNumByParentId(controller.temp.Account.perantAccount.id).ToString();
        }

        private void accountGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.Account.AccountsGroup=(AccountsGroup)accountGroup.SelectedItem;
        }

        private void city_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.City=(City)city.SelectedItem;
        }

        private void area_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.Area=(Area)area.SelectedItem;
        }

        private void DialogAddCustmore_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }
    }
}
