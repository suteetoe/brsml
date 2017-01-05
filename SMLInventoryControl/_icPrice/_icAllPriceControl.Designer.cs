namespace SMLInventoryControl._icPrice
{
    partial class _icAllPriceControl
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_normal_price = new System.Windows.Forms.TabPage();
            this._myTabControl2 = new MyLib._myTabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._gridNormalPrice0 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this._gridNormalPrice1 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this._gridNormalPrice2 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tab_standard_price = new System.Windows.Forms.TabPage();
            this._myTabControl3 = new MyLib._myTabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this._gridStandardPrice0 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this._gridStandardPrice1 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this._gridStandardPrice2 = new SMLInventoryControl._icPrice._icPriceDetail();
            this.tab_formula_price = new System.Windows.Forms.TabPage();
            this._gridPriceFormula = new SMLInventoryControl._icPriceFormulaGrid();
            this.tab_bracode_price = new System.Windows.Forms.TabPage();
            this._gridBarcode = new SMLInventoryControl._icmainGridBarCodeControl();
            this._myPanel1 = new MyLib._myPanel();
            this._icmainScreenTopControl1 = new SMLERPControl._icmainScreenTopControl();
            this._myToolBar.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_normal_price.SuspendLayout();
            this._myTabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tab_standard_price.SuspendLayout();
            this._myTabControl3.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tab_formula_price.SuspendLayout();
            this.tab_bracode_price.SuspendLayout();
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
            this._myToolBar.Size = new System.Drawing.Size(562, 25);
            this._myToolBar.TabIndex = 0;
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
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_standard_price);
            this._myTabControl1.Controls.Add(this.tab_normal_price);
            this._myTabControl1.Controls.Add(this.tab_formula_price);
            this._myTabControl1.Controls.Add(this.tab_bracode_price);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(3, 171);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(556, 499);
            this._myTabControl1.TabIndex = 2;
            this._myTabControl1.TableName = "ic_resource";
            // 
            // tab_normal_price
            // 
            this.tab_normal_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_normal_price.Controls.Add(this._myTabControl2);
            this.tab_normal_price.Location = new System.Drawing.Point(4, 23);
            this.tab_normal_price.Name = "tab_normal_price";
            this.tab_normal_price.Size = new System.Drawing.Size(548, 472);
            this.tab_normal_price.TabIndex = 0;
            this.tab_normal_price.Text = "ราคาขายทั่วไป";
            this.tab_normal_price.UseVisualStyleBackColor = true;
            // 
            // _myTabControl2
            // 
            this._myTabControl2.Controls.Add(this.tabPage5);
            this._myTabControl2.Controls.Add(this.tabPage6);
            this._myTabControl2.Controls.Add(this.tabPage7);
            this._myTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl2.FixedName = true;
            this._myTabControl2.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl2.Location = new System.Drawing.Point(0, 0);
            this._myTabControl2.Multiline = true;
            this._myTabControl2.Name = "_myTabControl2";
            this._myTabControl2.SelectedIndex = 0;
            this._myTabControl2.Size = new System.Drawing.Size(546, 470);
            this._myTabControl2.TabIndex = 0;
            this._myTabControl2.TableName = "";
            // 
            // tabPage5
            // 
            this.tabPage5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage5.Controls.Add(this._gridNormalPrice0);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(538, 443);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "ราคาขายทั่วไป";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // _gridNormalPrice0
            // 
            this._gridNormalPrice0._priceListType = _g.g._priceListType.ขาย_ราคาขายทั่วไป;
            this._gridNormalPrice0._priceType = _g.g._priceGridType.ราคาปรกติ;
            this._gridNormalPrice0.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridNormalPrice0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridNormalPrice0.Location = new System.Drawing.Point(0, 0);
            this._gridNormalPrice0.Name = "_gridNormalPrice0";
            this._gridNormalPrice0.Size = new System.Drawing.Size(536, 441);
            this._gridNormalPrice0.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage6.Controls.Add(this._gridNormalPrice1);
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(538, 443);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "ตามกลุ่มลูกค้า";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // _gridNormalPrice1
            // 
            this._gridNormalPrice1._priceListType = _g.g._priceListType.ขาย_ราคาขายตามกลุ่มลูกค้า;
            this._gridNormalPrice1._priceType = _g.g._priceGridType.ราคาตามกลุ่มลูกค้า;
            this._gridNormalPrice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridNormalPrice1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridNormalPrice1.Location = new System.Drawing.Point(0, 0);
            this._gridNormalPrice1.Name = "_gridNormalPrice1";
            this._gridNormalPrice1.Size = new System.Drawing.Size(536, 441);
            this._gridNormalPrice1.TabIndex = 1;
            // 
            // tabPage7
            // 
            this.tabPage7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage7.Controls.Add(this._gridNormalPrice2);
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(538, 443);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "ตามลูกค้า";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // _gridNormalPrice2
            // 
            this._gridNormalPrice2._priceListType = _g.g._priceListType.ขาย_ราคาขายตามลูกค้า;
            this._gridNormalPrice2._priceType = _g.g._priceGridType.ราคาตามลูกค้า;
            this._gridNormalPrice2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridNormalPrice2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridNormalPrice2.Location = new System.Drawing.Point(0, 0);
            this._gridNormalPrice2.Name = "_gridNormalPrice2";
            this._gridNormalPrice2.Size = new System.Drawing.Size(536, 441);
            this._gridNormalPrice2.TabIndex = 1;
            // 
            // tab_standard_price
            // 
            this.tab_standard_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_standard_price.Controls.Add(this._myTabControl3);
            this.tab_standard_price.Location = new System.Drawing.Point(4, 23);
            this.tab_standard_price.Name = "tab_standard_price";
            this.tab_standard_price.Size = new System.Drawing.Size(548, 472);
            this.tab_standard_price.TabIndex = 1;
            this.tab_standard_price.Text = "ราคาขายมาตรฐาน";
            this.tab_standard_price.UseVisualStyleBackColor = true;
            // 
            // _myTabControl3
            // 
            this._myTabControl3.Controls.Add(this.tabPage8);
            this._myTabControl3.Controls.Add(this.tabPage9);
            this._myTabControl3.Controls.Add(this.tabPage10);
            this._myTabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl3.FixedName = true;
            this._myTabControl3.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl3.Location = new System.Drawing.Point(0, 0);
            this._myTabControl3.Multiline = true;
            this._myTabControl3.Name = "_myTabControl3";
            this._myTabControl3.SelectedIndex = 0;
            this._myTabControl3.Size = new System.Drawing.Size(546, 470);
            this._myTabControl3.TabIndex = 1;
            this._myTabControl3.TableName = "";
            // 
            // tabPage8
            // 
            this.tabPage8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage8.Controls.Add(this._gridStandardPrice0);
            this.tabPage8.Location = new System.Drawing.Point(4, 23);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(538, 443);
            this.tabPage8.TabIndex = 0;
            this.tabPage8.Text = "ราคาขายทั่วไป";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // _gridStandardPrice0
            // 
            this._gridStandardPrice0._priceListType = _g.g._priceListType.ขาย_ราคาขายทั่วไป;
            this._gridStandardPrice0._priceType = _g.g._priceGridType.ราคาปรกติ;
            this._gridStandardPrice0.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridStandardPrice0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridStandardPrice0.Location = new System.Drawing.Point(0, 0);
            this._gridStandardPrice0.Name = "_gridStandardPrice0";
            this._gridStandardPrice0.Size = new System.Drawing.Size(536, 441);
            this._gridStandardPrice0.TabIndex = 0;
            // 
            // tabPage9
            // 
            this.tabPage9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage9.Controls.Add(this._gridStandardPrice1);
            this.tabPage9.Location = new System.Drawing.Point(4, 23);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(538, 443);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "ตามกลุ่มลูกค้า";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // _gridStandardPrice1
            // 
            this._gridStandardPrice1._priceListType = _g.g._priceListType.ขาย_ราคาขายตามกลุ่มลูกค้า;
            this._gridStandardPrice1._priceType = _g.g._priceGridType.ราคาตามกลุ่มลูกค้า;
            this._gridStandardPrice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridStandardPrice1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridStandardPrice1.Location = new System.Drawing.Point(0, 0);
            this._gridStandardPrice1.Name = "_gridStandardPrice1";
            this._gridStandardPrice1.Size = new System.Drawing.Size(536, 441);
            this._gridStandardPrice1.TabIndex = 1;
            // 
            // tabPage10
            // 
            this.tabPage10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage10.Controls.Add(this._gridStandardPrice2);
            this.tabPage10.Location = new System.Drawing.Point(4, 23);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(538, 443);
            this.tabPage10.TabIndex = 2;
            this.tabPage10.Text = "ตามลูกค้า";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // _gridStandardPrice2
            // 
            this._gridStandardPrice2._priceListType = _g.g._priceListType.ขาย_ราคาขายตามลูกค้า;
            this._gridStandardPrice2._priceType = _g.g._priceGridType.ราคาตามลูกค้า;
            this._gridStandardPrice2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridStandardPrice2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridStandardPrice2.Location = new System.Drawing.Point(0, 0);
            this._gridStandardPrice2.Name = "_gridStandardPrice2";
            this._gridStandardPrice2.Size = new System.Drawing.Size(536, 441);
            this._gridStandardPrice2.TabIndex = 1;
            // 
            // tab_formula_price
            // 
            this.tab_formula_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_formula_price.Controls.Add(this._gridPriceFormula);
            this.tab_formula_price.Location = new System.Drawing.Point(4, 23);
            this.tab_formula_price.Name = "tab_formula_price";
            this.tab_formula_price.Size = new System.Drawing.Size(548, 472);
            this.tab_formula_price.TabIndex = 2;
            this.tab_formula_price.Text = "สูตรราคาขาย";
            this.tab_formula_price.UseVisualStyleBackColor = true;
            // 
            // _gridPriceFormula
            // 
            this._gridPriceFormula._extraWordShow = true;
            this._gridPriceFormula._selectRow = -1;
            this._gridPriceFormula.BackColor = System.Drawing.SystemColors.Window;
            this._gridPriceFormula.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridPriceFormula.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridPriceFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPriceFormula.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridPriceFormula.Location = new System.Drawing.Point(0, 0);
            this._gridPriceFormula.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridPriceFormula.Name = "_gridPriceFormula";
            this._gridPriceFormula.Size = new System.Drawing.Size(546, 470);
            this._gridPriceFormula.TabIndex = 0;
            this._gridPriceFormula.TabStop = false;
            // 
            // tab_bracode_price
            // 
            this.tab_bracode_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_bracode_price.Controls.Add(this._gridBarcode);
            this.tab_bracode_price.Location = new System.Drawing.Point(4, 23);
            this.tab_bracode_price.Name = "tab_bracode_price";
            this.tab_bracode_price.Size = new System.Drawing.Size(548, 472);
            this.tab_bracode_price.TabIndex = 3;
            this.tab_bracode_price.Text = "บาร์โค๊ด";
            this.tab_bracode_price.UseVisualStyleBackColor = true;
            // 
            // _gridBarcode
            // 
            this._gridBarcode._extraWordShow = true;
            this._gridBarcode._selectRow = -1;
            this._gridBarcode.BackColor = System.Drawing.SystemColors.Window;
            this._gridBarcode.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridBarcode.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridBarcode.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridBarcode.Location = new System.Drawing.Point(0, 0);
            this._gridBarcode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridBarcode.Name = "_gridBarcode";
            this._gridBarcode.Size = new System.Drawing.Size(546, 470);
            this._gridBarcode.TabIndex = 0;
            this._gridBarcode.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myTabControl1);
            this._myPanel1.Controls.Add(this._icmainScreenTopControl1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(562, 673);
            this._myPanel1.TabIndex = 0;
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
            this._icmainScreenTopControl1.Size = new System.Drawing.Size(556, 168);
            this._icmainScreenTopControl1.TabIndex = 0;
            // 
            // _icAllPriceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolBar);
            this.Name = "_icAllPriceControl";
            this.Size = new System.Drawing.Size(562, 698);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myTabControl1.ResumeLayout(false);
            this.tab_normal_price.ResumeLayout(false);
            this._myTabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tab_standard_price.ResumeLayout(false);
            this._myTabControl3.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tab_formula_price.ResumeLayout(false);
            this.tab_bracode_price.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_normal_price;
        private System.Windows.Forms.TabPage tab_standard_price;
        private System.Windows.Forms.TabPage tab_formula_price;
        private System.Windows.Forms.TabPage tab_bracode_price;
        public MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib._myPanel _myPanel1;
        private MyLib._myTabControl _myTabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private MyLib._myTabControl _myTabControl3;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        public _icPriceDetail _gridNormalPrice0;
        public _icPriceDetail _gridNormalPrice1;
        public _icPriceDetail _gridNormalPrice2;
        public _icPriceDetail _gridStandardPrice0;
        public _icPriceDetail _gridStandardPrice1;
        public _icPriceDetail _gridStandardPrice2;
        public _icPriceFormulaGrid _gridPriceFormula;
        public _icmainGridBarCodeControl _gridBarcode;
        public SMLERPControl._icmainScreenTopControl _icmainScreenTopControl1;
        public System.Windows.Forms.ToolStrip _myToolBar;
    }
}
