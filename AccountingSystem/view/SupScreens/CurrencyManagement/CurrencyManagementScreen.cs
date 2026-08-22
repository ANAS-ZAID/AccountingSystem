using Guna.UI2.WinForms;
using Krypton.Toolkit;
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
using AccountingSystem.model;

namespace AccountingSystem.view.Screens.CurrencyManagement
{
    public partial class CurrencyManagementScreen : Form
    {
     
        TablePagination tablePagination;
        CurrencyController controller;
        public CurrencyManagementScreen()
        {  
            InitializeComponent();
            controller = new CurrencyController();
            tablePagination = new TablePagination();
            currencyName.TextOnly();
            rowCount.NumberOnly();
        
            table.Controls.Add(tablePagination.duildGroupBoxTable());
        }
        private void CurrencyManagementScreen_Load(object sender, EventArgs e)
        {
            if (model.LoginData.permissions["currency"].viewPermission.Value)
                tablePagination.fillData(controller.dataSource);
        }
     
        private void btnShowDialogAddCurrency_Click(object sender, EventArgs e)
        {
            controller.showDialogAdd();
        }

        private void btnShowDialogUpdateCurrency_Click(object sender, EventArgs e)
        {
            controller.showDialogUpdate(tablePagination.getKeyCurrentSelectedRow());
        }

        private void btnShowDialogViewCurrency_Click(object sender, EventArgs e)
        {
            controller.showDialogView(tablePagination.getCurrentSelectedRow());
        }

        private void btnShowDialogDeleteCurrency_Click(object sender, EventArgs e)
        {
            controller.delete(tablePagination.getKeysSelectedRows());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tablePagination.changePageSize(rowCount.Text);
            controller.search(currencyName.Text);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            rowCount.Clear();
            currencyName.Clear();
            tablePagination.changePageSize(rowCount.Text);
            controller.search(currencyName.Text);
        }
    }
}
