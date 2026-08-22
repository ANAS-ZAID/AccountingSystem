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
    public partial class ItemQuantities : Form
    {
        ItemQuantitiesController controller;
        public ItemQuantities()
        {
            InitializeComponent();
            controller = new ItemQuantitiesController();
        }
        private void ItemQuantities_Load(object sender, EventArgs e)
        {
            // if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                btnShowOrHideToolBar.EnabledShowOrHideToolBar();
                item.DataSource = controller.items;
                account.DataSource = controller.accounts;
                supplier.DataSource = controller.suppliers;
                group.DataSource = controller.groups;
                store.DataSource = controller.stores;
                invoiceType.DataSource = controller.invoiceTypes;
                startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
                item.TextOnly("nameAr");
                account.TextOnly();
                supplier.TextOnly();
                group.TextOnly();  
                store.TextOnly();
                // invoiceType.TextOnly();
                panelBody.ReportFelx();
                setComboBox();
            }
            controller.HasDataProcessed = true;
            //  else AppDialogAleart.showAleartNoPermissions();
        }
        private void setComboBox()
        {
            item.SelectedItem = controller.selectedItem;
            account.SelectedItem = controller.account;
            supplier.SelectedItem = controller.supplier;
            group.SelectedItem = controller.group;
            store.SelectedItem = controller.store;
            invoiceType.SelectedItem = controller.invoiceType;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
        }
        void lodeData()
        {

        dataSetItemQuantitiesBindingSource.DataSource = controller.dataSource.DataSource;
         //   reportViewer1.LocalReport.SetParameters(new ReportParameter("allAccounts", (controller.account.id == 0).ToString()));
           reportViewer.LocalReport.SetParameters(new ReportParameter("showOrHidePreviousBalance",(controller.startDate==null).ToString()));
            this.reportViewer.RefreshReport();

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

        private void account_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedAccount(account.SelectedItem);
        }

        private void supplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedSupplier(supplier.SelectedItem);
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

        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStore(store.SelectedItem);   
        }

        private void item_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selecteItem(item.SelectedItem);
        }

        private void withoutCompoundItems_CheckedChanged(object sender, EventArgs e)
        {
            controller.selectedWithoutCompoundItems(withoutCompoundItems.Checked);
        }

        private void withoutZeroItems_CheckedChanged(object sender, EventArgs e)
        {
            controller.selectedWithoutZeroItems(withoutZeroItems.Checked);
        }

        private void invoiceType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedInvoiceType(invoiceType.SelectedItem);       
        }
    }
}
