namespace SMLERPAPARControl
{
    partial class _ar_pay_bill_auto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_ar_pay_bill_auto));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this._myButtonChecking = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this._myPanel1 = new MyLib._myPanel();
            this._grouper3 = new MyLib._grouper();
            this._arGridPayBillAuto = new SMLERPAPARControl._arGridPayBillAuto();
            this._grouper1 = new MyLib._grouper();
            this._arScreenToPPayBillAuto1 = new SMLERPAPARControl._arScreenToPPayBillAuto();
            this._myToolbar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._grouper3.SuspendLayout();
            this._grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "selected.png");
            this.imageList1.Images.SetKeyName(1, "select.png");
            // 
            // _myToolbar
            // 
            this._myToolbar.BackColor = System.Drawing.Color.Transparent;
            this._myToolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this.toolStripSeparator1,
            this.toolStripMyButton1,
            this._myButtonChecking,
            this.toolStripSeparator2,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(970, 25);
            this._myToolbar.TabIndex = 3;
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
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.ResourceName = "กำหนดเงือนไข";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.Size = new System.Drawing.Size(143, 22);
            this.toolStripMyButton1.Text = "กำหหนดเงือนไข (F11)";
            this.toolStripMyButton1.Click += new System.EventHandler(this.toolStripMyButton1_Click);
            // 
            // _myButtonChecking
            // 
            this._myButtonChecking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._myButtonChecking.Name = "_myButtonChecking";
            this._myButtonChecking.ResourceName = "เลือกทั้งหมด";
            this._myButtonChecking.Padding = new System.Windows.Forms.Padding(1);
            this._myButtonChecking.Size = new System.Drawing.Size(109, 22);
            this._myButtonChecking.Text = "เลือกทั้งหมด (F10)";
            this._myButtonChecking.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this._myButtonChecking.Click += new System.EventHandler(this._myButtonChecking_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(970, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(400, 16);
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper3);
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(970, 495);
            this._myPanel1.TabIndex = 9;
            // 
            // _grouper3
            // 
            this._grouper3.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper3.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper3.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper3.BorderColor = System.Drawing.Color.White;
            this._grouper3.BorderThickness = 1F;
            this._grouper3.Controls.Add(this._arGridPayBillAuto);
            this._grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper3.GroupImage = null;
            this._grouper3.GroupTitle = "";
            this._grouper3.Location = new System.Drawing.Point(5, 67);
            this._grouper3.Name = "_grouper3";
            this._grouper3.Padding = new System.Windows.Forms.Padding(5);
            this._grouper3.PaintGroupBox = false;
            this._grouper3.RoundCorners = 10;
            this._grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper3.ShadowControl = false;
            this._grouper3.ShadowThickness = 3;
            this._grouper3.Size = new System.Drawing.Size(960, 423);
            this._grouper3.TabIndex = 14;
            // 
            // _arGridPayBillAuto
            // 
            this._arGridPayBillAuto.AutoSize = true;
            this._arGridPayBillAuto.BackColor = System.Drawing.SystemColors.Window;
            this._arGridPayBillAuto.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._arGridPayBillAuto.ColumnBackgroundAuto = false;
            this._arGridPayBillAuto.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._arGridPayBillAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this._arGridPayBillAuto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._arGridPayBillAuto.Location = new System.Drawing.Point(5, 5);
            this._arGridPayBillAuto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._arGridPayBillAuto.Name = "_arGridPayBillAuto";
            this._arGridPayBillAuto.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._arGridPayBillAuto.ShowTotal = true;
            this._arGridPayBillAuto.Size = new System.Drawing.Size(950, 413);
            this._arGridPayBillAuto.TabIndex = 0;
            this._arGridPayBillAuto.TabStop = false;
            // 
            // _grouper1
            // 
            this._grouper1.AutoSize = true;
            this._grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.White;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._arScreenToPPayBillAuto1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Top;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(5, 5);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(960, 62);
            this._grouper1.TabIndex = 12;
            // 
            // _arScreenToPPayBillAuto1
            // 
            this._arScreenToPPayBillAuto1._isChange = false;
            this._arScreenToPPayBillAuto1.AutoSize = true;
            this._arScreenToPPayBillAuto1.BackColor = System.Drawing.Color.Transparent;
            this._arScreenToPPayBillAuto1.Dock = System.Windows.Forms.DockStyle.Top;
            this._arScreenToPPayBillAuto1.Location = new System.Drawing.Point(5, 5);
            this._arScreenToPPayBillAuto1.Name = "_arScreenToPPayBillAuto1";
            this._arScreenToPPayBillAuto1.Padding = new System.Windows.Forms.Padding(3);
            this._arScreenToPPayBillAuto1.Size = new System.Drawing.Size(950, 52);
            this._arScreenToPPayBillAuto1.TabIndex = 0;
            // 
            // _ar_pay_bill_auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._myToolbar);
            this.Name = "_ar_pay_bill_auto";
            this.Size = new System.Drawing.Size(970, 542);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper3.ResumeLayout(false);
            this._grouper3.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public MyLib.ToolStripMyButton _closeButton;
        private MyLib.ToolStripMyButton toolStripMyButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private MyLib.ToolStripMyButton _myButtonChecking;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private MyLib._myPanel _myPanel1;
        private MyLib._grouper _grouper3;
        private _arGridPayBillAuto _arGridPayBillAuto;
        private MyLib._grouper _grouper1;
        private _arScreenToPPayBillAuto _arScreenToPPayBillAuto1;
    }
}
