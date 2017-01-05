namespace SMLERPControl.erp_user
{
    partial class _erp_userDetailpicture
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
            this._myManageDetail = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._myToolBar = new System.Windows.Forms.StatusStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closebutton = new MyLib.ToolStripMyButton();
            this._getPicture1 = new SMLERPControl._getPicture();
            this._screenerp_user1 = new SMLERPControl.erp_user._screenerp_user();
            this._myManageDetail._form2.SuspendLayout();
            this._myManageDetail.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageDetail
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 0);
            this._myManageDetail.Name = "_myManageDetail";
            // 
            // _myManageDetail.Panel1
            // 
            this._myManageDetail._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageDetail.Panel2
            // 
            this._myManageDetail._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail._form2.Controls.Add(this._myPanel1);
            this._myManageDetail._form2.Controls.Add(this._myToolBar);
            this._myManageDetail.Size = new System.Drawing.Size(798, 553);
            this._myManageDetail.TabIndex = 0;
            this._myManageDetail.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._getPicture1);
            this._myPanel1.Controls.Add(this._screenerp_user1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 24);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(611, 527);
            this._myPanel1.TabIndex = 1;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this._myToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closebutton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(611, 24);
            this._myToolBar.TabIndex = 1;
            this._myToolBar.Text = "statusStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก";
            this._saveButton.Size = new System.Drawing.Size(58, 22);
            this._saveButton.Text = "บันทึก";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closebutton
            // 
            this._closebutton.Image = global::SMLERPControl.Properties.Resources.exit;
            this._closebutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closebutton.Name = "_closebutton";
            this._closebutton.Padding = new System.Windows.Forms.Padding(1);
            this._closebutton.ResourceName = "ปิด";
            this._closebutton.Size = new System.Drawing.Size(43, 22);
            this._closebutton.Text = "ปิด";
            this._closebutton.Click += new System.EventHandler(this._close_Click);
            // 
            // _getPicture1
            // 
            this._getPicture1._DisplayPictureAmount = 2;
            this._getPicture1._isScanner = false;
            this._getPicture1._isWebcam = false;
            this._getPicture1.AutoSize = true;
            this._getPicture1.BackColor = System.Drawing.Color.Transparent;
            this._getPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._getPicture1.Location = new System.Drawing.Point(0, 253);
            this._getPicture1.Name = "_getPicture1";
            this._getPicture1.Size = new System.Drawing.Size(611, 274);
            this._getPicture1.TabIndex = 1;
            // 
            // _screenerp_user1
            // 
            this._screenerp_user1._isChange = false;
            this._screenerp_user1.AutoSize = true;
            this._screenerp_user1.BackColor = System.Drawing.Color.Transparent;
            this._screenerp_user1.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenerp_user1.Enabled = false;
            this._screenerp_user1.Location = new System.Drawing.Point(0, 0);
            this._screenerp_user1.Name = "_screenerp_user1";
            this._screenerp_user1.Size = new System.Drawing.Size(611, 253);
            this._screenerp_user1.TabIndex = 2;
            // 
            // _erp_userDetailpicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            this.Name = "_erp_userDetailpicture";
            this.Size = new System.Drawing.Size(798, 553);
            this._myManageDetail._form2.ResumeLayout(false);
            this._myManageDetail._form2.PerformLayout();
            this._myManageDetail.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageDetail;
        private System.Windows.Forms.StatusStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closebutton;
        private MyLib._myPanel _myPanel1;
        private _getPicture _getPicture1;
        private _screenerp_user _screenerp_user1;
    }
}
