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

namespace AccountingSystem.view.ReportPages
{
    public partial class AccountStatementScreen : Form
    {
        public AccountStatementScreen()
        {
            InitializeComponent();

        }

        private void AccountStatementScreen_SizeChanged(object sender, EventArgs e)
        {
            int newLeftLocation = (int)(Width * .01);
            int widght = 510 + 510;
            int margin = (int)(Width * .030);
            if (Width < widght)
            {
                panelAccountStatementWithHours.Width = (int)(Width * .50) - (int)(Width * .02);
                panelAccountStatement.Width = (int)(Width * .50) - (int)(Width * .02);
            }
            else
            {
                panelAccountStatementWithHours.Width = widght/2;
                panelAccountStatement.Width = widght/2;
                
            }
            newLeftLocation = (Width - (panelAccountStatementWithHours.Width+panelAccountStatement.Width+margin*2) ) / 2+margin/2;
            panelAccountStatementWithHours.Location = new Point(newLeftLocation, this.TopPadding());
            newLeftLocation += panelAccountStatementWithHours.Width + margin;
            panelAccountStatement.Location = new Point(newLeftLocation, this.TopPadding());
        }

        private void panelAccountStatement_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int margin = (int)(control.Width * 0.50) / 2;
            int width = (int)(control.Width * 0.50);
            int newLeftLocation =0;
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

        private void btnGoAccountStatement_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new AccountStatement(), changeColorActivBtn: false);
        }

        private void btnGoAccountStatementWithHours_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new AccountStatementWithNumberHours(), changeColorActivBtn: false);
        }
    }
}
