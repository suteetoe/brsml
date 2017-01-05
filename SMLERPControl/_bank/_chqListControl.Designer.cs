namespace SMLERPControl._bank
{
    partial class _chqListControl
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
            this._myManagechqList = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._chqListScreenTop = new SMLERPControl._bank._chqListScreenTopControl();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._chqMovement = new _chqMovement();
            this._myManagechqList._form2.SuspendLayout();
            this._myManagechqList.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManagechqList
            // 
            this._myManagechqList._mainMenuCode = "";
            this._myManagechqList._mainMenuId = "";
            this._myManagechqList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManagechqList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManagechqList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManagechqList.Location = new System.Drawing.Point(0, 0);
            this._myManagechqList.Name = "_myManagechqList";
            // 
            // _myManagechqList.Panel1
            // 
            this._myManagechqList._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManagechqList.Panel2
            // 
            this._myManagechqList._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManagechqList._form2.Controls.Add(this._myPanel1);
            this._myManagechqList.Size = new System.Drawing.Size(818, 695);
            this._myManagechqList.TabIndex = 0;
            this._myManagechqList.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._chqMovement);
            this._myPanel1.Controls.Add(this._chqListScreenTop);
            this._myPanel1.Controls.Add(this._myToolbar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(629, 693);
            this._myPanel1.TabIndex = 0;
            // 
            // _chqListScreenTop
            // 
            this._chqListScreenTop._isChange = false;
            this._chqListScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._chqListScreenTop._chqListControlType = SMLERPControl._bank._chqListControlTypeEnum.ว่าง;
            this._chqListScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._chqListScreenTop.Location = new System.Drawing.Point(0, 25);
            this._chqListScreenTop.Name = "_chqListScreenTop";
            this._chqListScreenTop.Size = new System.Drawing.Size(100, 10);
            this._chqListScreenTop.TabIndex = 5;
            //
            // _chqMovement
            //
            this._chqMovement._extraWordShow = true;
            this._chqMovement._selectRow = -1;
            this._chqMovement.BackColor = System.Drawing.SystemColors.Window;
            this._chqMovement.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._chqMovement.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._chqMovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chqMovement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._chqMovement.Location = new System.Drawing.Point(0, 161);
            this._chqMovement.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._chqMovement.Name = "_chqMovement";
            this._chqMovement.Size = new System.Drawing.Size(810, 649);
            this._chqMovement.TabIndex = 3;
            this._chqMovement.TabStop = false;
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
            this._myToolbar.Size = new System.Drawing.Size(629, 25);
            this._myToolbar.TabIndex = 4;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(94, 22);
            this._saveButton.Text = "บันทึก (F12)";
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
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Size = new System.Drawing.Size(79, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _chqListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManagechqList);
            this.Name = "_chqListControl";
            this.Size = new System.Drawing.Size(818, 695);
            this._myManagechqList._form2.ResumeLayout(false);
            this._myManagechqList.ResumeLayout(false);
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
        private _chqListScreenTopControl _chqListScreenTop;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib._myManageData _myManagechqList;
        public _chqMovement _chqMovement;
    }
}
