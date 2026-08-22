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
using AccountingSystem.controller.Screen;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.SalesSystem
{
    public partial class SalesReturnsScreen : Form
    {
        TablePagination tablePagination;

        SalesReturnsController controller;
        public SalesReturnsScreen()
        {
            InitializeComponent();
            controller = new SalesReturnsController();
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void SalesReturnsScreen_Load(object sender, EventArgs e)
        {
            
            if (controller.permissions.viewPermission.Value)
            {
                controller.startHSDP();
                store.DataSource = controller.stores;
                customer.DataSource = controller.customers;
                casheir.DataSource = controller.cashiers;
                employee.DataSource = controller.employees;
                paymentType.DataSource = SharedData.paymentTypes;
                orderType.DataSource = SharedData.orderTypes;
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
                controller.endHSDP();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        private void setComboBox()
        {

            controller.clearHomeTempData();
            startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
            customer.SelectedItem = null;
            employee.SelectedItem = null;
            store.SelectedItem = null;
            casheir.SelectedItem = null;
            paymentType.Text = null;
            orderType.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            controller.delete(tablePagination.getKeysSelectedRows());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            controller.print(tablePagination.getKeyCurrentSelectedRow());
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
           controller.selectStartDate(startDate.Value);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectEndDate(endDate.Value);
        }

        private void store_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectStore(store.SelectedItem);
        }

        private void customer_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCustomer(customer.SelectedItem);
        }

        private void casheir_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCashier(casheir.SelectedItem);
        }

        private void paymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectPaymentType(paymentType.SelectedItem);
        }

        private void orderType_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectOrderType(orderType.SelectedItem);
        }

        private void employee_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectEmployee(employee.SelectedItem);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData(true,number.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData();
            setComboBox();
        }
    }
}
