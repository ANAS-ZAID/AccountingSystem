using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.core.CustomControl
{
    public partial class InvoiceSearchScreen : FlowLayoutPanel
    {
         List<ClassifyGroup> groups;
         List<Classify> supItms;
        public Classify selectedItem;
        [Category("Custom")]
        [Browsable(true)]
        public Guna2TextBox searchItem;
        [Category("Custom")]
        [Browsable(true)]
        public FlowLayoutPanel panelBtnCard;
        [Category("Custom")]
        [Browsable(true)]
        public FlowLayoutPanel panelBtnGroup;
        [Category("Custom")]
        [Browsable(true)]
        public FlowLayoutPanel panelSearchItem;
        [Category("Custom")]
        [Browsable(true)]
        public Guna2TileButton btnActiveGroup;
        [Category("Custom")]
        [Browsable(true)]
        public EventHandler btnCardItem_Click;
        public InvoiceSearchScreen()
        {
            InitializeComponent();
            //EventHandlerBtnCardItem_Click = eventHandler;
            Initializ();

         
           
        }

       

       

        void Initializ()
        {
            selectedItem=new Classify();
            this.Controls.Add(this.PanelSearchItem());
            this.Controls.Add(this.PanelBtnGroup());
            this.Controls.Add(PanelBtnCard());
            this.Location = new System.Drawing.Point(1321, 3);
            this.Name = "reightScreen";
            this.Size = new System.Drawing.Size(600, 606);
            this.TabIndex = 4;
            this.SizeChanged += new System.EventHandler(this.reightScreen_SizeChanged);
            btnCardItem_Click = BtnCardItem_Click;
        }
        private void reightScreen_SizeChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            int childeHeight = 0;
            foreach (Control item in control.Controls)
            {
                item.Width = control.Width;
                if (item.Name != "panelBtnCard")
                    childeHeight += item.Height;
            }
            control.Controls[0].Controls[0].Width = (int)(control.Controls[0].Width * 0.35);
            control.Controls[2].Height = control.Height - childeHeight;

            //   control.Controls[1].Height = (int)(control.Height * 0.9);
        }
        FlowLayoutPanel PanelBtnCard()
        {
            panelBtnCard = new FlowLayoutPanel();
            panelBtnCard.AutoScroll = true;
            panelBtnCard.Location = new System.Drawing.Point(0, 129);
            panelBtnCard.Name = "panelBtnCard";
            panelBtnCard.Size = new System.Drawing.Size(597, 198);
            panelBtnCard.TabIndex = 5;
            return panelBtnCard;
        }
        FlowLayoutPanel PanelBtnGroup()
        {
            panelBtnGroup = new FlowLayoutPanel();
            panelBtnGroup.AutoScroll = true;
            panelBtnGroup.Location = new System.Drawing.Point(283, 65);
            panelBtnGroup.Name = "panelBtnGroup";
            panelBtnGroup.Size = new System.Drawing.Size(314, 58);
            panelBtnGroup.TabIndex = 5;
            panelBtnGroup.WrapContents = false;
            return panelBtnGroup;
        }
        FlowLayoutPanel PanelSearchItem()
        {
            panelSearchItem = new FlowLayoutPanel();
            panelSearchItem.Controls.Add(SearchItem());
            panelSearchItem.Location = new System.Drawing.Point(224, 3);
            panelSearchItem.Name = "panelSearchItem";
            panelSearchItem.Size = new System.Drawing.Size(373, 56);
            panelSearchItem.TabIndex = 5;
            return panelSearchItem;
        }
        Guna2TextBox SearchItem()
        {
            searchItem = new Guna2TextBox();
            searchItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            searchItem.BorderRadius = 18;
            searchItem.Cursor = System.Windows.Forms.Cursors.IBeam;
            searchItem.DefaultText = "";
            searchItem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            searchItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            searchItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            searchItem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            searchItem.FocusedState.BorderColor = System.Drawing.Color.Goldenrod;
            searchItem.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            searchItem.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            searchItem.IconLeft = global::AccountingSystem.Properties.Resources.AntDesignSettingFilled;
            searchItem.Location = new System.Drawing.Point(104, 7);
            searchItem.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            searchItem.Name = "searchItem";
            searchItem.PasswordChar = '\0';
            searchItem.PlaceholderText = "";
            searchItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            searchItem.SelectedText = "";
            searchItem.Size = new System.Drawing.Size(264, 43);
            searchItem.TabIndex = 81;
            searchItem.TextChanged += new System.EventHandler(this.searchItem_TextChanged);
            return searchItem;
        }
        private void searchItem_TextChanged(object sender, EventArgs e)
        {
            filterBtnCardItems();
        }
        public void buildeListCardItems()
        {
            panelBtnCardStartInt();
            //for (int i = 0; i < 50; i++)
            {
                foreach (var item in supItms)
                {
                    panelBtnCard.BeginInvoke(new Action(() => panelBtnCard.Controls.Add(BuildControls.buildCardItem(item, btnCardItem_Click))));
                }
            }
            panelBtnCardEndInt();

        }
        string name = "";
        int groupId = 0;
        int lenCardItems = 0;
        public void filterBtnCardItems()
        {
            name = searchItem.Text;
            groupId = int.Parse(btnActiveGroup.Name);
            lenCardItems = panelBtnCard.Controls.Count;
            Thread thread = new Thread(filterItems); thread.Start();

        }

        private void filterItems()
        {
            panelBtnCardStartInt();
            for (int i = 0; i < lenCardItems; i++)
            {
                panelBtnCard.Invoke(new Action(() =>
                {
                    Control btnCard = panelBtnCard.Controls[i];
                    if (btnCard.Tag != null)
                    {
                        Classify item = (Classify)btnCard.Tag;
                        if (item != null)
                        {
                            if ((item.nameAr.Contains(name) || item.nameEn.Contains(name)) && (groupId == 0 ? true : (item.ClassifyGroupId == groupId)))
                            {
                                btnCard.Visible = true;
                            }
                            else
                            {
                                btnCard.Visible = false;
                            }
                        }
                    }
                }));

            }
            panelBtnCardEndInt();
        }
        private void panelBtnGroupsEndInt()
        {
            panelBtnGroup.Invoke(new Action(() =>
            {
                panelBtnGroup.ResumeLayout();
                panelBtnGroup.Show();

            }));
        }

        private void panelBtnGroupsStartInt()
        {
            panelBtnGroup.Invoke(new Action(() =>
            {
                panelBtnGroup.SuspendLayout();
                panelBtnGroup.Hide();

            }));
        }
        void panelBtnCardStartInt()
        {
            panelBtnCard.Invoke(new Action(() =>
            {
                panelBtnCard.Hide();
                panelBtnCard.SuspendLayout();
            }));


        }
        void panelBtnCardEndInt()
        {
            panelBtnCard.Invoke(new Action(() =>
            {
                panelBtnCard.ResumeLayout();
                panelBtnCard.Show();

            }));
        }
        public void buildeListBtnGroup(EventHandler btnCardItem_Click)
        {
          this.btnCardItem_Click+=btnCardItem_Click;
            try
            {
                AccountingDbContext dBContext = new AccountingDbContext();
                {
                    groups = dBContext.ClassifyGroups.ToList();
                    supItms = dBContext.Classifies.Where(a => a.type == "فرعي").ToList();
                }
            } catch { AppDialogAleart.showAleartError(); }

            btnActiveGroup = BuildControls.buildBtnGroupItem(new NewModel.EFModel.ClassifyGroup() { id = 0, name = "الكل" }, btnGroup_Click);
            panelBtnGroup.Controls.Add(btnActiveGroup);
            FunctionsGUI.changeBtnToActiveOrUnActive(btnActiveGroup);
            Thread threadBtnsGroups = new Thread(buildeListBtnsGroups);
            threadBtnsGroups.Start();
            Thread threadBtnsCards = new Thread(buildeListCardItems);
            threadBtnsCards.Start();

        }
        void buildeListBtnsGroups()
        {
            panelBtnGroupsStartInt();
            //for (int i = 0; i < 50; i++)
            {
                foreach (var group in groups)
                {

                    panelBtnGroup.BeginInvoke(new Action(() => panelBtnGroup.Controls.Add(BuildControls.buildBtnGroupItem(group, btnGroup_Click))));
                }
            }
            panelBtnGroupsEndInt();
        }


        private void btnGroup_Click(object sender, EventArgs e)
        {
            FunctionsGUI.changeBtnToActiveOrUnActive(btnActiveGroup);
            btnActiveGroup = (Guna2TileButton)sender;
            FunctionsGUI.changeBtnToActiveOrUnActive(btnActiveGroup);
            filterBtnCardItems();


        }
        private void BtnCardItem_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            selectedItem = (Classify)((control is Guna2Panel) ? control.Tag : control.Parent.Tag);
    
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
