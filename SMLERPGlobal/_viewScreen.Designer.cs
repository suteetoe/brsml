namespace _viewCapture
{
    partial class _viewScreen
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1061, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::_g.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayoutPanel.Location = new System.Drawing.Point(0, 25);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Size = new System.Drawing.Size(1061, 758);
            this._flowLayoutPanel.TabIndex = 1;
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Interval = 5000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _viewScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._flowLayoutPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_viewScreen";
            this.Size = new System.Drawing.Size(1061, 783);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;
        private System.Windows.Forms.Timer _timer;
    }
}
