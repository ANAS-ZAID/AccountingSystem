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
using AccountingSystem.view.SupScreens.AreaManagement;

namespace AccountingSystem.view.Screens.BranchManagement
{
    public partial class DialogAddBramch : Form
    {
       
        BranchController controller;
        public DialogAddBramch(BranchController controller)
        {
            InitializeComponent();
            labelTitel.Text = Functions.getCurrentRoot();
            this.controller = controller;
         
           
        }
        private void DialogAddBramch_Load(object sender, EventArgs e)
        {
            comboBoxCity.DataSource = controller.allCity;
            comboBoxArea.DataSource = controller.allArea;
            comboBoxStore.DataSource = controller.allStore;
            branchName.TextOnly();
            administratorName.TextOnly();
            phoneNumber.NumberOnly();
            comboBoxCity.TextOnly();
            comboBoxArea.TextOnly();
            comboBoxStore.TextOnly();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
            setComboBox();
        }
        private void fillFeild()
        {
            branchName.Text = controller.temp.name;
            administratorName.Text = controller.temp.administratorName;
            phoneNumber.Text = controller.temp.phoneNumber;
            address.Text = controller.temp.address;
        }
        void setComboBox()
        {
            comboBoxCity.SelectedItem = controller.temp.City;
            comboBoxArea.SelectedItem = controller.temp.Area;
            comboBoxStore.SelectedItem = controller.temp.Area;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            setComboBox();
            if (controller.dataProcessing(branchName.Text, administratorName.Text, phoneNumber.Text, address.Text)) 
                this.Close();
        }
        private void clearFieldAndReferesh()
        {
           controller.clearTempData();
            branchName.Clear();
            administratorName.Clear();
            phoneNumber.Clear();
            address.Clear();
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

        private void comboBoxArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedArea(comboBoxArea.SelectedItem);

        }

        private void comboBoxStore_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStorey(comboBoxStore.SelectedItem);
        }

        private void addNewArea_Click(object sender, EventArgs e)
        { AreaController areaController =new AreaController();
            areaController.showDialogAdd();
            comboBoxArea.DataSource = controller.allArea;
            comboBoxArea.SelectedItem = controller.allArea.LastOrDefault();
            controller.selectedArea(comboBoxArea.SelectedItem);
            setComboBox();
        }

        private void addNewCity_Click(object sender, EventArgs e)
        {
            CityController cityController = new CityController();
            cityController.showDialogAdd();
            comboBoxCity.DataSource = controller.allCity;
            comboBoxCity.SelectedItem = controller.allCity.LastOrDefault();
            controller.selectedCity(comboBoxCity.SelectedItem);
            setComboBox();
        }

        private void DialogAddBramch_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }
    }
}
