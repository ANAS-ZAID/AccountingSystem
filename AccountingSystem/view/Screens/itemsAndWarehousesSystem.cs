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
using AccountingSystem.view.SupScreens.AccountGruop;
using AccountingSystem.view.SupScreens.ClassifyGroup;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using AccountingSystem.view.SupScreens.UnitGuide;
using AccountingSystem.view.SupScreens.WarehouseManagement;

namespace AccountingSystem
{
    public partial class itemsAndWarehousesSystem : Form
    {

        public itemsAndWarehousesSystem()
        {
            InitializeComponent();
         
        }

        private void btnGoClassifyGroup_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new ClassifyGroupScreen(), changeColorActivBtn: false);
        }

        private void btnGoUnitGuide_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new UnitGuideScreen(), changeColorActivBtn: false);
        }

        private void btnGoWarehouseManagement_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new WarehouseManagementScreen(), changeColorActivBtn: false    );
        }

        private void btnGoClassifyManagament_Click(object sender, EventArgs e)
        {
             Program.homeScereen().openChildForm(new ClassifyManagamentScreen(((Guna2TileButton)sender).Name== "btnGoTreeItems"), changeColorActivBtn: false);
        }
        private void btnGoFirstPeriodStock_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new FirstPeriodStock(), changeColorActivBtn: false);
        }
        private void itemsAndWarehousesSystem_SizeChanged(object sender, EventArgs e)
        {
            int newLeftLocation = (int)(Width * .01);
            int widght = 430 + 680;
            int margin = (int)(Width * .030);
            if (Width < widght)
            {
                panelSystemStores.Width = (int)(Width * .30) - (int)(Width * .02);
                panelSystemItems.Width = (int)(Width * .70) - (int)(Width * .02);
            }
            else
            {
                panelSystemStores.Width = 420;
                panelSystemItems.Width = 670;
            }
            newLeftLocation = (Width - (panelSystemItems.Width + panelSystemStores.Width+margin*2 )) / 2 + margin / 2;
            panelSystemStores.Location = new Point(newLeftLocation, this.TopPadding());
            newLeftLocation += panelSystemStores.Width + margin;
            panelSystemItems.Location = new Point(newLeftLocation, this.TopPadding());
        }

        private void panelSystemItems_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int margin = (int)(control.Width * 0.20) / 5;
            int width = (int)(control.Width * 0.20);
            int newLeftLocation = (int)(margin * 0.25);
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

        private void panelSystemStores_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int margin = (int)(control.Width * 0.30) / 3;
            int width = (int)(control.Width * 0.35);
            int newLeftLocation = (int)(margin * 0.20);
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
