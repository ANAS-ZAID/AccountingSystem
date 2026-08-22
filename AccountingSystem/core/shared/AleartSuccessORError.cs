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
    public partial class AleartSuccessORError : Form
    {
        bool isShow = false;
        public AleartSuccessORError(MessageType messageType,string message= "فشلت العمليه")
        { HomeScereen homeScereen = Program.homeScereen();
            InitializeComponent();
            if(messageType == MessageType.Error)
            {
              this.  message.Text =message;
                BackColor = Color.Red;
            }
            
            Width=homeScereen.Width-20;
            Top=homeScereen.Top-8;
            Location = new Point(homeScereen.Left+10,(homeScereen.Height -Height)+Top);
            this.message.Top =(Height - this.message.Height)  / 2;
            this.message.Left = (Width - this.message.Width)-5;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {// إنشاء كائن NotificationIcon
            //NotifyIcon notifyIcon = new NotifyIcon();
            //notifyIcon.Icon = SystemIcons.Information;
            //notifyIcon.Visible = true;

            //// عرض بالون منبثق
            //notifyIcon.ShowBalloonTip(5000, "عنوان الإشعار", "حمووود", ToolTipIcon.Info);
            if (!isShow)
            {
                isShow = true;
            }
            else
            {
                this.Close();
            }
        }
    }
}
