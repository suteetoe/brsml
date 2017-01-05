namespace SMLERPControl._bank
{
    partial class _pettyCashMasterControl
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
            this._myManagePettyCash = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._pettyCashMasterScreenTop = new SMLERPControl._bank._pettyCashMasterScreenTopControl();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myManagePettyCash._form2.SuspendLayout();
            this._myManagePettyCash.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManagePettyCash
            // 
            this._myManagePettyCash.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManagePettyCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManagePettyCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManagePettyCash.Location = new System.Drawing.Point(0, 0);
            this._myManagePettyCash.Name = "_myManagePettyCash";
            // 
            // _myManagePettyCash.Panel1
            // 
            this._myManagePettyCash._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManagePettyCash.Panel2
            // 
            this._myManagePettyCash._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManagePettyCash._form2.Controls.Add(this._myPanel1);
            this._myManagePettyCash.Size = new System.Drawing.Size(763, 604);
            this._myManagePettyCash.TabIndex = 0;
            this._myManagePettyCash.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._pettyCashMasterScreenTop);
            this._myPanel1.Controls.Add(this._myToolbar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(587, 602);
            this._myPanel1.TabIndex = 0;
            // 
            // _pettyCashMasterScreenTop
            // 
            this._pettyCashMasterScreenTop._isChange = false;
            this._pettyCashMasterScreenTop.AutoSize = true;
            this._pettyCashMasterScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._pettyCashMasterScreenTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pettyCashMasterScreenTop.Location = new System.Drawing.Point(0, 25);
            this._pettyCashMasterScreenTop.Name = "_pettyCashMasterScreenTop";
            this._pettyCashMasterScreenTop.pettyCashMasterControlType = _pettyCashMasterControlTypeEnum.ว่าง;
            this._pettyCashMasterScreenTop.Size = new System.Drawing.Size(587, 577);
            this._pettyCashMasterScreenTop.TabIndex = 7;
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
            this._myToolbar.Size = new System.Drawing.Size(587, 25);
            this._myToolbar.TabIndex = 6;
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
            // _pettyCashMasterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManagePettyCash);
            this.Name = "_pettyCashMasterControl";
            this.Size = new System.Drawing.Size(763, 604);
            this._myManagePettyCash._form2.ResumeLayout(false);
            this._myManagePettyCash._form2.PerformLayout();
            this._myManagePettyCash.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        public MyLib._myManageData _myManagePettyCash;
        private _pettyCashMasterScreenTopControl _pettyCashMasterScreenTop;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public MyLib.ToolStripMyButton _closeButton;
    }
}
