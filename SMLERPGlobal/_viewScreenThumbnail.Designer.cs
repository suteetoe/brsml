namespace _viewCapture
{
    partial class _viewScreenThumbnail
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
            this._infoLabel = new System.Windows.Forms.Label();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _infoLabel
            // 
            this._infoLabel.AutoSize = true;
            this._infoLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._infoLabel.Location = new System.Drawing.Point(0, 107);
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.Size = new System.Drawing.Size(25, 13);
            this._infoLabel.TabIndex = 0;
            this._infoLabel.Text = "Info";
            // 
            // _pictureBox
            // 
            this._pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBox.Location = new System.Drawing.Point(6, 6);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(153, 100);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 1;
            this._pictureBox.TabStop = false;
            this._pictureBox.Click += new System.EventHandler(this._pictureBox_Click);
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Interval = 5000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _viewScreenThumbnail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._infoLabel);
            this.Name = "_viewScreenThumbnail";
            this.Size = new System.Drawing.Size(165, 120);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _infoLabel;
        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.Timer _timer;
    }
}
