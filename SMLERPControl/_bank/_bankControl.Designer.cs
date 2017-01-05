namespace SMLERPControl._bank
{
    partial class _bankControl
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
            this._myManageBank = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._bankDetailGrid = new SMLERPControl._bank._bankDetailGridControl();
            this._bankScreenBottom = new SMLERPControl._bank._bankScreenBottomControl();
            this._bankScreenTop = new SMLERPControl._bank._bankScreenTopControl();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._myManageBank._form2.SuspendLayout();
            this._myManageBank.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageBank
            // 
            this._myManageBank.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageBank.Location = new System.Drawing.Point(0, 0);
            this._myManageBank.Name = "_myManageBank";
            // 
            // _myManageBank.Panel1
            // 
            this._myManageBank._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageBank.Panel2
            // 
            this._myManageBank._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageBank._form2.Controls.Add(this._myPanel1);
            this._myManageBank.Size = new System.Drawing.Size(1001, 730);
            this._myManageBank.TabIndex = 0;
            this._myManageBank.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._bankDetailGrid);
            this._myPanel1.Controls.Add(this._bankScreenBottom);
            this._myPanel1.Controls.Add(this._bankScreenTop);
            this._myPanel1.Controls.Add(this._myToolbar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(771, 728);
            this._myPanel1.TabIndex = 0;
            // 
            // _bankDetailGrid
            // 
            this._bankDetailGrid.AutoSize = true;
            this._bankDetailGrid.BackColor = System.Drawing.SystemColors.Window;
            this._bankDetailGrid.BankControlType = SMLERPControl._bank._bankControlTypeEnum.received;
            this._bankDetailGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._bankDetailGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._bankDetailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bankDetailGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._bankDetailGrid.Location = new System.Drawing.Point(0, 230);
            this._bankDetailGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._bankDetailGrid.Name = "_bankDetailGrid";
            this._bankDetailGrid.Size = new System.Drawing.Size(771, 452);
            this._bankDetailGrid.TabIndex = 5;
            this._bankDetailGrid.TabStop = false;
            // 
            // _bankScreenBottom
            // 
            this._bankScreenBottom._isChange = false;
            this._bankScreenBottom.AutoSize = true;
            this._bankScreenBottom.BackColor = System.Drawing.Color.Transparent;
            this._bankScreenBottom.BankControlType = SMLERPControl._bank._bankControlTypeEnum.received;
            this._bankScreenBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bankScreenBottom.Location = new System.Drawing.Point(0, 682);
            this._bankScreenBottom.Name = "_bankScreenBottom";
            this._bankScreenBottom.Size = new System.Drawing.Size(771, 46);
            this._bankScreenBottom.TabIndex = 4;
            // 
            // _bankScreenTop
            // 
            this._bankScreenTop._isChange = false;
            this._bankScreenTop.AutoSize = true;
            this._bankScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._bankScreenTop.BankControlType = SMLERPControl._bank._bankControlTypeEnum.received;
            this._bankScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._bankScreenTop.Location = new System.Drawing.Point(0, 25);
            this._bankScreenTop.Name = "_bankScreenTop";
            this._bankScreenTop.Size = new System.Drawing.Size(771, 205);
            this._bankScreenTop.TabIndex = 3;
            // 
            // _myToolbar
            // 
            this._myToolbar.BackColor = System.Drawing.Color.Transparent;
            this._myToolbar.BackgroundImage = global::SMLERPControl.Properties.Resources.bt031;
            this._myToolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this.toolStripSeparator1,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(771, 25);
            this._myToolbar.TabIndex = 2;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
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
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.Size = new System.Drawing.Size(44, 22);
            this._closeButton.Text = "ปิด";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _bankControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageBank);
            this.Name = "_bankControl";
            this.Size = new System.Drawing.Size(1001, 730);
            this._myManageBank._form2.ResumeLayout(false);
            this._myManageBank._form2.PerformLayout();
            this._myManageBank.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private _bankScreenTopControl _bankScreenTop;        
        private _bankScreenBottomControl _bankScreenBottom;
        private _bankDetailGridControl _bankDetailGrid;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib._myManageData _myManageBank;
        private System.Windows.Forms.Timer timer1;
    }
}
