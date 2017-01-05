namespace SMLBarcodeManage
{
    partial class _itemBarcodePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_itemBarcodePrint));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._itemList = new MyLib._myDataList();
            this.panel2 = new System.Windows.Forms.Panel();
            this._selectedGid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._previewButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._clearButton = new MyLib.ToolStripMyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._customHeightTextbox = new System.Windows.Forms.TextBox();
            this._customHeightLabel = new MyLib._myLabel();
            this._customWidthTextbox = new System.Windows.Forms.TextBox();
            this._customWidthLabel = new MyLib._myLabel();
            this._customPaperSizeCheckbox = new MyLib._myCheckBox();
            this._fixSizeCheckbox = new MyLib._myCheckBox();
            this._fixMarginCheckbox = new MyLib._myCheckBox();
            this._unitNameCheckbox = new MyLib._myCheckBox();
            this._borderCheckBox = new MyLib._myCheckBox();
            this._logoHeightTextBox = new System.Windows.Forms.TextBox();
            this._logoWidthTextBox = new System.Windows.Forms.TextBox();
            this._myLabel15 = new MyLib._myLabel();
            this._myLabel16 = new MyLib._myLabel();
            this._logoSelectButton = new System.Windows.Forms.Button();
            this._logoTextBox = new System.Windows.Forms.TextBox();
            this._myLabel14 = new MyLib._myLabel();
            this._shelfCheckBox = new MyLib._myCheckBox();
            this._formCodeTextBox = new System.Windows.Forms.TextBox();
            this._printFormCheckBox = new MyLib._myCheckBox();
            this._myLabel13 = new MyLib._myLabel();
            this._codeTextBox = new System.Windows.Forms.TextBox();
            this._priceComboBox = new System.Windows.Forms.ComboBox();
            this._showItemCodeCheckBox = new MyLib._myCheckBox();
            this._showPriceCheckBox = new MyLib._myCheckBox();
            this._ltdNameTextBox = new System.Windows.Forms.TextBox();
            this._myLabel12 = new MyLib._myLabel();
            this._printerComboBox = new System.Windows.Forms.ComboBox();
            this._myLabel11 = new MyLib._myLabel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this._myLabel10 = new MyLib._myLabel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this._formatLoadButton = new MyLib.ToolStripMyButton();
            this._formatSaveButton = new MyLib.ToolStripMyButton();
            this._formatDeleteButton = new MyLib.ToolStripMyButton();
            this._myLabel9 = new MyLib._myLabel();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._startColumnTextBox = new System.Windows.Forms.TextBox();
            this._startRowTextBox = new System.Windows.Forms.TextBox();
            this._maxColumnTextBox = new System.Windows.Forms.TextBox();
            this._maxRowTextBox = new System.Windows.Forms.TextBox();
            this._labelHeightTextBox = new System.Windows.Forms.TextBox();
            this._labelWidthTextBox = new System.Windows.Forms.TextBox();
            this._leftMarginTextBox = new System.Windows.Forms.TextBox();
            this._topMarginTextBox = new System.Windows.Forms.TextBox();
            this._myLabel4 = new MyLib._myLabel();
            this._myLabel7 = new MyLib._myLabel();
            this._myLabel5 = new MyLib._myLabel();
            this._myLabel6 = new MyLib._myLabel();
            this._myLabel8 = new MyLib._myLabel();
            this._myLabel3 = new MyLib._myLabel();
            this._myLabel1 = new MyLib._myLabel();
            this._myLabel2 = new MyLib._myLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._itemList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1269, 650);
            this.splitContainer1.SplitterDistance = 623;
            this.splitContainer1.TabIndex = 0;
            // 
            // _itemList
            // 
            this._itemList._extraWhere = "";
            this._itemList._fullMode = false;
            this._itemList._multiSelect = false;
            this._itemList._multiSelectColumnName = "";
            this._itemList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._itemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemList.Location = new System.Drawing.Point(0, 0);
            this._itemList.Margin = new System.Windows.Forms.Padding(0);
            this._itemList.Name = "_itemList";
            this._itemList.Size = new System.Drawing.Size(621, 648);
            this._itemList.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._selectedGid);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 324);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 324);
            this.panel2.TabIndex = 10;
            // 
            // _selectedGid
            // 
            this._selectedGid._extraWordShow = true;
            this._selectedGid._selectRow = -1;
            this._selectedGid.BackColor = System.Drawing.SystemColors.Window;
            this._selectedGid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._selectedGid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._selectedGid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectedGid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._selectedGid.Location = new System.Drawing.Point(0, 25);
            this._selectedGid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._selectedGid.Name = "_selectedGid";
            this._selectedGid.Size = new System.Drawing.Size(640, 299);
            this._selectedGid.TabIndex = 0;
            this._selectedGid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton,
            this._printButton,
            this.toolStripSeparator1,
            this._clearButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(640, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _previewButton
            // 
            this._previewButton.Image = ((System.Drawing.Image)(resources.GetObject("_previewButton.Image")));
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Padding = new System.Windows.Forms.Padding(1);
            this._previewButton.ResourceName = "แสดงบาร์โค๊ดก่อนพิมพ์";
            this._previewButton.Size = new System.Drawing.Size(132, 22);
            this._previewButton.Text = "แสดงบาร์โค๊ดก่อนพิมพ์";
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _printButton
            // 
            this._printButton.Image = ((System.Drawing.Image)(resources.GetObject("_printButton.Image")));
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "พิมพ์บาร์โค๊ด";
            this._printButton.Size = new System.Drawing.Size(88, 22);
            this._printButton.Text = "พิมพ์บาร์โค๊ด";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _clearButton
            // 
            this._clearButton.Image = ((System.Drawing.Image)(resources.GetObject("_clearButton.Image")));
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Padding = new System.Windows.Forms.Padding(1);
            this._clearButton.ResourceName = "เริ่มใหม่ทั้งหมด";
            this._clearButton.Size = new System.Drawing.Size(94, 22);
            this._clearButton.Text = "เริ่มใหม่ทั้งหมด";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._customHeightTextbox);
            this.panel1.Controls.Add(this._customHeightLabel);
            this.panel1.Controls.Add(this._customWidthTextbox);
            this.panel1.Controls.Add(this._customWidthLabel);
            this.panel1.Controls.Add(this._customPaperSizeCheckbox);
            this.panel1.Controls.Add(this._fixSizeCheckbox);
            this.panel1.Controls.Add(this._fixMarginCheckbox);
            this.panel1.Controls.Add(this._unitNameCheckbox);
            this.panel1.Controls.Add(this._borderCheckBox);
            this.panel1.Controls.Add(this._logoHeightTextBox);
            this.panel1.Controls.Add(this._logoWidthTextBox);
            this.panel1.Controls.Add(this._myLabel15);
            this.panel1.Controls.Add(this._myLabel16);
            this.panel1.Controls.Add(this._logoSelectButton);
            this.panel1.Controls.Add(this._logoTextBox);
            this.panel1.Controls.Add(this._myLabel14);
            this.panel1.Controls.Add(this._shelfCheckBox);
            this.panel1.Controls.Add(this._formCodeTextBox);
            this.panel1.Controls.Add(this._printFormCheckBox);
            this.panel1.Controls.Add(this._myLabel13);
            this.panel1.Controls.Add(this._codeTextBox);
            this.panel1.Controls.Add(this._priceComboBox);
            this.panel1.Controls.Add(this._showItemCodeCheckBox);
            this.panel1.Controls.Add(this._showPriceCheckBox);
            this.panel1.Controls.Add(this._ltdNameTextBox);
            this.panel1.Controls.Add(this._myLabel12);
            this.panel1.Controls.Add(this._printerComboBox);
            this.panel1.Controls.Add(this._myLabel11);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this._myLabel10);
            this.panel1.Controls.Add(this.toolStrip3);
            this.panel1.Controls.Add(this._myLabel9);
            this.panel1.Controls.Add(this._nameTextBox);
            this.panel1.Controls.Add(this._startColumnTextBox);
            this.panel1.Controls.Add(this._startRowTextBox);
            this.panel1.Controls.Add(this._maxColumnTextBox);
            this.panel1.Controls.Add(this._maxRowTextBox);
            this.panel1.Controls.Add(this._labelHeightTextBox);
            this.panel1.Controls.Add(this._labelWidthTextBox);
            this.panel1.Controls.Add(this._leftMarginTextBox);
            this.panel1.Controls.Add(this._topMarginTextBox);
            this.panel1.Controls.Add(this._myLabel4);
            this.panel1.Controls.Add(this._myLabel7);
            this.panel1.Controls.Add(this._myLabel5);
            this.panel1.Controls.Add(this._myLabel6);
            this.panel1.Controls.Add(this._myLabel8);
            this.panel1.Controls.Add(this._myLabel3);
            this.panel1.Controls.Add(this._myLabel1);
            this.panel1.Controls.Add(this._myLabel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 324);
            this.panel1.TabIndex = 9;
            // 
            // _customHeightTextbox
            // 
            this._customHeightTextbox.Enabled = false;
            this._customHeightTextbox.Location = new System.Drawing.Point(398, 201);
            this._customHeightTextbox.Name = "_customHeightTextbox";
            this._customHeightTextbox.Size = new System.Drawing.Size(61, 22);
            this._customHeightTextbox.TabIndex = 51;
            // 
            // _customHeightLabel
            // 
            this._customHeightLabel.AutoSize = true;
            this._customHeightLabel.Enabled = false;
            this._customHeightLabel.Location = new System.Drawing.Point(349, 204);
            this._customHeightLabel.Name = "_customHeightLabel";
            this._customHeightLabel.ResourceName = "Height";
            this._customHeightLabel.Size = new System.Drawing.Size(43, 14);
            this._customHeightLabel.TabIndex = 50;
            this._customHeightLabel.Text = "Height";
            // 
            // _customWidthTextbox
            // 
            this._customWidthTextbox.Enabled = false;
            this._customWidthTextbox.Location = new System.Drawing.Point(273, 201);
            this._customWidthTextbox.Name = "_customWidthTextbox";
            this._customWidthTextbox.Size = new System.Drawing.Size(61, 22);
            this._customWidthTextbox.TabIndex = 49;
            // 
            // _customWidthLabel
            // 
            this._customWidthLabel.AutoSize = true;
            this._customWidthLabel.Enabled = false;
            this._customWidthLabel.Location = new System.Drawing.Point(227, 204);
            this._customWidthLabel.Name = "_customWidthLabel";
            this._customWidthLabel.ResourceName = "Width";
            this._customWidthLabel.Size = new System.Drawing.Size(40, 14);
            this._customWidthLabel.TabIndex = 48;
            this._customWidthLabel.Text = "Width";
            // 
            // _customPaperSizeCheckbox
            // 
            this._customPaperSizeCheckbox._isQuery = true;
            this._customPaperSizeCheckbox.AutoSize = true;
            this._customPaperSizeCheckbox.Location = new System.Drawing.Point(129, 203);
            this._customPaperSizeCheckbox.Name = "_customPaperSizeCheckbox";
            this._customPaperSizeCheckbox.ResourceName = "Custom Size";
            this._customPaperSizeCheckbox.Size = new System.Drawing.Size(92, 18);
            this._customPaperSizeCheckbox.TabIndex = 47;
            this._customPaperSizeCheckbox.Text = "Custom Size";
            this._customPaperSizeCheckbox.UseVisualStyleBackColor = true;
            this._customPaperSizeCheckbox.CheckedChanged += new System.EventHandler(this._customPaperSizeCheckbox_CheckedChanged);
            // 
            // _fixSizeCheckbox
            // 
            this._fixSizeCheckbox._isQuery = true;
            this._fixSizeCheckbox.AutoSize = true;
            this._fixSizeCheckbox.Enabled = false;
            this._fixSizeCheckbox.Location = new System.Drawing.Point(398, 130);
            this._fixSizeCheckbox.Name = "_fixSizeCheckbox";
            this._fixSizeCheckbox.ResourceName = "ระบุขนาด";
            this._fixSizeCheckbox.Size = new System.Drawing.Size(71, 18);
            this._fixSizeCheckbox.TabIndex = 46;
            this._fixSizeCheckbox.Text = "ระบุขนาด";
            this._fixSizeCheckbox.UseVisualStyleBackColor = true;
            this._fixSizeCheckbox.CheckedChanged += new System.EventHandler(this._printFormCheckBox_CheckedChanged);
            // 
            // _fixMarginCheckbox
            // 
            this._fixMarginCheckbox._isQuery = true;
            this._fixMarginCheckbox.AutoSize = true;
            this._fixMarginCheckbox.Enabled = false;
            this._fixMarginCheckbox.Location = new System.Drawing.Point(398, 106);
            this._fixMarginCheckbox.Name = "_fixMarginCheckbox";
            this._fixMarginCheckbox.ResourceName = "ระบุขอบ";
            this._fixMarginCheckbox.Size = new System.Drawing.Size(66, 18);
            this._fixMarginCheckbox.TabIndex = 45;
            this._fixMarginCheckbox.Text = "ระบุขอบ";
            this._fixMarginCheckbox.UseVisualStyleBackColor = true;
            this._fixMarginCheckbox.CheckedChanged += new System.EventHandler(this._printFormCheckBox_CheckedChanged);
            // 
            // _unitNameCheckbox
            // 
            this._unitNameCheckbox._isQuery = true;
            this._unitNameCheckbox.AutoSize = true;
            this._unitNameCheckbox.Location = new System.Drawing.Point(497, 178);
            this._unitNameCheckbox.Name = "_unitNameCheckbox";
            this._unitNameCheckbox.ResourceName = "แสดงชื่อหน่วยนับ";
            this._unitNameCheckbox.Size = new System.Drawing.Size(111, 18);
            this._unitNameCheckbox.TabIndex = 44;
            this._unitNameCheckbox.Text = "แสดงชื่อหน่วยนับ";
            this._unitNameCheckbox.UseVisualStyleBackColor = true;
            // 
            // _borderCheckBox
            // 
            this._borderCheckBox._isQuery = true;
            this._borderCheckBox.AutoSize = true;
            this._borderCheckBox.Location = new System.Drawing.Point(497, 106);
            this._borderCheckBox.Name = "_borderCheckBox";
            this._borderCheckBox.ResourceName = "พิมพ์เส้นขอบ";
            this._borderCheckBox.Size = new System.Drawing.Size(87, 18);
            this._borderCheckBox.TabIndex = 43;
            this._borderCheckBox.Text = "พิมพ์เส้นขอบ";
            this._borderCheckBox.UseVisualStyleBackColor = true;
            // 
            // _logoHeightTextBox
            // 
            this._logoHeightTextBox.Location = new System.Drawing.Point(324, 296);
            this._logoHeightTextBox.Name = "_logoHeightTextBox";
            this._logoHeightTextBox.Size = new System.Drawing.Size(61, 22);
            this._logoHeightTextBox.TabIndex = 42;
            this._logoHeightTextBox.Text = "2";
            // 
            // _logoWidthTextBox
            // 
            this._logoWidthTextBox.Location = new System.Drawing.Point(129, 296);
            this._logoWidthTextBox.Name = "_logoWidthTextBox";
            this._logoWidthTextBox.Size = new System.Drawing.Size(61, 22);
            this._logoWidthTextBox.TabIndex = 41;
            this._logoWidthTextBox.Text = "2";
            // 
            // _myLabel15
            // 
            this._myLabel15.AutoSize = true;
            this._myLabel15.Location = new System.Drawing.Point(244, 299);
            this._myLabel15.Name = "_myLabel15";
            this._myLabel15.ResourceName = "Logo Height";
            this._myLabel15.Size = new System.Drawing.Size(74, 14);
            this._myLabel15.TabIndex = 40;
            this._myLabel15.Text = "Logo Height";
            // 
            // _myLabel16
            // 
            this._myLabel16.AutoSize = true;
            this._myLabel16.Location = new System.Drawing.Point(52, 299);
            this._myLabel16.Name = "_myLabel16";
            this._myLabel16.ResourceName = "Logo Width";
            this._myLabel16.Size = new System.Drawing.Size(71, 14);
            this._myLabel16.TabIndex = 39;
            this._myLabel16.Text = "Logo Width";
            // 
            // _logoSelectButton
            // 
            this._logoSelectButton.Location = new System.Drawing.Point(501, 272);
            this._logoSelectButton.Name = "_logoSelectButton";
            this._logoSelectButton.Size = new System.Drawing.Size(43, 23);
            this._logoSelectButton.TabIndex = 38;
            this._logoSelectButton.Text = "...";
            this._logoSelectButton.UseVisualStyleBackColor = true;
            this._logoSelectButton.Click += new System.EventHandler(this._logoSelectButton_Click);
            // 
            // _logoTextBox
            // 
            this._logoTextBox.Location = new System.Drawing.Point(129, 273);
            this._logoTextBox.Name = "_logoTextBox";
            this._logoTextBox.Size = new System.Drawing.Size(366, 22);
            this._logoTextBox.TabIndex = 37;
            // 
            // _myLabel14
            // 
            this._myLabel14.AutoSize = true;
            this._myLabel14.Location = new System.Drawing.Point(89, 276);
            this._myLabel14.Name = "_myLabel14";
            this._myLabel14.ResourceName = "Logo";
            this._myLabel14.Size = new System.Drawing.Size(34, 14);
            this._myLabel14.TabIndex = 36;
            this._myLabel14.Text = "Logo";
            // 
            // _shelfCheckBox
            // 
            this._shelfCheckBox._isQuery = true;
            this._shelfCheckBox.AutoSize = true;
            this._shelfCheckBox.Location = new System.Drawing.Point(398, 32);
            this._shelfCheckBox.Name = "_shelfCheckBox";
            this._shelfCheckBox.ResourceName = "พิมพ์ติด Shelf";
            this._shelfCheckBox.Size = new System.Drawing.Size(92, 18);
            this._shelfCheckBox.TabIndex = 35;
            this._shelfCheckBox.Text = "พิมพ์ติด Shelf";
            this._shelfCheckBox.UseVisualStyleBackColor = true;
            this._shelfCheckBox.CheckedChanged += new System.EventHandler(this._shelfCheckBox_CheckedChanged);
            // 
            // _formCodeTextBox
            // 
            this._formCodeTextBox.Location = new System.Drawing.Point(497, 78);
            this._formCodeTextBox.Name = "_formCodeTextBox";
            this._formCodeTextBox.Size = new System.Drawing.Size(93, 22);
            this._formCodeTextBox.TabIndex = 34;
            // 
            // _printFormCheckBox
            // 
            this._printFormCheckBox._isQuery = true;
            this._printFormCheckBox.AutoSize = true;
            this._printFormCheckBox.Location = new System.Drawing.Point(398, 82);
            this._printFormCheckBox.Name = "_printFormCheckBox";
            this._printFormCheckBox.ResourceName = "พิมพ์ด้วยฟอร์ม";
            this._printFormCheckBox.Size = new System.Drawing.Size(93, 18);
            this._printFormCheckBox.TabIndex = 31;
            this._printFormCheckBox.Text = "พิมพ์ด้วยฟอร์ม";
            this._printFormCheckBox.UseVisualStyleBackColor = true;
            this._printFormCheckBox.CheckedChanged += new System.EventHandler(this._printFormCheckBox_CheckedChanged);
            // 
            // _myLabel13
            // 
            this._myLabel13.AutoSize = true;
            this._myLabel13.Location = new System.Drawing.Point(96, 33);
            this._myLabel13.Name = "_myLabel13";
            this._myLabel13.ResourceName = "รหัส";
            this._myLabel13.Size = new System.Drawing.Size(27, 14);
            this._myLabel13.TabIndex = 30;
            this._myLabel13.Text = "รหัส";
            // 
            // _codeTextBox
            // 
            this._codeTextBox.Location = new System.Drawing.Point(129, 30);
            this._codeTextBox.MaxLength = 50;
            this._codeTextBox.Name = "_codeTextBox";
            this._codeTextBox.Size = new System.Drawing.Size(256, 22);
            this._codeTextBox.TabIndex = 29;
            // 
            // _priceComboBox
            // 
            this._priceComboBox.FormattingEnabled = true;
            this._priceComboBox.Items.AddRange(new object[] {
            "Price 1",
            "Price 2",
            "Price 3",
            "Price 4",
            "ราคากลาง"});
            this._priceComboBox.Location = new System.Drawing.Point(497, 152);
            this._priceComboBox.Name = "_priceComboBox";
            this._priceComboBox.Size = new System.Drawing.Size(93, 22);
            this._priceComboBox.TabIndex = 28;
            // 
            // _showItemCodeCheckBox
            // 
            this._showItemCodeCheckBox._isQuery = true;
            this._showItemCodeCheckBox.AutoSize = true;
            this._showItemCodeCheckBox.Checked = true;
            this._showItemCodeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showItemCodeCheckBox.Location = new System.Drawing.Point(398, 178);
            this._showItemCodeCheckBox.Name = "_showItemCodeCheckBox";
            this._showItemCodeCheckBox.ResourceName = "แสดงรหัสสินค้า";
            this._showItemCodeCheckBox.Size = new System.Drawing.Size(100, 18);
            this._showItemCodeCheckBox.TabIndex = 27;
            this._showItemCodeCheckBox.Text = "แสดงรหัสสินค้า";
            this._showItemCodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // _showPriceCheckBox
            // 
            this._showPriceCheckBox._isQuery = true;
            this._showPriceCheckBox.AutoSize = true;
            this._showPriceCheckBox.Checked = true;
            this._showPriceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showPriceCheckBox.Location = new System.Drawing.Point(398, 154);
            this._showPriceCheckBox.Name = "_showPriceCheckBox";
            this._showPriceCheckBox.ResourceName = "แสดงราคา";
            this._showPriceCheckBox.Size = new System.Drawing.Size(77, 18);
            this._showPriceCheckBox.TabIndex = 26;
            this._showPriceCheckBox.Text = "แสดงราคา";
            this._showPriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // _ltdNameTextBox
            // 
            this._ltdNameTextBox.Location = new System.Drawing.Point(129, 225);
            this._ltdNameTextBox.Name = "_ltdNameTextBox";
            this._ltdNameTextBox.Size = new System.Drawing.Size(415, 22);
            this._ltdNameTextBox.TabIndex = 24;
            // 
            // _myLabel12
            // 
            this._myLabel12.AutoSize = true;
            this._myLabel12.Location = new System.Drawing.Point(70, 228);
            this._myLabel12.Name = "_myLabel12";
            this._myLabel12.ResourceName = "ชื่อบริษัท";
            this._myLabel12.Size = new System.Drawing.Size(53, 14);
            this._myLabel12.TabIndex = 23;
            this._myLabel12.Text = "ชื่อบริษัท";
            // 
            // _printerComboBox
            // 
            this._printerComboBox.FormattingEnabled = true;
            this._printerComboBox.Location = new System.Drawing.Point(129, 249);
            this._printerComboBox.Name = "_printerComboBox";
            this._printerComboBox.Size = new System.Drawing.Size(415, 22);
            this._printerComboBox.TabIndex = 22;
            // 
            // _myLabel11
            // 
            this._myLabel11.AutoSize = true;
            this._myLabel11.Location = new System.Drawing.Point(51, 252);
            this._myLabel11.Name = "_myLabel11";
            this._myLabel11.ResourceName = "ชื่อเครื่องพิมพ์";
            this._myLabel11.Size = new System.Drawing.Size(72, 14);
            this._myLabel11.TabIndex = 21;
            this._myLabel11.Text = "ชื่อเครื่องพิมพ์";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(129, 79);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(86, 18);
            this.radioButton2.TabIndex = 20;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Centimeter";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(216, 79);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 18);
            this.radioButton1.TabIndex = 19;
            this.radioButton1.Text = "Inch";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // _myLabel10
            // 
            this._myLabel10.AutoSize = true;
            this._myLabel10.Location = new System.Drawing.Point(88, 81);
            this._myLabel10.Name = "_myLabel10";
            this._myLabel10.ResourceName = "หน่วย";
            this._myLabel10.Size = new System.Drawing.Size(35, 14);
            this._myLabel10.TabIndex = 18;
            this._myLabel10.Text = "หน่วย";
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._formatLoadButton,
            this._formatSaveButton,
            this._formatDeleteButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(640, 25);
            this.toolStrip3.TabIndex = 8;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // _formatLoadButton
            // 
            this._formatLoadButton.Image = ((System.Drawing.Image)(resources.GetObject("_formatLoadButton.Image")));
            this._formatLoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._formatLoadButton.Name = "_formatLoadButton";
            this._formatLoadButton.Padding = new System.Windows.Forms.Padding(1);
            this._formatLoadButton.ResourceName = "เรียกรูปแบบป้ายบาร์โค๊ด";
            this._formatLoadButton.Size = new System.Drawing.Size(136, 22);
            this._formatLoadButton.Text = "เรียกรูปแบบป้ายบาร์โค๊ด";
            this._formatLoadButton.Click += new System.EventHandler(this._formatLoadButton_Click);
            // 
            // _formatSaveButton
            // 
            this._formatSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("_formatSaveButton.Image")));
            this._formatSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._formatSaveButton.Name = "_formatSaveButton";
            this._formatSaveButton.Padding = new System.Windows.Forms.Padding(1);
            this._formatSaveButton.ResourceName = "บันทึกรูปแบบป้ายบาร์โค๊ด";
            this._formatSaveButton.Size = new System.Drawing.Size(143, 22);
            this._formatSaveButton.Text = "บันทึกรูปแบบป้ายบาร์โค๊ด";
            this._formatSaveButton.Click += new System.EventHandler(this._formatSaveButton_Click);
            // 
            // _formatDeleteButton
            // 
            this._formatDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("_formatDeleteButton.Image")));
            this._formatDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._formatDeleteButton.Name = "_formatDeleteButton";
            this._formatDeleteButton.Padding = new System.Windows.Forms.Padding(1);
            this._formatDeleteButton.ResourceName = "ลบรูปแบบป้ายบาร์โค๊ด";
            this._formatDeleteButton.Size = new System.Drawing.Size(128, 22);
            this._formatDeleteButton.Text = "ลบรูปแบบป้ายบาร์โค๊ด";
            this._formatDeleteButton.Click += new System.EventHandler(this._formatDeleteButton_Click);
            // 
            // _myLabel9
            // 
            this._myLabel9.AutoSize = true;
            this._myLabel9.Location = new System.Drawing.Point(65, 57);
            this._myLabel9.Name = "_myLabel9";
            this._myLabel9.ResourceName = "รายละเอียด";
            this._myLabel9.Size = new System.Drawing.Size(62, 14);
            this._myLabel9.TabIndex = 17;
            this._myLabel9.Text = "รายละเอียด";
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Location = new System.Drawing.Point(129, 54);
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.Size = new System.Drawing.Size(415, 22);
            this._nameTextBox.TabIndex = 7;
            this._nameTextBox.Text = "A4 14x5";
            // 
            // _startColumnTextBox
            // 
            this._startColumnTextBox.Location = new System.Drawing.Point(324, 176);
            this._startColumnTextBox.Name = "_startColumnTextBox";
            this._startColumnTextBox.Size = new System.Drawing.Size(61, 22);
            this._startColumnTextBox.TabIndex = 15;
            this._startColumnTextBox.Text = "1";
            // 
            // _startRowTextBox
            // 
            this._startRowTextBox.Location = new System.Drawing.Point(129, 176);
            this._startRowTextBox.Name = "_startRowTextBox";
            this._startRowTextBox.Size = new System.Drawing.Size(61, 22);
            this._startRowTextBox.TabIndex = 14;
            this._startRowTextBox.Text = "1";
            // 
            // _maxColumnTextBox
            // 
            this._maxColumnTextBox.Location = new System.Drawing.Point(324, 152);
            this._maxColumnTextBox.Name = "_maxColumnTextBox";
            this._maxColumnTextBox.Size = new System.Drawing.Size(61, 22);
            this._maxColumnTextBox.TabIndex = 13;
            this._maxColumnTextBox.Text = "5";
            // 
            // _maxRowTextBox
            // 
            this._maxRowTextBox.Location = new System.Drawing.Point(129, 152);
            this._maxRowTextBox.Name = "_maxRowTextBox";
            this._maxRowTextBox.Size = new System.Drawing.Size(61, 22);
            this._maxRowTextBox.TabIndex = 12;
            this._maxRowTextBox.Text = "14";
            // 
            // _labelHeightTextBox
            // 
            this._labelHeightTextBox.Location = new System.Drawing.Point(324, 128);
            this._labelHeightTextBox.Name = "_labelHeightTextBox";
            this._labelHeightTextBox.Size = new System.Drawing.Size(61, 22);
            this._labelHeightTextBox.TabIndex = 11;
            this._labelHeightTextBox.Text = "2.00";
            // 
            // _labelWidthTextBox
            // 
            this._labelWidthTextBox.Location = new System.Drawing.Point(129, 128);
            this._labelWidthTextBox.Name = "_labelWidthTextBox";
            this._labelWidthTextBox.Size = new System.Drawing.Size(61, 22);
            this._labelWidthTextBox.TabIndex = 10;
            this._labelWidthTextBox.Text = "4.00";
            // 
            // _leftMarginTextBox
            // 
            this._leftMarginTextBox.Location = new System.Drawing.Point(324, 104);
            this._leftMarginTextBox.Name = "_leftMarginTextBox";
            this._leftMarginTextBox.Size = new System.Drawing.Size(61, 22);
            this._leftMarginTextBox.TabIndex = 9;
            this._leftMarginTextBox.Text = "0.20";
            // 
            // _topMarginTextBox
            // 
            this._topMarginTextBox.Location = new System.Drawing.Point(129, 104);
            this._topMarginTextBox.Name = "_topMarginTextBox";
            this._topMarginTextBox.Size = new System.Drawing.Size(61, 22);
            this._topMarginTextBox.TabIndex = 8;
            this._topMarginTextBox.Text = "0.50";
            // 
            // _myLabel4
            // 
            this._myLabel4.AutoSize = true;
            this._myLabel4.Location = new System.Drawing.Point(24, 131);
            this._myLabel4.Name = "_myLabel4";
            this._myLabel4.ResourceName = "ความกว้างของป้าย";
            this._myLabel4.Size = new System.Drawing.Size(99, 14);
            this._myLabel4.TabIndex = 3;
            this._myLabel4.Text = "ความกว้างของป้าย";
            // 
            // _myLabel7
            // 
            this._myLabel7.AutoSize = true;
            this._myLabel7.Location = new System.Drawing.Point(199, 179);
            this._myLabel7.Name = "_myLabel7";
            this._myLabel7.ResourceName = "ตำแหน่งเริ่มต้นแนวนอน";
            this._myLabel7.Size = new System.Drawing.Size(119, 14);
            this._myLabel7.TabIndex = 7;
            this._myLabel7.Text = "ตำแหน่งเริ่มต้นแนวนอน";
            // 
            // _myLabel5
            // 
            this._myLabel5.AutoSize = true;
            this._myLabel5.Location = new System.Drawing.Point(30, 155);
            this._myLabel5.Name = "_myLabel5";
            this._myLabel5.ResourceName = "จำนวนแถวแนวตั้ง";
            this._myLabel5.Size = new System.Drawing.Size(93, 14);
            this._myLabel5.TabIndex = 4;
            this._myLabel5.Text = "จำนวนแถวแนวตั้ง";
            // 
            // _myLabel6
            // 
            this._myLabel6.AutoSize = true;
            this._myLabel6.Location = new System.Drawing.Point(217, 155);
            this._myLabel6.Name = "_myLabel6";
            this._myLabel6.ResourceName = "จำนวนแถวแนวนอน";
            this._myLabel6.Size = new System.Drawing.Size(101, 14);
            this._myLabel6.TabIndex = 5;
            this._myLabel6.Text = "จำนวนแถวแนวนอน";
            // 
            // _myLabel8
            // 
            this._myLabel8.AutoSize = true;
            this._myLabel8.Location = new System.Drawing.Point(12, 179);
            this._myLabel8.Name = "_myLabel8";
            this._myLabel8.ResourceName = "ตำแหน่งเริ่มต้นแนวตั้ง";
            this._myLabel8.Size = new System.Drawing.Size(111, 14);
            this._myLabel8.TabIndex = 6;
            this._myLabel8.Text = "ตำแหน่งเริ่มต้นแนวตั้ง";
            // 
            // _myLabel3
            // 
            this._myLabel3.AutoSize = true;
            this._myLabel3.Location = new System.Drawing.Point(231, 131);
            this._myLabel3.Name = "_myLabel3";
            this._myLabel3.ResourceName = "ความสูงของป้าย";
            this._myLabel3.Size = new System.Drawing.Size(87, 14);
            this._myLabel3.TabIndex = 2;
            this._myLabel3.Text = "ความสูงของป้าย";
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(39, 107);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "จากขอบด้านบน";
            this._myLabel1.Size = new System.Drawing.Size(84, 14);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "จากขอบด้านบน";
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.Location = new System.Drawing.Point(228, 107);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "จากขอบด้านซ้าย";
            this._myLabel2.Size = new System.Drawing.Size(90, 14);
            this._myLabel2.TabIndex = 1;
            this._myLabel2.Text = "จากขอบด้านซ้าย";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1269, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeButton.Image")));
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "Close";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _itemBarcodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_itemBarcodePrint";
            this.Size = new System.Drawing.Size(1269, 675);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myGrid _selectedGid;
        private MyLib._myDataList _itemList;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private MyLib.ToolStripMyButton _printButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox _startColumnTextBox;
        private System.Windows.Forms.TextBox _startRowTextBox;
        private System.Windows.Forms.TextBox _maxColumnTextBox;
        private System.Windows.Forms.TextBox _maxRowTextBox;
        private System.Windows.Forms.TextBox _labelHeightTextBox;
        private System.Windows.Forms.TextBox _labelWidthTextBox;
        private System.Windows.Forms.TextBox _leftMarginTextBox;
        private System.Windows.Forms.TextBox _topMarginTextBox;
        private MyLib._myLabel _myLabel4;
        private MyLib._myLabel _myLabel7;
        private MyLib._myLabel _myLabel5;
        private MyLib._myLabel _myLabel6;
        private MyLib._myLabel _myLabel8;
        private MyLib._myLabel _myLabel3;
        private MyLib._myLabel _myLabel1;
        private MyLib._myLabel _myLabel2;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private MyLib.ToolStripMyButton _formatLoadButton;
        private MyLib.ToolStripMyButton _formatSaveButton;
        private MyLib.ToolStripMyButton _formatDeleteButton;
        private MyLib._myLabel _myLabel9;
        private System.Windows.Forms.TextBox _nameTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private MyLib._myLabel _myLabel10;
        private System.Windows.Forms.ComboBox _printerComboBox;
        private MyLib._myLabel _myLabel11;
        private MyLib.ToolStripMyButton _previewButton;
        private MyLib._myCheckBox _showItemCodeCheckBox;
        private MyLib._myCheckBox _showPriceCheckBox;
        private System.Windows.Forms.TextBox _ltdNameTextBox;
        private MyLib._myLabel _myLabel12;
        private System.Windows.Forms.ComboBox _priceComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib.ToolStripMyButton _clearButton;
        private MyLib._myLabel _myLabel13;
        private System.Windows.Forms.TextBox _codeTextBox;
        private System.Windows.Forms.TextBox _formCodeTextBox;
        private MyLib._myCheckBox _printFormCheckBox;
        private System.Windows.Forms.Button _logoSelectButton;
        private System.Windows.Forms.TextBox _logoTextBox;
        private MyLib._myLabel _myLabel14;
        private MyLib._myCheckBox _shelfCheckBox;
        private System.Windows.Forms.TextBox _logoHeightTextBox;
        private System.Windows.Forms.TextBox _logoWidthTextBox;
        private MyLib._myLabel _myLabel15;
        private MyLib._myLabel _myLabel16;
        private MyLib._myCheckBox _borderCheckBox;
        private MyLib._myCheckBox _unitNameCheckbox;
        private MyLib._myCheckBox _fixSizeCheckbox;
        private MyLib._myCheckBox _fixMarginCheckbox;
        private MyLib._myCheckBox _customPaperSizeCheckbox;
        private System.Windows.Forms.TextBox _customHeightTextbox;
        private MyLib._myLabel _customHeightLabel;
        private System.Windows.Forms.TextBox _customWidthTextbox;
        private MyLib._myLabel _customWidthLabel;
    }
}
