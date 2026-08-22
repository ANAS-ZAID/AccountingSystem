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
using AccountingSystem.controller.Screen;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.PurchasesSystem
{
    public partial class PurchasesReturnsScreen : Form
    {
        TablePagination tablePagination;

        PurchasesReturnsController controller;
        public PurchasesReturnsScreen()
        {
            InitializeComponent();
            controller = new PurchasesReturnsController();
            tablePagination = new TablePagination();
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }

        private void PurchasesReturnsScreen_Load(object sender, EventArgs e)
        {
            if (controller.permissions.viewPermission.Value)
            {
                controller.startHSDP();
                store.DataSource = controller.stores;
                supplier.DataSource = controller.suppliers;
                casheir.DataSource = controller.cashiers;
                employee.DataSource = controller.employees;
                paymentType.DataSource = SharedData.paymentTypes;
                tablePagination.fillData(controller.dataSource);
                number.NumberOnly();
                rowCount.NumberOnly();
                store.TextOnly();
                casheir.TextOnly();
                supplier.TextOnly();
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
            supplier.SelectedItem = null;
            employee.SelectedItem = null;
            store.SelectedItem = null;
            casheir.SelectedItem = null;
            paymentType.Text = null;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData(true, number.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.lodeData();
            setComboBox();
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

        private void supplier_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectSupplier(supplier.SelectedItem);
        }

        private void casheir_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectCashier(casheir.SelectedItem);
        }

        private void paymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectPaymentType(paymentType.SelectedItem);
        }

        private void priceType_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectPriceType(priceType.SelectedItem);
        }

        private void employee_SelectedValueChanged(object sender, EventArgs e)
        {
            controller.selectEmployee(employee.SelectedItem);
        }
    }
}
