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
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.ReportPages
{
    public partial class CompositeItemsInventory : Form
    {
        CompositeItemsInventoryController controller;
        public CompositeItemsInventory()
        {
            InitializeComponent();
            controller = new CompositeItemsInventoryController();
        }

        private void CompositeItemsInventory_Load(object sender, EventArgs e)
        {
            btnShowOrHideToolBar.EnabledShowOrHideToolBar();
           
            store.DataSource = controller.stores;
 
            startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
     
            store.TextOnly();
            // invoiceType.TextOnly();
            panelBody.ReportFelx();
            setComboBox(); controller.HasDataProcessed = true;
        }
        private void setComboBox()
        {
            store.SelectedItem = controller.store;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
        }
        void lodeData()
        {

           dataSetCompositeItemsInventoryBindingSource.DataSource = controller.dataSource.DataSource;
         //reportViewer.LocalReport.SetParameters(new ReportParameter("allAccounts", (controller.account.id == 0).ToString()));
           reportViewer.LocalReport.SetParameters(new ReportParameter("showOrHidePreviousBalance", (controller.startDate == null).ToString()));
            this.reportViewer.RefreshReport();

        }



        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedStartDate(controller.startDate);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedEndDate(controller.endDate);
        }

        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStore(store.SelectedItem);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //  if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                if (controller.search())
                {
                    lodeData();
                }
                else AppDialogAleart.showAleartError("لم يتم العثور على بيانات");

            }
            //else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { reportViewer.PrintDialog(); } catch (Exception) { }
        }

      
    }
}
