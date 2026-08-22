namespace AccountingSystem.view.Screens.CurrencyManagement
{
    partial class DialogAddCurrency
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCurrencyMain = new Guna.UI2.WinForms.Guna2TileButton();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCurrencySecondary = new Guna.UI2.WinForms.Guna2TileButton();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.currencyCode = new Krypton.Toolkit.KryptonComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.exchangeRate = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.labelTitel = new System.Windows.Forms.Label();
            this.btnReferesh = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.currencyName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClear = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.kryptonContextMenuCheckBox1 = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.guna2Panel2.SuspendLayout();
            this.guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyCode)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.ForeColor = System.Drawing.Color.Goldenrod;
            this.label1.Location = new System.Drawing.Point(58, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "  نوع العملة*";
            // 
            // btnCurrencyMain
            // 
            this.btnCurrencyMain.BackColor = System.Drawing.Color.Transparent;
            this.btnCurrencyMain.BorderRadius = 6;
            this.btnCurrencyMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCurrencyMain.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCurrencyMain.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCurrencyMain.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCurrencyMain.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCurrencyMain.FillColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCurrencyMain.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrencyMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCurrencyMain.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCurrencyMain.HoverState.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCurrencyMain.ImageSize = new System.Drawing.Size(30, 30);
            this.btnCurrencyMain.Location = new System.Drawing.Point(110, 42);
            this.btnCurrencyMain.Name = "btnCurrencyMain";
            this.btnCurrencyMain.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCurrencyMain.PressedDepth = 10;
            this.btnCurrencyMain.ShadowDecoration.Color = System.Drawing.Color.Goldenrod;
            this.btnCurrencyMain.ShadowDecoration.Depth = 20;
            this.btnCurrencyMain.ShadowDecoration.Enabled = true;
            this.btnCurrencyMain.Size = new System.Drawing.Size(86, 42);
            this.btnCurrencyMain.TabIndex = 11;
            this.btnCurrencyMain.Text = "رئيسية";
            this.btnCurrencyMain.Click += new System.EventHandler(this.btnsCurrencyType_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.Controls.Add(this.label1);
            this.guna2Panel2.Controls.Add(this.btnCurrencySecondary);
            this.guna2Panel2.Controls.Add(this.btnCurrencyMain);
            this.guna2Panel2.Location = new System.Drawing.Point(84, 197);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(200, 89);
            this.guna2Panel2.TabIndex = 43;
            // 
            // btnCurrencySecondary
            // 
            this.btnCurrencySecondary.BackColor = System.Drawing.Color.Transparent;
            this.btnCurrencySecondary.BorderRadius = 6;
            this.btnCurrencySecondary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCurrencySecondary.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCurrencySecondary.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCurrencySecondary.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCurrencySecondary.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCurrencySecondary.FillColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCurrencySecondary.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrencySecondary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCurrencySecondary.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCurrencySecondary.HoverState.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCurrencySecondary.ImageSize = new System.Drawing.Size(30, 30);
            this.btnCurrencySecondary.Location = new System.Drawing.Point(5, 42);
            this.btnCurrencySecondary.Name = "btnCurrencySecondary";
            this.btnCurrencySecondary.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCurrencySecondary.PressedDepth = 10;
            this.btnCurrencySecondary.ShadowDecoration.Color = System.Drawing.Color.Goldenrod;
            this.btnCurrencySecondary.ShadowDecoration.Depth = 20;
            this.btnCurrencySecondary.ShadowDecoration.Enabled = true;
            this.btnCurrencySecondary.Size = new System.Drawing.Size(86, 42);
            this.btnCurrencySecondary.TabIndex = 12;
            this.btnCurrencySecondary.Text = "ثانوية";
            this.btnCurrencySecondary.Click += new System.EventHandler(this.btnsCurrencyType_Click);
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2Panel2);
            this.guna2CustomGradientPanel1.Controls.Add(this.currencyCode);
            this.guna2CustomGradientPanel1.Controls.Add(this.label5);
            this.guna2CustomGradientPanel1.Controls.Add(this.exchangeRate);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2Panel1);
            this.guna2CustomGradientPanel1.Controls.Add(this.currencyName);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2Panel3);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.ShadowDecoration.Color = System.Drawing.SystemColors.GradientActiveCaption;
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(800, 700);
            this.guna2CustomGradientPanel1.TabIndex = 2;
            // 
            // currencyCode
            // 
            this.currencyCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.currencyCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.currencyCode.CornerRoundingRadius = 20F;
            this.currencyCode.CueHint.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.currencyCode.CueHint.CueHintText = "رمز العملة *";
            this.currencyCode.DropDownWidth = 200;
            this.currencyCode.IntegralHeight = false;
            this.currencyCode.Location = new System.Drawing.Point(14, 138);
            this.currencyCode.Name = "currencyCode";
            this.currencyCode.Size = new System.Drawing.Size(368, 41);
            this.currencyCode.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyCode.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyCode.StateCommon.ComboBox.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.currencyCode.StateCommon.ComboBox.Border.Rounding = 20F;
            this.currencyCode.StateCommon.ComboBox.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyCode.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currencyCode.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.currencyCode.StateCommon.DropBack.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currencyCode.StateCommon.DropBack.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currencyCode.StateTracking.Item.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyCode.StateTracking.Item.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyCode.StateTracking.Item.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currencyCode.StateTracking.Item.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currencyCode.StateTracking.Item.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.currencyCode.StateTracking.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currencyCode.StateTracking.Item.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currencyCode.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(512, 59);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(288, 17);
            this.label5.TabIndex = 39;
            this.label5.Text = "ملاحظه جميع الحقول التي تحتوي على * مطلوبة";
            // 
            // exchangeRate
            // 
            this.exchangeRate.BackColor = System.Drawing.Color.Transparent;
            this.exchangeRate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.exchangeRate.BorderRadius = 15;
            this.exchangeRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.exchangeRate.DefaultText = "";
            this.exchangeRate.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.exchangeRate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.exchangeRate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.exchangeRate.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.exchangeRate.FocusedState.BorderColor = System.Drawing.Color.Goldenrod;
            this.exchangeRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.exchangeRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.exchangeRate.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            this.exchangeRate.Location = new System.Drawing.Point(422, 236);
            this.exchangeRate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.exchangeRate.Name = "exchangeRate";
            this.exchangeRate.PasswordChar = '\0';
            this.exchangeRate.PlaceholderText = " * سعر التحويل";
            this.exchangeRate.SelectedText = "";
            this.exchangeRate.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.exchangeRate.Size = new System.Drawing.Size(368, 50);
            this.exchangeRate.TabIndex = 35;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Controls.Add(this.labelTitel);
            this.guna2Panel1.Controls.Add(this.btnReferesh);
            this.guna2Panel1.Controls.Add(this.btnClose);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(800, 59);
            this.guna2Panel1.TabIndex = 26;
            // 
            // labelTitel
            // 
            this.labelTitel.AutoSize = true;
            this.labelTitel.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelTitel.Font = new System.Drawing.Font("Tahoma", 14F);
            this.labelTitel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.labelTitel.Location = new System.Drawing.Point(673, 0);
            this.labelTitel.Name = "labelTitel";
            this.labelTitel.Size = new System.Drawing.Size(127, 29);
            this.labelTitel.TabIndex = 25;
            this.labelTitel.Text = "اضافة عملة";
            // 
            // btnReferesh
            // 
            this.btnReferesh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReferesh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReferesh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReferesh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReferesh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReferesh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnReferesh.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnReferesh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnReferesh.Image = global::AccountingSystem.Properties.Resources.MdiRefresh;
            this.btnReferesh.ImageSize = new System.Drawing.Size(23, 23);
            this.btnReferesh.Location = new System.Drawing.Point(65, 9);
            this.btnReferesh.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnReferesh.Name = "btnReferesh";
            this.btnReferesh.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnReferesh.Size = new System.Drawing.Size(41, 43);
            this.btnReferesh.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnReferesh, "تحديث البيانات");
            this.btnReferesh.Click += new System.EventHandler(this.btnReferesh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnClose.Image = global::AccountingSystem.Properties.Resources.TopcoatCancel;
            this.btnClose.Location = new System.Drawing.Point(14, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(41, 43);
            this.btnClose.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnClose, "إغلاق");
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // currencyName
            // 
            this.currencyName.BackColor = System.Drawing.Color.Transparent;
            this.currencyName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyName.BorderRadius = 15;
            this.currencyName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.currencyName.DefaultText = "";
            this.currencyName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.currencyName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.currencyName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currencyName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currencyName.FocusedState.BorderColor = System.Drawing.Color.Goldenrod;
            this.currencyName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.currencyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyName.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            this.currencyName.Location = new System.Drawing.Point(421, 134);
            this.currencyName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.currencyName.Name = "currencyName";
            this.currencyName.PasswordChar = '\0';
            this.currencyName.PlaceholderText = "*أسم العملة";
            this.currencyName.SelectedText = "";
            this.currencyName.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currencyName.Size = new System.Drawing.Size(368, 50);
            this.currencyName.TabIndex = 27;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.guna2Panel3.Controls.Add(this.btnClear);
            this.guna2Panel3.Controls.Add(this.btnCancel);
            this.guna2Panel3.Controls.Add(this.btnSave);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel3.Location = new System.Drawing.Point(0, 641);
            this.guna2Panel3.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(800, 59);
            this.guna2Panel3.TabIndex = 30;
            // 
            // btnClear
            // 
            this.btnClear.BorderRadius = 15;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClear.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClear.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClear.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClear.FillColor = System.Drawing.SystemColors.Control;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnClear.Image = global::AccountingSystem.Properties.Resources.TopcoatRefresh;
            this.btnClear.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnClear.ImageSize = new System.Drawing.Size(17, 17);
            this.btnClear.IndicateFocus = true;
            this.btnClear.Location = new System.Drawing.Point(329, 9);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClear.Size = new System.Drawing.Size(102, 43);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "تصفيه ";
            this.btnClear.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 15;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnCancel.Image = global::AccountingSystem.Properties.Resources.TopcoatCancel__1_;
            this.btnCancel.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnCancel.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCancel.IndicateFocus = true;
            this.btnCancel.Location = new System.Drawing.Point(227, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCancel.Size = new System.Drawing.Size(94, 43);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "الغاء";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 15;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.SystemColors.Control;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSave.Image = global::AccountingSystem.Properties.Resources.FeatherSave__1_;
            this.btnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnSave.IndicateFocus = true;
            this.btnSave.Location = new System.Drawing.Point(440, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Size = new System.Drawing.Size(102, 43);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "حفظ";
            this.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonContextMenuCheckBox1
            // 
            this.kryptonContextMenuCheckBox1.ExtraText = "";
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockForm = false;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.DragForm = false;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // DialogAddCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DialogAddCurrency";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ادارة العملات ";
            this.Load += new System.EventHandler(this.DialogAddCurrency_Load);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyCode)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TileButton btnCurrencyMain;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2TileButton btnCurrencySecondary;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Krypton.Toolkit.KryptonComboBox currencyCode;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox exchangeRate;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label labelTitel;
        private Guna.UI2.WinForms.Guna2CircleButton btnReferesh;
        private System.Windows.Forms.ToolTip toolTip1;
        private Guna.UI2.WinForms.Guna2CircleButton btnClose;
        private Guna.UI2.WinForms.Guna2TextBox currencyName;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Krypton.Toolkit.KryptonContextMenuCheckBox kryptonContextMenuCheckBox1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
    }
}