namespace SMLInventoryControl
{
    partial class _icTransControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icTransControl));
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._myManageTrans = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._tab = new MyLib._myTabControl();
            this._tab_detail = new System.Windows.Forms.TabPage();
            this._panel1 = new System.Windows.Forms.Panel();
            this._icTransItemGrid = new SMLInventoryControl._icTransItemGridControl();
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this._icTransScreenBottom = new SMLInventoryControl._icTransScreenBottomControl();
            this._toolStripExtra = new System.Windows.Forms.ToolStrip();
            this._purchasePointButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._checkPurchasePermiumButton = new MyLib.ToolStripMyButton();
            this._tab_pay = new System.Windows.Forms.TabPage();
            this._tab_wht = new System.Windows.Forms.TabPage();
            this._tab_vat = new System.Windows.Forms.TabPage();
            this._tab_advance = new System.Windows.Forms.TabPage();
            this._tab_more = new System.Windows.Forms.TabPage();
            this._myPanelMore = new MyLib._myPanel();
            this._icTransScreenMore = new SMLInventoryControl._icTransScreenMoreControl();
            this._icTransRef = new SMLInventoryControl._icTransRefControl();
            this._mySelectBar = new System.Windows.Forms.ToolStrip();
            this._itemApprovalSelectButton = new MyLib.ToolStripMyButton();
            this._selectAllButton = new MyLib.ToolStripMyButton();
            this._itemApprovalButton = new MyLib.ToolStripMyButton();
            this._addButton = new MyLib.ToolStripMyButton();
            this._icTransScreenTop = new SMLInventoryControl._icTransScreenTopControl();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._printButton = new MyLib.ToolStripMyButton();
            this._toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._tab.SuspendLayout();
            this._tab_detail.SuspendLayout();
            this._panel1.SuspendLayout();
            this._toolStripExtra.SuspendLayout();
            this._tab_more.SuspendLayout();
            this._myPanelMore.SuspendLayout();
            this._mySelectBar.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "document_edit.png");
            this._imageList.Images.SetKeyName(1, "document_add.png");
            this._imageList.Images.SetKeyName(2, "money.png");
            this._imageList.Images.SetKeyName(3, "cashier.png");
            this._imageList.Images.SetKeyName(4, "document_notebook.png");
            // 
            // _myManageTrans
            // 
            this._myManageTrans._mainMenuCode = "";
            this._myManageTrans._mainMenuId = "";
            this._myManageTrans.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageTrans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageTrans.Location = new System.Drawing.Point(0, 0);
            this._myManageTrans.Name = "_myManageTrans";
            this._myManageTrans.Size = new System.Drawing.Size(1289, 488);
            this._myManageTrans.TabIndex = 0;
            this._myManageTrans.TabStop = false;

            this._myManageTrans._form2.Controls.Add(this._myPanel1);
            this._myManageTrans._form2.Controls.Add(this._myToolBar);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tab);
            this._myPanel1.Controls.Add(this._icTransRef);
            this._myPanel1.Controls.Add(this._mySelectBar);
            this._myPanel1.Controls.Add(this._icTransScreenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(1204, 442);
            this._myPanel1.TabIndex = 1;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this._tab_detail);
            this._tab.Controls.Add(this._tab_pay);
            this._tab.Controls.Add(this._tab_wht);
            this._tab.Controls.Add(this._tab_vat);
            this._tab.Controls.Add(this._tab_advance);
            this._tab.Controls.Add(this._tab_more);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.ImageList = this._imageList;
            this._tab.Location = new System.Drawing.Point(3, 149);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(1198, 290);
            this._tab.TabIndex = 7;
            this._tab.TableName = "ic_trans";
            // 
            // tab_detail
            // 
            this._tab_detail.Controls.Add(this._panel1);
            this._tab_detail.Controls.Add(this._icTransScreenBottom);
            this._tab_detail.Controls.Add(this._toolStripExtra);
            this._tab_detail.ImageKey = "document_edit.png";
            this._tab_detail.Location = new System.Drawing.Point(4, 23);
            this._tab_detail.Name = "tab_detail";
            this._tab_detail.Size = new System.Drawing.Size(1190, 263);
            this._tab_detail.TabIndex = 0;
            this._tab_detail.Text = "tab_detail";
            this._tab_detail.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this._panel1.Controls.Add(this._icTransItemGrid);
            this._panel1.Controls.Add(this._webBrowser);
            this._panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel1.Location = new System.Drawing.Point(0, 25);
            this._panel1.Margin = new System.Windows.Forms.Padding(0);
            this._panel1.Name = "panel1";
            this._panel1.Size = new System.Drawing.Size(1190, 238);
            this._panel1.TabIndex = 7;
            // 
            // _icTransItemGrid
            // 
            this._icTransItemGrid._custCode = "";
            this._icTransItemGrid._extraWordShow = true;
            this._icTransItemGrid._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._icTransItemGrid._icTransRef = null;
            this._icTransItemGrid._selectRow = -1;
            this._icTransItemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icTransItemGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._icTransItemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icTransItemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icTransItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icTransItemGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icTransItemGrid.Location = new System.Drawing.Point(0, 0);
            this._icTransItemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icTransItemGrid.Name = "_icTransItemGrid";
            this._icTransItemGrid.ShowTotal = true;
            this._icTransItemGrid.Size = new System.Drawing.Size(1190, 218);
            this._icTransItemGrid.TabIndex = 0;
            this._icTransItemGrid.TabStop = false;
            // 
            // _webBrowser
            // 
            this._webBrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._webBrowser.Location = new System.Drawing.Point(0, 218);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.ScrollBarsEnabled = false;
            this._webBrowser.Size = new System.Drawing.Size(1190, 20);
            this._webBrowser.TabIndex = 1;
            // 
            // _icTransScreenBottom
            // 
            this._icTransScreenBottom._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._icTransScreenBottom._isChange = false;
            this._icTransScreenBottom._vatType = _g.g._vatTypeEnum.ภาษีแยกนอก;
            this._icTransScreenBottom.AutoSize = true;
            this._icTransScreenBottom.BackColor = System.Drawing.Color.Honeydew;
            this._icTransScreenBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._icTransScreenBottom.Location = new System.Drawing.Point(0, 263);
            this._icTransScreenBottom.Name = "_icTransScreenBottom";
            this._icTransScreenBottom.Size = new System.Drawing.Size(1190, 0);
            this._icTransScreenBottom.TabIndex = 5;
            // 
            // _toolStripExtra
            // 
            this._toolStripExtra.BackgroundImage = global::SMLInventoryControl.Properties.Resources.bt03;
            this._toolStripExtra.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolStripExtra.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStripExtra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._purchasePointButton,
            this.toolStripSeparator3,
            this._checkPurchasePermiumButton});
            this._toolStripExtra.Location = new System.Drawing.Point(0, 0);
            this._toolStripExtra.Name = "_toolStripExtra";
            this._toolStripExtra.Size = new System.Drawing.Size(1190, 25);
            this._toolStripExtra.TabIndex = 6;
            this._toolStripExtra.Text = "_toolBar";
            // 
            // _purchasePointButton
            // 
            this._purchasePointButton.Image = global::SMLInventoryControl.Properties.Resources.cube_yellow_new;
            this._purchasePointButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._purchasePointButton.Name = "_purchasePointButton";
            this._purchasePointButton.Padding = new System.Windows.Forms.Padding(1);
            this._purchasePointButton.ResourceName = "ค้นหาสินค้าถึงจุดสั่งซื้อ";
            this._purchasePointButton.Size = new System.Drawing.Size(139, 22);
            this._purchasePointButton.Text = "ค้นหาสินค้าถึงจุดสั่งซื้อ";
            this._purchasePointButton.Click += new System.EventHandler(this._purchasePointButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _checkPurchasePermiumButton
            // 
            this._checkPurchasePermiumButton.Image = global::SMLInventoryControl.Properties.Resources.cube_green_add;
            this._checkPurchasePermiumButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._checkPurchasePermiumButton.Name = "_checkPurchasePermiumButton";
            this._checkPurchasePermiumButton.Padding = new System.Windows.Forms.Padding(1);
            this._checkPurchasePermiumButton.ResourceName = "ประมวลผลของแถม";
            this._checkPurchasePermiumButton.Size = new System.Drawing.Size(122, 22);
            this._checkPurchasePermiumButton.Text = "ประมวลผลของแถม";
            this._checkPurchasePermiumButton.Click += new System.EventHandler(this._checkPurchasePermiumButton_Click);
            // 
            // tab_pay
            // 
            this._tab_pay.ImageKey = "cashier.png";
            this._tab_pay.Location = new System.Drawing.Point(4, 23);
            this._tab_pay.Name = "tab_pay";
            this._tab_pay.Size = new System.Drawing.Size(66, 0);
            this._tab_pay.TabIndex = 3;
            this._tab_pay.Text = "tab_pay";
            this._tab_pay.UseVisualStyleBackColor = true;
            // 
            // tab_wht
            // 
            this._tab_wht.ImageKey = "money.png";
            this._tab_wht.Location = new System.Drawing.Point(4, 23);
            this._tab_wht.Name = "tab_wht";
            this._tab_wht.Size = new System.Drawing.Size(66, 0);
            this._tab_wht.TabIndex = 2;
            this._tab_wht.Text = "tab_wht";
            this._tab_wht.UseVisualStyleBackColor = true;
            // 
            // tab_vat
            // 
            this._tab_vat.ImageKey = "document_notebook.png";
            this._tab_vat.Location = new System.Drawing.Point(4, 23);
            this._tab_vat.Name = "tab_vat";
            this._tab_vat.Size = new System.Drawing.Size(66, 0);
            this._tab_vat.TabIndex = 4;
            this._tab_vat.Text = "tab_vat";
            this._tab_vat.UseVisualStyleBackColor = true;
            // 
            // tab_advance
            // 
            this._tab_advance.Location = new System.Drawing.Point(4, 23);
            this._tab_advance.Name = "tab_advance";
            this._tab_advance.Size = new System.Drawing.Size(66, 0);
            this._tab_advance.TabIndex = 5;
            this._tab_advance.Text = "tab_advance";
            this._tab_advance.UseVisualStyleBackColor = true;
            // 
            // tab_more
            // 
            this._tab_more.Controls.Add(this._myPanelMore);
            this._tab_more.ImageKey = "document_add.png";
            this._tab_more.Location = new System.Drawing.Point(4, 23);
            this._tab_more.Name = "tab_more";
            this._tab_more.Size = new System.Drawing.Size(66, 0);
            this._tab_more.TabIndex = 1;
            this._tab_more.Text = "tab_more";
            this._tab_more.UseVisualStyleBackColor = true;
            // 
            // _myPanelMore
            // 
            this._myPanelMore._switchTabAuto = false;
            this._myPanelMore.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanelMore.Controls.Add(this._icTransScreenMore);
            this._myPanelMore.CornerPicture = null;
            this._myPanelMore.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanelMore.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanelMore.Location = new System.Drawing.Point(0, 0);
            this._myPanelMore.Name = "_myPanel2";
            this._myPanelMore.Padding = new System.Windows.Forms.Padding(4);
            this._myPanelMore.Size = new System.Drawing.Size(880, 282);
            this._myPanelMore.TabIndex = 1;
            // 
            // _icTransScreenMore
            // 
            this._icTransScreenMore._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._icTransScreenMore._isChange = false;
            this._icTransScreenMore.BackColor = System.Drawing.Color.Transparent;
            this._icTransScreenMore.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icTransScreenMore.Location = new System.Drawing.Point(4, 4);
            this._icTransScreenMore.Name = "_icTransScreenMore";
            this._icTransScreenMore.Size = new System.Drawing.Size(872, 274);
            this._icTransScreenMore.TabIndex = 0;
            // 
            // _icTransRef
            // 
            this._icTransRef._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._icTransRef._refCheckStatus = false;
            this._icTransRef.Dock = System.Windows.Forms.DockStyle.Top;
            this._icTransRef.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icTransRef.Location = new System.Drawing.Point(3, 28);
            this._icTransRef.Name = "_icTransRef";
            this._icTransRef.Size = new System.Drawing.Size(1198, 121);
            this._icTransRef.TabIndex = 1;
            // 
            // _mySelectBar
            // 
            this._mySelectBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._mySelectBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._itemApprovalSelectButton,
            this._selectAllButton,
            this._itemApprovalButton,
            this._addButton});
            this._mySelectBar.Location = new System.Drawing.Point(3, 3);
            this._mySelectBar.Name = "_mySelectBar";
            this._mySelectBar.Size = new System.Drawing.Size(1198, 25);
            this._mySelectBar.TabIndex = 0;
            this._mySelectBar.Text = "toolStrip1";
            // 
            // _itemApprovalSelectButton
            // 
            this._itemApprovalSelectButton.Image = global::SMLInventoryControl.Properties.Resources.Tasks24;
            this._itemApprovalSelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._itemApprovalSelectButton.Name = "_itemApprovalSelectButton";
            this._itemApprovalSelectButton.Padding = new System.Windows.Forms.Padding(1);
            this._itemApprovalSelectButton.ResourceName = "รายการที่ขออนุมัติ";
            this._itemApprovalSelectButton.Size = new System.Drawing.Size(112, 22);
            this._itemApprovalSelectButton.Text = "รายการที่ขออนุมัติ";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::SMLInventoryControl.Properties.Resources.preferences;
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectAllButton.ResourceName = "เลือกทั้งหมด";
            this._selectAllButton.Size = new System.Drawing.Size(88, 22);
            this._selectAllButton.Text = "เลือกทั้งหมด";
            // 
            // _itemApprovalButton
            // 
            this._itemApprovalButton.Image = global::SMLInventoryControl.Properties.Resources.contract;
            this._itemApprovalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._itemApprovalButton.Name = "_itemApprovalButton";
            this._itemApprovalButton.Padding = new System.Windows.Forms.Padding(1);
            this._itemApprovalButton.ResourceName = "อนุมัติรายการ";
            this._itemApprovalButton.Size = new System.Drawing.Size(90, 22);
            this._itemApprovalButton.Text = "อนุมัติรายการ";
            // 
            // _addButton
            // 
            this._addButton.Image = global::SMLInventoryControl.Properties.Resources.document_preferences;
            this._addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addButton.Name = "_addButton";
            this._addButton.Padding = new System.Windows.Forms.Padding(1);
            this._addButton.ResourceName = "เลือกรายการ";
            this._addButton.Size = new System.Drawing.Size(87, 22);
            this._addButton.Text = "เลือกรายการ";
            // 
            // _icTransScreenTop
            // 
            this._icTransScreenTop._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._icTransScreenTop._isChange = false;
            this._icTransScreenTop.AutoSize = true;
            this._icTransScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icTransScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icTransScreenTop.Location = new System.Drawing.Point(3, 3);
            this._icTransScreenTop.Name = "_icTransScreenTop";
            this._icTransScreenTop.Size = new System.Drawing.Size(1198, 0);
            this._icTransScreenTop.TabIndex = 4;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLInventoryControl.Properties.Resources.bt03;
            this._myToolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._toolStripSeparator1,
            this._printButton,
            this._toolStripSeparator2,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(1204, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "_toolBar";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(122, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLInventoryControl.Properties.Resources.printer;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "พิมพ์เอกสาร";
            this._printButton.Size = new System.Drawing.Size(86, 22);
            this._printButton.Text = "พิมพ์เอกสาร";
            this._printButton.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this._toolStripSeparator2.Name = "toolStripSeparator2";
            this._toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLInventoryControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Size = new System.Drawing.Size(79, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _icTransControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageTrans);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icTransControl";
            this.Size = new System.Drawing.Size(1289, 488);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._tab.ResumeLayout(false);
            this._tab_detail.ResumeLayout(false);
            this._tab_detail.PerformLayout();
            this._panel1.ResumeLayout(false);
            this._toolStripExtra.ResumeLayout(false);
            this._toolStripExtra.PerformLayout();
            this._tab_more.ResumeLayout(false);
            this._myPanelMore.ResumeLayout(false);
            this._mySelectBar.ResumeLayout(false);
            this._mySelectBar.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib.ToolStripMyButton _saveButton;
        public MyLib._myManageData _myManageTrans;
        private MyLib.ToolStripMyButton _itemApprovalSelectButton;
        private MyLib.ToolStripMyButton _selectAllButton;
        private MyLib.ToolStripMyButton _itemApprovalButton;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private MyLib.ToolStripMyButton _addButton;
        private _icTransItemGridControl _icTransItemGrid;
        private _icTransScreenTopControl _icTransScreenTop;
        private _icTransScreenBottomControl _icTransScreenBottom;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib.ToolStripMyButton _printButton;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator2;
        public System.Windows.Forms.ToolStrip _myToolBar;
        public System.Windows.Forms.ToolStrip _mySelectBar;
        private MyLib._myTabControl _tab;
        private System.Windows.Forms.TabPage _tab_detail;
        public System.Windows.Forms.ToolStrip _toolStripExtra;
        private MyLib.ToolStripMyButton _purchasePointButton;
        private MyLib.ToolStripMyButton _checkPurchasePermiumButton;
        private System.Windows.Forms.TabPage _tab_more;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ImageList _imageList;
        private _icTransScreenMoreControl _icTransScreenMore;
        private MyLib._myPanel _myPanelMore;
        private System.Windows.Forms.TabPage _tab_wht;
        private System.Windows.Forms.TabPage _tab_pay;
        private System.Windows.Forms.TabPage _tab_vat;
        public _icTransRefControl _icTransRef;
        private System.Windows.Forms.TabPage _tab_advance;
        private System.Windows.Forms.Panel _panel1;
        private System.Windows.Forms.WebBrowser _webBrowser;
    }
}
