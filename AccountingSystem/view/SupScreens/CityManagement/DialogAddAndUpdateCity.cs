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

namespace AccountingSystem.view.SupScreens.CityManagement
{
    public partial class DialogAddAndUpdateCity : Form
    {
        CityController controller;
     
        public DialogAddAndUpdateCity( CityController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
            cityName.TextOnly();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
        }
        private void fillFeild()
        {
            cityName.Text = controller.temp.name;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
              if(controller.dataProcessing(cityName.Text))
                this.Close();
      
        }
        private void clearFieldAndReferesh()
        {
           controller.clearTempData();
            cityName.Clear();

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void DialogAddAndUpdateCity_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }
    }
}
