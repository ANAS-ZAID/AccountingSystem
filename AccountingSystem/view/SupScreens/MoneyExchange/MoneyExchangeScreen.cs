using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.view.SupScreens.CompoundJournalEntries;

namespace AccountingSystem.view.SupScreens.MoneyExchange
{
    public partial class MoneyExchangeScreen : Form
    {
        bool displayOptionSearchAndPrint = false;
        public MoneyExchangeScreen()
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
                PanelOptionSearchAndPrint.Height = 110;
                linkDisplayOptionsSearchAndPrint.Text = "إخفاء خيارات البحث و الطباعه";
                displayOptionSearchAndPrint = true;
            }
        }

        private void btnShowDialogAddMoneyExchange_Click(object sender, EventArgs e)
        {
            DialogAddAndUpdateMoneyExchange dialogAddAndUpdate = new DialogAddAndUpdateMoneyExchange(core.shared.ProsessesType.add, " اضافة عملية بيع وشراءالعمل");
            dialogAddAndUpdate.ShowDialog();
        }

        private void btnShowDialogUpdateMoneyExchange_Click(object sender, EventArgs e)
        {
            DialogAddAndUpdateMoneyExchange dialogAddAndUpdate = new DialogAddAndUpdateMoneyExchange(core.shared.ProsessesType.update, " تعديل عملية بيع وشراءالعمل");
            dialogAddAndUpdate.ShowDialog();
        }

        private void btnShowDialogViewMoneyExchange_Click(object sender, EventArgs e)
        {
            DialogAddAndUpdateMoneyExchange dialogAddAndUpdate = new DialogAddAndUpdateMoneyExchange(core.shared.ProsessesType.view, " عرض عملية بيع وشراءالعمل");
            dialogAddAndUpdate.ShowDialog();
        }
    }
}
