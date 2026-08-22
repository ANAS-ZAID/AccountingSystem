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
    public partial class ScreensReportsGeneralProfessor : Form
    {
        public ScreensReportsGeneralProfessor()
        {
            InitializeComponent();
        }

        private void btnAllAccounts_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new GeneralProfessor(AccountLocations.الكل),changeColorActivBtn:false);
        }

        private void btnGoChest_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new GeneralProfessor(AccountLocations.الصناديق), changeColorActivBtn: false);
        }

        private void btnGoSuppliers_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new GeneralProfessor(AccountLocations.الموردين), changeColorActivBtn: false);
        }

        private void btnGoStores_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new GeneralProfessor(AccountLocations.المخازن), changeColorActivBtn: false);
        }

        private void btnGoEmployees_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new GeneralProfessor(AccountLocations.الموظفين), changeColorActivBtn: false);
        }

        private void btnGoCustomers_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new GeneralProfessor(AccountLocations.العملاء), changeColorActivBtn: false);
        }

        private void ScreensReportsGeneralProfessor_SizeChanged(object sender, EventArgs e)
        {
            if (Width < 1160)
                panelGeneralProfessor.Width = Width - (int)(Width * .01);
            else
                panelGeneralProfessor.Width = 1150;
            panelGeneralProfessor.Location = new Point((Width - panelGeneralProfessor.Width) / 2, this.TopPadding());
        }

        private void panelGeneralProfessor_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int margin = (int)(control.Width * 0.05) / 6;
            int width = (int)(control.Width * 0.15);
            int newLeftLocation = (int)(control.Width * 0.05) / 2;
            newLeftLocation += (int)(margin * .8);
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control item = control.Controls[i];
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
