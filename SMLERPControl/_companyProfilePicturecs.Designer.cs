namespace SMLERPControl
{
    partial class _companyProfilePicturecs
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
            this._myPanel1 = new MyLib._myPanel();
            this._grouper1 = new MyLib._grouper();
            this._getPicture1 = new SMLERPControl._getPicture();
            this._companyProfileScreen1 = new SMLERPControl._companyProfileScreen();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.Controls.Add(this._companyProfileScreen1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(536, 469);
            this._myPanel1.TabIndex = 0;
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._getPicture1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "Logo";
            this._grouper1.Location = new System.Drawing.Point(5, 55);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(5, 28, 5, 20);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(526, 409);
            this._grouper1.TabIndex = 1;
            // 
            // _getPicture1
            // 
            this._getPicture1._DisplayPictureAmount = 0;
            this._getPicture1._isScanner = false;
            this._getPicture1._isWebcam = false;
            this._getPicture1.AutoSize = true;
            this._getPicture1.BackColor = System.Drawing.Color.Transparent;
            this._getPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._getPicture1.Location = new System.Drawing.Point(5, 28);
            this._getPicture1.Name = "_getPicture1";
            this._getPicture1.Size = new System.Drawing.Size(516, 361);
            this._getPicture1.TabIndex = 0;
            // 
            // _companyProfileScreen1
            // 
            this._companyProfileScreen1._isChange = false;
            this._companyProfileScreen1.BackColor = System.Drawing.Color.Transparent;
            this._companyProfileScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._companyProfileScreen1.Location = new System.Drawing.Point(5, 5);
            this._companyProfileScreen1.Name = "_companyProfileScreen1";
            this._companyProfileScreen1.Size = new System.Drawing.Size(526, 50);
            this._companyProfileScreen1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this.toolStripSeparator1,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(536, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.Size = new System.Drawing.Size(75, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _companyProfilePicturecs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 494);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_companyProfilePicturecs";
            this.Text = "Logo Select";
            this.Load += new System.EventHandler(this._companyProfilePicturecs_Load);
            this._myPanel1.ResumeLayout(false);
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._grouper _grouper1;
        private _getPicture _getPicture1;
        private _companyProfileScreen _companyProfileScreen1;
    }
}