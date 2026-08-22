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
using AccountingSystem.core.shared;
using AccountingSystem.view.SupScreens.AccountGruop;
using AccountingSystem.view.SupScreens.CompoundJournalEntries;
using AccountingSystem.view.SupScreens.MoneyExchange;
using AccountingSystem.view.SupScreens.Receipt;
using AccountingSystem.view.SupScreens.SimpleJournalEntries;

namespace AccountingSystem
{
    public partial class financialSecuritiesSystem : Form
    {
  
        public financialSecuritiesSystem()
        {
            InitializeComponent();
          
        }
        private void btnGoReceipt_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new ReceiptScreen(TransactionType.سند_قبض), changeColorActivBtn: false);
        }

        private void btnGoExpande_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new ReceiptScreen(TransactionType.سند_صرف), changeColorActivBtn: false);
        }

        private void btnGoSimpleJournalEntries_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new SimpleJournalEntriesScreen(), changeColorActivBtn: false);
        }

        private void btnGoCompoundJournalEntries_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new CompoundJournalEntriesScreen(TransactionType.قيد_مركب), changeColorActivBtn: false);
        }

        private void btnGoOpeningBalances_Click(object sender, EventArgs e)
        {
            Program.homeScereen().openChildForm(new CompoundJournalEntriesScreen(TransactionType.رصيد_إفتتاحي), changeColorActivBtn: false);

        }

        private void btnGoMoneyExchange_Click(object sender, EventArgs e)
        {
              Program.homeScereen().openChildForm(new MoneyExchangeScreen(), changeColorActivBtn: false    );
        }

        private void financialSecuritiesSystem_SizeChanged(object sender, EventArgs e)
        {
            if (Width < 1160)
                panelBtns.Width = Width - (int)(Width * .01);
            else
                panelBtns.Width = 1150;
            panelBtns.Location = new Point((Width - panelBtns.Width) / 2,this.TopPadding());
           
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
            int margin = (int)(control.Width * 0.05) / 6;
            int width = (int)(control.Width * 0.15);
            int newLeftLocation = (int)(control.Width * 0.05) / 2;
            newLeftLocation += (int)(margin * .8);
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
