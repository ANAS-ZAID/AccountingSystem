using Guna.UI2.WinForms;

namespace AccountingSystem
{
    partial class HomeScereen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeMyComponent() {

            foreach (System.Windows.Forms.Control item in groupSaideBarTap.Controls)
            {
                if (item.GetType() == typeof(Guna2Panel))
                {
                    foreach (System.Windows.Forms.Control item2 in item.Controls)
                    {
                        if (item2.GetType() == typeof(System.Windows.Forms.Button))
                        {
                            System.Windows.Forms.Button button=item2 as System.Windows.Forms.Button;
                            button.Left = 1;
                            button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(70, 95, 118);

                        }
                    }

                }

            }

            leftBorderBtn=new System.Windows.Forms.Panel();
            leftBorderBtn.Size = new System.Drawing.Size(7, 80);
            
          //  groupSaideBarTap.Controls.Add(leftBorderBtn);
          

        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScereen));
            this.headerSaideBar = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureSaidBar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.panelGroupTapBtns = new Guna.UI2.WinForms.Guna2Panel();
            this.lainBtnSettings = new Guna.UI2.WinForms.Guna2Panel();
            this.lainBtnReports = new Guna.UI2.WinForms.Guna2Panel();
            this.lainBtnSystems = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSystems = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.timerOpenOrCloseSaideBar = new System.Windows.Forms.Timer(this.components);
            this.supTitel = new Krypton.Toolkit.KryptonPanel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelViewRoot = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panelMainBody = new Guna.UI2.WinForms.Guna2Panel();
            this.flowMainBody = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelOptionSearchAndPrint = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnCardCustomer = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator3 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnCardSalse = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnCardPurchases = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel6 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator4 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnCardItems = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator5 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnCardEmpolyee = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel8 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator6 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnCashier = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.titel = new Guna.UI2.WinForms.Guna2Panel();
            this.labelpicturePersonal = new System.Windows.Forms.Label();
            this.picturePersonal = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.labelTitelTopBar = new System.Windows.Forms.Label();
            this.btnOpenOrCloseSaideBarr = new System.Windows.Forms.PictureBox();
            this.saideBar = new Guna.UI2.WinForms.Guna2Panel();
            this.groupSaideBarTap = new Guna.UI2.WinForms.Guna2Panel();
            this.PanelBtnsSettings = new Guna.UI2.WinForms.Guna2Panel();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.PanelBtnsSystems = new Guna.UI2.WinForms.Guna2Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.btnGoPurchasingAndSalesSystem = new System.Windows.Forms.Button();
            this.btnGoFinancialSecurities = new System.Windows.Forms.Button();
            this.btnGoItemsAndWarehouses = new System.Windows.Forms.Button();
            this.btnGoToCustomerAndSupplierSystem = new System.Windows.Forms.Button();
            this.btnGotoAccountingSystemScereen = new System.Windows.Forms.Button();
            this.btnGoToInformationGuide = new System.Windows.Forms.Button();
            this.btnGoToHome = new System.Windows.Forms.Button();
            this.PanelBtnsReports = new Guna.UI2.WinForms.Guna2Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.btnGoReportsInvoicesAndStores = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnGoScreensReportsGeneralProfessor = new System.Windows.Forms.Button();
            this.btnGoAccountStatement = new System.Windows.Forms.Button();
            this.btnGoTreeAccounts = new System.Windows.Forms.Button();
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.headerSaideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSaidBar)).BeginInit();
            this.panelGroupTapBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supTitel)).BeginInit();
            this.supTitel.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.panelMainBody.SuspendLayout();
            this.flowMainBody.SuspendLayout();
            this.PanelOptionSearchAndPrint.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel5.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.guna2Panel6.SuspendLayout();
            this.guna2Panel7.SuspendLayout();
            this.guna2Panel8.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.titel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePersonal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenOrCloseSaideBarr)).BeginInit();
            this.saideBar.SuspendLayout();
            this.groupSaideBarTap.SuspendLayout();
            this.PanelBtnsSettings.SuspendLayout();
            this.PanelBtnsSystems.SuspendLayout();
            this.PanelBtnsReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerSaideBar
            // 
            this.headerSaideBar.Controls.Add(this.label1);
            this.headerSaideBar.Controls.Add(this.pictureSaidBar);
            this.headerSaideBar.Controls.Add(this.panelGroupTapBtns);
            this.headerSaideBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerSaideBar.Location = new System.Drawing.Point(0, 0);
            this.headerSaideBar.Margin = new System.Windows.Forms.Padding(4);
            this.headerSaideBar.Name = "headerSaideBar";
            this.headerSaideBar.Size = new System.Drawing.Size(290, 303);
            this.headerSaideBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.label1.Location = new System.Drawing.Point(111, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "مشروع ";
            // 
            // pictureSaidBar
            // 
            this.pictureSaidBar.ImageRotate = 0F;
            this.pictureSaidBar.Location = new System.Drawing.Point(105, 40);
            this.pictureSaidBar.Name = "pictureSaidBar";
            this.pictureSaidBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pictureSaidBar.Size = new System.Drawing.Size(100, 100);
            this.pictureSaidBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureSaidBar.TabIndex = 3;
            this.pictureSaidBar.TabStop = false;
            this.pictureSaidBar.Click += new System.EventHandler(this.image_Click);
            // 
            // panelGroupTapBtns
            // 
            this.panelGroupTapBtns.Controls.Add(this.lainBtnSettings);
            this.panelGroupTapBtns.Controls.Add(this.lainBtnReports);
            this.panelGroupTapBtns.Controls.Add(this.lainBtnSystems);
            this.panelGroupTapBtns.Controls.Add(this.btnSystems);
            this.panelGroupTapBtns.Controls.Add(this.btnSettings);
            this.panelGroupTapBtns.Controls.Add(this.btnReports);
            this.panelGroupTapBtns.Location = new System.Drawing.Point(0, 202);
            this.panelGroupTapBtns.Margin = new System.Windows.Forms.Padding(4);
            this.panelGroupTapBtns.Name = "panelGroupTapBtns";
            this.panelGroupTapBtns.Size = new System.Drawing.Size(290, 101);
            this.panelGroupTapBtns.TabIndex = 2;
            // 
            // lainBtnSettings
            // 
            this.lainBtnSettings.BorderRadius = 2;
            this.lainBtnSettings.FillColor = System.Drawing.Color.White;
            this.lainBtnSettings.Location = new System.Drawing.Point(36, 88);
            this.lainBtnSettings.Name = "lainBtnSettings";
            this.lainBtnSettings.Size = new System.Drawing.Size(38, 6);
            this.lainBtnSettings.TabIndex = 11;
            this.lainBtnSettings.Tag = "btnSettings";
            this.lainBtnSettings.Visible = false;
            // 
            // lainBtnReports
            // 
            this.lainBtnReports.BorderRadius = 2;
            this.lainBtnReports.FillColor = System.Drawing.Color.White;
            this.lainBtnReports.Location = new System.Drawing.Point(136, 88);
            this.lainBtnReports.Name = "lainBtnReports";
            this.lainBtnReports.Size = new System.Drawing.Size(38, 6);
            this.lainBtnReports.TabIndex = 11;
            this.lainBtnReports.Tag = "btnReports";
            this.lainBtnReports.Visible = false;
            // 
            // lainBtnSystems
            // 
            this.lainBtnSystems.BorderRadius = 2;
            this.lainBtnSystems.FillColor = System.Drawing.Color.White;
            this.lainBtnSystems.Location = new System.Drawing.Point(229, 88);
            this.lainBtnSystems.Name = "lainBtnSystems";
            this.lainBtnSystems.Size = new System.Drawing.Size(38, 6);
            this.lainBtnSystems.TabIndex = 11;
            this.lainBtnSystems.Tag = "btnSystems";
            // 
            // btnSystems
            // 
            this.btnSystems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSystems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSystems.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnSystems.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSystems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSystems.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSystems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnSystems.Image = global::AccountingSystem.Properties.Resources.WhhSystemfolder;
            this.btnSystems.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSystems.Location = new System.Drawing.Point(208, 6);
            this.btnSystems.Margin = new System.Windows.Forms.Padding(4);
            this.btnSystems.Name = "btnSystems";
            this.btnSystems.Size = new System.Drawing.Size(80, 79);
            this.btnSystems.TabIndex = 10;
            this.btnSystems.Tag = "btnSystems";
            this.btnSystems.Text = "أنظمه";
            this.btnSystems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSystems.UseVisualStyleBackColor = false;
            this.btnSystems.Click += new System.EventHandler(this.btnSystems_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnSettings.Image = global::AccountingSystem.Properties.Resources.OouiSettings;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSettings.Location = new System.Drawing.Point(16, 6);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(80, 79);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Tag = "btnSettings";
            this.btnSettings.Text = "إعدادات";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnReports.Image = global::AccountingSystem.Properties.Resources.WhhNews;
            this.btnReports.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReports.Location = new System.Drawing.Point(113, 6);
            this.btnReports.Margin = new System.Windows.Forms.Padding(4);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(80, 79);
            this.btnReports.TabIndex = 6;
            this.btnReports.Tag = "btnReports";
            this.btnReports.Text = "تقارير ";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // timerOpenOrCloseSaideBar
            // 
            this.timerOpenOrCloseSaideBar.Tick += new System.EventHandler(this.timerOpenOrCloseSaideBar_Tick);
            // 
            // supTitel
            // 
            this.supTitel.Controls.Add(this.guna2Panel2);
            this.supTitel.Controls.Add(this.labelViewRoot);
            this.supTitel.Controls.Add(this.guna2PictureBox1);
            this.supTitel.Location = new System.Drawing.Point(19, 4);
            this.supTitel.Margin = new System.Windows.Forms.Padding(4);
            this.supTitel.Name = "supTitel";
            this.supTitel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.supTitel.Size = new System.Drawing.Size(1346, 42);
            this.supTitel.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(144)))), ((int)(((byte)(164)))));
            this.supTitel.TabIndex = 7;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderRadius = 8;
            this.guna2Panel2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2Panel2.Controls.Add(this.label2);
            this.guna2Panel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(179)))), ((int)(((byte)(8)))));
            this.guna2Panel2.Location = new System.Drawing.Point(1139, 7);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(50, 24);
            this.guna2Panel2.TabIndex = 18;
            this.guna2Panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(5, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "2222";
            // 
            // labelViewRoot
            // 
            this.labelViewRoot.AutoSize = true;
            this.labelViewRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(144)))), ((int)(((byte)(164)))));
            this.labelViewRoot.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelViewRoot.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelViewRoot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.labelViewRoot.Location = new System.Drawing.Point(1233, 0);
            this.labelViewRoot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelViewRoot.Name = "labelViewRoot";
            this.labelViewRoot.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.labelViewRoot.Size = new System.Drawing.Size(63, 37);
            this.labelViewRoot.TabIndex = 3;
            this.labelViewRoot.Text = "الروت";
            this.labelViewRoot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(144)))), ((int)(((byte)(164)))));
            this.guna2PictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2PictureBox1.Image = global::AccountingSystem.Properties.Resources.IonHome__2_;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(1296, 0);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Padding = new System.Windows.Forms.Padding(7, 7, 7, 2);
            this.guna2PictureBox1.Size = new System.Drawing.Size(50, 42);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 2;
            this.guna2PictureBox1.TabStop = false;
            // 
            // panelMainBody
            // 
            this.panelMainBody.BackColor = System.Drawing.Color.White;
            this.panelMainBody.Controls.Add(this.flowMainBody);
            this.panelMainBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            this.panelMainBody.Font = new System.Drawing.Font("Tahoma", 12F);
            this.panelMainBody.Location = new System.Drawing.Point(0, 57);
            this.panelMainBody.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainBody.Name = "panelMainBody";
            this.panelMainBody.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.panelMainBody.Size = new System.Drawing.Size(1369, 998);
            this.panelMainBody.TabIndex = 9;
            this.panelMainBody.SizeChanged += new System.EventHandler(this.panelMainBody_SizeChanged);
            // 
            // flowMainBody
            // 
            this.flowMainBody.BackColor = System.Drawing.Color.Transparent;
            this.flowMainBody.Controls.Add(this.supTitel);
            this.flowMainBody.Controls.Add(this.PanelOptionSearchAndPrint);
            this.flowMainBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMainBody.Location = new System.Drawing.Point(0, 0);
            this.flowMainBody.Name = "flowMainBody";
            this.flowMainBody.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowMainBody.Size = new System.Drawing.Size(1369, 998);
            this.flowMainBody.TabIndex = 19;
            this.flowMainBody.SizeChanged += new System.EventHandler(this.flowMainBody_SizeChanged);
            // 
            // PanelOptionSearchAndPrint
            // 
            this.PanelOptionSearchAndPrint.Controls.Add(this.guna2Panel3);
            this.PanelOptionSearchAndPrint.Controls.Add(this.guna2Panel5);
            this.PanelOptionSearchAndPrint.Controls.Add(this.guna2Panel4);
            this.PanelOptionSearchAndPrint.Controls.Add(this.guna2Panel6);
            this.PanelOptionSearchAndPrint.Controls.Add(this.guna2Panel7);
            this.PanelOptionSearchAndPrint.Controls.Add(this.guna2Panel8);
            this.PanelOptionSearchAndPrint.Location = new System.Drawing.Point(16, 53);
            this.PanelOptionSearchAndPrint.Name = "PanelOptionSearchAndPrint";
            this.PanelOptionSearchAndPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PanelOptionSearchAndPrint.Size = new System.Drawing.Size(1350, 937);
            this.PanelOptionSearchAndPrint.TabIndex = 19;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Controls.Add(this.guna2Separator1);
            this.guna2Panel3.Controls.Add(this.btnCardCustomer);
            this.guna2Panel3.Location = new System.Drawing.Point(1083, 3);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(264, 178);
            this.guna2Panel3.TabIndex = 19;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.White;
            this.guna2Separator1.Location = new System.Drawing.Point(20, 68);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(229, 10);
            this.guna2Separator1.TabIndex = 15;
            // 
            // btnCardCustomer
            // 
            this.btnCardCustomer.BackColor = System.Drawing.Color.Transparent;
            this.btnCardCustomer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardCustomer.BorderRadius = 10;
            this.btnCardCustomer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCardCustomer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCardCustomer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCardCustomer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCardCustomer.FillColor = System.Drawing.Color.White;
            this.btnCardCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCardCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardCustomer.HoverState.FillColor = System.Drawing.Color.White;
            this.btnCardCustomer.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardCustomer.HoverState.Image = global::AccountingSystem.Properties.Resources.HeroiconsUsers20Solid;
            this.btnCardCustomer.Image = global::AccountingSystem.Properties.Resources.HeroiconsUsers20Solid;
            this.btnCardCustomer.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCardCustomer.ImageOffset = new System.Drawing.Point(23, -40);
            this.btnCardCustomer.ImageSize = new System.Drawing.Size(34, 34);
            this.btnCardCustomer.Location = new System.Drawing.Point(20, 3);
            this.btnCardCustomer.Name = "btnCardCustomer";
            this.btnCardCustomer.PressedDepth = 0;
            this.btnCardCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCardCustomer.ShadowDecoration.BorderRadius = 15;
            this.btnCardCustomer.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnCardCustomer.ShadowDecoration.Depth = 60;
            this.btnCardCustomer.ShadowDecoration.Enabled = true;
            this.btnCardCustomer.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 0, 10, 10);
            this.btnCardCustomer.Size = new System.Drawing.Size(229, 156);
            this.btnCardCustomer.TabIndex = 14;
            this.btnCardCustomer.Text = "العملاء";
            this.btnCardCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCardCustomer.TextOffset = new System.Drawing.Point(0, 40);
            this.btnCardCustomer.Click += new System.EventHandler(this.btnCardCustomer_Click);
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Controls.Add(this.guna2Separator3);
            this.guna2Panel5.Controls.Add(this.btnCardSalse);
            this.guna2Panel5.Location = new System.Drawing.Point(813, 3);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(264, 178);
            this.guna2Panel5.TabIndex = 19;
            // 
            // guna2Separator3
            // 
            this.guna2Separator3.BackColor = System.Drawing.Color.White;
            this.guna2Separator3.Location = new System.Drawing.Point(19, 68);
            this.guna2Separator3.Name = "guna2Separator3";
            this.guna2Separator3.Size = new System.Drawing.Size(229, 10);
            this.guna2Separator3.TabIndex = 16;
            // 
            // btnCardSalse
            // 
            this.btnCardSalse.BackColor = System.Drawing.Color.Transparent;
            this.btnCardSalse.BorderRadius = 10;
            this.btnCardSalse.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCardSalse.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCardSalse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCardSalse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCardSalse.FillColor = System.Drawing.Color.White;
            this.btnCardSalse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCardSalse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardSalse.HoverState.FillColor = System.Drawing.Color.White;
            this.btnCardSalse.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardSalse.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsShoppingCart;
            this.btnCardSalse.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCardSalse.ImageOffset = new System.Drawing.Point(23, -40);
            this.btnCardSalse.ImageSize = new System.Drawing.Size(34, 34);
            this.btnCardSalse.Location = new System.Drawing.Point(19, 3);
            this.btnCardSalse.Name = "btnCardSalse";
            this.btnCardSalse.PressedDepth = 0;
            this.btnCardSalse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCardSalse.ShadowDecoration.BorderRadius = 15;
            this.btnCardSalse.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnCardSalse.ShadowDecoration.Depth = 60;
            this.btnCardSalse.ShadowDecoration.Enabled = true;
            this.btnCardSalse.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 0, 10, 10);
            this.btnCardSalse.Size = new System.Drawing.Size(229, 156);
            this.btnCardSalse.TabIndex = 12;
            this.btnCardSalse.Text = "مبيعات";
            this.btnCardSalse.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCardSalse.TextOffset = new System.Drawing.Point(0, 40);
            this.btnCardSalse.Click += new System.EventHandler(this.btnCardSalse_Click);
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Controls.Add(this.guna2Separator2);
            this.guna2Panel4.Controls.Add(this.btnCardPurchases);
            this.guna2Panel4.Location = new System.Drawing.Point(543, 3);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(264, 178);
            this.guna2Panel4.TabIndex = 20;
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.BackColor = System.Drawing.Color.White;
            this.guna2Separator2.Location = new System.Drawing.Point(15, 68);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(229, 10);
            this.guna2Separator2.TabIndex = 16;
            // 
            // btnCardPurchases
            // 
            this.btnCardPurchases.BackColor = System.Drawing.Color.Transparent;
            this.btnCardPurchases.BorderRadius = 10;
            this.btnCardPurchases.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCardPurchases.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCardPurchases.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCardPurchases.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCardPurchases.FillColor = System.Drawing.Color.White;
            this.btnCardPurchases.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCardPurchases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardPurchases.HoverState.FillColor = System.Drawing.Color.White;
            this.btnCardPurchases.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardPurchases.Image = global::AccountingSystem.Properties.Resources.MingcuteShoppingBag1Fill;
            this.btnCardPurchases.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCardPurchases.ImageOffset = new System.Drawing.Point(23, -40);
            this.btnCardPurchases.ImageSize = new System.Drawing.Size(34, 34);
            this.btnCardPurchases.Location = new System.Drawing.Point(15, 3);
            this.btnCardPurchases.Name = "btnCardPurchases";
            this.btnCardPurchases.PressedDepth = 0;
            this.btnCardPurchases.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCardPurchases.ShadowDecoration.BorderRadius = 15;
            this.btnCardPurchases.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnCardPurchases.ShadowDecoration.Depth = 60;
            this.btnCardPurchases.ShadowDecoration.Enabled = true;
            this.btnCardPurchases.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 0, 10, 10);
            this.btnCardPurchases.Size = new System.Drawing.Size(229, 156);
            this.btnCardPurchases.TabIndex = 13;
            this.btnCardPurchases.Text = "مشتريات";
            this.btnCardPurchases.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCardPurchases.TextOffset = new System.Drawing.Point(0, 40);
            this.btnCardPurchases.Click += new System.EventHandler(this.btnCardPurchases_Click);
            // 
            // guna2Panel6
            // 
            this.guna2Panel6.Controls.Add(this.guna2Separator4);
            this.guna2Panel6.Controls.Add(this.btnCardItems);
            this.guna2Panel6.Location = new System.Drawing.Point(273, 3);
            this.guna2Panel6.Name = "guna2Panel6";
            this.guna2Panel6.Size = new System.Drawing.Size(264, 178);
            this.guna2Panel6.TabIndex = 19;
            // 
            // guna2Separator4
            // 
            this.guna2Separator4.BackColor = System.Drawing.Color.White;
            this.guna2Separator4.Location = new System.Drawing.Point(17, 68);
            this.guna2Separator4.Name = "guna2Separator4";
            this.guna2Separator4.Size = new System.Drawing.Size(229, 10);
            this.guna2Separator4.TabIndex = 16;
            // 
            // btnCardItems
            // 
            this.btnCardItems.BackColor = System.Drawing.Color.Transparent;
            this.btnCardItems.BorderRadius = 10;
            this.btnCardItems.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCardItems.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCardItems.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCardItems.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCardItems.FillColor = System.Drawing.Color.White;
            this.btnCardItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCardItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardItems.HoverState.FillColor = System.Drawing.Color.White;
            this.btnCardItems.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardItems.Image = global::AccountingSystem.Properties.Resources.BxsCategory;
            this.btnCardItems.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCardItems.ImageOffset = new System.Drawing.Point(23, -40);
            this.btnCardItems.ImageSize = new System.Drawing.Size(34, 34);
            this.btnCardItems.Location = new System.Drawing.Point(17, 3);
            this.btnCardItems.Name = "btnCardItems";
            this.btnCardItems.PressedDepth = 0;
            this.btnCardItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCardItems.ShadowDecoration.BorderRadius = 15;
            this.btnCardItems.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnCardItems.ShadowDecoration.Depth = 60;
            this.btnCardItems.ShadowDecoration.Enabled = true;
            this.btnCardItems.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 0, 10, 10);
            this.btnCardItems.Size = new System.Drawing.Size(229, 156);
            this.btnCardItems.TabIndex = 11;
            this.btnCardItems.Text = "الاصناف";
            this.btnCardItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCardItems.TextOffset = new System.Drawing.Point(0, 40);
            this.btnCardItems.Click += new System.EventHandler(this.btnCardItems_Click);
            // 
            // guna2Panel7
            // 
            this.guna2Panel7.Controls.Add(this.guna2Separator5);
            this.guna2Panel7.Controls.Add(this.btnCardEmpolyee);
            this.guna2Panel7.Location = new System.Drawing.Point(3, 3);
            this.guna2Panel7.Name = "guna2Panel7";
            this.guna2Panel7.Size = new System.Drawing.Size(264, 178);
            this.guna2Panel7.TabIndex = 19;
            // 
            // guna2Separator5
            // 
            this.guna2Separator5.BackColor = System.Drawing.Color.White;
            this.guna2Separator5.Location = new System.Drawing.Point(19, 68);
            this.guna2Separator5.Name = "guna2Separator5";
            this.guna2Separator5.Size = new System.Drawing.Size(229, 10);
            this.guna2Separator5.TabIndex = 16;
            // 
            // btnCardEmpolyee
            // 
            this.btnCardEmpolyee.BackColor = System.Drawing.Color.Transparent;
            this.btnCardEmpolyee.BorderRadius = 10;
            this.btnCardEmpolyee.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCardEmpolyee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCardEmpolyee.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCardEmpolyee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCardEmpolyee.FillColor = System.Drawing.Color.White;
            this.btnCardEmpolyee.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCardEmpolyee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardEmpolyee.HoverState.FillColor = System.Drawing.Color.White;
            this.btnCardEmpolyee.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCardEmpolyee.Image = global::AccountingSystem.Properties.Resources.EntypoUsers__3_;
            this.btnCardEmpolyee.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCardEmpolyee.ImageOffset = new System.Drawing.Point(23, -40);
            this.btnCardEmpolyee.ImageSize = new System.Drawing.Size(34, 34);
            this.btnCardEmpolyee.Location = new System.Drawing.Point(19, 3);
            this.btnCardEmpolyee.Name = "btnCardEmpolyee";
            this.btnCardEmpolyee.PressedDepth = 0;
            this.btnCardEmpolyee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCardEmpolyee.ShadowDecoration.BorderRadius = 15;
            this.btnCardEmpolyee.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnCardEmpolyee.ShadowDecoration.Depth = 60;
            this.btnCardEmpolyee.ShadowDecoration.Enabled = true;
            this.btnCardEmpolyee.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 0, 10, 10);
            this.btnCardEmpolyee.Size = new System.Drawing.Size(229, 156);
            this.btnCardEmpolyee.TabIndex = 11;
            this.btnCardEmpolyee.Text = "الموظفين";
            this.btnCardEmpolyee.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCardEmpolyee.TextOffset = new System.Drawing.Point(0, 40);
            this.btnCardEmpolyee.Click += new System.EventHandler(this.btnCardEmpolyee_Click);
            // 
            // guna2Panel8
            // 
            this.guna2Panel8.Controls.Add(this.guna2Separator6);
            this.guna2Panel8.Controls.Add(this.btnCashier);
            this.guna2Panel8.Location = new System.Drawing.Point(1083, 187);
            this.guna2Panel8.Name = "guna2Panel8";
            this.guna2Panel8.Size = new System.Drawing.Size(264, 178);
            this.guna2Panel8.TabIndex = 19;
            // 
            // guna2Separator6
            // 
            this.guna2Separator6.BackColor = System.Drawing.Color.White;
            this.guna2Separator6.Location = new System.Drawing.Point(18, 78);
            this.guna2Separator6.Name = "guna2Separator6";
            this.guna2Separator6.Size = new System.Drawing.Size(229, 10);
            this.guna2Separator6.TabIndex = 18;
            // 
            // btnCashier
            // 
            this.btnCashier.BackColor = System.Drawing.Color.Transparent;
            this.btnCashier.BorderRadius = 10;
            this.btnCashier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCashier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCashier.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCashier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCashier.FillColor = System.Drawing.Color.White;
            this.btnCashier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCashier.HoverState.FillColor = System.Drawing.Color.White;
            this.btnCashier.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCashier.Image = global::AccountingSystem.Properties.Resources.MemoryChestFill;
            this.btnCashier.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCashier.ImageOffset = new System.Drawing.Point(23, -40);
            this.btnCashier.ImageSize = new System.Drawing.Size(34, 34);
            this.btnCashier.Location = new System.Drawing.Point(18, 11);
            this.btnCashier.Name = "btnCashier";
            this.btnCashier.PressedDepth = 0;
            this.btnCashier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCashier.ShadowDecoration.BorderRadius = 15;
            this.btnCashier.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.btnCashier.ShadowDecoration.Depth = 60;
            this.btnCashier.ShadowDecoration.Enabled = true;
            this.btnCashier.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 0, 10, 10);
            this.btnCashier.Size = new System.Drawing.Size(229, 156);
            this.btnCashier.TabIndex = 17;
            this.btnCashier.Text = "الصناديق";
            this.btnCashier.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCashier.TextOffset = new System.Drawing.Point(0, 40);
            this.btnCashier.Click += new System.EventHandler(this.btnCashier_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.titel);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1369, 57);
            this.guna2Panel1.TabIndex = 5;
            // 
            // titel
            // 
            this.titel.BackColor = System.Drawing.Color.White;
            this.titel.Controls.Add(this.labelpicturePersonal);
            this.titel.Controls.Add(this.picturePersonal);
            this.titel.Controls.Add(this.labelTitelTopBar);
            this.titel.Controls.Add(this.btnOpenOrCloseSaideBarr);
            this.titel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titel.Location = new System.Drawing.Point(0, 0);
            this.titel.Margin = new System.Windows.Forms.Padding(4);
            this.titel.Name = "titel";
            this.titel.Size = new System.Drawing.Size(1369, 57);
            this.titel.TabIndex = 1;
            // 
            // labelpicturePersonal
            // 
            this.labelpicturePersonal.AutoSize = true;
            this.labelpicturePersonal.BackColor = System.Drawing.Color.Transparent;
            this.labelpicturePersonal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpicturePersonal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.labelpicturePersonal.Location = new System.Drawing.Point(107, 9);
            this.labelpicturePersonal.Name = "labelpicturePersonal";
            this.labelpicturePersonal.Size = new System.Drawing.Size(39, 17);
            this.labelpicturePersonal.TabIndex = 6;
            this.labelpicturePersonal.Text = "مرحبا";
            // 
            // picturePersonal
            // 
            this.picturePersonal.BackColor = System.Drawing.Color.Transparent;
            this.picturePersonal.FillColor = System.Drawing.Color.WhiteSmoke;
            this.picturePersonal.Image = global::AccountingSystem.Properties.Resources.SolarUserCircleBold;
            this.picturePersonal.ImageRotate = 0F;
            this.picturePersonal.InitialImage = global::AccountingSystem.Properties.Resources.SolarUserCircleBold;
            this.picturePersonal.Location = new System.Drawing.Point(34, 3);
            this.picturePersonal.Name = "picturePersonal";
            this.picturePersonal.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picturePersonal.Size = new System.Drawing.Size(53, 51);
            this.picturePersonal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturePersonal.TabIndex = 5;
            this.picturePersonal.TabStop = false;
            this.toolTip1.SetToolTip(this.picturePersonal, "أنقر هنا لتحميل الصوره الشخصيه المناسبه لك");
            this.picturePersonal.Click += new System.EventHandler(this.picturePersonal_Click);
            // 
            // labelTitelTopBar
            // 
            this.labelTitelTopBar.AutoSize = true;
            this.labelTitelTopBar.BackColor = System.Drawing.Color.White;
            this.labelTitelTopBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelTitelTopBar.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.labelTitelTopBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.labelTitelTopBar.Location = new System.Drawing.Point(1234, 0);
            this.labelTitelTopBar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitelTopBar.Name = "labelTitelTopBar";
            this.labelTitelTopBar.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelTitelTopBar.Size = new System.Drawing.Size(85, 37);
            this.labelTitelTopBar.TabIndex = 1;
            this.labelTitelTopBar.Text = "العنوان";
            this.labelTitelTopBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpenOrCloseSaideBarr
            // 
            this.btnOpenOrCloseSaideBarr.BackColor = System.Drawing.Color.White;
            this.btnOpenOrCloseSaideBarr.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenOrCloseSaideBarr.Image = global::AccountingSystem.Properties.Resources.menu__2_;
            this.btnOpenOrCloseSaideBarr.Location = new System.Drawing.Point(1319, 0);
            this.btnOpenOrCloseSaideBarr.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenOrCloseSaideBarr.Name = "btnOpenOrCloseSaideBarr";
            this.btnOpenOrCloseSaideBarr.Size = new System.Drawing.Size(50, 57);
            this.btnOpenOrCloseSaideBarr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenOrCloseSaideBarr.TabIndex = 0;
            this.btnOpenOrCloseSaideBarr.TabStop = false;
            this.btnOpenOrCloseSaideBarr.Click += new System.EventHandler(this.btnOpenOrCloseSaideBarr_Click);
            // 
            // saideBar
            // 
            this.saideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.saideBar.Controls.Add(this.groupSaideBarTap);
            this.saideBar.Controls.Add(this.headerSaideBar);
            this.saideBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.saideBar.Location = new System.Drawing.Point(1369, 0);
            this.saideBar.Margin = new System.Windows.Forms.Padding(4);
            this.saideBar.Name = "saideBar";
            this.saideBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.saideBar.Size = new System.Drawing.Size(290, 1055);
            this.saideBar.TabIndex = 6;
            // 
            // groupSaideBarTap
            // 
            this.groupSaideBarTap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.groupSaideBarTap.Controls.Add(this.PanelBtnsSettings);
            this.groupSaideBarTap.Controls.Add(this.PanelBtnsSystems);
            this.groupSaideBarTap.Controls.Add(this.PanelBtnsReports);
            this.groupSaideBarTap.Location = new System.Drawing.Point(0, 304);
            this.groupSaideBarTap.Margin = new System.Windows.Forms.Padding(4);
            this.groupSaideBarTap.Name = "groupSaideBarTap";
            this.groupSaideBarTap.Size = new System.Drawing.Size(290, 743);
            this.groupSaideBarTap.TabIndex = 1;
            // 
            // PanelBtnsSettings
            // 
            this.PanelBtnsSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.PanelBtnsSettings.Controls.Add(this.button23);
            this.PanelBtnsSettings.Controls.Add(this.button24);
            this.PanelBtnsSettings.Controls.Add(this.button25);
            this.PanelBtnsSettings.Controls.Add(this.button26);
            this.PanelBtnsSettings.Location = new System.Drawing.Point(0, 0);
            this.PanelBtnsSettings.Margin = new System.Windows.Forms.Padding(4);
            this.PanelBtnsSettings.Name = "PanelBtnsSettings";
            this.PanelBtnsSettings.Size = new System.Drawing.Size(290, 328);
            this.PanelBtnsSettings.TabIndex = 11;
            this.PanelBtnsSettings.Tag = "btnSettings";
            this.PanelBtnsSettings.Visible = false;
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button23.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button23.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button23.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsRuleSettingsRounded;
            this.button23.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button23.Location = new System.Drawing.Point(0, 165);
            this.button23.Margin = new System.Windows.Forms.Padding(4);
            this.button23.Name = "button23";
            this.button23.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button23.Size = new System.Drawing.Size(290, 80);
            this.button23.TabIndex = 6;
            this.button23.Text = "        اعدادت  متقدمه";
            this.button23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button23.UseVisualStyleBackColor = false;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button24.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button24.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button24.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button24.Image = global::AccountingSystem.Properties.Resources.IcOutlineLock;
            this.button24.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button24.Location = new System.Drawing.Point(0, 246);
            this.button24.Margin = new System.Windows.Forms.Padding(4);
            this.button24.Name = "button24";
            this.button24.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button24.Size = new System.Drawing.Size(290, 80);
            this.button24.TabIndex = 5;
            this.button24.Text = "        الاقفال السنوي";
            this.button24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button24.UseVisualStyleBackColor = false;
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button25.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button25.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button25.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button25.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button25.Image = global::AccountingSystem.Properties.Resources.FaRefresh__2_;
            this.button25.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button25.Location = new System.Drawing.Point(0, 84);
            this.button25.Margin = new System.Windows.Forms.Padding(4);
            this.button25.Name = "button25";
            this.button25.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button25.Size = new System.Drawing.Size(290, 80);
            this.button25.TabIndex = 4;
            this.button25.Text = "         تحديث نظام         ";
            this.button25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button25.UseVisualStyleBackColor = false;
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button26.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button26.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button26.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button26.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button26.Image = global::AccountingSystem.Properties.Resources.TdesignSetting1;
            this.button26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button26.Location = new System.Drawing.Point(0, 3);
            this.button26.Margin = new System.Windows.Forms.Padding(4);
            this.button26.Name = "button26";
            this.button26.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button26.Size = new System.Drawing.Size(290, 80);
            this.button26.TabIndex = 3;
            this.button26.Text = "        اعدادات عامة";
            this.button26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button26.UseVisualStyleBackColor = false;
            // 
            // PanelBtnsSystems
            // 
            this.PanelBtnsSystems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.PanelBtnsSystems.Controls.Add(this.button11);
            this.PanelBtnsSystems.Controls.Add(this.btnGoPurchasingAndSalesSystem);
            this.PanelBtnsSystems.Controls.Add(this.btnGoFinancialSecurities);
            this.PanelBtnsSystems.Controls.Add(this.btnGoItemsAndWarehouses);
            this.PanelBtnsSystems.Controls.Add(this.btnGoToCustomerAndSupplierSystem);
            this.PanelBtnsSystems.Controls.Add(this.btnGotoAccountingSystemScereen);
            this.PanelBtnsSystems.Controls.Add(this.btnGoToInformationGuide);
            this.PanelBtnsSystems.Controls.Add(this.btnGoToHome);
            this.PanelBtnsSystems.Location = new System.Drawing.Point(0, 0);
            this.PanelBtnsSystems.Margin = new System.Windows.Forms.Padding(4);
            this.PanelBtnsSystems.Name = "PanelBtnsSystems";
            this.PanelBtnsSystems.Size = new System.Drawing.Size(290, 654);
            this.PanelBtnsSystems.TabIndex = 11;
            this.PanelBtnsSystems.Tag = "btnSystems";
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button11.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button11.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button11.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsPhoneAndroidOutline;
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(0, 572);
            this.button11.Margin = new System.Windows.Forms.Padding(4);
            this.button11.Name = "button11";
            this.button11.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button11.Size = new System.Drawing.Size(290, 80);
            this.button11.TabIndex = 10;
            this.button11.Text = "        نظام التطبيق                    ";
            this.button11.UseVisualStyleBackColor = false;
            // 
            // btnGoPurchasingAndSalesSystem
            // 
            this.btnGoPurchasingAndSalesSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoPurchasingAndSalesSystem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoPurchasingAndSalesSystem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoPurchasingAndSalesSystem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoPurchasingAndSalesSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoPurchasingAndSalesSystem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoPurchasingAndSalesSystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoPurchasingAndSalesSystem.Image = global::AccountingSystem.Properties.Resources.FluentReceiptMoney16Regular;
            this.btnGoPurchasingAndSalesSystem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoPurchasingAndSalesSystem.Location = new System.Drawing.Point(0, 491);
            this.btnGoPurchasingAndSalesSystem.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoPurchasingAndSalesSystem.Name = "btnGoPurchasingAndSalesSystem";
            this.btnGoPurchasingAndSalesSystem.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoPurchasingAndSalesSystem.Size = new System.Drawing.Size(290, 80);
            this.btnGoPurchasingAndSalesSystem.TabIndex = 9;
            this.btnGoPurchasingAndSalesSystem.Text = "        نظام المبيعات والمشتريات                     ";
            this.btnGoPurchasingAndSalesSystem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGoPurchasingAndSalesSystem.UseVisualStyleBackColor = false;
            this.btnGoPurchasingAndSalesSystem.Click += new System.EventHandler(this.btnGoPurchasingAndSalesSystem_Click);
            // 
            // btnGoFinancialSecurities
            // 
            this.btnGoFinancialSecurities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoFinancialSecurities.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoFinancialSecurities.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoFinancialSecurities.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoFinancialSecurities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoFinancialSecurities.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoFinancialSecurities.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoFinancialSecurities.Image = global::AccountingSystem.Properties.Resources.VaadinInvoice;
            this.btnGoFinancialSecurities.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoFinancialSecurities.Location = new System.Drawing.Point(0, 410);
            this.btnGoFinancialSecurities.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoFinancialSecurities.Name = "btnGoFinancialSecurities";
            this.btnGoFinancialSecurities.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoFinancialSecurities.Size = new System.Drawing.Size(290, 80);
            this.btnGoFinancialSecurities.TabIndex = 8;
            this.btnGoFinancialSecurities.Text = "        نظام السندات المالية                      ";
            this.btnGoFinancialSecurities.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGoFinancialSecurities.UseVisualStyleBackColor = false;
            this.btnGoFinancialSecurities.Click += new System.EventHandler(this.btnGoFinancialSecurities_Click);
            // 
            // btnGoItemsAndWarehouses
            // 
            this.btnGoItemsAndWarehouses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoItemsAndWarehouses.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoItemsAndWarehouses.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoItemsAndWarehouses.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoItemsAndWarehouses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoItemsAndWarehouses.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoItemsAndWarehouses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoItemsAndWarehouses.Image = global::AccountingSystem.Properties.Resources.MingcuteClassify2Fill;
            this.btnGoItemsAndWarehouses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoItemsAndWarehouses.Location = new System.Drawing.Point(0, 329);
            this.btnGoItemsAndWarehouses.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoItemsAndWarehouses.Name = "btnGoItemsAndWarehouses";
            this.btnGoItemsAndWarehouses.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoItemsAndWarehouses.Size = new System.Drawing.Size(290, 80);
            this.btnGoItemsAndWarehouses.TabIndex = 7;
            this.btnGoItemsAndWarehouses.Text = "        نظام الاصناف والمخازن                     ";
            this.btnGoItemsAndWarehouses.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGoItemsAndWarehouses.UseVisualStyleBackColor = false;
            this.btnGoItemsAndWarehouses.Click += new System.EventHandler(this.btnGoItemsAndWarehouses_Click);
            // 
            // btnGoToCustomerAndSupplierSystem
            // 
            this.btnGoToCustomerAndSupplierSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToCustomerAndSupplierSystem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToCustomerAndSupplierSystem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoToCustomerAndSupplierSystem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToCustomerAndSupplierSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToCustomerAndSupplierSystem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToCustomerAndSupplierSystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoToCustomerAndSupplierSystem.Image = global::AccountingSystem.Properties.Resources.EntypoUsers__2_;
            this.btnGoToCustomerAndSupplierSystem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoToCustomerAndSupplierSystem.Location = new System.Drawing.Point(0, 247);
            this.btnGoToCustomerAndSupplierSystem.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoToCustomerAndSupplierSystem.Name = "btnGoToCustomerAndSupplierSystem";
            this.btnGoToCustomerAndSupplierSystem.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoToCustomerAndSupplierSystem.Size = new System.Drawing.Size(290, 80);
            this.btnGoToCustomerAndSupplierSystem.TabIndex = 6;
            this.btnGoToCustomerAndSupplierSystem.Text = "        نظام العملاء والموردين                    ";
            this.btnGoToCustomerAndSupplierSystem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGoToCustomerAndSupplierSystem.UseVisualStyleBackColor = false;
            this.btnGoToCustomerAndSupplierSystem.Click += new System.EventHandler(this.btnGoToCustomerAndSupplierSystem_Click);
            // 
            // btnGotoAccountingSystemScereen
            // 
            this.btnGotoAccountingSystemScereen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGotoAccountingSystemScereen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGotoAccountingSystemScereen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGotoAccountingSystemScereen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGotoAccountingSystemScereen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGotoAccountingSystemScereen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoAccountingSystemScereen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGotoAccountingSystemScereen.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsAccountTreeRounded;
            this.btnGotoAccountingSystemScereen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGotoAccountingSystemScereen.Location = new System.Drawing.Point(0, 166);
            this.btnGotoAccountingSystemScereen.Margin = new System.Windows.Forms.Padding(4);
            this.btnGotoAccountingSystemScereen.Name = "btnGotoAccountingSystemScereen";
            this.btnGotoAccountingSystemScereen.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGotoAccountingSystemScereen.Size = new System.Drawing.Size(290, 80);
            this.btnGotoAccountingSystemScereen.TabIndex = 5;
            this.btnGotoAccountingSystemScereen.Text = "        النظام المحاسبي                    ";
            this.btnGotoAccountingSystemScereen.UseVisualStyleBackColor = false;
            this.btnGotoAccountingSystemScereen.Click += new System.EventHandler(this.btnGotoAccountingSystemScereen_Click);
            // 
            // btnGoToInformationGuide
            // 
            this.btnGoToInformationGuide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToInformationGuide.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToInformationGuide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoToInformationGuide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToInformationGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToInformationGuide.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToInformationGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoToInformationGuide.Image = global::AccountingSystem.Properties.Resources.PajamasInformation;
            this.btnGoToInformationGuide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoToInformationGuide.Location = new System.Drawing.Point(0, 85);
            this.btnGoToInformationGuide.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoToInformationGuide.Name = "btnGoToInformationGuide";
            this.btnGoToInformationGuide.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoToInformationGuide.Size = new System.Drawing.Size(290, 80);
            this.btnGoToInformationGuide.TabIndex = 4;
            this.btnGoToInformationGuide.Text = "        دليل المعلومات                     ";
            this.btnGoToInformationGuide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoToInformationGuide.UseVisualStyleBackColor = false;
            this.btnGoToInformationGuide.Click += new System.EventHandler(this.btnGoToInformationGuide_Click);
            // 
            // btnGoToHome
            // 
            this.btnGoToHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToHome.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoToHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoToHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToHome.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoToHome.Image = global::AccountingSystem.Properties.Resources.IonHome__2_;
            this.btnGoToHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoToHome.Location = new System.Drawing.Point(0, 4);
            this.btnGoToHome.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoToHome.Name = "btnGoToHome";
            this.btnGoToHome.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoToHome.Size = new System.Drawing.Size(290, 80);
            this.btnGoToHome.TabIndex = 3;
            this.btnGoToHome.Text = "         الرئيسية";
            this.btnGoToHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoToHome.UseVisualStyleBackColor = false;
            this.btnGoToHome.Click += new System.EventHandler(this.btnGoToHome_Click);
            // 
            // PanelBtnsReports
            // 
            this.PanelBtnsReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.PanelBtnsReports.Controls.Add(this.button10);
            this.PanelBtnsReports.Controls.Add(this.button9);
            this.PanelBtnsReports.Controls.Add(this.button8);
            this.PanelBtnsReports.Controls.Add(this.btnGoReportsInvoicesAndStores);
            this.PanelBtnsReports.Controls.Add(this.button6);
            this.PanelBtnsReports.Controls.Add(this.btnGoScreensReportsGeneralProfessor);
            this.PanelBtnsReports.Controls.Add(this.btnGoAccountStatement);
            this.PanelBtnsReports.Controls.Add(this.btnGoTreeAccounts);
            this.PanelBtnsReports.Location = new System.Drawing.Point(0, 0);
            this.PanelBtnsReports.Margin = new System.Windows.Forms.Padding(4);
            this.PanelBtnsReports.Name = "PanelBtnsReports";
            this.PanelBtnsReports.Size = new System.Drawing.Size(290, 653);
            this.PanelBtnsReports.TabIndex = 2;
            this.PanelBtnsReports.Tag = "btnReports";
            this.PanelBtnsReports.Visible = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button10.Image = global::AccountingSystem.Properties.Resources.CarbonRequestQuote;
            this.button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.Location = new System.Drawing.Point(0, 572);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button10.Size = new System.Drawing.Size(290, 80);
            this.button10.TabIndex = 10;
            this.button10.Text = "        طلبات مشتريات التطبيق";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button9.Image = global::AccountingSystem.Properties.Resources.AkarIconsStatisticUp;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(0, 491);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button9.Size = new System.Drawing.Size(290, 80);
            this.button9.TabIndex = 9;
            this.button9.Text = "        تقارير بيع وشراء العملات ";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button8.Image = global::AccountingSystem.Properties.Resources.WhhReport__1_;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(0, 410);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button8.Size = new System.Drawing.Size(290, 80);
            this.button8.TabIndex = 8;
            this.button8.Text = "        تقارير متنوعة ";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // btnGoReportsInvoicesAndStores
            // 
            this.btnGoReportsInvoicesAndStores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoReportsInvoicesAndStores.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoReportsInvoicesAndStores.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoReportsInvoicesAndStores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoReportsInvoicesAndStores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoReportsInvoicesAndStores.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoReportsInvoicesAndStores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoReportsInvoicesAndStores.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsReceiptLongOutline;
            this.btnGoReportsInvoicesAndStores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoReportsInvoicesAndStores.Location = new System.Drawing.Point(0, 329);
            this.btnGoReportsInvoicesAndStores.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoReportsInvoicesAndStores.Name = "btnGoReportsInvoicesAndStores";
            this.btnGoReportsInvoicesAndStores.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoReportsInvoicesAndStores.Size = new System.Drawing.Size(290, 80);
            this.btnGoReportsInvoicesAndStores.TabIndex = 7;
            this.btnGoReportsInvoicesAndStores.Text = "        الفواتير والمخزون";
            this.btnGoReportsInvoicesAndStores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoReportsInvoicesAndStores.UseVisualStyleBackColor = false;
            this.btnGoReportsInvoicesAndStores.Click += new System.EventHandler(this.btnGoReportsInvoicesAndStores_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.button6.Image = global::AccountingSystem.Properties.Resources.FluentMdl2FinancialSolid;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(0, 247);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.button6.Size = new System.Drawing.Size(290, 80);
            this.button6.TabIndex = 6;
            this.button6.Text = "        الارباح والخسائر";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // btnGoScreensReportsGeneralProfessor
            // 
            this.btnGoScreensReportsGeneralProfessor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoScreensReportsGeneralProfessor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoScreensReportsGeneralProfessor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoScreensReportsGeneralProfessor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoScreensReportsGeneralProfessor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoScreensReportsGeneralProfessor.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoScreensReportsGeneralProfessor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoScreensReportsGeneralProfessor.Image = global::AccountingSystem.Properties.Resources.OuiReporter1;
            this.btnGoScreensReportsGeneralProfessor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoScreensReportsGeneralProfessor.Location = new System.Drawing.Point(0, 166);
            this.btnGoScreensReportsGeneralProfessor.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoScreensReportsGeneralProfessor.Name = "btnGoScreensReportsGeneralProfessor";
            this.btnGoScreensReportsGeneralProfessor.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoScreensReportsGeneralProfessor.Size = new System.Drawing.Size(290, 80);
            this.btnGoScreensReportsGeneralProfessor.TabIndex = 5;
            this.btnGoScreensReportsGeneralProfessor.Text = "        الاستاذ العام";
            this.btnGoScreensReportsGeneralProfessor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoScreensReportsGeneralProfessor.UseVisualStyleBackColor = false;
            this.btnGoScreensReportsGeneralProfessor.Click += new System.EventHandler(this.btnGoScreensReportsGeneralProfessor_Click);
            // 
            // btnGoAccountStatement
            // 
            this.btnGoAccountStatement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoAccountStatement.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoAccountStatement.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoAccountStatement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoAccountStatement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoAccountStatement.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoAccountStatement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoAccountStatement.Image = global::AccountingSystem.Properties.Resources.WhhInvoice;
            this.btnGoAccountStatement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoAccountStatement.Location = new System.Drawing.Point(0, 85);
            this.btnGoAccountStatement.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoAccountStatement.Name = "btnGoAccountStatement";
            this.btnGoAccountStatement.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoAccountStatement.Size = new System.Drawing.Size(290, 80);
            this.btnGoAccountStatement.TabIndex = 4;
            this.btnGoAccountStatement.Text = "        كشف حساب ";
            this.btnGoAccountStatement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoAccountStatement.UseVisualStyleBackColor = false;
            this.btnGoAccountStatement.Click += new System.EventHandler(this.btnGoAccountStatement_Click);
            // 
            // btnGoTreeAccounts
            // 
            this.btnGoTreeAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoTreeAccounts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoTreeAccounts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(86)))));
            this.btnGoTreeAccounts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnGoTreeAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoTreeAccounts.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoTreeAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnGoTreeAccounts.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsAccountTreeRounded;
            this.btnGoTreeAccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoTreeAccounts.Location = new System.Drawing.Point(0, 4);
            this.btnGoTreeAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoTreeAccounts.Name = "btnGoTreeAccounts";
            this.btnGoTreeAccounts.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnGoTreeAccounts.Size = new System.Drawing.Size(290, 80);
            this.btnGoTreeAccounts.TabIndex = 3;
            this.btnGoTreeAccounts.Text = "        شجرة الحسابات  ";
            this.btnGoTreeAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoTreeAccounts.UseVisualStyleBackColor = false;
            this.btnGoTreeAccounts.Click += new System.EventHandler(this.btnGoTreeAccounts_Click);
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.BaseFont = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonCustomPaletteBase1.BaseFontSize = 9F;
            this.kryptonCustomPaletteBase1.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.Rounding = 15F;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 10;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Content.Padding = new System.Windows.Forms.Padding(-1, -1, 20, -1);
            this.kryptonCustomPaletteBase1.ThemeName = "";
            this.kryptonCustomPaletteBase1.UseKryptonFileDialogs = true;
            // 
            // HomeScereen
            // 
            this.AllowStatusStripMerge = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1659, 1055);
            this.Controls.Add(this.panelMainBody);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.saideBar);
            this.FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HomeScereen";
            this.Palette = this.kryptonCustomPaletteBase1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeScereen_FormClosing);
            this.SizeChanged += new System.EventHandler(this.HomeScereen_SizeChanged);
            this.Resize += new System.EventHandler(this.HomeScereen_SizeChanged);
            this.headerSaideBar.ResumeLayout(false);
            this.headerSaideBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSaidBar)).EndInit();
            this.panelGroupTapBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.supTitel)).EndInit();
            this.supTitel.ResumeLayout(false);
            this.supTitel.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.panelMainBody.ResumeLayout(false);
            this.flowMainBody.ResumeLayout(false);
            this.PanelOptionSearchAndPrint.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.guna2Panel6.ResumeLayout(false);
            this.guna2Panel7.ResumeLayout(false);
            this.guna2Panel8.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.titel.ResumeLayout(false);
            this.titel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePersonal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenOrCloseSaideBarr)).EndInit();
            this.saideBar.ResumeLayout(false);
            this.groupSaideBarTap.ResumeLayout(false);
            this.PanelBtnsSettings.ResumeLayout(false);
            this.PanelBtnsSystems.ResumeLayout(false);
            this.PanelBtnsReports.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Krypton.Toolkit.KryptonPanel supTitel;
        private Guna.UI2.WinForms.Guna2Panel panelMainBody;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel saideBar;
        private Guna.UI2.WinForms.Guna2Panel groupSaideBarTap;
        private Guna.UI2.WinForms.Guna2Panel PanelBtnsSettings;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button26;
        private Guna.UI2.WinForms.Guna2Panel PanelBtnsSystems;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button btnGoPurchasingAndSalesSystem;
        private System.Windows.Forms.Button btnGoFinancialSecurities;
        private System.Windows.Forms.Button btnGoItemsAndWarehouses;
        private System.Windows.Forms.Button btnGoToCustomerAndSupplierSystem;
        private System.Windows.Forms.Button btnGotoAccountingSystemScereen;
        private System.Windows.Forms.Button btnGoToInformationGuide;
        private System.Windows.Forms.Button btnGoToHome;
        private Guna.UI2.WinForms.Guna2Panel PanelBtnsReports;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btnGoReportsInvoicesAndStores;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnGoScreensReportsGeneralProfessor;
        private System.Windows.Forms.Button btnGoAccountStatement;
        private System.Windows.Forms.Button btnGoTreeAccounts;
        private Guna.UI2.WinForms.Guna2Panel headerSaideBar;
        private Guna.UI2.WinForms.Guna2Panel panelGroupTapBtns;
        private System.Windows.Forms.Button btnSystems;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Timer timerOpenOrCloseSaideBar;
        private Guna2Panel lainBtnSystems;
        private Guna2Panel lainBtnSettings;
        private Guna2Panel lainBtnReports;
        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel leftBorderBtn;
        private Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label labelViewRoot;
        private System.Windows.Forms.Label label1;
        private Guna2CirclePictureBox pictureSaidBar;
        private Guna2Panel titel;
        private System.Windows.Forms.Label labelpicturePersonal;
        private Guna2CirclePictureBox picturePersonal;
        private System.Windows.Forms.Label labelTitelTopBar;
        private System.Windows.Forms.PictureBox btnOpenOrCloseSaideBarr;
        private System.Windows.Forms.ToolTip toolTip1;
        private Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label2;
        private Guna2Panel guna2Panel3;
        private Guna2Button btnCardCustomer;
        private Guna2Separator guna2Separator1;
        private Guna2Panel guna2Panel5;
        private Guna2Panel guna2Panel6;
        private Guna2Panel guna2Panel7;
        private Guna2Separator guna2Separator3;
        private Guna2Button btnCardSalse;
        private Guna2Separator guna2Separator4;
        private Guna2Button btnCardItems;
        private Guna2Separator guna2Separator5;
        private Guna2Button btnCardEmpolyee;
        private Guna2Panel guna2Panel8;
        private Guna2Separator guna2Separator6;
        private Guna2Button btnCashier;
        private System.Windows.Forms.FlowLayoutPanel flowMainBody;
        private System.Windows.Forms.FlowLayoutPanel PanelOptionSearchAndPrint;
        private Guna2Panel guna2Panel4;
        private Guna2Separator guna2Separator2;
        private Guna2Button btnCardPurchases;
    }
}