using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.ReportPages
{
    public partial class AccountStatement : Form
    {
        AccountStatementController controller;
        public AccountStatement()
        {
            InitializeComponent();
            controller = new AccountStatementController();
            InitializeAppComponent();
 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

           // if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                account.DataSource = controller.accounts;
                currency.DataSource = controller.currencies;
                group.DataSource = controller.groups;
                startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
                account.TextOnly();
                 rowCount.NumberOnly();
                 currency.TextOnly();
                 group.TextOnly();
                setComboBox();
            }
            controller.HasDataProcessed = true;
            //  else AppDialogAleart.showAleartNoPermissions();
        }
        private void setComboBox()
        {
          account.SelectedItem= controller.account;
          currency.SelectedItem= controller.currency;
          group.SelectedItem= controller.group;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                  if (controller.search())
                {
                    lodeData();
                }
                  else AppDialogAleart.showAleartError("لم يتم العثور على بيانات");
                
            }
          //  else AppDialogAleart.showAleartNoPermissions();
        }
    
       

        private void account_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedAccount(account.SelectedItem);
        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedStartDate(startDate.Value);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedEndDate(endDate.Value);
        }

        private void group_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedGroup(group.SelectedItem);
        }

        private void total_CheckedChanged(object sender, EventArgs e)
        {
            controller.selectedTotal(total.Checked);
        }

        private void noOpeningBalance_CheckedChanged(object sender, EventArgs e)
        {
            controller.selectedNoOpeningBalance(noOpeningBalance.Checked);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { reportViewer.PrintDialog(); }catch(Exception) { }
              
       
        }

       

     
    }
    
}
