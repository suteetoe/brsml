namespace SMLERPIC
{
    partial class _icBarcodePicture
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
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._grouper1 = new MyLib._grouper();
            this._icmainScreenTop = new SMLERPControl._icmainScreenTopControl();
            this._getPicture1 = new SMLERPControl._getPicture();
            this._myManageDetail = new MyLib._myManageData();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(798, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Size = new System.Drawing.Size(113, 22);
            this._buttonSave.Text = "บันทึกข้อมูล (F12)";
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.Controls.Add(this._icmainScreenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(798, 764);
            this._myPanel1.TabIndex = 1;
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.Horizontal;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._getPicture1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = ":: Picture ::";
            this._grouper1.Location = new System.Drawing.Point(0, 186);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(20);
            this._grouper1.PaintGroupBox = true;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(798, 578);
            this._grouper1.TabIndex = 1;
            // 
            // _icmainScreenTopControl1
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.DisplayScreen = false;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenTop.Name = "_icmainScreenTopControl1";
            this._icmainScreenTop.Size = new System.Drawing.Size(798, 186);
            this._icmainScreenTop.TabIndex = 0;
            // 
            // _getPicture1
            // 
            this._getPicture1._DisplayPictureAmount = 6;
            this._getPicture1._isScanner = false;
            this._getPicture1._isWebcam = false;
            this._getPicture1.AutoSize = true;
            this._getPicture1.BackColor = System.Drawing.Color.Transparent;
            this._getPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._getPicture1.Location = new System.Drawing.Point(20, 20);
            this._getPicture1.Name = "_getPicture1";
            this._getPicture1.Size = new System.Drawing.Size(758, 538);
            this._getPicture1.TabIndex = 0;
            // 
            // _myManageData1
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
            this._myManageDetail.Size = new System.Drawing.Size(570, 580);
            this._myManageDetail.TabIndex = 1;
            this._myManageDetail.TabStop = false;
            // 
            // _icBarcodePicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            //this.Controls.Add(this._myPanel1);
            //this.Controls.Add(this._myToolBar);
            this.Name = "_icBarcodePicture";
            this.Size = new System.Drawing.Size(798, 789);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib._myPanel _myPanel1;
        private SMLERPControl._icmainScreenTopControl _icmainScreenTop;
        private MyLib._grouper _grouper1;
        private SMLERPControl._getPicture _getPicture1;
        private MyLib._myManageData _myManageDetail;
    }
}
