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
using AccountingSystem.core.shared;

namespace AccountingSystem.view.ReportPages
{
    public partial class Form1 : Form
    {
        AccountStatementWithNumberHoursController controller;
        public Form1()
        {
            InitializeComponent();

            controller= new AccountStatementWithNumberHoursController();    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            controller.search();
          
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource() { Value= controller.dataSource.DataSource ,Name= "DataSetAccountStatementWithNumberHours" });
            reportViewer.LocalReport.SetParameters(new ReportParameter("allAccounts", (controller.account?.id == 0).ToString()));
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
    }
}
