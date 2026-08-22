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
using System.Xml.Linq;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.AccountingGuide
{
    public partial class DialogAddAndUpdateAccounting : Form
    {
        AccountingGuideController controller;
       
        public DialogAddAndUpdateAccounting(AccountingGuideController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
        }
        private void fillFeild()
        {   numberAccount.Text = controller.tempChartOfAccount.accountNumber.ToString();
            name.Text = controller.tempChartOfAccount.name;

            if (controller.tempChartOfAccount.natureOfAccount == btnBalanceSheet.Text)
                FunctionsGUI.changeColorActiveBtn(btnBalanceSheet);
            else
                FunctionsGUI.changeColorActiveBtn(btnProfitAndLoss);
            if (controller.tempChartOfAccount.type == btnMain.Text)
                FunctionsGUI.changeColorActiveBtn(btnMain);
            else
                FunctionsGUI.changeColorActiveBtn(btnSup);
        }
        void setComboBox()
        {
            combParentAccount.SelectedItem = controller.tempChartOfAccount.perantAccount;
            combGroupAccount.SelectedItem = controller.tempChartOfAccount.AccountsGroup;
            combLocationAccount.SelectedItem = controller.tempChartOfAccount.accountLocation;
        }
        private void DialogAddAndUpdateAccounting_Load(object sender, EventArgs e)
        {
            combParentAccount.DataSource = controller.mainAccounts;
            combGroupAccount.DataSource = controller.accountsGroups;
            combLocationAccount.DataSource = controller.accountLocations;
            name.TextOnly();
            combParentAccount.TextOnly();
            numberAccount.NumberOnly();
            combGroupAccount.TextOnly();
            combLocationAccount.TextOnly("");
            setComboBox();
            if (controller.prosessesType == ProsessesType.update)
            {
       
                fillFeild();
            }
      
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsNatureOfAccount_Click(object sender, EventArgs e)
        {

            Guna2Button button = (Guna2Button)sender;
            if (button.Tag == null)
            {
                FunctionsGUI.reChangeColorActiveBtn(btnBalanceSheet);
                FunctionsGUI.reChangeColorActiveBtn(btnProfitAndLoss);
                FunctionsGUI.changeColorActiveBtn(button);
                controller.selectedNature(button.Text);
            }
        }
        private void btnsTypeOfAccount_Click(object sender, EventArgs e)
        {

            Guna2Button button = (Guna2Button)sender;
            if (button.Tag == null)
            {
                FunctionsGUI.reChangeColorActiveBtn(btnMain);
                FunctionsGUI.reChangeColorActiveBtn(btnSup);
                FunctionsGUI.changeColorActiveBtn(button);
                controller.selectedType(button.Text) ;
            }
        }
        private void clearFieldAndReferesh()
        {
            name.Clear();
          //  if(controller.prosessesType==ProsessesType.add)
            numberAccount.Clear();
            controller.clearTempData();

            setComboBox();
            FunctionsGUI.reChangeColorActiveBtn(btnMain);
            FunctionsGUI.reChangeColorActiveBtn(btnSup);
            FunctionsGUI.reChangeColorActiveBtn(btnBalanceSheet);
            FunctionsGUI.reChangeColorActiveBtn(btnProfitAndLoss);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
               setComboBox();
            if (controller.dataProcessing(numberAccount.Text,name.Text)) 
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

        private void combParentAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
                numberAccount.Text = controller.selectedParent(combParentAccount.SelectedItem);
        }

        private void combLocationAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedLocation(combLocationAccount.SelectedItem);
        }

        private void combGroupAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedGroup( combGroupAccount.SelectedItem);
        }

        private void DialogAddAndUpdateAccounting_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
              
        }
    }
}
