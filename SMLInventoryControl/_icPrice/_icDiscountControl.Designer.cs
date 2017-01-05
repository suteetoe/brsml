namespace SMLInventoryControl._icPrice
{
    partial class _icDiscountControl
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
            this._myTabControlDetail = new MyLib._myTabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._gridNormalPrice0 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this._gridNormalPrice1 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this._gridNormalPrice2 = new SMLInventoryControl._icPrice._icPriceDetail();
            this._icmainScreenTopControl1 = new SMLERPControl._icmainScreenTopControl();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myTabControlDetail.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(712, 25);
            this._myToolBar.TabIndex = 2;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทึก";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLInventoryControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myTabControlDetail);
            this._myPanel1.Controls.Add(this._icmainScreenTopControl1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(712, 602);
            this._myPanel1.TabIndex = 1;
            // 
            // _myTabControlDetail
            // 
            this._myTabControlDetail.Controls.Add(this.tabPage5);
            this._myTabControlDetail.Controls.Add(this.tabPage6);
            this._myTabControlDetail.Controls.Add(this.tabPage7);
            this._myTabControlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControlDetail.FixedName = true;
            this._myTabControlDetail.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControlDetail.Location = new System.Drawing.Point(3, 185);
            this._myTabControlDetail.Multiline = true;
            this._myTabControlDetail.Name = "_myTabControlDetail";
            this._myTabControlDetail.SelectedIndex = 0;
            this._myTabControlDetail.Size = new System.Drawing.Size(706, 414);
            this._myTabControlDetail.TabIndex = 0;
            this._myTabControlDetail.TableName = "";
            // 
            // tabPage5
            // 
            this.tabPage5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage5.Controls.Add(this._gridNormalPrice0);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(698, 387);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "ส่วนลดทั่วไป";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // _gridNormalPrice0
            // 
            this._gridNormalPrice0._priceListType = _g.g._priceListType.ขาย_ราคาขายทั่วไป;
            this._gridNormalPrice0._priceType = _g.g._priceGridType.ลดทั่วไป;
            this._gridNormalPrice0.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridNormalPrice0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridNormalPrice0.Location = new System.Drawing.Point(0, 0);
            this._gridNormalPrice0.Name = "_gridNormalPrice0";
            this._gridNormalPrice0.Size = new System.Drawing.Size(696, 385);
            this._gridNormalPrice0.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage6.Controls.Add(this._gridNormalPrice1);
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(698, 401);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "ตามกลุ่มลูกค้า";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // _gridNormalPrice1
            // 
            this._gridNormalPrice1._priceListType = _g.g._priceListType.ขาย_ราคาขายตามกลุ่มลูกค้า;
            this._gridNormalPrice1._priceType = _g.g._priceGridType.ลดตามกลุ่มลูกค้า;
            this._gridNormalPrice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridNormalPrice1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridNormalPrice1.Location = new System.Drawing.Point(0, 0);
            this._gridNormalPrice1.Name = "_gridNormalPrice1";
            this._gridNormalPrice1.Size = new System.Drawing.Size(696, 399);
            this._gridNormalPrice1.TabIndex = 1;
            // 
            // tabPage7
            // 
            this.tabPage7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage7.Controls.Add(this._gridNormalPrice2);
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(698, 401);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "ตามลูกค้า";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // _gridNormalPrice2
            // 
            this._gridNormalPrice2._priceListType = _g.g._priceListType.ขาย_ราคาขายตามลูกค้า;
            this._gridNormalPrice2._priceType = _g.g._priceGridType.ลดตามลูกค้า;
            this._gridNormalPrice2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridNormalPrice2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridNormalPrice2.Location = new System.Drawing.Point(0, 0);
            this._gridNormalPrice2.Name = "_gridNormalPrice2";
            this._gridNormalPrice2.Size = new System.Drawing.Size(696, 399);
            this._gridNormalPrice2.TabIndex = 1;
            // 
            // _icmainScreenTopControl1
            // 
            this._icmainScreenTopControl1._isChange = false;
            this._icmainScreenTopControl1.AutoSize = true;
            this._icmainScreenTopControl1.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTopControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTopControl1.Enabled = false;
            this._icmainScreenTopControl1.Location = new System.Drawing.Point(3, 3);
            this._icmainScreenTopControl1.Name = "_icmainScreenTopControl1";
            this._icmainScreenTopControl1.Size = new System.Drawing.Size(706, 182);
            this._icmainScreenTopControl1.TabIndex = 0;
            // 
            // _icDiscountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icDiscountControl";
            this.Size = new System.Drawing.Size(712, 627);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myTabControlDetail.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myPanel _myPanel1;
        private MyLib._myTabControl _myTabControlDetail;
        private System.Windows.Forms.TabPage tabPage5;
        public _icPriceDetail _gridNormalPrice0;
        private System.Windows.Forms.TabPage tabPage6;
        public _icPriceDetail _gridNormalPrice1;
        private System.Windows.Forms.TabPage tabPage7;
        public _icPriceDetail _gridNormalPrice2;
        public SMLERPControl._icmainScreenTopControl _icmainScreenTopControl1;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib.ToolStripMyButton _saveButton;
        public System.Windows.Forms.ToolStrip _myToolBar;
    }
}
