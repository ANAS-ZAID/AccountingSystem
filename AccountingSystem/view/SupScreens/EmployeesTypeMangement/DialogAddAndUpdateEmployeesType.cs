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
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.EmployeesTypeMangement
{
    public partial class DialogAddAndUpdateEmployeesType : Form
    {
        EmployeesTypeController controller;
        public DialogAddAndUpdateEmployeesType()
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
           controller = new EmployeesTypeController();
            groupName.TextOnly();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
        }
        private void fillFeild()
        {
            groupName.Text = controller.temp.name;
        }
        private void DialogAddAndUpdateEmployeesType_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (controller.prosessesType == ProsessesType.add)
                if (!controller.add(groupName.Text))
                    return;
            if (controller.prosessesType == ProsessesType.update)
                if (!controller.update(groupName.Text))
                    return;
            this.Close();
        }
        private void clearFieldAndReferesh()
        {
            groupName.Clear();
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
