using Krypton.Toolkit;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.ReportPages
{
    public partial class AccountStatement
    {
       
        void InitializeAppComponent()
        {

            btnShowOrHideToolBar.EnabledShowOrHideToolBar();
            panelBody.ReportFelx();

        }
        
        void lodeData()
        {

            dataSetAccountStatementBindingSource.DataSource = controller.dataSource.DataSource;
            reportViewer.LocalReport.SetParameters(new ReportParameter("allAccounts", (controller.account.id==0).ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("total", controller.total.ToString()));
            reportViewer.LocalReport.SetParameters(new ReportParameter("currencyCode", controller.mainCurrencyCode));
            if(controller.account != null)
            reportViewer.LocalReport.SetParameters(new ReportParameter("account", controller.account.name.ToString()));
            else
                reportViewer.LocalReport.SetParameters(new ReportParameter("account"," "));  
            if(controller.startDate != null)
            reportViewer.LocalReport.SetParameters(new ReportParameter("fromDate", controller.startDate.Value.ToString(SharedData.formatDisplayDate)));
            else
                reportViewer.LocalReport.SetParameters(new ReportParameter("fromDate", " "));
            if(controller.endDate != null)
            reportViewer.LocalReport.SetParameters(new ReportParameter("toDate", controller.endDate.Value.ToString(SharedData.formatDisplayDate)));
            else
                reportViewer.LocalReport.SetParameters(new ReportParameter("toDate", DateTime.Now.ToString(SharedData.formatDisplayDate)));
            this.reportViewer.RefreshReport();
           
        }
    } public partial class TreeAccounts
    {

        void InitializeAppComponent()
        {

            guna2CircleButton1.EnabledShowOrHideToolBar();
            panelBody.ReportFelx();
            loadData();
        }
     
        void loadData()
        {

            // TODO: This line of code loads data into the 'dataSetReports.ChartOfAccounts' table. You can move, or remove it, as needed.
            this.chartOfAccountsTableAdapter.Fill(this.dataSetReports.ChartOfAccounts);
            // TODO: This line of code loads data into the 'dataSetReports.ChartOfAccounts' table. You can move, or remove it, as needed.
            this.chartOfAccountsTableAdapter.Fill(this.dataSetReports.ChartOfAccounts);

            this.reportViewer1.RefreshReport();
        }
    }
}
