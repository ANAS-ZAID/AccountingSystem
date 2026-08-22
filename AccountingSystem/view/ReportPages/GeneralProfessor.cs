using Microsoft.Reporting.WinForms;
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

namespace AccountingSystem.view.ReportPages
{
    public partial class GeneralProfessor : Form
    {
        GeneralProfessorController controller;
        public GeneralProfessor(AccountLocations accountLocations)
        {
            InitializeComponent();
            controller = new GeneralProfessorController(accountLocations);
            controller.search();

        }
        private void GeneralProfessor_Load(object sender, EventArgs e)
        {

            //if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            //{

                currency.DataSource = controller.currencies;
                group.DataSource = controller.groups;
                startDate.Value = DateTime.Now; endDate.Value = DateTime.Now;
            panelBody.ReportFelx();
                //rowCount.NumberOnly();
                currency.TextOnly();
                group.TextOnly();
          //  reportViewer1.LocalReport.DataSources.Add(new ReportDataSource() { Name = "DataSet1", Value = controller.dataSource.DataSource });
            btnShowOrHideToolBar.EnabledShowOrHideToolBar();
                setComboBox();
            //}
            //else AppDialogAleart.showAleartNoPermissions();

          
        }
        private void setComboBox()
        {
            currency.SelectedItem = controller.currency;
            group.SelectedItem = controller.group;
            controller.selectedStartDate(null);
            controller.selectedEndDate(null);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
           // if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            //{
                if (controller.search())
                {
                    lodeData();
                }
              //  else AppDialogAleart.showAleartError("لم يتم العثور على بيانات");

            //}
            //else AppDialogAleart.showAleartNoPermissions();
        }
        void lodeData()
        {

            dataSetGeneralProfessorBindingSource.DataSource = controller.dataSource.DataSource;
            
           reportViewer1.LocalReport.SetParameters(new ReportParameter("showOpeningBalance", (controller.startDate == null).ToString()));
            this.reportViewer1.RefreshReport();

        }

        private void currency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCurrency(currency.SelectedItem);
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedStartDate(startDate.Value);
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            controller.selectedEndDate(endDate.Value);
        }

        private void group_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedGroup(group.SelectedItem);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { reportViewer1.PrintDialog(); } catch (Exception) { }
        }


    }
}
