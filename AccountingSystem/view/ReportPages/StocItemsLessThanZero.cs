using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.ReportPages
{
    public partial class StocItemsLessThanZero : Form
    {
        StocItemsLessThanZeroController controller; 
        public StocItemsLessThanZero()
        {
            InitializeComponent();
            controller = new StocItemsLessThanZeroController();
        }

        private void StocItemsLessThanZero_Load(object sender, EventArgs e)
        {
            // if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                btnShowOrHideToolBar.EnabledShowOrHideToolBar();
               
                store.DataSource = controller.stores;
               
                number.NumberOnly();
                store.TextOnly();
                panelBody.ReportFelx();
                setComboBox();
               ((Label)number.Tag).Visible = true;
            }
            controller.HasDataProcessed = true;
            //  else AppDialogAleart.showAleartNoPermissions();
        }
        private void setComboBox()
        {
          
            store.SelectedItem = controller.store;
        }
        void lodeData()
        {

           dataSetStocItemsLessThanZeroBindingSource.DataSource = controller.dataSource.DataSource;

            this.reportViewer1.RefreshReport();

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //  if (model.LoginData.permissions["accountStatement"].viewPermission.Value)
            {
                if (controller.search(int.Parse(number.Text)))
                {
                    lodeData();
                }
                //   else AppDialogAleart.showAleartError("لم يتم العثور على بيانات");

            }
            //else AppDialogAleart.showAleartNoPermissions();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { reportViewer1.PrintDialog(); } catch (Exception) { }
        }
        private void store_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedStore(store.SelectedItem);
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Back&& number.Text.Length == 1)
                e.Handled = true;
        }
    }
}
