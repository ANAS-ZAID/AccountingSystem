using Krypton.Toolkit;
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
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.view.ReportPages
{
    public partial class ViewPrintingBills : KryptonForm
    {
        public ViewPrintingBills(List<DataSetBills> detail, dynamic bill)
        {
            InitializeComponent();
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] {
                new ReportParameter("name", bill.name), 
                new ReportParameter("number", bill.number),
                new ReportParameter("type", bill.type),
                new ReportParameter("date", bill.date), 
                new ReportParameter("store", bill.store), 
                new ReportParameter("currencyName", bill.currencyName), 
                new ReportParameter("currencyCode", bill.currencyCode), 
                new ReportParameter("amountPaid", bill.amountPaid), 
                new ReportParameter("total", bill.total),
                new ReportParameter("user", bill.user),
                new ReportParameter("printTiming",DateTime.Now.ToString())

            });
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetBill", detail));
          
        }

        private void ViewPrintingBills_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
           // this.reportViewer2.RefreshReport();
        }
    }
}
