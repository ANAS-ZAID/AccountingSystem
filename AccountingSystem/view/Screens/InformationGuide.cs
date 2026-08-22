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
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.view.Screens.BranchManagement;
using AccountingSystem.view.Screens.CurrencyManagement;
using AccountingSystem.view.SupScreens.AreaManagement;
using AccountingSystem.view.SupScreens.CityManagement;

namespace AccountingSystem
{
    public partial class InformationGuide : Form
    {
        
        public InformationGuide()
        {
            
            InitializeComponent();
        }

        private void guna2Cien_Resize(object sender, EventArgs e)
        {
           // int w = guna2Panel2.Width;
           // // MessageBox.Show(((Guna2CustomGradientPanel)sender).Width.ToString());
           // Guna2CustomGradientPanel sen = (Guna2CustomGradientPanel)sender;
           // guna2Cien.Height= (w) + guna2Cien.Height;
           //// sen.Height=(850-sen.Width)+sen.Height;
        }

        private void btnGoCurrencyManagement_Click(object sender, EventArgs e)
        {
        
            Program.homeScereen().openChildForm(new CurrencyManagementScreen(), changeColorActivBtn:false);
      
        }

        private void btnGoBranchManagement_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new BranchManagementScreen(), changeColorActivBtn: false);
        }

        private void btnGoCityManagement_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new CityManagementScreen(), changeColorActivBtn: false);
        }

        private void btnGoAreaManagement_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new AreaManagementScreen(), changeColorActivBtn: false );
        }

        private void InformationGuide_SizeChanged(object sender, EventArgs e)
        {
            if (Width < 1160)
                panelBtns.Width = Width - (int)(Width * .01);
            else
                panelBtns.Width = 1150;
            panelBtns.Location = new Point((Width - panelBtns.Width) / 2 , this.TopPadding());

            //AppDialogAleart.showAleartNoPermissions();
        }

        private void panelBtns_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            Thread thread = new Thread(() => { sizeChanged(control); });
            if (IsHandleCreated)
                thread.Start();
        }

        private void sizeChanged(Control control)
        {
            int margin = (int)(control.Width * 0.20) / 4;
            int width = (int)(control.Width * 0.18);
            int newLeftLocation = (int)(control.Width * 0.08) / 2;
            newLeftLocation += (int)(margin * .6);
            for (int i = 0; i < control.Controls.Count; i++)
            {

                Control item = control.Controls[i];
               if(item.InvokeRequired)
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
