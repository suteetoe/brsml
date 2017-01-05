namespace SMLInventoryControl
{
    partial class _icUpdateControl
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
            this._icUnitGrid = new SMLERPControl._ic._icmainGridUnitControl();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._icPriceFormulaGrid = new _icPriceFormulaGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this._toolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._icBarcodeGrid = new SMLInventoryControl._icmainGridBarCodeControl();
            this._icMainScreen = new SMLInventoryControl._icMainFastScreen();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this._toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 70);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._icUnitGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(695, 706);
            this.splitContainer2.SplitterDistance = 176;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // _icUnitGrid
            // 
            this._icUnitGrid._extraWordShow = true;
            this._icUnitGrid._selectRow = -1;
            this._icUnitGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icUnitGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icUnitGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icUnitGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icUnitGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icUnitGrid.Location = new System.Drawing.Point(0, 0);
            this._icUnitGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icUnitGrid.Name = "_icUnitGrid";
            this._icUnitGrid.Size = new System.Drawing.Size(693, 174);
            this._icUnitGrid.TabIndex = 0;
            this._icUnitGrid.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._icBarcodeGrid);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._icPriceFormulaGrid);
            this.splitContainer3.Size = new System.Drawing.Size(695, 525);
            this.splitContainer3.SplitterDistance = 224;
            this.splitContainer3.TabIndex = 0;
            // 
            // _icPriceFormulaGrid
            // 
            this._icPriceFormulaGrid._extraWordShow = true;
            this._icPriceFormulaGrid._selectRow = -1;
            this._icPriceFormulaGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icPriceFormulaGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icPriceFormulaGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icPriceFormulaGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icPriceFormulaGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icPriceFormulaGrid.Location = new System.Drawing.Point(0, 0);
            this._icPriceFormulaGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icPriceFormulaGrid.Name = "_icPriceFormulaGrid";
            this._icPriceFormulaGrid.Size = new System.Drawing.Size(693, 295);
            this._icPriceFormulaGrid.TabIndex = 0;
            this._icPriceFormulaGrid.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Controls.Add(this._icMainScreen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 776);
            this.panel1.TabIndex = 3;
            // 
            // _toolbar
            // 
            this._toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._toolbar.Location = new System.Drawing.Point(0, 0);
            this._toolbar.Name = "_toolbar";
            this._toolbar.Size = new System.Drawing.Size(695, 25);
            this._toolbar.TabIndex = 2;
            this._toolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก (F12)";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click_1);
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
            // _icBarcodeGrid
            // 
            this._icBarcodeGrid._extraWordShow = true;
            this._icBarcodeGrid._selectRow = -1;
            this._icBarcodeGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icBarcodeGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icBarcodeGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icBarcodeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icBarcodeGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icBarcodeGrid.Location = new System.Drawing.Point(0, 0);
            this._icBarcodeGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icBarcodeGrid.Name = "_icBarcodeGrid";
            this._icBarcodeGrid.Size = new System.Drawing.Size(693, 222);
            this._icBarcodeGrid.TabIndex = 0;
            this._icBarcodeGrid.TabStop = false;
            // 
            // _icMainScreen
            // 
            this._icMainScreen._isChange = false;
            this._icMainScreen.BackColor = System.Drawing.Color.Transparent;
            this._icMainScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._icMainScreen.Location = new System.Drawing.Point(0, 0);
            this._icMainScreen.Name = "_icMainScreen";
            this._icMainScreen.Size = new System.Drawing.Size(695, 70);
            this._icMainScreen.TabIndex = 0;
            // 
            // _icUpdateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._toolbar);
            this.Name = "_icUpdateControl";
            this.Size = new System.Drawing.Size(695, 801);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel1;
        public _icMainFastScreen _icMainScreen;
        public SMLERPControl._ic._icmainGridUnitControl _icUnitGrid;
        public _icmainGridBarCodeControl _icBarcodeGrid;
        public _icPriceFormulaGrid _icPriceFormulaGrid;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib.ToolStripMyButton _saveButton;
        public System.Windows.Forms.ToolStrip _toolbar;
    }
}
