namespace SMLERPAR
{
    partial class _ar_label_print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_ar_label_print));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._itemList = new MyLib._myDataList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this._topMarginTextBox = new System.Windows.Forms.TextBox();
            this._leftMarginTextBox = new System.Windows.Forms.TextBox();
            this._labelHeightTextBox = new System.Windows.Forms.TextBox();
            this._labelWidthTextBox = new System.Windows.Forms.TextBox();
            this._maxColumnTextBox = new System.Windows.Forms.TextBox();
            this._maxRowTextBox = new System.Windows.Forms.TextBox();
            this._startColumnTextBox = new System.Windows.Forms.TextBox();
            this._startRowTextBox = new System.Windows.Forms.TextBox();
            this._printerComboBox = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._previewButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this._resetButton = new MyLib.ToolStripMyButton();
            this._selectedGid = new MyLib._myGrid();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._itemList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._selectedGid);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(1396, 809);
            this.splitContainer1.SplitterDistance = 655;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // _itemList
            // 
            this._itemList._extraWhere = "";
            this._itemList._multiSelect = false;
            this._itemList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._itemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemList.Location = new System.Drawing.Point(0, 0);
            this._itemList.Margin = new System.Windows.Forms.Padding(0);
            this._itemList.Name = "_itemList";
            this._itemList.Size = new System.Drawing.Size(655, 809);
            this._itemList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this._printerComboBox);
            this.panel1.Controls.Add(this._startRowTextBox);
            this.panel1.Controls.Add(this._startColumnTextBox);
            this.panel1.Controls.Add(this._maxRowTextBox);
            this.panel1.Controls.Add(this._maxColumnTextBox);
            this.panel1.Controls.Add(this._labelWidthTextBox);
            this.panel1.Controls.Add(this._labelHeightTextBox);
            this.panel1.Controls.Add(this._leftMarginTextBox);
            this.panel1.Controls.Add(this._topMarginTextBox);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 158);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ตำแหน่งเริ่มต้นแนวตั้ง";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ชื่อเครื่องพิมพ์";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "ตำแหน่งเริ่มต้นแนวนอน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "จำนวนแถวตั้ง";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "จำนวนแถวนอน";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "ความกว้างของป้าย";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(241, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "ความสูงของป้าย";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "จากขอบด้านบน";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(238, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "จากขอบด้านซ้าย";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(93, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 9;
            this.label10.Text = "หน่วย";
            // 
            // _topMarginTextBox
            // 
            this._topMarginTextBox.Location = new System.Drawing.Point(134, 27);
            this._topMarginTextBox.Name = "_topMarginTextBox";
            this._topMarginTextBox.Size = new System.Drawing.Size(60, 22);
            this._topMarginTextBox.TabIndex = 11;
            this._topMarginTextBox.Text = "0.50";
            // 
            // _leftMarginTextBox
            // 
            this._leftMarginTextBox.Location = new System.Drawing.Point(334, 27);
            this._leftMarginTextBox.Name = "_leftMarginTextBox";
            this._leftMarginTextBox.Size = new System.Drawing.Size(60, 22);
            this._leftMarginTextBox.TabIndex = 12;
            this._leftMarginTextBox.Text = "0.02";
            // 
            // _labelHeightTextBox
            // 
            this._labelHeightTextBox.Location = new System.Drawing.Point(334, 51);
            this._labelHeightTextBox.Name = "_labelHeightTextBox";
            this._labelHeightTextBox.Size = new System.Drawing.Size(60, 22);
            this._labelHeightTextBox.TabIndex = 13;
            this._labelHeightTextBox.Text = "2.00";
            // 
            // _labelWidthTextBox
            // 
            this._labelWidthTextBox.Location = new System.Drawing.Point(134, 51);
            this._labelWidthTextBox.Name = "_labelWidthTextBox";
            this._labelWidthTextBox.Size = new System.Drawing.Size(60, 22);
            this._labelWidthTextBox.TabIndex = 14;
            this._labelWidthTextBox.Text = "4.00";
            // 
            // _maxColumnTextBox
            // 
            this._maxColumnTextBox.Location = new System.Drawing.Point(334, 75);
            this._maxColumnTextBox.Name = "_maxColumnTextBox";
            this._maxColumnTextBox.Size = new System.Drawing.Size(60, 22);
            this._maxColumnTextBox.TabIndex = 15;
            this._maxColumnTextBox.Text = "5";
            // 
            // _maxRowTextBox
            // 
            this._maxRowTextBox.Location = new System.Drawing.Point(134, 75);
            this._maxRowTextBox.Name = "_maxRowTextBox";
            this._maxRowTextBox.Size = new System.Drawing.Size(60, 22);
            this._maxRowTextBox.TabIndex = 16;
            this._maxRowTextBox.Text = "14";
            // 
            // _startColumnTextBox
            // 
            this._startColumnTextBox.Location = new System.Drawing.Point(334, 99);
            this._startColumnTextBox.Name = "_startColumnTextBox";
            this._startColumnTextBox.Size = new System.Drawing.Size(60, 22);
            this._startColumnTextBox.TabIndex = 17;
            this._startColumnTextBox.Text = "1";
            // 
            // _startRowTextBox
            // 
            this._startRowTextBox.Location = new System.Drawing.Point(134, 99);
            this._startRowTextBox.Name = "_startRowTextBox";
            this._startRowTextBox.Size = new System.Drawing.Size(60, 22);
            this._startRowTextBox.TabIndex = 18;
            this._startRowTextBox.Text = "1";
            // 
            // _printerComboBox
            // 
            this._printerComboBox.FormattingEnabled = true;
            this._printerComboBox.Location = new System.Drawing.Point(134, 123);
            this._printerComboBox.Name = "_printerComboBox";
            this._printerComboBox.Size = new System.Drawing.Size(322, 22);
            this._printerComboBox.TabIndex = 19;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(134, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(86, 18);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Centimeter";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(232, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(49, 18);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.Text = "Inch";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1396, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(77, 22);
            this.toolStripButton1.Text = "เลือกทั้งหด";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton,
            this._printButton,
            this._resetButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(736, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _previewButton
            // 
            this._previewButton.Image = ((System.Drawing.Image)(resources.GetObject("_previewButton.Image")));
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Padding = new System.Windows.Forms.Padding(1);
            this._previewButton.ResourceName = "";
            this._previewButton.Size = new System.Drawing.Size(91, 22);
            this._previewButton.Text = "แสดงตัวอย่าง";
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _printButton
            // 
            this._printButton.Image = ((System.Drawing.Image)(resources.GetObject("_printButton.Image")));
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "";
            this._printButton.Size = new System.Drawing.Size(70, 22);
            this._printButton.Text = "เริ่มพิมพ์";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _resetButton
            // 
            this._resetButton.Image = ((System.Drawing.Image)(resources.GetObject("_resetButton.Image")));
            this._resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._resetButton.Name = "_resetButton";
            this._resetButton.Padding = new System.Windows.Forms.Padding(1);
            this._resetButton.ResourceName = "";
            this._resetButton.Size = new System.Drawing.Size(100, 22);
            this._resetButton.Text = "เริ่มใหม่ทั้งหมด";
            this._resetButton.Click += new System.EventHandler(this._resetButton_Click);
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
            this._selectedGid.Location = new System.Drawing.Point(0, 183);
            this._selectedGid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._selectedGid.Name = "_selectedGid";
            this._selectedGid.Size = new System.Drawing.Size(736, 626);
            this._selectedGid.TabIndex = 2;
            this._selectedGid.TabStop = false;
            // 
            // _ar_label_print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_ar_label_print";
            this.Size = new System.Drawing.Size(1396, 834);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myDataList _itemList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _printerComboBox;
        private System.Windows.Forms.TextBox _startRowTextBox;
        private System.Windows.Forms.TextBox _startColumnTextBox;
        private System.Windows.Forms.TextBox _maxRowTextBox;
        private System.Windows.Forms.TextBox _maxColumnTextBox;
        private System.Windows.Forms.TextBox _labelWidthTextBox;
        private System.Windows.Forms.TextBox _labelHeightTextBox;
        private System.Windows.Forms.TextBox _leftMarginTextBox;
        private System.Windows.Forms.TextBox _topMarginTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private MyLib.ToolStripMyButton _previewButton;
        private MyLib.ToolStripMyButton _printButton;
        private MyLib.ToolStripMyButton _resetButton;
        private MyLib._myGrid _selectedGid;
    }
}
