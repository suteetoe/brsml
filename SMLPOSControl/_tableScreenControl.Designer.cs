namespace SMLPOSControl
{
    partial class _tableScreenControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_tableScreenControl));
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._panel = new MyLib._myPanel();
            this._myScreen1 = new MyLib._myScreen();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._qrCodeGenButton = new System.Windows.Forms.ToolStripButton();
            this._qrCodePrintButton = new System.Windows.Forms.ToolStripButton();
            this._barcodePrintButton = new System.Windows.Forms.ToolStripButton();
            this._printDocument = new System.Drawing.Printing.PrintDocument();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this._printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this._qrCodeLocalButton = new System.Windows.Forms.ToolStripButton();
            this._panel.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(73, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(111, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _panel
            // 
            this._panel._switchTabAuto = false;
            this._panel.BackColor = System.Drawing.Color.Transparent;
            this._panel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Controls.Add(this._myScreen1);
            this._panel.CornerPicture = null;
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(831, 504);
            this._panel.TabIndex = 2;
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myScreen1.Location = new System.Drawing.Point(0, 0);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(831, 504);
            this._myScreen1.TabIndex = 0;
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton,
            this.toolStripSeparator1,
            this._qrCodeGenButton,
            this._qrCodePrintButton,
            this._barcodePrintButton,
            this._qrCodeLocalButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(831, 25);
            this._toolStrip.TabIndex = 3;
            this._toolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _qrCodeGenButton
            // 
            this._qrCodeGenButton.Image = ((System.Drawing.Image)(resources.GetObject("_qrCodeGenButton.Image")));
            this._qrCodeGenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._qrCodeGenButton.Name = "_qrCodeGenButton";
            this._qrCodeGenButton.Size = new System.Drawing.Size(152, 22);
            this._qrCodeGenButton.Text = "สร้างรหัส QR Code ทุกโต๊ะ";
            // 
            // _qrCodePrintButton
            // 
            this._qrCodePrintButton.Image = ((System.Drawing.Image)(resources.GetObject("_qrCodePrintButton.Image")));
            this._qrCodePrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._qrCodePrintButton.Name = "_qrCodePrintButton";
            this._qrCodePrintButton.Size = new System.Drawing.Size(134, 22);
            this._qrCodePrintButton.Text = "พิมพ์ QR Code ทุกโต๊ะ";
            this._qrCodePrintButton.Click += new System.EventHandler(this._qrCodePrintButton_Click_1);
            // 
            // _barcodePrintButton
            // 
            this._barcodePrintButton.Image = ((System.Drawing.Image)(resources.GetObject("_barcodePrintButton.Image")));
            this._barcodePrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._barcodePrintButton.Name = "_barcodePrintButton";
            this._barcodePrintButton.Size = new System.Drawing.Size(130, 22);
            this._barcodePrintButton.Text = "พิมพ์ Barcode ทุกโต๊ะ";
            this._barcodePrintButton.ToolTipText = "พิมพ์ Barcode ทุกโต๊ะ";
            this._barcodePrintButton.Click += new System.EventHandler(this._barcodePrintButton_Click);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // _printPreviewDialog
            // 
            this._printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this._printPreviewDialog.Enabled = true;
            this._printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("_printPreviewDialog.Icon")));
            this._printPreviewDialog.Name = "_printPreviewDialog";
            this._printPreviewDialog.Visible = false;
            // 
            // _qrCodeLocalButton
            // 
            this._qrCodeLocalButton.Image = ((System.Drawing.Image)(resources.GetObject("_qrCodeLocalButton.Image")));
            this._qrCodeLocalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._qrCodeLocalButton.Name = "_qrCodeLocalButton";
            this._qrCodeLocalButton.Size = new System.Drawing.Size(136, 22);
            this._qrCodeLocalButton.Text = "พิมพ์ QR Code ท้องถิ่น";
            this._qrCodeLocalButton.Click += new System.EventHandler(this._qrCodeLocalButton_Click);
            // 
            // _tableScreenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_tableScreenControl";
            this.Size = new System.Drawing.Size(831, 529);
            this._panel.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStripButton _closeButton;
        public System.Windows.Forms.ToolStripButton _saveButton;
        public MyLib._myPanel _panel;
        public System.Windows.Forms.ToolStrip _toolStrip;
        public MyLib._myScreen _myScreen1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _qrCodeGenButton;
        private System.Windows.Forms.ToolStripButton _qrCodePrintButton;
        private System.Drawing.Printing.PrintDocument _printDocument;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.PrintPreviewDialog _printPreviewDialog;
        private System.Windows.Forms.ToolStripButton _barcodePrintButton;
        private System.Windows.Forms.ToolStripButton _qrCodeLocalButton;
    }
}
