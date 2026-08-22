
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
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;

namespace AccountingSystem.view.SupScreens.AreaManagement
{
    public partial class DialogAddAndUpdateArea : Form
    {
        AreaController controller;
        public DialogAddAndUpdateArea(AreaController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
     
            comboBoxCity.DataSource = controller.allCity;
            areaName.TextOnly();
            comboBoxCity.TextOnly();
            setComboBox();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
        }
        private void fillFeild()
        {
            areaName.Text = controller.temp.name;
        }
        void setComboBox()
        {
            comboBoxCity.SelectedItem = controller.temp.City;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            setComboBox();
           if (controller.dataProcessing(areaName.Text))
              this.Close();
            
        }
        private void clearFieldAndReferesh()
        {
           controller.clearTempData();
            areaName.Clear();
            setComboBox();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void btnReferesh_Click(object sender, EventArgs e)
        {
            clearFieldAndReferesh();
        }

        private void comboBoxCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCity(comboBoxCity.SelectedItem);
        }

        private void addNewCity_Click(object sender, EventArgs e)
        {CityController cityController =new CityController();
            cityController.showDialogAdd();
            comboBoxCity.DataSource = controller.allCity;
           comboBoxCity.SelectedItem= controller.allCity.LastOrDefault();
            controller.selectedCity(comboBoxCity.SelectedItem);
            setComboBox();

        }

        private void DialogAddAndUpdateArea_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }
    }
}
