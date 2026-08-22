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
using AccountingSystem.model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using AccountingSystem.Model;

namespace AccountingSystem.view.SupScreens.SalesSystem
{
    public partial class SalesSystemScreen : Form
    {
        TablePagination tablePagination;
     
        SalesSystemController controller;

    
        public SalesSystemScreen(bool isReturned=false)
        {
            InitializeComponent();
           
            controller = new SalesSystemController(TransactionType.فاتورة_مبيعات);
          
            tablePagination = new TablePagination();
         
            table.Controls.Add(tablePagination.duildGroupBoxTable());

        }
        private void SalesSystemScreen_Load(object sender, EventArgs e)
        {
          
            if (model.LoginData.permissions["sale"].viewPermission.Value)
            {
                store.DataSource = controller.stores;
                customer.DataSource = controller.customers;
                casheir.DataSource = controller.cashiers;
                employee.DataSource = controller.employees;
                paymentType.DataSource = controller.paymentTypes;
                orderType.DataSource = controller.orderTypes;
               
               tablePagination.fillData(controller.dataSource);
            number.NumberOnly();
            rowCount.NumberOnly();
            store.TextOnly();
            casheir.TextOnly();
            customer.TextOnly();
            orderType.TextOnly();
            paymentType.TextOnly();
            employee.TextOnly();
            setComboBox(); 
         
            }
            else AppDialogAleart.showAleartNoPermissions();

        }

        private void setComboBox()
        {
            controller.HasHomeScreenDataProcessed = false;
            startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
            controller.tempSale = new Sale();
            customer.SelectedItem = null;
            employee.SelectedItem = null;
            store.SelectedItem = null;
            casheir.SelectedItem = null;
            paymentType.Text = null;
           orderType.SelectedItem=null;
           controller.HasHomeScreenDataProcessed = true;
        }

       
        private void btnShowDialogAddSalse_Click(object sender, EventArgs e)
        {

            controller.showDialogAdd();
            //homeScereen.Hide();
         //   homeScereen.btnGoToHome_Click(null, null);
        }
        private void btnShowDialogUpdateSalse_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }
        private void btnShowDialogViewSalse_Click(object sender, EventArgs e)
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
            // controller.clearTempData();

                setComboBox();
                rowCount.Clear();
                number.Clear();
                tablePagination.changePageSize(rowCount.Text);
                controller.search(number.Text);

        }

        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStore(store.SelectedItem);
        }

        private void customer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCustomerOrSupplier(customer.SelectedItem);
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
            controller.selectedoOrderType(orderType.SelectedItem);
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

        private void btnReturned_Click(object sender, EventArgs e)
        {
            controller.addReturn(tablePagination.getKeyCurrentSelectedRow());
        }
    }
}
