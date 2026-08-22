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
using AccountingSystem.core.shared;
using AccountingSystem.view.Screens.CustmoreMangement;
using AccountingSystem.view.SupScreens.AccountGruop;
using AccountingSystem.view.SupScreens.SupplierManagement;

namespace AccountingSystem.view.Screens.supliresAndCustmoreSystem
{
    public partial class supliresAndCustmoreSystemScreen : Form
    {

        public supliresAndCustmoreSystemScreen()
        {
            InitializeComponent();
    
        }

        private void btnGoCustmoreMangement_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new CustmoreMangementScreen(), changeColorActivBtn: false);
        }

        private void btnGoSupplierManagement_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new SupplierManagementScreen(), changeColorActivBtn: false);
        }

        private void supliresAndCustmoreSystemScreen_SizeChanged(object sender, EventArgs e)
        {
            int newLeftLocation = (int)(Width * .01);
            int widght = 430 + 700;
            if (Width < widght)
            {
                panelSupplierManagement.Width = (int)(Width * .35) - (int)(Width * .01);
                panelCustmoreMangement.Width = (int)(Width *.65) - (int)(Width * .01);
            }
            else
            {
                panelSupplierManagement.Width = 420;

                panelCustmoreMangement.Width = 690;
            }
            newLeftLocation=( Width-(panelCustmoreMangement.Width + panelSupplierManagement.Width)) / 2;
            panelSupplierManagement.Location = new Point(newLeftLocation, this.TopPadding());
            newLeftLocation +=panelSupplierManagement.Width+ (int)(Width * .012);
            panelCustmoreMangement.Location = new Point(newLeftLocation, this.TopPadding());
        }

        private void panelCustmoreMangement_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int margin = (int)(control.Width * 0.30) / 3;
            int width = (int)(control.Width * 0.35);
            int newLeftLocation = 0;
            for (int i = 0; i < control.Controls.Count; i++)
            {

                Control item = control.Controls[i];
                if (item is Guna2TileButton)
                {   newLeftLocation += margin;
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
        }

        private void panelSupplierManagement_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int margin = (int)(control.Width * 0.60) / 2;
            int width = (int)(control.Width * 0.40);
            int newLeftLocation = 0;
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
        }
    }
}
