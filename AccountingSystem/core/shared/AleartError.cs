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
using static AccountingSystem.core.Functions.AppDialogAleart;

namespace AccountingSystem.core.shared
{
    public partial class AleartError : Form
    {
       // bool isShow=false;
      
        public AleartError(string message, MessageType messageType)
        {

            InitializeComponent();
            image.Image = Functions.readImage(SharedData.pathImageBrand);
            this.message.Text = message;
            if(messageType == MessageType.Error)
            {
                btnCancel.Visible = false;
                btnOk.Left = (footer.Width - btnOk.Width) / 2;
            }
           
        }

       

        private void message_ClientSizeChanged(object sender, EventArgs e)
        {
        message.Left=(body.Width-message.Width)/2;
        message.Top=(body.Height-message.Height)/2;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

           DialogResult = DialogResult.OK;
           Close();

        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            DialogResult=DialogResult.Cancel;
            Close();
        }
    }
}
