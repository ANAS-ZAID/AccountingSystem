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


namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class DialogAddOrUpdateFirstPeriodStock : Form
    {

        FirstPeriodStockController controller;
        public DialogAddOrUpdateFirstPeriodStock(FirstPeriodStockController controller)
        {
            InitializeComponent();
          this.controller = controller;
            var style = new AppTableStyle() { 
                HeaderStyle=new AppStyle() {BackColor = Color.Transparent, ForColor = AppColor.primary },
                RowStyle=new AppStyle() { Size=new Size() { Height=40} }
            };
            customTable1.build(controller.dataSourceInventory, style);
        }

        private void footer_SizeChanged(object sender, EventArgs e)
        {
            int margin = (int)(footer.Width * 0.01) / 3;
            int width = (int)(footer.Width * 0.10);
            width = footer.Width < 300 ? (int)(footer.Width * 0.30) : width < 100 ? 97 : width;
            int newLeftLocation = (footer.Width - width * 3) / 2;
            for (int i = 0; i < footer.Controls.Count; i++)
            {
                Control item = footer.Controls[i];
                if (!(item is Label))
                {
                    newLeftLocation += margin;
                    item.Width = width;
                    item.Location = new Point(newLeftLocation, (footer.Height - item.Height) / 2);
                    newLeftLocation += item.Width;
                }
            }
        }

        private void DialogAddOrUpdateFirstPeriodStock_Load(object sender, EventArgs e)
        {
       
            //table.table.BringToFront();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            controller.neDataTable(customTable1.newData);
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
