namespace SMLERPControl._coupon
{
    partial class _couponGenerateControl
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
            this._screenTop = new MyLib._myScreen();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripMyLabel1 = new MyLib.ToolStripMyLabel();
            this._formatTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMyLabel2 = new MyLib.ToolStripMyLabel();
            this._amountTextbox = new System.Windows.Forms.ToolStripTextBox();
            this._couponGenGrid = new MyLib._myGrid();
            this._genButton = new MyLib.ToolStripMyButton();
            this._clearButton = new MyLib.ToolStripMyButton();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myToolBar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(655, 25);
            this._myToolBar.TabIndex = 1;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(0, 25);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(655, 10);
            this._screenTop.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMyLabel1,
            this._formatTextbox,
            this.toolStripMyLabel2,
            this._amountTextbox,
            this._genButton,
            this._clearButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 35);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(655, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripMyLabel1
            // 
            this.toolStripMyLabel1.Name = "toolStripMyLabel1";
            this.toolStripMyLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyLabel1.ResourceName = "หมายเลขคูปองเริ่มต้น : ";
            this.toolStripMyLabel1.Size = new System.Drawing.Size(120, 22);
            this.toolStripMyLabel1.Text = "หมายเลขคูปองเริ่มต้น : ";
            // 
            // _formatTextbox
            // 
            this._formatTextbox.Name = "_formatTextbox";
            this._formatTextbox.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripMyLabel2
            // 
            this.toolStripMyLabel2.Name = "toolStripMyLabel2";
            this.toolStripMyLabel2.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyLabel2.ResourceName = "จำนวน";
            this.toolStripMyLabel2.Size = new System.Drawing.Size(42, 22);
            this.toolStripMyLabel2.Text = "จำนวน";
            // 
            // _amountTextbox
            // 
            this._amountTextbox.Name = "_amountTextbox";
            this._amountTextbox.Size = new System.Drawing.Size(80, 25);
            // 
            // _couponGenGrid
            // 
            this._couponGenGrid._extraWordShow = true;
            this._couponGenGrid._selectRow = -1;
            this._couponGenGrid.BackColor = System.Drawing.SystemColors.Window;
            this._couponGenGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._couponGenGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._couponGenGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._couponGenGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._couponGenGrid.Location = new System.Drawing.Point(0, 60);
            this._couponGenGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._couponGenGrid.Name = "_couponGenGrid";
            this._couponGenGrid.Size = new System.Drawing.Size(655, 686);
            this._couponGenGrid.TabIndex = 4;
            this._couponGenGrid.TabStop = false;
            // 
            // _genButton
            // 
            this._genButton.Image = global::SMLERPControl.Properties.Resources.magic_wand;
            this._genButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._genButton.Name = "_genButton";
            this._genButton.Padding = new System.Windows.Forms.Padding(1);
            this._genButton.ResourceName = "";
            this._genButton.Size = new System.Drawing.Size(76, 22);
            this._genButton.Text = "Generate";
            this._genButton.Click += new System.EventHandler(this._genButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::SMLERPControl.Properties.Resources.brush3;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Padding = new System.Windows.Forms.Padding(1);
            this._clearButton.ResourceName = "";
            this._clearButton.Size = new System.Drawing.Size(81, 22);
            this._clearButton.Text = "ล้างรายการ";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(94, 22);
            this._saveButton.Text = "บันทึก (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "ปิดจอ";
            // 
            // _couponGenerateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._couponGenGrid);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._screenTop);
            this.Controls.Add(this._myToolBar);
            this.Name = "_couponGenerateControl";
            this.Size = new System.Drawing.Size(655, 746);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip _myToolBar;
        public MyLib._myScreen _screenTop;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib._myGrid _couponGenGrid;
        public MyLib.ToolStripMyLabel toolStripMyLabel1;
        public System.Windows.Forms.ToolStripTextBox _formatTextbox;
        public MyLib.ToolStripMyLabel toolStripMyLabel2;
        public System.Windows.Forms.ToolStripTextBox _amountTextbox;
        public MyLib.ToolStripMyButton _genButton;
        public MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        private MyLib.ToolStripMyButton _clearButton;
    }
}
