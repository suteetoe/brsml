namespace SMLERPIC
{
    partial class _icDescription
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
            this._myPanel1 = new MyLib._myPanel();
            this._icmainScreenTop = new SMLERPControl._icmainScreenTopControl();
            this._htmlwysiwyg = new HTMLwysiwygLib._htmlEditor();
            this._myManageDetail = new MyLib._myManageData();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(654, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPIC.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._htmlwysiwyg);
            this._myPanel1.Controls.Add(this._icmainScreenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(654, 660);
            this._myPanel1.TabIndex = 1;
            // 
            // _icmainScreenTop
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenTop.Name = "_icmainScreenTop";
            this._icmainScreenTop.Size = new System.Drawing.Size(654, 186);
            this._icmainScreenTop.TabIndex = 0;
            // 
            // _htmlwysiwyg
            // 
            this._htmlwysiwyg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._htmlwysiwyg.Dock = System.Windows.Forms.DockStyle.Fill;
            this._htmlwysiwyg.Location = new System.Drawing.Point(0, 186);
            this._htmlwysiwyg.Name = "_htmlwysiwyg";
            //this._htmlwysiwyg.ShowAlignCenterButton = true;
            //this._htmlwysiwyg.ShowAlignLeftButton = true;
            //this._htmlwysiwyg.ShowAlignRightButton = true;
            //this._htmlwysiwyg.ShowBackColorButton = true;
            //this._htmlwysiwyg.ShowBolButton = true;
            //this._htmlwysiwyg.ShowBulletButton = true;
            //this._htmlwysiwyg.ShowCopyButton = true;
            //this._htmlwysiwyg.ShowCutButton = true;
            //this._htmlwysiwyg.ShowFontFamilyButton = false;
            //this._htmlwysiwyg.ShowFontSizeButton = true;
            //this._htmlwysiwyg.ShowIndentButton = true;
            //this._htmlwysiwyg.ShowItalicButton = true;
            //this._htmlwysiwyg.ShowJustifyButton = true;
            //this._htmlwysiwyg.ShowLinkButton = true;
            //this._htmlwysiwyg.ShowNewButton = true;
            //this._htmlwysiwyg.ShowOrderedListButton = true;
            //this._htmlwysiwyg.ShowOutdentButton = true;
            //this._htmlwysiwyg.ShowPasteButton = true;
            //this._htmlwysiwyg.ShowPrintButton = true;
            //this._htmlwysiwyg.ShowTxtBGButton = true;
            //this._htmlwysiwyg.ShowTxtColorButton = true;
            //this._htmlwysiwyg.ShowUnderlineButton = true;
            //this._htmlwysiwyg.ShowUnlinkButton = true;
            this._htmlwysiwyg.Size = new System.Drawing.Size(654, 474);
            this._htmlwysiwyg.TabIndex = 1;
            // 
            // _myManageDetail
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 25);
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

            this._myManageDetail.Size = new System.Drawing.Size(654, 660);
            this._myManageDetail.TabIndex = 2;
            this._myManageDetail.TabStop = false;
            // 
            // _icDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            //this.Controls.Add(this._myPanel1);
            //this.Controls.Add(this._myToolBar);
            this.Name = "_icDescription";
            this.Size = new System.Drawing.Size(654, 685);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel1;
        private SMLERPControl._icmainScreenTopControl _icmainScreenTop;
        private HTMLwysiwygLib._htmlEditor _htmlwysiwyg;
        private MyLib._myManageData _myManageDetail;
    }
}
