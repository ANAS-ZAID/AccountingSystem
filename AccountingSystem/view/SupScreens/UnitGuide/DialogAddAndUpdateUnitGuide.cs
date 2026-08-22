using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.SupScreens.UnitGuide
{
    public partial class DialogAddAndUpdateUnitGuide : Form
    {
        UnitGuideController controller;
        public DialogAddAndUpdateUnitGuide(UnitGuideController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
            name.TextOnly();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
        }
        private void fillFeild()
        {
            name.Text = controller.tempUnit.name;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(name.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(name.Text))
                    return;
            this.Close();
        }
        private void clearFieldAndReferesh()
        {
            name.Clear();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

         
        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }
    }
}
