namespace SMLICIAdmin
{
    partial class _frmViewProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_frmViewProcess));
            this._lbllog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lbllog
            // 
            this._lbllog.AutoSize = true;
            this._lbllog.Dock = System.Windows.Forms.DockStyle.Top;
            this._lbllog.Location = new System.Drawing.Point(0, 0);
            this._lbllog.Name = "_lbllog";
            this._lbllog.Size = new System.Drawing.Size(25, 14);
            this._lbllog.TabIndex = 2;
            this._lbllog.Text = "xxx";
            // 
            // _frmViewProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(776, 456);
            this.Controls.Add(this._lbllog);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_frmViewProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewProcess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label _lbllog;
    }
}