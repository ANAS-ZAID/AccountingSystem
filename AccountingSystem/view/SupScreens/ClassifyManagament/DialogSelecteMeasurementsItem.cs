using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class DialogSelecteMeasurementsItem : Form
    {
        public static MeasurementsItem selectedMeasurementsItem { get; set; }
        public DialogSelecteMeasurementsItem(List<MeasurementsItem> measurementsItems)
        {
            InitializeComponent();
            fillCardsMeasurementsItem(measurementsItems);
            selectedMeasurementsItem = new MeasurementsItem();

            image.Image=Functions.readImage(SharedData.pathImageBrand);
        }
        private void cardMeasurementsItem_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            Guna2Panel panel = (sender is Guna2Panel) ? (Guna2Panel)control : (Guna2Panel)control.Parent;
            selectedMeasurementsItem = (MeasurementsItem)panel.Tag;
           // AppDialogAleart.showAleartNoPermissions(selectedMeasurementsItem.sellingPrice.ToString() + "kljkkl");
            this.Close();
        }
        void fillCardsMeasurementsItem(List<MeasurementsItem> measurementsItems)
        {
            foreach (var measurementsItem in measurementsItems)
            {
                flowLayoutPanel1.Controls.Add(BuildControls.buildeCardMeasurementsItem(measurementsItem, cardMeasurementsItem_Click));
            }

        }
        static public MeasurementsItem DialogSelectedMeasurementsItem(object value)
        {
            MeasurementsItem measurementsItem = null;
            if (value is Classify)
            {
                Classify item = (Classify)value ?? null;
                if (item.id != 0)
                {
                    if (item.MeasurementsItems.Count > 1)
                    {
                        DialogSelecteMeasurementsItem dialogSelectedMeasurementsItem = new DialogSelecteMeasurementsItem(item.MeasurementsItems.ToList());
                        dialogSelectedMeasurementsItem.ShowDialog();

                    }
                    else
                    {
                        DialogSelecteMeasurementsItem.selectedMeasurementsItem = item.MeasurementsItems.FirstOrDefault();
                    }
                    measurementsItem = DialogSelecteMeasurementsItem.selectedMeasurementsItem;
                }
            }
            return measurementsItem;
        }

    }
}
