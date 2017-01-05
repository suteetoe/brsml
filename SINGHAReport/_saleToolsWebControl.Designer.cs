namespace SINGHAReport
{
    partial class _saleToolsWebControl
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
            this._browser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.closeButton = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _browser
            // 
            this._browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._browser.Location = new System.Drawing.Point(0, 25);
            this._browser.MinimumSize = new System.Drawing.Size(20, 20);
            this._browser.Name = "_browser";
            this._browser.ScriptErrorsSuppressed = true;
            this._browser.Size = new System.Drawing.Size(822, 759);
            this._browser.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(822, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // closeButton
            // 
            this.closeButton.Image = global::SINGHAReport.Properties.Resources.error;
            this.closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeButton.Name = "closeButton";
            this.closeButton.Padding = new System.Windows.Forms.Padding(1);
            this.closeButton.ResourceName = "ปิดจอ";
            this.closeButton.Size = new System.Drawing.Size(55, 22);
            this.closeButton.Text = "ปิดจอ";
            this.closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // _saleToolsWebControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._browser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_saleToolsWebControl";
            this.Size = new System.Drawing.Size(822, 784);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser _browser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton closeButton;
    }
}
