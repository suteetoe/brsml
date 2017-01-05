namespace SMLERPIC
{
    partial class _icAddBarcodeFastControl
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._barCodeGrid = new SMLInventoryControl._icmainGridBarCodeControl();
            this._unitGrid = new SMLERPControl._ic._icmainGridUnitControl();
            this._gridWarehouseLocation = new SMLInventoryControl._icmainGridWarehouseLocationControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this._shelfTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._autoFocusCheckBox = new System.Windows.Forms.CheckBox();
            this._barcodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this._warehouseLocationLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._barcodeLabel = new System.Windows.Forms.Label();
            this._shelfLabel = new System.Windows.Forms.Label();
            this._itemUnitLabel = new System.Windows.Forms.Label();
            this._itemCodeLabel = new System.Windows.Forms.Label();
            this._itemNameLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._switchWarehouseShelfButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._mainTab = new MyLib._myTabControl();
            this.tab_description = new MyLib._myTabPage();
            this.tab_newinventory = new MyLib._myTabPage();
            this._icUpdateControl = new SMLInventoryControl._icUpdateControl();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._mainTab.SuspendLayout();
            this.tab_description.SuspendLayout();
            this.tab_newinventory.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 28);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._gridWarehouseLocation);
            this.splitContainer2.Size = new System.Drawing.Size(916, 264);
            this.splitContainer2.SplitterDistance = 173;
            this.splitContainer2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 171);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._barCodeGrid);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._unitGrid);
            this.splitContainer3.Size = new System.Drawing.Size(914, 171);
            this.splitContainer3.SplitterDistance = 583;
            this.splitContainer3.TabIndex = 2;
            // 
            // _barCodeGrid
            // 
            this._barCodeGrid._extraWordShow = true;
            this._barCodeGrid._selectRow = -1;
            this._barCodeGrid.AllowDrop = true;
            this._barCodeGrid.BackColor = System.Drawing.SystemColors.Window;
            this._barCodeGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._barCodeGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._barCodeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._barCodeGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._barCodeGrid.Location = new System.Drawing.Point(0, 0);
            this._barCodeGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._barCodeGrid.Name = "_barCodeGrid";
            this._barCodeGrid.Size = new System.Drawing.Size(583, 171);
            this._barCodeGrid.TabIndex = 1;
            this._barCodeGrid.TabStop = false;
            // 
            // _unitGrid
            // 
            this._unitGrid._extraWordShow = true;
            this._unitGrid._selectRow = -1;
            this._unitGrid.BackColor = System.Drawing.SystemColors.Window;
            this._unitGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._unitGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._unitGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._unitGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._unitGrid.Location = new System.Drawing.Point(0, 0);
            this._unitGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._unitGrid.Name = "_unitGrid";
            this._unitGrid.Size = new System.Drawing.Size(327, 171);
            this._unitGrid.TabIndex = 0;
            this._unitGrid.TabStop = false;
            // 
            // _gridWarehouseLocation
            // 
            this._gridWarehouseLocation._extraWordShow = true;
            this._gridWarehouseLocation._selectRow = -1;
            this._gridWarehouseLocation.BackColor = System.Drawing.SystemColors.Window;
            this._gridWarehouseLocation.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridWarehouseLocation.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridWarehouseLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridWarehouseLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridWarehouseLocation.Location = new System.Drawing.Point(0, 0);
            this._gridWarehouseLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridWarehouseLocation.Name = "_gridWarehouseLocation";
            this._gridWarehouseLocation.Size = new System.Drawing.Size(914, 85);
            this._gridWarehouseLocation.TabIndex = 0;
            this._gridWarehouseLocation.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this._shelfTextBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this._autoFocusCheckBox);
            this.panel2.Controls.Add(this._barcodeTextBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(930, 44);
            this.panel2.TabIndex = 0;
            // 
            // _shelfTextBox
            // 
            this._shelfTextBox.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._shelfTextBox.Location = new System.Drawing.Point(642, 5);
            this._shelfTextBox.Name = "_shelfTextBox";
            this._shelfTextBox.Size = new System.Drawing.Size(281, 33);
            this._shelfTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(544, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Update Shelf :";
            // 
            // _autoFocusCheckBox
            // 
            this._autoFocusCheckBox.AutoSize = true;
            this._autoFocusCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._autoFocusCheckBox.Location = new System.Drawing.Point(7, 21);
            this._autoFocusCheckBox.Name = "_autoFocusCheckBox";
            this._autoFocusCheckBox.Size = new System.Drawing.Size(136, 18);
            this._autoFocusCheckBox.TabIndex = 2;
            this._autoFocusCheckBox.Text = "Barcode Auto Focus";
            this._autoFocusCheckBox.UseVisualStyleBackColor = true;
            // 
            // _barcodeTextBox
            // 
            this._barcodeTextBox.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._barcodeTextBox.Location = new System.Drawing.Point(149, 5);
            this._barcodeTextBox.Name = "_barcodeTextBox";
            this._barcodeTextBox.Size = new System.Drawing.Size(370, 33);
            this._barcodeTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(51, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Barcode (F2) :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this._warehouseLocationLabel);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this._barcodeLabel);
            this.panel3.Controls.Add(this._shelfLabel);
            this.panel3.Controls.Add(this._itemUnitLabel);
            this.panel3.Controls.Add(this._itemCodeLabel);
            this.panel3.Controls.Add(this._itemNameLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(930, 254);
            this.panel3.TabIndex = 2;
            // 
            // _warehouseLocationLabel
            // 
            this._warehouseLocationLabel.AutoSize = true;
            this._warehouseLocationLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._warehouseLocationLabel.ForeColor = System.Drawing.Color.White;
            this._warehouseLocationLabel.Location = new System.Drawing.Point(277, 2);
            this._warehouseLocationLabel.Name = "_warehouseLocationLabel";
            this._warehouseLocationLabel.Size = new System.Drawing.Size(257, 33);
            this._warehouseLocationLabel.TabIndex = 13;
            this._warehouseLocationLabel.Text = "Warehouse/Location";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(4, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(277, 33);
            this.label8.TabIndex = 12;
            this.label8.Text = "Default WH/Location :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(152, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 33);
            this.label3.TabIndex = 11;
            this.label3.Text = "Barcode :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(141, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 33);
            this.label4.TabIndex = 10;
            this.label4.Text = "Shelf List :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(85, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 33);
            this.label5.TabIndex = 9;
            this.label5.Text = "Unit Standard :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(89, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 33);
            this.label6.TabIndex = 8;
            this.label6.Text = "Product Code :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(80, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 33);
            this.label7.TabIndex = 7;
            this.label7.Text = "Product Name :";
            // 
            // _barcodeLabel
            // 
            this._barcodeLabel.AutoSize = true;
            this._barcodeLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._barcodeLabel.ForeColor = System.Drawing.Color.White;
            this._barcodeLabel.Location = new System.Drawing.Point(277, 43);
            this._barcodeLabel.Name = "_barcodeLabel";
            this._barcodeLabel.Size = new System.Drawing.Size(111, 33);
            this._barcodeLabel.TabIndex = 6;
            this._barcodeLabel.Text = "Barcode";
            // 
            // _shelfLabel
            // 
            this._shelfLabel.AutoSize = true;
            this._shelfLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._shelfLabel.ForeColor = System.Drawing.Color.White;
            this._shelfLabel.Location = new System.Drawing.Point(277, 207);
            this._shelfLabel.Name = "_shelfLabel";
            this._shelfLabel.Size = new System.Drawing.Size(74, 33);
            this._shelfLabel.TabIndex = 5;
            this._shelfLabel.Text = "Shelf";
            // 
            // _itemUnitLabel
            // 
            this._itemUnitLabel.AutoSize = true;
            this._itemUnitLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemUnitLabel.ForeColor = System.Drawing.Color.White;
            this._itemUnitLabel.Location = new System.Drawing.Point(277, 166);
            this._itemUnitLabel.Name = "_itemUnitLabel";
            this._itemUnitLabel.Size = new System.Drawing.Size(63, 33);
            this._itemUnitLabel.TabIndex = 2;
            this._itemUnitLabel.Text = "Unit";
            // 
            // _itemCodeLabel
            // 
            this._itemCodeLabel.AutoSize = true;
            this._itemCodeLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemCodeLabel.ForeColor = System.Drawing.Color.White;
            this._itemCodeLabel.Location = new System.Drawing.Point(277, 84);
            this._itemCodeLabel.Name = "_itemCodeLabel";
            this._itemCodeLabel.Size = new System.Drawing.Size(75, 33);
            this._itemCodeLabel.TabIndex = 1;
            this._itemCodeLabel.Text = "Code";
            // 
            // _itemNameLabel
            // 
            this._itemNameLabel.AutoSize = true;
            this._itemNameLabel.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemNameLabel.ForeColor = System.Drawing.Color.White;
            this._itemNameLabel.Location = new System.Drawing.Point(277, 125);
            this._itemNameLabel.Name = "_itemNameLabel";
            this._itemNameLabel.Size = new System.Drawing.Size(84, 33);
            this._itemNameLabel.TabIndex = 0;
            this._itemNameLabel.Text = "Name";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._switchWarehouseShelfButton,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(930, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _switchWarehouseShelfButton
            // 
            this._switchWarehouseShelfButton.Image = global::SMLERPIC.Properties.Resources.replace2;
            this._switchWarehouseShelfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._switchWarehouseShelfButton.Name = "_switchWarehouseShelfButton";
            this._switchWarehouseShelfButton.Size = new System.Drawing.Size(175, 22);
            this._switchWarehouseShelfButton.Text = "Switch Warehouse/Location";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(80, 22);
            this._saveButton.Text = "Save (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::SMLERPIC.Properties.Resources.error1;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(56, 22);
            this.toolStripButton1.Text = "Close";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // _mainTab
            // 
            this._mainTab.Controls.Add(this.tab_description);
            this._mainTab.Controls.Add(this.tab_newinventory);
            this._mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._mainTab.Location = new System.Drawing.Point(0, 323);
            this._mainTab.Multiline = true;
            this._mainTab.Name = "_mainTab";
            this._mainTab.SelectedIndex = 0;
            this._mainTab.Size = new System.Drawing.Size(930, 322);
            this._mainTab.TabIndex = 2;
            this._mainTab.TableName = "ic_resource";
            // 
            // tab_description
            // 
            this.tab_description.Controls.Add(this.splitContainer2);
            this.tab_description.Controls.Add(this.toolStrip2);
            this.tab_description.Location = new System.Drawing.Point(4, 23);
            this.tab_description.Name = "tab_description";
            this.tab_description.Padding = new System.Windows.Forms.Padding(3);
            this.tab_description.Size = new System.Drawing.Size(922, 295);
            this.tab_description.TabIndex = 0;
            this.tab_description.Text = "1.tab_description";
            this.tab_description.UseVisualStyleBackColor = true;
            // 
            // tab_newinventory
            // 
            this.tab_newinventory.Controls.Add(this._icUpdateControl);
            this.tab_newinventory.Location = new System.Drawing.Point(4, 23);
            this.tab_newinventory.Name = "tab_newinventory";
            this.tab_newinventory.Padding = new System.Windows.Forms.Padding(3);
            this.tab_newinventory.Size = new System.Drawing.Size(922, 295);
            this.tab_newinventory.TabIndex = 1;
            this.tab_newinventory.Text = "2.tab_newinventory";
            this.tab_newinventory.UseVisualStyleBackColor = true;
            // 
            // _icUpdateControl
            // 
            this._icUpdateControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icUpdateControl.Location = new System.Drawing.Point(3, 3);
            this._icUpdateControl.Name = "_icUpdateControl";
            this._icUpdateControl.Size = new System.Drawing.Size(916, 289);
            this._icUpdateControl.TabIndex = 0;
            this._icUpdateControl._saveButton.Text = "เพิ่มสินค้า (F10)";
            this._icUpdateControl._saveButton.ResourceName = "เพิ่มสินค้า (F10)";

            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this._saveButton
            });

            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(916, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _icAddBarcodeFastControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this._mainTab);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icAddBarcodeFastControl";
            this.Size = new System.Drawing.Size(930, 645);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._mainTab.ResumeLayout(false);
            this.tab_description.ResumeLayout(false);
            this.tab_description.PerformLayout();
            this.tab_newinventory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private SMLInventoryControl._icmainGridBarCodeControl _barCodeGrid;
        private System.Windows.Forms.ToolStripButton _saveButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox _barcodeTextBox;
        private System.Windows.Forms.Label label1;
        private SMLERPControl._ic._icmainGridUnitControl _unitGrid;
        private System.Windows.Forms.CheckBox _autoFocusCheckBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label _itemUnitLabel;
        private System.Windows.Forms.Label _itemCodeLabel;
        private System.Windows.Forms.Label _itemNameLabel;
        private System.Windows.Forms.Label _shelfLabel;
        private System.Windows.Forms.ToolStripButton _switchWarehouseShelfButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox _shelfTextBox;
        private System.Windows.Forms.Label label2;
        private SMLInventoryControl._icmainGridWarehouseLocationControl _gridWarehouseLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label _barcodeLabel;
        private System.Windows.Forms.Label _warehouseLocationLabel;
        private System.Windows.Forms.Label label8;
        private MyLib._myTabControl _mainTab;
        private MyLib._myTabPage tab_description;
        private MyLib._myTabPage tab_newinventory;
        private SMLInventoryControl._icUpdateControl _icUpdateControl;
        private System.Windows.Forms.ToolStrip toolStrip2;
    }
}
