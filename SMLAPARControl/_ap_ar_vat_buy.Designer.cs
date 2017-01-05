namespace SMLERPAPARControl
{
    partial class _ap_ar_vat_buy
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
            this._vatBuy1 = new SMLERPGLControl._vatBuy();
            this._screenTop = new SMLERPGLControl._journalScreen();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(956, 524);
            this._myPanel1.TabIndex = 11;
            // 
            // _grouper1
            // 
            this._grouper1.AutoSize = true;
            this._grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.White;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._vatBuy1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(5, 126);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(946, 393);
            this._grouper1.TabIndex = 19;
            // 
            // _vatBuy1
            // 
            this._vatBuy1.BackColor = System.Drawing.Color.Transparent;
            this._vatBuy1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatBuy1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._vatBuy1.Location = new System.Drawing.Point(5, 5);
            this._vatBuy1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._vatBuy1.Name = "_vatBuy1";
            this._vatBuy1.Size = new System.Drawing.Size(936, 383);
            this._vatBuy1.TabIndex = 0;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(5, 5);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Padding = new System.Windows.Forms.Padding(3);
            this._screenTop.Size = new System.Drawing.Size(946, 121);
            this._screenTop.TabIndex = 18;
            // 
            // _closeButton
            // 
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.Size = new System.Drawing.Size(44, 22);
            this._closeButton.Text = "ปิด";
            // 
            // _myToolbar
            // 
            this._myToolbar.BackColor = System.Drawing.Color.Transparent;
            this._myToolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this.toolStripSeparator1,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(956, 25);
            this._myToolbar.TabIndex = 10;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.Size = new System.Drawing.Size(94, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _ap_ar_vat_buy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(956, 549);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "_ap_ar_vat_buy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        public MyLib.ToolStripMyButton _closeButton;
        public System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib._grouper _grouper1;
        private SMLERPGLControl._vatBuy _vatBuy1;
        private SMLERPGLControl._journalScreen _screenTop;
    }
}