namespace SMLBarcodeManage
{
    partial class _itemBarcodeCheckerEan13
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._processButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new MyLib.ToolStripMyLabel();
            this._startDigitTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._changeButton = new MyLib.ToolStripMyButton();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._selectedGid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this._startDigitTextBox,
            this._changeButton,
            this._saveButton,
            this.toolStripSeparator2,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(904, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLBarcodeManage.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Padding = new System.Windows.Forms.Padding(1);
            this._processButton.ResourceName = "เริ่มตรวจสอบ";
            this._processButton.Size = new System.Drawing.Size(86, 22);
            this._processButton.Text = "เริ่มตรวจสอบ";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripLabel1.ResourceName = "ตัวเลขนำหน้าบาร์โค๊ดใหม่";
            this.toolStripLabel1.Size = new System.Drawing.Size(121, 22);
            this.toolStripLabel1.Text = "ตัวเลขนำหน้าบาร์โค๊ดใหม่";
            // 
            // _startDigitTextBox
            // 
            this._startDigitTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._startDigitTextBox.Name = "_startDigitTextBox";
            this._startDigitTextBox.Size = new System.Drawing.Size(100, 25);
            this._startDigitTextBox.Text = "2";
            // 
            // _changeButton
            // 
            this._changeButton.Image = global::SMLBarcodeManage.Properties.Resources.bell;
            this._changeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._changeButton.Name = "_changeButton";
            this._changeButton.Padding = new System.Windows.Forms.Padding(1);
            this._changeButton.ResourceName = "สร้างบาร์โค๊ดทดแทนทั้งหมด";
            this._changeButton.Size = new System.Drawing.Size(153, 22);
            this._changeButton.Text = "สร้างบาร์โค๊ดทดแทนทั้งหมด";
            this._changeButton.Click += new System.EventHandler(this._changeButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLBarcodeManage.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "นำบาร์โค๊ดใหม่ไปใช้แทนบาร์โค๊ดที่ผิดพลาด";
            this._saveButton.Size = new System.Drawing.Size(221, 22);
            this._saveButton.Text = "นำบาร์โค๊ดใหม่ไปใช้แทนบาร์โค๊ดที่ผิดพลาด";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLBarcodeManage.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "Close";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
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
            this._selectedGid.Size = new System.Drawing.Size(904, 549);
            this._selectedGid.TabIndex = 1;
            this._selectedGid.TabStop = false;
            // 
            // _itemBarcodeCheckerEan13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._selectedGid);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_itemBarcodeCheckerEan13";
            this.Size = new System.Drawing.Size(904, 574);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _processButton;
        private MyLib.ToolStripMyButton _changeButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myGrid _selectedGid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private MyLib.ToolStripMyLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox _startDigitTextBox;
    }
}
