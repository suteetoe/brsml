namespace SMLERPAPARControl
{
    partial class _search_bill_auto
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
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._myPanel1 = new MyLib._myPanel();
            this._grouper2 = new MyLib._grouper();
            this._OkButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._arScreenSearchPayBillAuto1 = new SMLERPAPARControl._arScreenSearchPayBillAuto();
            this._myToolbar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._grouper2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.BackColor = System.Drawing.Color.Transparent;
            this._myToolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OkButton,
            this.toolStripSeparator1,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Padding = new System.Windows.Forms.Padding(0);
            this._myToolbar.Size = new System.Drawing.Size(536, 25);
            this._myToolbar.TabIndex = 5;
            this._myToolbar.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(536, 141);
            this._myPanel1.TabIndex = 6;
            // 
            // _grouper2
            // 
            this._grouper2.AutoSize = true;
            this._grouper2.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper2.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper2.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper2.BorderColor = System.Drawing.Color.White;
            this._grouper2.BorderThickness = 1F;
            this._grouper2.Controls.Add(this._arScreenSearchPayBillAuto1);
            this._grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper2.Dock = System.Windows.Forms.DockStyle.Top;
            this._grouper2.GroupImage = null;
            this._grouper2.GroupTitle = "";
            this._grouper2.Location = new System.Drawing.Point(3, 3);
            this._grouper2.Name = "_grouper2";
            this._grouper2.Padding = new System.Windows.Forms.Padding(5);
            this._grouper2.PaintGroupBox = false;
            this._grouper2.RoundCorners = 10;
            this._grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper2.ShadowControl = false;
            this._grouper2.ShadowThickness = 3;
            this._grouper2.Size = new System.Drawing.Size(530, 133);
            this._grouper2.TabIndex = 14;
            // 
            // _OkButton
            // 
            this._OkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OkButton.Name = "_OkButton";
            this._OkButton.ResourceName = "ตกลง";
            this._OkButton.Padding = new System.Windows.Forms.Padding(1);
            this._OkButton.Size = new System.Drawing.Size(90, 22);
            this._OkButton.Text = "ตกลง (F12)";
            this._OkButton.Click += new System.EventHandler(this._OkButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.Size = new System.Drawing.Size(44, 22);
            this._closeButton.Text = "ปิด";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _arScreenSearchPayBillAuto1
            // 
            this._arScreenSearchPayBillAuto1._isChange = false;
            this._arScreenSearchPayBillAuto1.AutoSize = true;
            this._arScreenSearchPayBillAuto1.BackColor = System.Drawing.Color.Transparent;
            this._arScreenSearchPayBillAuto1.Dock = System.Windows.Forms.DockStyle.Top;
            this._arScreenSearchPayBillAuto1.Location = new System.Drawing.Point(5, 5);
            this._arScreenSearchPayBillAuto1.Name = "_arScreenSearchPayBillAuto1";
            this._arScreenSearchPayBillAuto1.Padding = new System.Windows.Forms.Padding(3);
            this._arScreenSearchPayBillAuto1.Size = new System.Drawing.Size(520, 123);
            this._arScreenSearchPayBillAuto1.TabIndex = 0;
            // 
            // _search_bill_auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(536, 166);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_search_bill_auto";
            this.Opacity = 0.8;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper2.ResumeLayout(false);
            this._grouper2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _OkButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel1;
        private MyLib._grouper _grouper2;
        private _arScreenSearchPayBillAuto _arScreenSearchPayBillAuto1;

    }
}