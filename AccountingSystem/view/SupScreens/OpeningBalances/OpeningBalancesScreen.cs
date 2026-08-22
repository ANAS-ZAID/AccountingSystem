using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingSystem.view.SupScreens.OpeningBalances
{
    public partial class OpeningBalancesScreen : Form
    {
        bool displayOptionSearchAndPrint = false;
        public OpeningBalancesScreen()
        {
            InitializeComponent();
        }

        private void linkDisplayOptionsSearchAndPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (displayOptionSearchAndPrint)
            {
                PanelOptionSearchAndPrint.Height = 0;
                linkDisplayOptionsSearchAndPrint.Text = "عرض خيارات البحث و الطباعه";
                displayOptionSearchAndPrint = false;

            }
            else
            {
                PanelOptionSearchAndPrint.Height = 60;
                linkDisplayOptionsSearchAndPrint.Text = "إخفاء خيارات البحث و الطباعه";
                displayOptionSearchAndPrint = true;
            }
        }
    }
}
