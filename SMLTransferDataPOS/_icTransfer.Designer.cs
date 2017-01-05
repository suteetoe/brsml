namespace SMLTransferDataPOS
{
    partial class _icTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icTransfer));
            this._processButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._processToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._resultTextBox = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._myGrid1 = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._removeAllButton = new System.Windows.Forms.ToolStripButton();
            this._selectAllButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._fullTransferResultCheckbox = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _processButton
            // 
            this._processButton.Location = new System.Drawing.Point(196, 93);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(75, 23);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "Process";
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processToolStripButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(557, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _processToolStripButton
            // 
            this._processToolStripButton.Image = global::SMLTransferDataPOS.Properties.Resources.flash1;
            this._processToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processToolStripButton.Name = "_processToolStripButton";
            this._processToolStripButton.Size = new System.Drawing.Size(70, 22);
            this._processToolStripButton.Text = "Process ";
            this._processToolStripButton.Click += new System.EventHandler(this._processToolStripButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLTransferDataPOS.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextBox.Location = new System.Drawing.Point(3, 3);
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.ReadOnly = true;
            this._resultTextBox.Size = new System.Drawing.Size(543, 527);
            this._resultTextBox.TabIndex = 2;
            this._resultTextBox.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(557, 559);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._resultTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(549, 533);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._myGrid1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(549, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Table Transfer Option";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _myGrid1
            // 
            this._myGrid1._extraWordShow = true;
            this._myGrid1._selectRow = -1;
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(3, 64);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(543, 466);
            this._myGrid1.TabIndex = 0;
            this._myGrid1.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._removeAllButton,
            this._selectAllButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(543, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _removeAllButton
            // 
            this._removeAllButton.Image = ((System.Drawing.Image)(resources.GetObject("_removeAllButton.Image")));
            this._removeAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._removeAllButton.Name = "_removeAllButton";
            this._removeAllButton.Size = new System.Drawing.Size(87, 22);
            this._removeAllButton.Text = "Remove All";
            this._removeAllButton.Click += new System.EventHandler(this._removeAllButton_Click);
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = ((System.Drawing.Image)(resources.GetObject("_selectAllButton.Image")));
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(75, 22);
            this._selectAllButton.Text = "Select All";
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._fullTransferResultCheckbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 36);
            this.panel1.TabIndex = 2;
            // 
            // _fullTransferResultCheckbox
            // 
            this._fullTransferResultCheckbox.AutoSize = true;
            this._fullTransferResultCheckbox.Location = new System.Drawing.Point(17, 12);
            this._fullTransferResultCheckbox.Name = "_fullTransferResultCheckbox";
            this._fullTransferResultCheckbox.Size = new System.Drawing.Size(217, 17);
            this._fullTransferResultCheckbox.TabIndex = 0;
            this._fullTransferResultCheckbox.Text = "แสดงข้อมูลการถ่ายโอนข้อมูลอย่างละเอียด";
            this._fullTransferResultCheckbox.UseVisualStyleBackColor = true;
            // 
            // _icTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._processButton);
            this.Name = "_icTransfer";
            this.Size = new System.Drawing.Size(557, 584);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _processToolStripButton;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.RichTextBox _resultTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MyLib._myGrid _myGrid1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _removeAllButton;
        private System.Windows.Forms.ToolStripButton _selectAllButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox _fullTransferResultCheckbox;
    }
}
