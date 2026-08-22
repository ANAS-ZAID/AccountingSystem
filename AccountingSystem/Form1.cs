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
using AccountingSystem.view.SupScreens.SalesSystem;

namespace AccountingSystem
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
            //tabPage1.Click += TabPage1_Click;
            //tabControl1.Click += TabControl1_Click;
         //   tabPage1.IsAccessible = true;
        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
            //DialogAddAndUpdteSalesSystem dialog = new DialogAddAndUpdteSalesSystem();
            //dialog.MdiParent = this;
            //dialog.Show();
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {
            //DialogAddAndUpdteSalesSystem dialog = new DialogAddAndUpdteSalesSystem();
            //dialog.MdiParent = this;
            //dialog.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

         //   this.reportViewer1.RefreshReport();
        }
    }
}
