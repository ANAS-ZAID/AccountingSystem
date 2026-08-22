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
    public partial class BillsExchange : Form
    {
        BillsExchangeController controller;
        public BillsExchange()
        {
            InitializeComponent();
            controller =new BillsExchangeController();
        }

        private void BillsExchange_Load(object sender, EventArgs e)
        {
            btnShowOrHideToolBar.EnabledShowOrHideToolBar();
            number.NumberOnly();
            panelBody.ReportFelx();
            ((Label)number.Tag).Visible = true;
        }
        void lodeData()
        {

            dataSetJournalEntryBindingSource.DataSource = controller.dataSource.DataSource;

            this.reportViewer1.RefreshReport();

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //  if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                if (controller.search(number.Text))
                {
                    lodeData();
                }
                //   else AppDialogAleart.showAleartError("لم يتم العثور على بيانات");

            }
            //else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { reportViewer1.PrintDialog(); } catch (Exception) { }
        }
    }
}
