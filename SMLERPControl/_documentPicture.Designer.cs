namespace SMLERPControl
{
    partial class _documentPicture
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._documentPictureScreenTopControl1 = new SMLERPControl._documentPictureScreenTopControl();
            this._getPicture1 = new SMLERPControl._getPicture();
            this._myPanel1 = new MyLib._myPanel();
            this._myManageData1 = new MyLib._myManageData();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "toolStrip1";
            this._myToolBar.Size = new System.Drawing.Size(839, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _documentPictureScreenTopControl1
            // 
            this._documentPictureScreenTopControl1._isChange = false;
            this._documentPictureScreenTopControl1.BackColor = System.Drawing.Color.Transparent;
            this._documentPictureScreenTopControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this._documentPictureScreenTopControl1.Location = new System.Drawing.Point(0, 25);
            this._documentPictureScreenTopControl1.Name = "_documentPictureScreenTopControl1";
            this._documentPictureScreenTopControl1.Size = new System.Drawing.Size(839, 103);
            this._documentPictureScreenTopControl1.TabIndex = 1;
            // 
            // _getPicture1
            // 
            this._getPicture1._DisplayPictureAmount = 8;
            this._getPicture1._isScanner = false;
            this._getPicture1._isWebcam = false;
            this._getPicture1.AutoSize = true;
            this._getPicture1.BackColor = System.Drawing.Color.Transparent;
            this._getPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._getPicture1.Location = new System.Drawing.Point(0, 128);
            this._getPicture1.Name = "_getPicture1";
            this._getPicture1.Size = new System.Drawing.Size(839, 438);
            this._getPicture1.TabIndex = 2;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //this._myPanel1.Controls.Add(this._myManageData1);
            this._myPanel1.Controls.Add(this._getPicture1);
            this._myPanel1.Controls.Add(this._documentPictureScreenTopControl1);
            //this._myPanel1.Controls.Add(this.toolStrip1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(839, 566);
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.TabIndex = 3;
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 128);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(839, 438);
            this._myManageData1.TabIndex = 3;
            this._myManageData1.TabStop = false;            // 
            // _myManageData1.Panal1 
            //
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _myManageData1.Panal2
            //
            this._myManageData1._form2.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._myToolBar);

            // 
            // _documentPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_documentPicture";
            this.Size = new System.Drawing.Size(839, 566);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        private _documentPictureScreenTopControl _documentPictureScreenTopControl1;
        private _getPicture _getPicture1;
        private MyLib._myPanel _myPanel1;
        public MyLib._myManageData _myManageData1;
    }
}
