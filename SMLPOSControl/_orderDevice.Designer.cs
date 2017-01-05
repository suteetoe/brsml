namespace SMLPOSControl
{
    partial class _orderDevice
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
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._orderDeviceScreenTop1 = new SMLPOSControl._orderDeviceScreenTop();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(994, 499);
            this._myManageData1.TabIndex = 4;
            this._myManageData1.TabStop = false;
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.White;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this.toolStrip1);
            this._myManageData1.Size = new System.Drawing.Size(462, 418);
            this._myManageData1.TabIndex = 4;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._orderDeviceScreenTop1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(909, 453);
            this._myPanel1.TabIndex = 1;
            // 
            // _orderDeviceScreenTop1
            // 
            this._orderDeviceScreenTop1._isChange = false;
            this._orderDeviceScreenTop1.BackColor = System.Drawing.Color.Transparent;
            this._orderDeviceScreenTop1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderDeviceScreenTop1.Location = new System.Drawing.Point(0, 0);
            this._orderDeviceScreenTop1.Name = "_orderDeviceScreenTop1";
            this._orderDeviceScreenTop1.Size = new System.Drawing.Size(909, 453);
            this._orderDeviceScreenTop1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(909, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(113, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            this._saveButton.Click += new System.EventHandler(_saveButton_Click);
            // 
            // _saveButton
            // 
            this._closeButton.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(113, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.Click += new System.EventHandler(_closeButton_Click);
            // 
            // _orderDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_orderDevice";
            this.Size = new System.Drawing.Size(994, 499);
            this._myPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private _orderDeviceScreenTop _orderDeviceScreenTop1;
    }
}
