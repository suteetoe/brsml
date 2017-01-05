namespace MyLib
{
    partial class _screenCaptureForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._refreshButton = new System.Windows.Forms.ToolStripButton();
            this._statusLabel = new System.Windows.Forms.ToolStripLabel();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._refreshButton,
            this._statusLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(776, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _pictureBox
            // 
            this._pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pictureBox.Location = new System.Drawing.Point(0, 25);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(776, 594);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 1;
            this._pictureBox.TabStop = false;
            // 
            // _refreshButton
            // 
            this._refreshButton.Image = global::MyLib.Properties.Resources.refresh;
            this._refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(66, 22);
            this._refreshButton.Text = "Refresh";
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(39, 22);
            this._statusLabel.Text = "Status";
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _screenCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 619);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_screenCaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_screenCaptureForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _refreshButton;
        private System.Windows.Forms.ToolStripLabel _statusLabel;
        private System.Windows.Forms.Timer _timer;
    }
}