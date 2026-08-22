using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;

namespace AccountingSystem.view.ReportPages
{
    public partial class TreeAccounts : Form
    {
        TreeAccountsController controller;
        public TreeAccounts()
        {
            InitializeComponent();
            controller = new TreeAccountsController();
            InitializeAppComponent();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            // fillTableData();
            // تعيين المعلمات للتقرير (إذا لزم الأمر)
            loadData();

            // تصدير التقرير إلى PDF
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)

            //{
            //    reportViewer1.LocalReport.ExportToPdf(saveFileDialog1.FileName);
            //    MessageBox.Show("تم تصدير التقرير بنجاح.");
            //}
        }

        private void TreeAccounts_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { reportViewer1.PrintDialog(); } catch (Exception) { }
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "Excel files (*.Excel)|*.Excel";
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)

            //{//saveFileDialog1.f
            //    //    reportViewer1.PrintDialog();
            //    //reportViewer1.LocalReport.Render("Excel");
            //    Warning[] warnings;
            //    string[] streamids;
            //    string mimeType;
            //    string encoding;
            //    string filenameExtension;
            //    byte[] bytes = reportViewer1.LocalReport.Render("Excel");// ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            //    string FilePath = saveFileDialog1.FileName;
            //    //  FilePath += "ReportName.pdf";
            //    using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            //    {
            //        fs.Write(bytes, 0, bytes.Length);
            //    }
        }
    }
}
