namespace AccountingSystem.view.ReportPages
{
    partial class GeneralProfessor
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dataSetGeneralProfessorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.group = new Krypton.Toolkit.KryptonComboBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.currency = new Krypton.Toolkit.KryptonComboBox();
            this.startDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnPrint = new Guna.UI2.WinForms.Guna2Button();
            this.endDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button6 = new Guna.UI2.WinForms.Guna2Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.btnShowOrHideToolBar = new Guna.UI2.WinForms.Guna2CircleButton();
            this.dataSetStocItemsLessThanZeroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetAccountStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGeneralProfessorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currency)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.guna2Panel5.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel7.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetStocItemsLessThanZeroBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAccountStatementBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSetGeneralProfessorBindingSource
            // 
            this.dataSetGeneralProfessorBindingSource.DataSource = typeof(AccountingSystem.NewModel.RCLDModel.DataSetGeneralProfessor);
            // 
            // group
            // 
            this.group.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.group.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.group.CornerRoundingRadius = 20F;
            this.group.CueHint.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.group.CueHint.CueHintText = "مجموعة الحساب";
            this.group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group.DropDownWidth = 50;
            this.group.IntegralHeight = false;
            this.group.Location = new System.Drawing.Point(0, 0);
            this.group.Name = "group";
            this.group.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.group.Size = new System.Drawing.Size(136, 38);
            this.group.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.group.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.group.StateCommon.ComboBox.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.group.StateCommon.ComboBox.Border.Rounding = 20F;
            this.group.StateCommon.ComboBox.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.group.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.group.StateCommon.DropBack.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.group.StateCommon.DropBack.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.group.StateTracking.Item.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.group.StateTracking.Item.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.group.StateTracking.Item.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.group.StateTracking.Item.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.group.StateTracking.Item.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.group.StateTracking.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.group.StateTracking.Item.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.group.TabIndex = 62;
            this.group.SelectionChangeCommitted += new System.EventHandler(this.group_SelectionChangeCommitted);
            // 
            // btnSearch
            // 
            this.btnSearch.Animated = true;
            this.btnSearch.BorderRadius = 15;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnSearch.Image = global::AccountingSystem.Properties.Resources.MynauiInfoOctagonSolid__2_;
            this.btnSearch.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnSearch.IndicateFocus = true;
            this.btnSearch.Location = new System.Drawing.Point(258, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "عرض ";
            this.btnSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // currency
            // 
            this.currency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.currency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.currency.CornerRoundingRadius = 20F;
            this.currency.CueHint.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.currency.CueHint.CueHintText = "العمله";
            this.currency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currency.DropDownWidth = 50;
            this.currency.IntegralHeight = false;
            this.currency.Location = new System.Drawing.Point(0, 0);
            this.currency.Name = "currency";
            this.currency.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.currency.Size = new System.Drawing.Size(109, 38);
            this.currency.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currency.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currency.StateCommon.ComboBox.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.currency.StateCommon.ComboBox.Border.Rounding = 20F;
            this.currency.StateCommon.ComboBox.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currency.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currency.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.currency.StateCommon.DropBack.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currency.StateCommon.DropBack.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currency.StateTracking.Item.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currency.StateTracking.Item.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.currency.StateTracking.Item.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currency.StateTracking.Item.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currency.StateTracking.Item.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.currency.StateTracking.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currency.StateTracking.Item.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.currency.TabIndex = 52;
            this.currency.SelectionChangeCommitted += new System.EventHandler(this.currency_SelectionChangeCommitted);
            // 
            // startDate
            // 
            this.startDate.Animated = true;
            this.startDate.AutoRoundedCorners = true;
            this.startDate.BackColor = System.Drawing.Color.Transparent;
            this.startDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.startDate.BorderRadius = 14;
            this.startDate.BorderThickness = 1;
            this.startDate.Checked = true;
            this.startDate.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.startDate.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.startDate.FillColor = System.Drawing.Color.White;
            this.startDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.startDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(7, 3);
            this.startDate.MaxDate = new System.DateTime(2050, 1, 7, 0, 0, 0, 0);
            this.startDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.startDate.Name = "startDate";
            this.startDate.ShadowDecoration.Color = System.Drawing.Color.Goldenrod;
            this.startDate.Size = new System.Drawing.Size(117, 30);
            this.startDate.TabIndex = 59;
            this.startDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startDate.Value = new System.DateTime(2024, 10, 19, 0, 0, 0, 0);
            this.startDate.ValueChanged += new System.EventHandler(this.startDate_ValueChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.BorderRadius = 15;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrint.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.btnPrint.Image = global::AccountingSystem.Properties.Resources.MaterialSymbolsPrint;
            this.btnPrint.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPrint.ImageSize = new System.Drawing.Size(18, 18);
            this.btnPrint.IndicateFocus = true;
            this.btnPrint.Location = new System.Drawing.Point(175, 4);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrint.Size = new System.Drawing.Size(75, 28);
            this.btnPrint.TabIndex = 66;
            this.btnPrint.Text = "طباعه";
            this.btnPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // endDate
            // 
            this.endDate.Animated = true;
            this.endDate.AutoRoundedCorners = true;
            this.endDate.BackColor = System.Drawing.Color.Transparent;
            this.endDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.endDate.BorderRadius = 14;
            this.endDate.BorderThickness = 1;
            this.endDate.Checked = true;
            this.endDate.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.endDate.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.endDate.FillColor = System.Drawing.Color.White;
            this.endDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.endDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(8, 3);
            this.endDate.MaxDate = new System.DateTime(2050, 1, 7, 0, 0, 0, 0);
            this.endDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.endDate.Name = "endDate";
            this.endDate.ShadowDecoration.Color = System.Drawing.Color.Goldenrod;
            this.endDate.Size = new System.Drawing.Size(117, 30);
            this.endDate.TabIndex = 60;
            this.endDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.endDate.Value = new System.DateTime(2024, 10, 19, 0, 0, 0, 0);
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dataSetGeneralProfessorBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AccountingSystem.view.Reports.ReportsGeneralProfessor.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(41, 89);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.ShowStopButton = false;
            this.reportViewer1.ShowToolBar = false;
            this.reportViewer1.Size = new System.Drawing.Size(961, 169);
            this.reportViewer1.TabIndex = 14;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel5);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel3);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel7);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel4);
            this.flowLayoutPanel1.Controls.Add(this.btnSearch);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Controls.Add(this.guna2Button4);
            this.flowLayoutPanel1.Controls.Add(this.guna2Button6);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(146, 37);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(856, 46);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel5.Controls.Add(this.currency);
            this.guna2Panel5.Location = new System.Drawing.Point(744, 5);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(109, 36);
            this.guna2Panel5.TabIndex = 0;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel3.Controls.Add(this.label3);
            this.guna2Panel3.Controls.Add(this.startDate);
            this.guna2Panel3.Location = new System.Drawing.Point(619, 3);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(119, 40);
            this.guna2Panel3.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.label3.Location = new System.Drawing.Point(36, -5);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = " من تاريخ ";
            // 
            // guna2Panel7
            // 
            this.guna2Panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel7.Controls.Add(this.label4);
            this.guna2Panel7.Controls.Add(this.endDate);
            this.guna2Panel7.Location = new System.Drawing.Point(487, 3);
            this.guna2Panel7.Name = "guna2Panel7";
            this.guna2Panel7.Size = new System.Drawing.Size(126, 40);
            this.guna2Panel7.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.label4.Location = new System.Drawing.Point(36, -4);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 69;
            this.label4.Text = " الى تاريخ ";
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel4.Controls.Add(this.group);
            this.guna2Panel4.Location = new System.Drawing.Point(345, 4);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(136, 38);
            this.guna2Panel4.TabIndex = 2;
            // 
            // guna2Button4
            // 
            this.guna2Button4.BorderRadius = 15;
            this.guna2Button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button4.FillColor = System.Drawing.Color.DarkTurquoise;
            this.guna2Button4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.guna2Button4.Image = global::AccountingSystem.Properties.Resources.MdiMicrosoftExcel;
            this.guna2Button4.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button4.ImageSize = new System.Drawing.Size(18, 18);
            this.guna2Button4.IndicateFocus = true;
            this.guna2Button4.Location = new System.Drawing.Point(92, 4);
            this.guna2Button4.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button4.Size = new System.Drawing.Size(75, 28);
            this.guna2Button4.TabIndex = 67;
            this.guna2Button4.Text = "إكسل";
            this.guna2Button4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // guna2Button6
            // 
            this.guna2Button6.BorderRadius = 15;
            this.guna2Button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button6.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button6.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button6.FillColor = System.Drawing.Color.DodgerBlue;
            this.guna2Button6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.guna2Button6.Image = global::AccountingSystem.Properties.Resources.BxShareAlt;
            this.guna2Button6.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button6.ImageSize = new System.Drawing.Size(18, 18);
            this.guna2Button6.IndicateFocus = true;
            this.guna2Button6.Location = new System.Drawing.Point(4, 4);
            this.guna2Button6.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button6.Name = "guna2Button6";
            this.guna2Button6.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button6.Size = new System.Drawing.Size(80, 28);
            this.guna2Button6.TabIndex = 65;
            this.guna2Button6.Text = "مشاركة";
            this.guna2Button6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1001, 28);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Controls.Add(this.flowLayoutPanel2);
            this.panelBody.Controls.Add(this.flowLayoutPanel1);
            this.panelBody.Controls.Add(this.reportViewer1);
            this.panelBody.Controls.Add(this.btnShowOrHideToolBar);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panelBody.Size = new System.Drawing.Size(1005, 711);
            this.panelBody.TabIndex = 17;
            // 
            // btnShowOrHideToolBar
            // 
            this.btnShowOrHideToolBar.BackColor = System.Drawing.Color.Transparent;
            this.btnShowOrHideToolBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowOrHideToolBar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowOrHideToolBar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowOrHideToolBar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowOrHideToolBar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowOrHideToolBar.FillColor = System.Drawing.Color.Transparent;
            this.btnShowOrHideToolBar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnShowOrHideToolBar.ForeColor = System.Drawing.Color.Transparent;
            this.btnShowOrHideToolBar.Image = global::AccountingSystem.Properties.Resources.TablerArrowBadgeDownFilled;
            this.btnShowOrHideToolBar.Location = new System.Drawing.Point(14, 88);
            this.btnShowOrHideToolBar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnShowOrHideToolBar.Name = "btnShowOrHideToolBar";
            this.btnShowOrHideToolBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnShowOrHideToolBar.Size = new System.Drawing.Size(20, 20);
            this.btnShowOrHideToolBar.TabIndex = 15;
            // 
            // dataSetStocItemsLessThanZeroBindingSource
            // 
            this.dataSetStocItemsLessThanZeroBindingSource.DataSource = typeof(AccountingSystem.NewModel.RCLDModel.DataSetStocItemsLessThanZero);
            // 
            // dataSetAccountStatementBindingSource
            // 
            this.dataSetAccountStatementBindingSource.DataSource = typeof(AccountingSystem.NewModel.RCLDModel.DataSetAccountStatement);
            // 
            // GeneralProfessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 711);
            this.Controls.Add(this.panelBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GeneralProfessor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "الأستاذ العام";
            this.Load += new System.EventHandler(this.GeneralProfessor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGeneralProfessorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currency)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            this.guna2Panel7.ResumeLayout(false);
            this.guna2Panel7.PerformLayout();
            this.guna2Panel4.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetStocItemsLessThanZeroBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAccountStatementBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnPrint;
        private Krypton.Toolkit.KryptonComboBox group;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Krypton.Toolkit.KryptonComboBox currency;
        private Guna.UI2.WinForms.Guna2DateTimePicker startDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker endDate;
        private System.Windows.Forms.BindingSource dataSetGeneralProfessorBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2Button guna2Button4;
        private Guna.UI2.WinForms.Guna2Button guna2Button6;
        private System.Windows.Forms.BindingSource dataSetStocItemsLessThanZeroBindingSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel panelBody;
        private System.Windows.Forms.BindingSource dataSetAccountStatementBindingSource;
        private Guna.UI2.WinForms.Guna2CircleButton btnShowOrHideToolBar;
    }
}