namespace AccountingSystem
{
    partial class Test2
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
            
            this.customTable1 = new AccountingSystem.core.CustomControl.CustomTable();
            this.SuspendLayout();
            // 
            // invoiceRightScreen2
            // 
          
            // 
            // customTable1
            // 
            this.customTable1.Location = new System.Drawing.Point(844, 133);
            this.customTable1.Name = "customTable1";
            this.customTable1.Size = new System.Drawing.Size(189, 84);
            this.customTable1.style = null;
            this.customTable1.TabIndex = 5;
            // 
            // Test2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 493);
            this.Controls.Add(this.customTable1);
          
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Test2";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "Test2";
            this.Load += new System.EventHandler(this.Test2_Load);
            this.ResumeLayout(false);

        }

        #endregion
      
    
        private core.CustomControl.CustomTable customTable1;
    }
}