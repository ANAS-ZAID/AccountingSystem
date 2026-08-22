using System;

namespace AccountingSystem.view.ReportPages
{
    partial class AccountStatementWithNumberHours
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnShowOrHideToolBar = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnPrint = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button5 = new Guna.UI2.WinForms.Guna2Button();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.noOpeningBalance = new Guna.UI2.WinForms.Guna2CheckBox();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.total = new Guna.UI2.WinForms.Guna2CheckBox();
            this.guna2Panel18 = new Guna.UI2.WinForms.Guna2Panel();
            this.rowCount = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.group = new Krypton.Toolkit.KryptonComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.endDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.startDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.currency = new Krypton.Toolkit.KryptonComboBox();
            this.guna2Panel17 = new Guna.UI2.WinForms.Guna2Panel();
            this.account = new Krypton.Toolkit.KryptonComboBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.showHour = new Guna.UI2.WinForms.Guna2CheckBox();
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.dataSetAccountStatementWithHourBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.guna2Panel5.SuspendLayout();
            this.guna2Panel18.SuspendLayout();
            this.guna2Panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.group)).BeginInit();
            this.guna2Panel7.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currency)).BeginInit();
            this.guna2Panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.account)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAccountStatementWithHourBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSetAccountStatementWithNumberHours";
            reportDataSource1.Value = this.dataSetAccountStatementWithHourBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AccountingSystem.view.Reports.AccountStatementWithNumberHours.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(30, 127);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.ShowBackButton = false;
            this.reportViewer.ShowFindControls = false;
            this.reportViewer.ShowPageNavigationControls = false;
            this.reportViewer.ShowStopButton = false;
            this.reportViewer.ShowToolBar = false;
            this.reportViewer.Size = new System.Drawing.Size(954, 176);
            this.reportViewer.TabIndex = 13;
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
            this.btnShowOrHideToolBar.Location = new System.Drawing.Point(956, 308);
            this.btnShowOrHideToolBar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnShowOrHideToolBar.Name = "btnShowOrHideToolBar";
            this.btnShowOrHideToolBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnShowOrHideToolBar.Size = new System.Drawing.Size(27, 20);
            this.btnShowOrHideToolBar.TabIndex = 17;
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
            this.btnPrint.Location = new System.Drawing.Point(875, 50);
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
            // guna2Button2
            // 
            this.guna2Button2.BorderRadius = 15;
            this.guna2Button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.DarkTurquoise;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.guna2Button2.Image = global::AccountingSystem.Properties.Resources.MdiMicrosoftExcel;
            this.guna2Button2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button2.ImageSize = new System.Drawing.Size(18, 18);
            this.guna2Button2.IndicateFocus = true;
            this.guna2Button2.Location = new System.Drawing.Point(792, 50);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button2.Size = new System.Drawing.Size(75, 28);
            this.guna2Button2.TabIndex = 67;
            this.guna2Button2.Text = "إكسل";
            this.guna2Button2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // guna2Button5
            // 
            this.guna2Button5.BorderRadius = 15;
            this.guna2Button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button5.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button5.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button5.FillColor = System.Drawing.Color.DodgerBlue;
            this.guna2Button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.guna2Button5.Image = global::AccountingSystem.Properties.Resources.BxShareAlt;
            this.guna2Button5.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button5.ImageSize = new System.Drawing.Size(18, 18);
            this.guna2Button5.IndicateFocus = true;
            this.guna2Button5.Location = new System.Drawing.Point(704, 50);
            this.guna2Button5.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button5.Name = "guna2Button5";
            this.guna2Button5.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button5.Size = new System.Drawing.Size(80, 28);
            this.guna2Button5.TabIndex = 65;
            this.guna2Button5.Text = "مشاركة";
            this.guna2Button5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.btnSearch.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSearch.IndicateFocus = true;
            this.btnSearch.Location = new System.Drawing.Point(616, 50);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "عرض ";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // noOpeningBalance
            // 
            this.noOpeningBalance.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.noOpeningBalance.CheckedState.BorderRadius = 0;
            this.noOpeningBalance.CheckedState.BorderThickness = 1;
            this.noOpeningBalance.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.noOpeningBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noOpeningBalance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noOpeningBalance.Location = new System.Drawing.Point(0, 0);
            this.noOpeningBalance.Name = "noOpeningBalance";
            this.noOpeningBalance.Size = new System.Drawing.Size(158, 27);
            this.noOpeningBalance.TabIndex = 63;
            this.noOpeningBalance.Text = "بدون رصيد افتتاحي";
            this.noOpeningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.noOpeningBalance.UncheckedState.BorderColor = System.Drawing.Color.White;
            this.noOpeningBalance.UncheckedState.BorderRadius = 0;
            this.noOpeningBalance.UncheckedState.BorderThickness = 0;
            this.noOpeningBalance.UncheckedState.FillColor = System.Drawing.Color.White;
            this.noOpeningBalance.CheckedChanged += new System.EventHandler(this.noOpeningBalance_CheckedChanged);
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel5.Controls.Add(this.noOpeningBalance);
            this.guna2Panel5.Location = new System.Drawing.Point(3, 9);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(158, 27);
            this.guna2Panel5.TabIndex = 0;
            // 
            // total
            // 
            this.total.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.total.CheckedState.BorderRadius = 0;
            this.total.CheckedState.BorderThickness = 1;
            this.total.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.total.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.Location = new System.Drawing.Point(0, 0);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(86, 27);
            this.total.TabIndex = 7;
            this.total.Text = "اجمالي ";
            this.total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.total.UncheckedState.BorderColor = System.Drawing.Color.White;
            this.total.UncheckedState.BorderRadius = 0;
            this.total.UncheckedState.BorderThickness = 0;
            this.total.UncheckedState.FillColor = System.Drawing.Color.White;
            this.total.CheckedChanged += new System.EventHandler(this.total_CheckedChanged);
            // 
            // guna2Panel18
            // 
            this.guna2Panel18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel18.Controls.Add(this.total);
            this.guna2Panel18.Location = new System.Drawing.Point(167, 9);
            this.guna2Panel18.Name = "guna2Panel18";
            this.guna2Panel18.Size = new System.Drawing.Size(86, 27);
            this.guna2Panel18.TabIndex = 0;
            // 
            // rowCount
            // 
            this.rowCount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.rowCount.BorderRadius = 15;
            this.rowCount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rowCount.DefaultText = "";
            this.rowCount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.rowCount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.rowCount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.rowCount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.rowCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowCount.FocusedState.BorderColor = System.Drawing.Color.Goldenrod;
            this.rowCount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.rowCount.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            this.rowCount.Location = new System.Drawing.Point(0, 0);
            this.rowCount.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.rowCount.Name = "rowCount";
            this.rowCount.PasswordChar = '\0';
            this.rowCount.PlaceholderText = "العدد";
            this.rowCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rowCount.SelectedText = "";
            this.rowCount.Size = new System.Drawing.Size(82, 30);
            this.rowCount.TabIndex = 1;
            // 
            // guna2Panel10
            // 
            this.guna2Panel10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel10.Controls.Add(this.rowCount);
            this.guna2Panel10.Location = new System.Drawing.Point(259, 8);
            this.guna2Panel10.Name = "guna2Panel10";
            this.guna2Panel10.Size = new System.Drawing.Size(82, 30);
            this.guna2Panel10.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(-17, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1001, 28);
            this.flowLayoutPanel2.TabIndex = 6;
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
            this.group.Size = new System.Drawing.Size(88, 38);
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
            this.endDate.Location = new System.Drawing.Point(4, 5);
            this.endDate.MaxDate = new System.DateTime(2050, 1, 7, 0, 0, 0, 0);
            this.endDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.endDate.Name = "endDate";
            this.endDate.ShadowDecoration.Color = System.Drawing.Color.Goldenrod;
            this.endDate.Size = new System.Drawing.Size(117, 30);
            this.endDate.TabIndex = 74;
            this.endDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.endDate.Value = new System.DateTime(2024, 9, 12, 6, 36, 14, 0);
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // guna2Panel7
            // 
            this.guna2Panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel7.Controls.Add(this.label4);
            this.guna2Panel7.Controls.Add(this.endDate);
            this.guna2Panel7.Location = new System.Drawing.Point(441, 3);
            this.guna2Panel7.Name = "guna2Panel7";
            this.guna2Panel7.Size = new System.Drawing.Size(126, 40);
            this.guna2Panel7.TabIndex = 8;
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
            this.startDate.Location = new System.Drawing.Point(3, 5);
            this.startDate.MaxDate = new System.DateTime(2050, 1, 7, 0, 0, 0, 0);
            this.startDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.startDate.Name = "startDate";
            this.startDate.ShadowDecoration.Color = System.Drawing.Color.Goldenrod;
            this.startDate.Size = new System.Drawing.Size(113, 30);
            this.startDate.TabIndex = 72;
            this.startDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startDate.Value = new System.DateTime(2024, 9, 12, 6, 36, 14, 0);
            this.startDate.ValueChanged += new System.EventHandler(this.startDate_ValueChanged);
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel3.Controls.Add(this.label3);
            this.guna2Panel3.Controls.Add(this.startDate);
            this.guna2Panel3.Location = new System.Drawing.Point(573, 3);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(119, 40);
            this.guna2Panel3.TabIndex = 7;
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
            // guna2Panel17
            // 
            this.guna2Panel17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel17.Controls.Add(this.currency);
            this.guna2Panel17.Location = new System.Drawing.Point(698, 6);
            this.guna2Panel17.Name = "guna2Panel17";
            this.guna2Panel17.Size = new System.Drawing.Size(109, 33);
            this.guna2Panel17.TabIndex = 0;
            // 
            // account
            // 
            this.account.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.account.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.account.CornerRoundingRadius = 20F;
            this.account.CueHint.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.account.CueHint.CueHintText = "الحساب ";
            this.account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.account.DropDownWidth = 50;
            this.account.IntegralHeight = false;
            this.account.Location = new System.Drawing.Point(0, 0);
            this.account.Name = "account";
            this.account.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.account.Size = new System.Drawing.Size(138, 38);
            this.account.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.account.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.account.StateCommon.ComboBox.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.account.StateCommon.ComboBox.Border.Rounding = 20F;
            this.account.StateCommon.ComboBox.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.account.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.account.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.account.StateCommon.DropBack.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.account.StateCommon.DropBack.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.account.StateTracking.Item.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.account.StateTracking.Item.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.account.StateTracking.Item.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.account.StateTracking.Item.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.account.StateTracking.Item.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.account.StateTracking.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.account.StateTracking.Item.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.account.TabIndex = 50;
            this.account.SelectionChangeCommitted += new System.EventHandler(this.account_SelectionChangeCommitted);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel2.Controls.Add(this.account);
            this.guna2Panel2.Location = new System.Drawing.Point(813, 5);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(138, 36);
            this.guna2Panel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel2);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel17);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel3);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel7);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel4);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel10);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel18);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel5);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Controls.Add(this.guna2Button2);
            this.flowLayoutPanel1.Controls.Add(this.guna2Button5);
            this.flowLayoutPanel1.Controls.Add(this.btnSearch);
            this.flowLayoutPanel1.Controls.Add(this.guna2Panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(30, 37);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(954, 84);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel4.Controls.Add(this.group);
            this.guna2Panel4.Location = new System.Drawing.Point(347, 8);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(88, 30);
            this.guna2Panel4.TabIndex = 2;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel1.Controls.Add(this.showHour);
            this.guna2Panel1.Location = new System.Drawing.Point(451, 51);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(158, 27);
            this.guna2Panel1.TabIndex = 0;
            // 
            // showHour
            // 
            this.showHour.Checked = true;
            this.showHour.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showHour.CheckedState.BorderRadius = 0;
            this.showHour.CheckedState.BorderThickness = 1;
            this.showHour.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showHour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showHour.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showHour.Location = new System.Drawing.Point(0, 0);
            this.showHour.Name = "showHour";
            this.showHour.Size = new System.Drawing.Size(158, 27);
            this.showHour.TabIndex = 63;
            this.showHour.Text = "إضهار عدد الساعات";
            this.showHour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showHour.UncheckedState.BorderColor = System.Drawing.Color.White;
            this.showHour.UncheckedState.BorderRadius = 0;
            this.showHour.UncheckedState.BorderThickness = 0;
            this.showHour.UncheckedState.FillColor = System.Drawing.Color.White;
            this.showHour.CheckedChanged += new System.EventHandler(this.showHour_CheckedChanged);
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Controls.Add(this.flowLayoutPanel2);
            this.panelBody.Controls.Add(this.flowLayoutPanel1);
            this.panelBody.Controls.Add(this.reportViewer);
            this.panelBody.Controls.Add(this.btnShowOrHideToolBar);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panelBody.Size = new System.Drawing.Size(987, 753);
            this.panelBody.TabIndex = 14;
            // 
            // dataSetAccountStatementWithHourBindingSource
            // 
            this.dataSetAccountStatementWithHourBindingSource.DataSource = typeof(AccountingSystem.NewModel.RCLDModel.DataSetAccountStatementWithNumberHours);
            // 
            // AccountStatementWithNumberHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 753);
            this.Controls.Add(this.panelBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AccountStatementWithNumberHours";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "كشف حساب";
            this.Load += new System.EventHandler(this.AccountStatementWithNumberHours_Load);
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel18.ResumeLayout(false);
            this.guna2Panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.group)).EndInit();
            this.guna2Panel7.ResumeLayout(false);
            this.guna2Panel7.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currency)).EndInit();
            this.guna2Panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.account)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetAccountStatementWithHourBindingSource)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion
        private System.Windows.Forms.BindingSource dataSetAccountStatementWithHourBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Guna.UI2.WinForms.Guna2CircleButton btnShowOrHideToolBar;
        private Guna.UI2.WinForms.Guna2Button btnPrint;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button5;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2CheckBox noOpeningBalance;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2CheckBox total;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel18;
        private Guna.UI2.WinForms.Guna2TextBox rowCount;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Krypton.Toolkit.KryptonComboBox group;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2DateTimePicker endDate;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2DateTimePicker startDate;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Krypton.Toolkit.KryptonComboBox currency;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel17;
        private Krypton.Toolkit.KryptonComboBox account;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.FlowLayoutPanel panelBody;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2CheckBox showHour;
    }
}