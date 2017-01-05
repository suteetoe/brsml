namespace SMLEDIControl
{
    partial class _ediExternalScreenControl
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
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._myPanel = new MyLib._myPanel();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_product = new System.Windows.Forms.TabPage();
            this._gridProduct = new MyLib._myGrid();
            this.tab_barcode = new System.Windows.Forms.TabPage();
            this._gridBarcode = new MyLib._myGrid();
            this.tab_unitcode = new System.Windows.Forms.TabPage();
            this._gridUnit = new MyLib._myGrid();
            this._screenTop = new MyLib._myScreen();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.tab_customer = new System.Windows.Forms.TabPage();
            this._gridCustomer = new MyLib._myGrid();
            this._myToolbar.SuspendLayout();
            this._myPanel.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_product.SuspendLayout();
            this.tab_barcode.SuspendLayout();
            this.tab_unitcode.SuspendLayout();
            this.tab_customer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(526, 25);
            this._myToolbar.TabIndex = 0;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Controls.Add(this._myTabControl1);
            this._myPanel.Controls.Add(this._screenTop);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel.Size = new System.Drawing.Size(526, 635);
            this._myPanel.TabIndex = 1;
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_product);
            this._myTabControl1.Controls.Add(this.tab_barcode);
            this._myTabControl1.Controls.Add(this.tab_unitcode);
            this._myTabControl1.Controls.Add(this.tab_customer);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(3, 14);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(520, 618);
            this._myTabControl1.TabIndex = 2;
            this._myTabControl1.TableName = "edi_external";
            // 
            // tab_product
            // 
            this.tab_product.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_product.Controls.Add(this._gridProduct);
            this.tab_product.Location = new System.Drawing.Point(4, 23);
            this.tab_product.Name = "tab_product";
            this.tab_product.Size = new System.Drawing.Size(512, 591);
            this.tab_product.TabIndex = 0;
            this.tab_product.Text = "tab_product";
            this.tab_product.UseVisualStyleBackColor = true;
            // 
            // _gridProduct
            // 
            this._gridProduct._extraWordShow = true;
            this._gridProduct._selectRow = -1;
            this._gridProduct.BackColor = System.Drawing.SystemColors.Window;
            this._gridProduct.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridProduct.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridProduct.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridProduct.Location = new System.Drawing.Point(0, 0);
            this._gridProduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridProduct.Name = "_gridProduct";
            this._gridProduct.Size = new System.Drawing.Size(510, 589);
            this._gridProduct.TabIndex = 0;
            this._gridProduct.TabStop = false;
            // 
            // tab_barcode
            // 
            this.tab_barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_barcode.Controls.Add(this._gridBarcode);
            this.tab_barcode.Location = new System.Drawing.Point(4, 23);
            this.tab_barcode.Name = "tab_barcode";
            this.tab_barcode.Size = new System.Drawing.Size(512, 591);
            this.tab_barcode.TabIndex = 1;
            this.tab_barcode.Text = "tab_barcode";
            this.tab_barcode.UseVisualStyleBackColor = true;
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
            this._gridBarcode.Size = new System.Drawing.Size(510, 589);
            this._gridBarcode.TabIndex = 1;
            this._gridBarcode.TabStop = false;
            // 
            // tab_unitcode
            // 
            this.tab_unitcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_unitcode.Controls.Add(this._gridUnit);
            this.tab_unitcode.Location = new System.Drawing.Point(4, 23);
            this.tab_unitcode.Name = "tab_unitcode";
            this.tab_unitcode.Size = new System.Drawing.Size(512, 591);
            this.tab_unitcode.TabIndex = 2;
            this.tab_unitcode.Text = "tab_unitcode";
            this.tab_unitcode.UseVisualStyleBackColor = true;
            // 
            // _gridUnit
            // 
            this._gridUnit._extraWordShow = true;
            this._gridUnit._selectRow = -1;
            this._gridUnit.BackColor = System.Drawing.SystemColors.Window;
            this._gridUnit.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridUnit.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridUnit.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridUnit.Location = new System.Drawing.Point(0, 0);
            this._gridUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridUnit.Name = "_gridUnit";
            this._gridUnit.Size = new System.Drawing.Size(510, 589);
            this._gridUnit.TabIndex = 1;
            this._gridUnit.TabStop = false;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(3, 3);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(520, 11);
            this._screenTop.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLEDIControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทึก";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLEDIControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            // 
            // tab_customer
            // 
            this.tab_customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_customer.Controls.Add(this._gridCustomer);
            this.tab_customer.Location = new System.Drawing.Point(4, 23);
            this.tab_customer.Name = "tab_customer";
            this.tab_customer.Size = new System.Drawing.Size(512, 591);
            this.tab_customer.TabIndex = 3;
            this.tab_customer.Text = "tab_customer";
            this.tab_customer.UseVisualStyleBackColor = true;
            // 
            // _gridCustomer
            // 
            this._gridCustomer._extraWordShow = true;
            this._gridCustomer._selectRow = -1;
            this._gridCustomer.BackColor = System.Drawing.SystemColors.Window;
            this._gridCustomer.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridCustomer.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCustomer.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridCustomer.Location = new System.Drawing.Point(0, 0);
            this._gridCustomer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridCustomer.Name = "_gridCustomer";
            this._gridCustomer.Size = new System.Drawing.Size(510, 589);
            this._gridCustomer.TabIndex = 2;
            this._gridCustomer.TabStop = false;
            // 
            // _ediExternalScreenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel);
            this.Controls.Add(this._myToolbar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ediExternalScreenControl";
            this.Size = new System.Drawing.Size(526, 660);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this._myTabControl1.ResumeLayout(false);
            this.tab_product.ResumeLayout(false);
            this.tab_barcode.ResumeLayout(false);
            this.tab_unitcode.ResumeLayout(false);
            this.tab_customer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_product;
        private System.Windows.Forms.TabPage tab_barcode;
        private System.Windows.Forms.TabPage tab_unitcode;
        public System.Windows.Forms.ToolStrip _myToolbar;
        public MyLib._myScreen _screenTop;
        public MyLib._myGrid _gridProduct;
        public MyLib._myGrid _gridBarcode;
        public MyLib._myGrid _gridUnit;
        public MyLib._myPanel _myPanel;
        private System.Windows.Forms.TabPage tab_customer;
        public MyLib._myGrid _gridCustomer;
    }
}
