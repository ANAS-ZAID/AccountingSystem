using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using AccountingSystem.core.Functions;

namespace AccountingSystem.core.shared
{
    public static class ExtensionsFunctionsUI
    {
        public static int TopPadding(this Form form) => (int)(form.Height * .03);
        public static void ReportFelx(this FlowLayoutPanel layoutPanel)
        {
            layoutPanel.AutoSizeMode =AutoSizeMode.GrowOnly;    
            layoutPanel.SizeChanged+= ReportSizeChanged;
        }
        public static void ReportSizeChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel flowLayout = (FlowLayoutPanel)sender;
            int heightChaild = 0;
            int indexTable = -1;
            for (int i = 0; i < flowLayout.Controls.Count; i++)
            {
                System.Windows.Forms.Control control = flowLayout.Controls[i];

                if (!(control is ReportViewer))
                {
                    if (!(control is Guna2CircleButton))
                    {   control.Width = flowLayout.Width - 5;
                        if (control.Visible)
                            heightChaild += control.Height;
                    }
                }
                else
                {

                    control.Width = flowLayout.Width - 50;
                    indexTable = i;
                }

            }
            if (indexTable > 0)
                flowLayout.Controls[indexTable].Height = flowLayout.Height - heightChaild - 20;
        }
        static public void Active(this Guna2Button button)
        {
            button.ShadowDecoration.Color = AppColor.btnShadowColor;
            button.FillColor = AppColor.primary;
            button.ForeColor = Color.FromArgb(245, 250, 254);
            button.Tag = "active";

        }
        static public void Inactive(this Guna2Button button)
        {
            button.ShadowDecoration.Color = AppColor.btnShadowColor;
            button.FillColor = Color.FromArgb(245, 250, 254);
            button.ForeColor = AppColor.primary;
            button.Tag = null;
        }
        static public void EnabledShowOrHideToolBar(this Guna2CircleButton button)
        {
            button.Click += BtnShowOrHideToolBar_Click;
        }

        private static void BtnShowOrHideToolBar_Click(object sender, EventArgs e)
        {
           
                Guna2CircleButton senderButton = (Guna2CircleButton)sender;
                // ToolTip toolTi;
                Form form = senderButton.FindForm();
                ReportViewer reportViewer = new ReportViewer();
                System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
                foreach (var item in senderButton.Parent.Controls)
                {
                    if (item is ReportViewer)
                        reportViewer = (ReportViewer)item;
                }
                foreach (var item in form.Controls)
                {
                    if (item is System.Windows.Forms.ToolTip)
                        toolTip = (System.Windows.Forms.ToolTip)item;
                }

                if (senderButton.Tag == null)
                {
                    reportViewer.ShowToolBar = true;
                    senderButton.Top = reportViewer.Top+22;
                    senderButton.Image = Properties.Resources.TablerArrowBadgeUpFilled;
                    senderButton.Tag = "active";
                    toolTip.SetToolTip(senderButton, "إخفاء");
                    //AppDialogAleart.showAleartNoPermissions(guna2CircleButton1.Parent.Name);
                }
                else
                {
                    reportViewer.ShowToolBar = false;
                    senderButton.Top = reportViewer.Top+1;
                    senderButton.Image = Properties.Resources.TablerArrowBadgeDownFilled;
                    senderButton.Tag = null;
                    toolTip.SetToolTip(senderButton, "عرض المزيد من خيارات الطباعه");
                }
           
        }
    }


    public class FunctionsGUI
    { bool displayOptionSearchAndPrint = false;
        Guna2Panel paneleOptionSearch;
        LinkLabel linkLabe;

        int max;
        int min = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paneleOptionSearch"></param>
        /// <param name="linkLabe"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// 
  

        public void panelAndLinkOptionSearch(Guna2Panel paneleOptionSearch, LinkLabel linkLabe ,int max=58,int min=0)
        {
          
            this.linkLabe = linkLabe;
            this.paneleOptionSearch = paneleOptionSearch;
            this.max = max;
            this.min = min; 
        }
        public void displayOrHaideOptionSearch()
        {
            if (displayOptionSearchAndPrint)
            {
                paneleOptionSearch.Height = min;
                linkLabe.Text = "عرض خيارات البحث و الطباعه";
                displayOptionSearchAndPrint = false;

            }
            else
            {
                paneleOptionSearch.Height = max;
                linkLabe.Text = "إخفاء خيارات البحث و الطباعه";
                displayOptionSearchAndPrint = true;
            }
        }
        static public void changeColorActiveBtn(Guna2Button button)
        {
            button.ShadowDecoration.Color = AppColor.third;
            button.FillColor = AppColor.primary;
            button.ForeColor = AppColor.third;
            button.Tag = "active";


        }
     
        static public void changeBtnToActiveOrUnActive(Guna2Button button)
        {
            if (button.Tag==null)
            {
                changeColorActiveBtn(button);
            }
            else
            {
                reChangeColorActiveBtn(button);
                button.ShadowDecoration.Color = AppColor.btnShadowColor;
            }


        }
        static public void reChangeColorActiveBtn(Guna2Button button)
        {
            button.ShadowDecoration.Color = AppColor.secondary;
            button.FillColor = AppColor.defaultColor;
            button.ForeColor = AppColor.primary;
            button.Tag = null;
        }

      public  static AppTableStyle TableDetailsStyle => new AppTableStyle()
        {
            flex = true,
            BtnsTable = new BtnsTable() { AddBtn = new BtnTable() { Show = true }, DeleteBtn = new BtnTable() { Show = true } },
        };
    }
}
