using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.view.SupScreens.AccountGruop;
using AccountingSystem.view.SupScreens.InventoryTransferManagament;
using AccountingSystem.view.SupScreens.PurchasesSystem;
using AccountingSystem.view.SupScreens.SalesSystem;

namespace AccountingSystem.view.Screens
{
    public partial class PurchasingAndSalesSystem : Form
    {
       
        public PurchasingAndSalesSystem()
        {
            InitializeComponent();
       
        }

        private void btnGoSales_Click(object sender, EventArgs e)
        {

             Program.homeScereen().openChildForm(new SalesSystemScreen(), changeColorActivBtn: false);
        }

        private void btnGoPurchases_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new PurchasesSystemScreen(), changeColorActivBtn: false);
        }

        private void PurchasingAndSalesSystem_SizeChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            int newLeftLocation = (int)(Width * .01);
            int widght = 430 + 700;
            int margin = (int)(Width * .030);
            if (Width < widght)
            {
                panelMangCommands.Width = (int)(Width * .35) - (int)(Width * .02);
                panelMangInvoices.Width = (int)(Width * .65) - (int)(Width * .02);
            }
            else
            {
                panelMangCommands.Width = 420;
                panelMangInvoices.Width = 690;
            }
            newLeftLocation = (Width - (panelMangCommands.Width + panelMangInvoices.Width + margin * 2)) / 2 + margin / 2;
            panelMangCommands.Location = new Point(newLeftLocation, this.TopPadding());
            newLeftLocation += panelMangCommands.Width + margin;
            panelMangInvoices.Location = new Point(newLeftLocation, this.TopPadding());
            ResumeLayout();
        }

        private void panelMangInvoices_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
        control.SuspendLayout();
            int margin = (int)(control.Width * 0.20) / 6;
            int width = (int)(control.Width * 0.45);
        
            int currentIndex = 0;
            for (int i = 0; i < control.Controls.Count; i++)
            {
                int newLeftLocation = 0;
                for (int j = 0; j < control.Controls.Count  / 2; j++)
                {
                    if (currentIndex < control.Controls.Count)
                    {
                        Control item = control.Controls[currentIndex];
                        if (item is Guna2TileButton)
                        {
                            
                            newLeftLocation += margin + (currentIndex == 0 || currentIndex == 1 ? width / 3 : 0);
                            item.Width = +(currentIndex == 0 || currentIndex == 1 ? width / 2 : width);
                            item.Location = new Point(newLeftLocation, item.Top);
                            newLeftLocation += item.Width ;
                        }
                     
                        //AppDialogAleart.showAleartNoPermissions(item.Name+currentIndex);
                    }
                    currentIndex++;
                }

            }
            control.ResumeLayout();
        }

        private void panelMangCommands_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.SuspendLayout();
            int margin = (int)(control.Width * 0.40) / 3;
            int width = (int)(control.Width * 0.60);
            int newLeftLocation = (int)(margin * 0.50);
            for (int i = 0; i < control.Controls.Count; i++)
            {

                Control item = control.Controls[i];
                if (item is Guna2TileButton)
                {
                    newLeftLocation += margin;
                    item.Width = width;
                    item.Location = new Point(newLeftLocation, item.Top);
                    newLeftLocation += item.Width;
                }
                else
                {
                    var fontSize = this.CreateGraphics().MeasureString(item.Controls[0].Text, item.Controls[0].Font).ToSize();
                    item.Controls[0].Location = new Point((item.Width - fontSize.Width) / 2, (item.Height - fontSize.Height) / 2);
                }
            }
            control.ResumeLayout();
        }

        private void titel_SizeChanged(object sender, EventArgs e)
        {
        Control control = (Control)sender;
           
            var fontSize = this.CreateGraphics().MeasureString(control.Controls[0].Text, control.Controls[0].Font).ToSize();
            control.Controls[0].Location = new Point((control.Width - fontSize.Width) / 2, (control.Height - fontSize.Height) / 2);
     
        }

        private void btnGoInventoryTransfer_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new InventoryTransferManagementScreen(), changeColorActivBtn: false);
        }

        private void btnGoReturnedSales_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new SalesReturnsScreen(), changeColorActivBtn: false);
        }

        private void btnGoReturnedPurchases_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new PurchasesReturnsScreen(), changeColorActivBtn: false);
        }
    }
}
