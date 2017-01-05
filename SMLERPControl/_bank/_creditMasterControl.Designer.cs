namespace SMLERPControl._bank
{
    partial class _creditMasterControl
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
            this._myManageCreditMaster = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._creditMasterScreenTop = new SMLERPControl._bank._creditMasterScreenTopControl();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myManageCreditMaster._form2.SuspendLayout();
            this._myManageCreditMaster.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageCreditMaster
            // 
            this._myManageCreditMaster.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageCreditMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageCreditMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageCreditMaster.Location = new System.Drawing.Point(0, 0);
            this._myManageCreditMaster.Name = "_myManageCreditMaster";
            // 
            // _myManageCreditMaster.Panel1
            // 
            this._myManageCreditMaster._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageCreditMaster.Panel2
            // 
            this._myManageCreditMaster._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageCreditMaster._form2.Controls.Add(this._myPanel1);
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._creditMasterScreenTop);
            this._myPanel1.Controls.Add(this._myToolbar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(548, 629);
            this._myPanel1.TabIndex = 0;
            // 
            // _creditMasterScreenTop
            // 
            this._creditMasterScreenTop._isChange = false;
            this._creditMasterScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._creditMasterScreenTop.creditMasterControlType = SMLERPControl._bank._creditMasterControlTypeEnum.credit_master_receive;
            this._creditMasterScreenTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._creditMasterScreenTop.Location = new System.Drawing.Point(0, 25);
            this._creditMasterScreenTop.Name = "_creditMasterScreenTop";
            this._creditMasterScreenTop.Size = new System.Drawing.Size(548, 604);
            this._creditMasterScreenTop.TabIndex = 6;
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
            this._myToolbar.Size = new System.Drawing.Size(548, 25);
            this._myToolbar.TabIndex = 5;
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
            // _creditMasterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageCreditMaster);
            this.Name = "_creditMasterControl";
            this.Size = new System.Drawing.Size(713, 631);
            this._myManageCreditMaster._form2.ResumeLayout(false);
            this._myManageCreditMaster.ResumeLayout(false);
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
        private _creditMasterScreenTopControl _creditMasterScreenTop;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib._myManageData _myManageCreditMaster;
    }
}
