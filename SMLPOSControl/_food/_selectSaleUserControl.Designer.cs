namespace SMLPOSControl._food
{
    partial class _selectSaleUserControl
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
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._zoomInButton = new System.Windows.Forms.ToolStripButton();
            this._zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayoutPanel.Location = new System.Drawing.Point(0, 25);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Size = new System.Drawing.Size(1080, 626);
            this._flowLayoutPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._zoomInButton,
            this._zoomOutButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1080, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _zoomInButton
            // 
            this._zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._zoomInButton.Name = "_zoomInButton";
            this._zoomInButton.Size = new System.Drawing.Size(56, 22);
            this._zoomInButton.Text = "Zoom In";
            this._zoomInButton.Click += new System.EventHandler(this._zoomInButton_Click);
            // 
            // _zoomOutButton
            // 
            this._zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._zoomOutButton.Name = "_zoomOutButton";
            this._zoomOutButton.Size = new System.Drawing.Size(66, 22);
            this._zoomOutButton.Text = "Zoom Out";
            this._zoomOutButton.Click += new System.EventHandler(this._zoomOutButton_Click);
            // 
            // _selectSaleUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._flowLayoutPanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_selectSaleUserControl";
            this.Size = new System.Drawing.Size(1080, 651);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _zoomInButton;
        private System.Windows.Forms.ToolStripButton _zoomOutButton;
    }
}
