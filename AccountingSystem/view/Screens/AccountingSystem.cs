using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using AccountingSystem.core.shared;
using AccountingSystem.view.Screens.AccountingGuide;
using AccountingSystem.view.Screens.CurrencyManagement;
using AccountingSystem.view.SupScreens.AccountGruop;
using AccountingSystem.view.SupScreens.ChestManagement;
using AccountingSystem.view.SupScreens.EmployeesManagement;

namespace AccountingSystem.view.Screens
{
    public partial class AccountingSystem : Form
    {
        
        public AccountingSystem()
        {
            InitializeComponent();
          

        }

        private void btnGoAccountGruop_Click(object sender, EventArgs e)
        {
           Program.homeScereen().openChildForm(new AccountGruopManagementScreen(),changeColorActivBtn: false);
        }

        private void btnGoAccountingGuide_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new AccountingGuideScereen(false), changeColorActivBtn: false);
        }

        private void btnGoChartOfAccounts_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new AccountingGuideScereen(true), changeColorActivBtn: false);
        }

        private void btnGoEmployeeManagement_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new EmployeeManagementScreen(), changeColorActivBtn: false);
        }

        private void btnGoChestManagement_Click(object sender, EventArgs e)
        {

             Program.homeScereen().openChildForm(new ChestManagementScreen(), changeColorActivBtn: false);
        }

        private void AccountingSystem_SizeChanged(object sender, EventArgs e)
        {
            if (Width < 1160)
                panelBtns.Width = Width-(int)(Width*.01);
            else
                panelBtns.Width = 1150;
            panelBtns.Location = new Point((Width-panelBtns.Width)/2, this.TopPadding());
        }

        private void panelBtns_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            Thread thread = new Thread(() => { sizeChanged(control); });
            if(IsHandleCreated)
                thread.Start();

        }

        private void sizeChanged(Control control)
        {
            
            int margin = (int)(control.Width * 0.10) / 5;
            int width = (int)(control.Width * 0.16);
            int newLeftLocation = (int)(control.Width * 0.10) / 2;
            newLeftLocation += (int)(margin * .6);
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control item = control.Controls[i];
                if (item.InvokeRequired)
                    Invoke(new Action(() => {
                        if (item is Guna2TileButton)
                        {
                            item.Width = width;
                            item.Location = new Point(newLeftLocation, item.Top);
                            newLeftLocation += item.Width;
                            newLeftLocation += margin;
                        }
                        else
                        {
                            var fontSize = this.CreateGraphics().MeasureString(item.Controls[0].Text, item.Controls[0].Font).ToSize();
                            item.Controls[0].Location = new Point((item.Width - fontSize.Width) / 2, (item.Height - fontSize.Height) / 2);
                        }
                    }));
                else
                {
                    if (item is Guna2TileButton)
                    {
                        item.Width = width;
                        item.Location = new Point(newLeftLocation, item.Top);
                        newLeftLocation += item.Width;
                        newLeftLocation += margin;
                    }
                    else
                    {
                        var fontSize = this.CreateGraphics().MeasureString(item.Controls[0].Text, item.Controls[0].Font).ToSize();
                        item.Controls[0].Location = new Point((item.Width - fontSize.Width) / 2, (item.Height - fontSize.Height) / 2);
                    }
                }
            }

        }
    }
}
