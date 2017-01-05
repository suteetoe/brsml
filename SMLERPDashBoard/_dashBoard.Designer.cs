namespace SMLERPDashBoard
{
    partial class _dashBoard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.purchase_system = new System.Windows.Forms.ToolStripMenuItem();
            this.po_over_due = new System.Windows.Forms.ToolStripMenuItem();
            this.information = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 638);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(923, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchase_system,
            this.information});
            this.toolStripDropDownButton1.Image = global::SMLERPDashBoard.Properties.Resources.add;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // purchase_system
            // 
            this.purchase_system.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.po_over_due});
            this.purchase_system.Name = "purchase_system";
            this.purchase_system.Size = new System.Drawing.Size(126, 22);
            this.purchase_system.Text = "ระบบซื้อ";
            // 
            // po_over_due
            // 
            this.po_over_due.Name = "po_over_due";
            this.po_over_due.Size = new System.Drawing.Size(165, 22);
            this.po_over_due.Text = "ใบสั่งซื้อครบกำหนด";
            // 
            // information
            // 
            this.information.Name = "information";
            this.information.Size = new System.Drawing.Size(126, 22);
            this.information.Text = "ข้อมูลทั่วไป";
            this.information.Click += new System.EventHandler(this.information_Click);
            // 
            // _dashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_dashBoard";
            this.Size = new System.Drawing.Size(923, 663);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem purchase_system;
        private System.Windows.Forms.ToolStripMenuItem po_over_due;
        private System.Windows.Forms.ToolStripMenuItem information;
    }
}
