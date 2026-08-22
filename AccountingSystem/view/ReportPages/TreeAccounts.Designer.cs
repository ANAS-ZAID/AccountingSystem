namespace AccountingSystem.view.ReportPages
{
    partial class TreeAccounts
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
            this.chartOfAccountsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetReports = new AccountingSystem.NewModel.RCLDModel.DataSetReports();
            this.btnReferesh = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnPrint = new Guna.UI2.WinForms.Guna2Button();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chartOfAccountsTableAdapter = new AccountingSystem.NewModel.RCLDModel.DataSetReportsTableAdapters.ChartOfAccountsTableAdapter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chartOfAccountsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetReports)).BeginInit();
            this.panelBody.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartOfAccountsBindingSource
            // 
            this.chartOfAccountsBindingSource.DataMember = "ChartOfAccounts";
            this.chartOfAccountsBindingSource.DataSource = this.dataSetReports;
            // 
            // dataSetReports
            // 
            this.dataSetReports.DataSetName = "DataSetReports";
            this.dataSetReports.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.btnReferesh.Location = new System.Drawing.Point(459, 36);
            this.btnReferesh.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnReferesh.Name = "btnReferesh";
            this.btnReferesh.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnReferesh.Size = new System.Drawing.Size(35, 35);
            this.btnReferesh.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnReferesh, "تحديث");
            this.btnReferesh.Click += new System.EventHandler(this.btnReferesh_Click);
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
            this.guna2Button4.IndicateFocus = true;
            this.guna2Button4.Location = new System.Drawing.Point(171, 4);
            this.guna2Button4.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button4.Size = new System.Drawing.Size(159, 30);
            this.guna2Button4.TabIndex = 5;
            this.guna2Button4.Text = "إكسل";
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 15;
            this.guna2Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.DodgerBlue;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.guna2Button1.Image = global::AccountingSystem.Properties.Resources.BxShareAlt;
            this.guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.IndicateFocus = true;
            this.guna2Button1.Location = new System.Drawing.Point(4, 4);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.guna2Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.guna2Button1.Size = new System.Drawing.Size(159, 30);
            this.guna2Button1.TabIndex = 3;
            this.guna2Button1.Text = "مشاركة";
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
            this.btnPrint.IndicateFocus = true;
            this.btnPrint.Location = new System.Drawing.Point(338, 4);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrint.Size = new System.Drawing.Size(159, 30);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "طباعه";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Image = global::AccountingSystem.Properties.Resources.TablerArrowBadgeDownFilled;
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(23, 23);
            this.guna2CircleButton1.Location = new System.Drawing.Point(6, 80);
            this.guna2CircleButton1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(23, 22);
            this.guna2CircleButton1.TabIndex = 14;
            this.toolTip1.SetToolTip(this.guna2CircleButton1, "عرض المزيد من خيارات الطباعه");
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 1;
            reportDataSource1.Name = "DataSetTreeAccounts";
            reportDataSource1.Value = this.chartOfAccountsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AccountingSystem.view.Reports.ReportTreeAccounts.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(36, 81);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowCredentialPrompts = false;
            this.reportViewer1.ShowDocumentMapButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.ShowProgress = false;
            this.reportViewer1.ShowStopButton = false;
            this.reportViewer1.ShowToolBar = false;
            this.reportViewer1.Size = new System.Drawing.Size(966, 126);
            this.reportViewer1.TabIndex = 13;
            // 
            // chartOfAccountsTableAdapter
            // 
            this.chartOfAccountsTableAdapter.ClearBeforeFill = true;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Controls.Add(this.flowLayoutPanel4);
            this.panelBody.Controls.Add(this.flowLayoutPanel6);
            this.panelBody.Controls.Add(this.btnReferesh);
            this.panelBody.Controls.Add(this.reportViewer1);
            this.panelBody.Controls.Add(this.guna2CircleButton1);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panelBody.Size = new System.Drawing.Size(1005, 706);
            this.panelBody.TabIndex = 20;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Location = new System.Drawing.Point(1, 3);
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
            this.flowLayoutPanel6.Controls.Add(this.btnPrint);
            this.flowLayoutPanel6.Controls.Add(this.guna2Button4);
            this.flowLayoutPanel6.Controls.Add(this.guna2Button1);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(501, 37);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel6.Size = new System.Drawing.Size(501, 38);
            this.flowLayoutPanel6.TabIndex = 16;
            // 
            // TreeAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 706);
            this.Controls.Add(this.panelBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TreeAccounts";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "شجرة الحسابات";
            this.Load += new System.EventHandler(this.TreeAccounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartOfAccountsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetReports)).EndInit();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button guna2Button4;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button btnPrint;
        private Guna.UI2.WinForms.Guna2CircleButton btnReferesh;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private NewModel.RCLDModel.DataSetReports dataSetReports;
        private System.Windows.Forms.BindingSource chartOfAccountsBindingSource;
        private NewModel.RCLDModel.DataSetReportsTableAdapters.ChartOfAccountsTableAdapter chartOfAccountsTableAdapter;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel panelBody;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
    }
}