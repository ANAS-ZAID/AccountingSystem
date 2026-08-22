using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AccountingSystem.controller;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.model;
using AccountingSystem.Model;

namespace AccountingSystem.view.SupScreens.SupplierManagement
{
    public partial class DialogAddAndUpdateSupplier : Form
    {
        SupplierController controller;
        public DialogAddAndUpdateSupplier(SupplierController controller)
        {
            InitializeComponent();
            this.controller = controller;

            //address.KeyPress += ValidatingData.eventTextBoxTextOnly;
        }
        private void DialogAddAndUpdateSupplier_Load(object sender, EventArgs e)
        {
            perantAccount.DataSource = controller.mainAccounts;
            phoneNumber.PhoneOnly();
            name.TextOnly();
            accountNumber.NumberOnly();
            perantAccount.TextOnly();
            perantAccount.SelectedItem=controller.temp.Account?.perantAccount??null;
            if (controller.prosessesType == ProsessesType.update)
            {

                fillFeild();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            perantAccount.SelectedItem = controller.temp.Account?.perantAccount ?? null;
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(name.Text, accountNumber.Text, phoneNumber.Text, address.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(name.Text, accountNumber.Text, phoneNumber.Text, address.Text))
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
        private void clearFieldAndReferesh()
        {
            controller.clearTempData();
            accountNumber.Clear();
            name.Clear();
            phoneNumber.Clear();
            address.Clear();
            perantAccount.SelectedItem = controller.temp.Account?.perantAccount ?? null;

        }
        private void fillFeild()
        {
            accountNumber.Text = controller.temp.Account.accountNumber.ToString();
            name.Text = controller.temp.name;
            phoneNumber.Text = controller.temp.phoneNumber;
            address.Text = controller.temp.address;
            perantAccount.SelectedItem = controller.temp.Account?.perantAccount ?? null;
         
        }

        private void perantAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.temp.Account.perantAccount = (ChartOfAccount)perantAccount.SelectedItem;
            accountNumber.Text = AppDBFunctions.getNewAccountNumByParentId(controller.temp.Account.perantAccount.id).ToString();
        }
    }
}
