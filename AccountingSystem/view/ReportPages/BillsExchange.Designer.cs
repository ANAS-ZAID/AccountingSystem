namespace AccountingSystem.view.ReportPages
{
    partial class BillsExchange
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
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.number = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.btnPrint = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button9 = new Guna.UI2.WinForms.Guna2Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnShowOrHideToolBar = new Guna.UI2.WinForms.Guna2CircleButton();
            this.dataSetJournalEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelBody.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetJournalEntryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Controls.Add(this.flowLayoutPanel4);
            this.panelBody.Controls.Add(this.flowLayoutPanel6);
            this.panelBody.Controls.Add(this.reportViewer1);
            this.panelBody.Controls.Add(this.btnShowOrHideToolBar);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panelBody.Size = new System.Drawing.Size(987, 753);
            this.panelBody.TabIndex = 21;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Location = new System.Drawing.Point(-17, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1001, 28);
            this.flowLayoutPanel4.TabIndex = 6;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.flowLayoutPanel6.Controls.Add(this.guna2Panel5);
            this.flowLayoutPanel6.Controls.Add(this.guna2Panel2);
            this.flowLayoutPanel6.Controls.Add(this.btnSearch);
            this.flowLayoutPanel6.Controls.Add(this.btnPrint);
            this.flowLayoutPanel6.Controls.Add(this.guna2Button2);
            this.flowLayoutPanel6.Controls.Add(this.guna2Button9);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(288, 37);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel6.Size = new System.Drawing.Size(696, 46);
            this.flowLayoutPanel6.TabIndex = 16;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel5.Location = new System.Drawing.Point(558, 5);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(135, 36);
            this.guna2Panel5.TabIndex = 0;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel2.Controls.Add(this.number);
            this.guna2Panel2.Location = new System.Drawing.Point(345, 3);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(207, 40);
            this.guna2Panel2.TabIndex = 7;
            // 
            // number
            // 
            this.number.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.number.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            this.number.BorderRadius = 15;
            this.number.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.number.DefaultText = "";
            this.number.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.number.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.number.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.number.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.number.FocusedState.BorderColor = System.Drawing.Color.Goldenrod;
            this.number.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.number.HoverState.BorderColor = System.Drawing.Color.Goldenrod;
            this.number.Location = new System.Drawing.Point(5, 0);
            this.number.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.number.Name = "number";
            this.number.PasswordChar = '\0';
            this.number.PlaceholderText = "رقم الفاتوره";
            this.number.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.number.SelectedText = "";
            this.number.Size = new System.Drawing.Size(205, 29);
            this.number.TabIndex = 68;
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
            this.guna2Button2.Location = new System.Drawing.Point(92, 4);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button2.Size = new System.Drawing.Size(75, 28);
            this.guna2Button2.TabIndex = 67;
            this.guna2Button2.Text = "إكسل";
            this.guna2Button2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // guna2Button9
            // 
            this.guna2Button9.BorderRadius = 15;
            this.guna2Button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button9.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button9.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button9.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button9.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button9.FillColor = System.Drawing.Color.DodgerBlue;
            this.guna2Button9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.guna2Button9.Image = global::AccountingSystem.Properties.Resources.BxShareAlt;
            this.guna2Button9.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button9.ImageSize = new System.Drawing.Size(18, 18);
            this.guna2Button9.IndicateFocus = true;
            this.guna2Button9.Location = new System.Drawing.Point(4, 4);
            this.guna2Button9.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button9.Name = "guna2Button9";
            this.guna2Button9.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button9.Size = new System.Drawing.Size(80, 28);
            this.guna2Button9.TabIndex = 65;
            this.guna2Button9.Text = "مشاركة";
            this.guna2Button9.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSetsJournalEntry";
            reportDataSource1.Value = this.dataSetJournalEntryBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AccountingSystem.view.Reports.ReportBillsExchange.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(20, 89);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.ShowStopButton = false;
            this.reportViewer1.ShowToolBar = false;
            this.reportViewer1.Size = new System.Drawing.Size(964, 109);
            this.reportViewer1.TabIndex = 13;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
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
            this.btnShowOrHideToolBar.Location = new System.Drawing.Point(963, 203);
            this.btnShowOrHideToolBar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnShowOrHideToolBar.Name = "btnShowOrHideToolBar";
            this.btnShowOrHideToolBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnShowOrHideToolBar.Size = new System.Drawing.Size(20, 20);
            this.btnShowOrHideToolBar.TabIndex = 15;
            // 
            // dataSetJournalEntryBindingSource
            // 
            this.dataSetJournalEntryBindingSource.DataSource = typeof(AccountingSystem.NewModel.RCLDModel.DataSetJournalEntry);
            // 
            // BillsExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 753);
            this.Controls.Add(this.panelBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BillsExchange";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "سندات الفاتوره";
            this.Load += new System.EventHandler(this.BillsExchange_Load);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetJournalEntryBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelBody;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2TextBox number;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2Button btnPrint;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button9;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Guna.UI2.WinForms.Guna2CircleButton btnShowOrHideToolBar;
        private System.Windows.Forms.BindingSource dataSetJournalEntryBindingSource;
    }
}