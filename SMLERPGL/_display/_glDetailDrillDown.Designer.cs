namespace SMLERPGL._display
{
    partial class _glDetailDrillDown
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
            this._glDetail1 = new SMLERPGLControl._glDetail();
            this._screenTop = new SMLERPGLControl._journalScreen();
            this.SuspendLayout();
            // 
            // _glDetail1
            // 
            this._glDetail1.BackColor = System.Drawing.Color.Transparent;
            this._glDetail1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._glDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetail1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetail1.Location = new System.Drawing.Point(4, 119);
            this._glDetail1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetail1.Name = "_glDetail1";
            this._glDetail1.Size = new System.Drawing.Size(762, 403);
            this._glDetail1.TabIndex = 1;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Enabled = false;
            this._screenTop.Location = new System.Drawing.Point(4, 4);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(762, 115);
            this._screenTop.TabIndex = 0;
            // 
            // _glDetailDrillDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(770, 526);
            this.Controls.Add(this._glDetail1);
            this.Controls.Add(this._screenTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_glDetailDrillDown";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_glDetailDrillDown";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPGLControl._glDetail _glDetail1;
        public SMLERPGLControl._journalScreen _screenTop;

    }
}