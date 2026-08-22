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

namespace AccountingSystem.core.shared
{
    public partial class DialogShowDetailsRecorde : Form
    {List<string> columnName = new List<string>();
        DataGridViewRow recorde;


        public DialogShowDetailsRecorde(List<string> columnName, DataGridViewRow recorde)
        {
            InitializeComponent();
            this.columnName = columnName;
            this.recorde = recorde;

        }
        private void DialogShowDetailsRecorde_Load(object sender, EventArgs e)
        {
            if (recorde != null)
            {
                guna2GroupBox1.Text += recorde.Cells[0].Value;
                for (int i = 1; i < columnName.Count; i++)
                {

                    guna2DataGridView1.Rows.Add();
                    guna2DataGridView1.Rows[i - 1].Cells[0].Value = columnName[i];
                    guna2DataGridView1.Rows[i - 1].Cells[1].Value = recorde.Cells[i].Value;
                }
            }
            else
            {
                AppDialogAleart.showAleartError("لم تقم بتحديد اي بيانات للعرض ");
                this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
