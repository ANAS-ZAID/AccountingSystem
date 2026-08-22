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

namespace AccountingSystem.view.SupScreens.MoneyExchange
{
    public partial class DialogAddAndUpdateMoneyExchange : Form
    {
        public DialogAddAndUpdateMoneyExchange(ProsessesType prosessesType, string name)
        {
            InitializeComponent();
            labelTiel.Text = name;  
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
