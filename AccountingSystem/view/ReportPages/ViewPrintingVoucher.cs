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
using System.Windows.Markup;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.ReportPages
{
    public partial class ViewPrintingVoucher : Form
    {
        public ViewPrintingVoucher(dynamic voucher)
        {
            InitializeComponent();
          //  data = new { date = temp.date.Value.ToString(SharedData.formatDisplayDate),
          //  type = "(" + transactionType.ToString().Replace("_", " ") + ")",
          //  number = temp.id.ToString(), user = temp.Employee?.name ?? "",
          //  account = temp.Account.name, accountNumber = temp.Account.accountNumber.ToString(),
          //  amount = (temp.amount ?? 0).ToString(), currency = temp.Currency.name };

            reportViewer1.LocalReport.SetParameters(new ReportParameter[] {
                new ReportParameter("account", voucher.account),
                new ReportParameter("accountNumber", voucher.accountNumber),
                new ReportParameter("number", voucher.number),
                new ReportParameter("type", voucher.type),
                new ReportParameter("date", voucher.date),
                new ReportParameter("user",    voucher.user),
                new ReportParameter("currency", voucher.currency),
                new ReportParameter("amount", voucher.amount),
                new ReportParameter("description", voucher.description),
                new ReportParameter("currencyCode", voucher.currencyCode),
                new ReportParameter("printTiming",DateTime.Now.ToString())

            });
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource() { });
        }

        private void ViewPrintingVoucher_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
           
        }
    }
}
