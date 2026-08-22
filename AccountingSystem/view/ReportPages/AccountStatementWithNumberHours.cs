using Microsoft.Reporting.WinForms;
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

namespace AccountingSystem.view.ReportPages
{
    public partial class AccountStatementWithNumberHours : Form
    {
        AccountStatementWithNumberHoursController controller;
        public AccountStatementWithNumberHours()
        {
            InitializeComponent();
            controller = new AccountStatementWithNumberHoursController();
        }
        
        private void AccountStatementWithNumberHours_Load(object sender, EventArgs e)
        {

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
                panelBody.ReportFelx();
                btnShowOrHideToolBar.EnabledShowOrHideToolBar();
            }

            controller.HasDataProcessed = true;


        }
        private void setComboBox()
        {
            account.SelectedItem = controller.account;
            currency.SelectedItem = controller.currency;
            group.SelectedItem = controller.group;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
        }
        void lodeData()
        {
            dataSetAccountStatementWithHourBindingSource.DataSource = controller.dataSource.DataSource;
            reportViewer.LocalReport.SetParameters(new ReportParameter("allAccounts", (controller.mainAccount).ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("hideHour", (!showHour.Checked).ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("total", controller.total.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("currencyCode", controller.mainCurrencyCode));
            if (controller.account != null)
                reportViewer.LocalReport.SetParameters(new ReportParameter("account", controller.account.name.ToString()));
            else
                reportViewer.LocalReport.SetParameters(new ReportParameter("account", " "));
            if (controller.startDate != null)
                reportViewer.LocalReport.SetParameters(new ReportParameter("fromDate", controller.startDate.Value.ToString(SharedData.formatDisplayDate)));
            else
                reportViewer.LocalReport.SetParameters(new ReportParameter("fromDate", " "));
            if (controller.endDate != null)
                reportViewer.LocalReport.SetParameters(new ReportParameter("toDate", controller.endDate.Value.ToString(SharedData.formatDisplayDate)));
            else
                reportViewer.LocalReport.SetParameters(new ReportParameter("toDate", DateTime.Now.ToString(SharedData.formatDisplayDate)));
            this.reportViewer.RefreshReport();
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
            try { reportViewer.PrintDialog(); } catch (Exception) { }


        }

        private void showHour_CheckedChanged(object sender, EventArgs e)
        {
            //lodeData();
        }
    }
}
