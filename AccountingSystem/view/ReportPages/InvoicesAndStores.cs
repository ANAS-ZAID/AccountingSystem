using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingSystem.view.ReportPages
{
    public partial class InvoicesAndStores : Form
    {
        public InvoicesAndStores()
        {
            InitializeComponent();
        }

        private void btnItemQuantities_Click(object sender, EventArgs e)
        {
           Program.homeScereen().openChildForm(new ItemQuantities(),changeColorActivBtn:false);
        }

        private void btnMovementOfItems_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new MovementOfItems(), changeColorActivBtn: false);
        }

        private void btnMovementOfItemsToSupplier_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new MovementOfItemsToSupplier(), changeColorActivBtn: false);
        }

        private void btnStocItemsLessThanZero_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new StocItemsLessThanZero(), changeColorActivBtn: false);
        }

        private void btnCompositeItemsInventory_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new CompositeItemsInventory(), changeColorActivBtn: false);
        }

        private void btnGoWarehouseQuantities_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new WarehouseQuantities(), changeColorActivBtn: false);
        }

        private void btnGoInvoicesNumber_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new InvoicesNumber(), changeColorActivBtn: false);
        }

        private void btnGoBillsExchange_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new BillsExchange(), changeColorActivBtn: false);
        }
    }
}
