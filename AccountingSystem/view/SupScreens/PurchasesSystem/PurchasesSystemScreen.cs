using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.PurchasesSystem
{
    public partial class PurchasesSystemScreen : Form
    {
        TablePagination tablePagination;
      
        SalesSystemController controller;
        HomeScereen homeScereen;
        public PurchasesSystemScreen()
        {
            InitializeComponent();
            homeScereen = Program.homeScereen();
            controller = new SalesSystemController(TransactionType.فاتورة_مشتريات);
        
            tablePagination = new TablePagination();
         
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void PurchasesSystemScreen_Load(object sender, EventArgs e)
        {
            if (model.LoginData.permissions["purchase"].viewPermission.Value)
            {
                store.DataSource = controller.stores;
                supplier.DataSource = controller.suppliers;
                casheir.DataSource = controller.cashiers;
                employee.DataSource = controller.employees;
                paymentType.DataSource = controller.paymentTypes;
                priceType.DataSource = controller.priceTypes;
                
                number.NumberOnly();
                rowCount.NumberOnly();
                store.TextOnly();
                casheir.TextOnly();
                supplier.TextOnly();
                priceType.TextOnly();
                paymentType.TextOnly();
                employee.TextOnly();
                setComboBox();
                tablePagination.fillData(controller.dataSource);
                
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        private void setComboBox()
        {
            controller.HasHomeScreenDataProcessed = false;
            startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
            controller.tempPurchase = new Purchase();
            supplier.SelectedItem = null;
            employee.SelectedItem = null;
            store.SelectedItem = null;
            casheir.SelectedItem = null;
            paymentType.Text = null;
            priceType.SelectedItem = null;
            controller.HasHomeScreenDataProcessed = true;
        }
        private void btnShowDialogAdd_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();

        }

        private void btnShowDialogUpdate_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }

        private void btnShowDialogView_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            controller.delete(tablePagination.getKeysSelectedRows());

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
                tablePagination.changePageSize(rowCount.Text);
                controller.search(number.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {

            if (model.LoginData.permissions["purchase"].viewPermission.Value)
            {
                setComboBox();
                rowCount.Clear();
                number.Clear();
                tablePagination.changePageSize(rowCount.Text);
                controller.search(number.Text);
            }
        }

        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStore(store.SelectedItem);
        }

        private void supplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCustomerOrSupplier(supplier.SelectedItem);
        }

        private void cashir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCashier(casheir.SelectedItem);   
        }

        private void paymentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedPaymentType(paymentType.SelectedItem);
        }

        private void typeRequest_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedPriceType(priceType.Text);   
        }

        

        private void employee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedEmployee(employee.SelectedItem);
        }

        private void branch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedStartDate(startDate.Value);
        }
        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedEndDate(endDate.Value);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            controller.printInvoice(tablePagination.getKeyCurrentSelectedRow());
        }
    }
}
