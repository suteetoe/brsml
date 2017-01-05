namespace SMLERPReportTool
{
    partial class _formPrintOption
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this._previewPrintCheck = new System.Windows.Forms.CheckBox();
            this._printCheck = new System.Windows.Forms.CheckBox();
            this._showagainCheck = new System.Windows.Forms.CheckBox();
            this._printerCombo = new MyLib._myComboBox();
            this._myPanel1 = new MyLib._myPanel();
            this._myPanel4 = new System.Windows.Forms.Panel();
            this._myPanel6 = new System.Windows.Forms.Panel();
            this._myScreen1 = new SMLERPReportTool._selectFormScreen();
            this._myPanel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._myPanel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this._myPanel2 = new System.Windows.Forms.Panel();
            this._printRangeGroupbox = new MyLib._myGroupBox();
            this._panelIncludeDoc = new System.Windows.Forms.Panel();
            this._includeDocSeriesCheckbox = new System.Windows.Forms.CheckBox();
            this._panelDocSeries = new System.Windows.Forms.Panel();
            this._printDocSeriesNoTextBox = new System.Windows.Forms.TextBox();
            this._someSeriesRadio = new System.Windows.Forms.RadioButton();
            this._allSeriesRadio = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this._printRangePageTextbox = new System.Windows.Forms.TextBox();
            this._printSomePageRadio = new System.Windows.Forms.RadioButton();
            this._printAllPageRadio = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._cancelButton = new System.Windows.Forms.Button();
            this._processButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._myPanel1.SuspendLayout();
            this._myPanel4.SuspendLayout();
            this._myPanel6.SuspendLayout();
            this._myPanel5.SuspendLayout();
            this._myPanel3.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._printRangeGroupbox.SuspendLayout();
            this._panelIncludeDoc.SuspendLayout();
            this._panelDocSeries.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer : ";
            // 
            // _previewPrintCheck
            // 
            this._previewPrintCheck.AutoSize = true;
            this._previewPrintCheck.BackColor = System.Drawing.Color.Transparent;
            this._previewPrintCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._previewPrintCheck.Location = new System.Drawing.Point(72, 3);
            this._previewPrintCheck.Name = "_previewPrintCheck";
            this._previewPrintCheck.Size = new System.Drawing.Size(134, 18);
            this._previewPrintCheck.TabIndex = 2;
            this._previewPrintCheck.Text = "แสดงตัวอย่างก่อนพิมพ์";
            this._previewPrintCheck.UseVisualStyleBackColor = false;
            // 
            // _printCheck
            // 
            this._printCheck.AutoSize = true;
            this._printCheck.BackColor = System.Drawing.Color.Transparent;
            this._printCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._printCheck.Location = new System.Drawing.Point(72, 25);
            this._printCheck.Name = "_printCheck";
            this._printCheck.Size = new System.Drawing.Size(156, 18);
            this._printCheck.TabIndex = 3;
            this._printCheck.Text = "พิมพ์เอกสารไปที่เครื่องพิมพ์";
            this._printCheck.UseVisualStyleBackColor = false;
            // 
            // _showagainCheck
            // 
            this._showagainCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._showagainCheck.AutoSize = true;
            this._showagainCheck.BackColor = System.Drawing.Color.Transparent;
            this._showagainCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._showagainCheck.Location = new System.Drawing.Point(6, 210);
            this._showagainCheck.Name = "_showagainCheck";
            this._showagainCheck.Size = new System.Drawing.Size(124, 18);
            this._showagainCheck.TabIndex = 5;
            this._showagainCheck.Text = "ไม่ต้องการให้ถามอีก";
            this._showagainCheck.UseVisualStyleBackColor = false;
            // 
            // _printerCombo
            // 
            this._printerCombo._isQuery = true;
            this._printerCombo._maxColumn = 1;
            this._printerCombo.BackColor = System.Drawing.Color.White;
            this._printerCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._printerCombo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._printerCombo.FormattingEnabled = true;
            this._printerCombo.Location = new System.Drawing.Point(72, 6);
            this._printerCombo.Name = "_printerCombo";
            this._printerCombo.Size = new System.Drawing.Size(315, 22);
            this._printerCombo.TabIndex = 6;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myPanel4);
            this._myPanel1.Controls.Add(this._myPanel3);
            this._myPanel1.Controls.Add(this._myPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(466, 288);
            this._myPanel1.TabIndex = 11;
            // 
            // _myPanel4
            // 
            this._myPanel4.AutoSize = true;
            this._myPanel4.BackColor = System.Drawing.Color.Transparent;
            this._myPanel4.Controls.Add(this._myPanel6);
            this._myPanel4.Controls.Add(this._myPanel5);
            this._myPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel4.Location = new System.Drawing.Point(0, 33);
            this._myPanel4.Name = "_myPanel4";
            this._myPanel4.Size = new System.Drawing.Size(466, 20);
            this._myPanel4.TabIndex = 2;
            // 
            // _myPanel6
            // 
            this._myPanel6.AutoSize = true;
            this._myPanel6.Controls.Add(this._myScreen1);
            this._myPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel6.Location = new System.Drawing.Point(72, 0);
            this._myPanel6.Name = "_myPanel6";
            this._myPanel6.Size = new System.Drawing.Size(394, 20);
            this._myPanel6.TabIndex = 1;
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Location = new System.Drawing.Point(0, 0);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(340, 1);
            this._myScreen1.TabIndex = 0;
            // 
            // _myPanel5
            // 
            this._myPanel5.BackColor = System.Drawing.Color.Transparent;
            this._myPanel5.Controls.Add(this.label2);
            this._myPanel5.Dock = System.Windows.Forms.DockStyle.Left;
            this._myPanel5.Location = new System.Drawing.Point(0, 0);
            this._myPanel5.Name = "_myPanel5";
            this._myPanel5.Size = new System.Drawing.Size(72, 20);
            this._myPanel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ฟอร์ม : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _myPanel3
            // 
            this._myPanel3.BackColor = System.Drawing.Color.Transparent;
            this._myPanel3.Controls.Add(this.button1);
            this._myPanel3.Controls.Add(this._printerCombo);
            this._myPanel3.Controls.Add(this.label1);
            this._myPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel3.Location = new System.Drawing.Point(0, 0);
            this._myPanel3.Name = "_myPanel3";
            this._myPanel3.Size = new System.Drawing.Size(466, 33);
            this._myPanel3.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Image = global::SMLERPReportTool.Properties.Resources.refresh;
            this.button1.Location = new System.Drawing.Point(392, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _myPanel2
            // 
            this._myPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myPanel2.Controls.Add(this._printRangeGroupbox);
            this._myPanel2.Controls.Add(this._showagainCheck);
            this._myPanel2.Controls.Add(this.pictureBox1);
            this._myPanel2.Controls.Add(this._cancelButton);
            this._myPanel2.Controls.Add(this._processButton);
            this._myPanel2.Controls.Add(this._printCheck);
            this._myPanel2.Controls.Add(this._previewPrintCheck);
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel2.Location = new System.Drawing.Point(0, 53);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Size = new System.Drawing.Size(466, 235);
            this._myPanel2.TabIndex = 0;
            // 
            // _printRangeGroupbox
            // 
            this._printRangeGroupbox.Controls.Add(this._panelIncludeDoc);
            this._printRangeGroupbox.Controls.Add(this.panel2);
            this._printRangeGroupbox.Font = new System.Drawing.Font("Tahoma", 9F);
            this._printRangeGroupbox.Location = new System.Drawing.Point(72, 49);
            this._printRangeGroupbox.Name = "_printRangeGroupbox";
            this._printRangeGroupbox.ResourceName = "";
            this._printRangeGroupbox.Size = new System.Drawing.Size(384, 149);
            this._printRangeGroupbox.TabIndex = 11;
            this._printRangeGroupbox.TabStop = false;
            this._printRangeGroupbox.Text = "ขอบเขตการพิมพ์";
            // 
            // _panelIncludeDoc
            // 
            this._panelIncludeDoc.Controls.Add(this._includeDocSeriesCheckbox);
            this._panelIncludeDoc.Controls.Add(this._panelDocSeries);
            this._panelIncludeDoc.Enabled = false;
            this._panelIncludeDoc.Location = new System.Drawing.Point(10, 70);
            this._panelIncludeDoc.Name = "_panelIncludeDoc";
            this._panelIncludeDoc.Size = new System.Drawing.Size(372, 78);
            this._panelIncludeDoc.TabIndex = 10;
            // 
            // _includeDocSeriesCheckbox
            // 
            this._includeDocSeriesCheckbox.AutoSize = true;
            this._includeDocSeriesCheckbox.Checked = true;
            this._includeDocSeriesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._includeDocSeriesCheckbox.Location = new System.Drawing.Point(8, 0);
            this._includeDocSeriesCheckbox.Name = "_includeDocSeriesCheckbox";
            this._includeDocSeriesCheckbox.Size = new System.Drawing.Size(171, 18);
            this._includeDocSeriesCheckbox.TabIndex = 4;
            this._includeDocSeriesCheckbox.Text = "รวมไปถึงเอกสารที่ออกเป็นชุด";
            this._includeDocSeriesCheckbox.UseVisualStyleBackColor = true;
            this._includeDocSeriesCheckbox.CheckedChanged += new System.EventHandler(this._includeDocSeriesCheckbox_CheckedChanged);
            // 
            // _panelDocSeries
            // 
            this._panelDocSeries.Controls.Add(this._printDocSeriesNoTextBox);
            this._panelDocSeries.Controls.Add(this._someSeriesRadio);
            this._panelDocSeries.Controls.Add(this._allSeriesRadio);
            this._panelDocSeries.Location = new System.Drawing.Point(19, 22);
            this._panelDocSeries.Name = "_panelDocSeries";
            this._panelDocSeries.Size = new System.Drawing.Size(350, 51);
            this._panelDocSeries.TabIndex = 8;
            // 
            // _printDocSeriesNoTextBox
            // 
            this._printDocSeriesNoTextBox.Enabled = false;
            this._printDocSeriesNoTextBox.Location = new System.Drawing.Point(128, 26);
            this._printDocSeriesNoTextBox.Name = "_printDocSeriesNoTextBox";
            this._printDocSeriesNoTextBox.Size = new System.Drawing.Size(216, 22);
            this._printDocSeriesNoTextBox.TabIndex = 8;
            // 
            // _someSeriesRadio
            // 
            this._someSeriesRadio.AutoSize = true;
            this._someSeriesRadio.Font = new System.Drawing.Font("Tahoma", 9F);
            this._someSeriesRadio.Location = new System.Drawing.Point(8, 27);
            this._someSeriesRadio.Name = "_someSeriesRadio";
            this._someSeriesRadio.Size = new System.Drawing.Size(114, 18);
            this._someSeriesRadio.TabIndex = 7;
            this._someSeriesRadio.Text = "เฉพาะเอกสารชุดที่";
            this._someSeriesRadio.UseVisualStyleBackColor = true;
            this._someSeriesRadio.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // _allSeriesRadio
            // 
            this._allSeriesRadio.AutoSize = true;
            this._allSeriesRadio.Checked = true;
            this._allSeriesRadio.Font = new System.Drawing.Font("Tahoma", 9F);
            this._allSeriesRadio.Location = new System.Drawing.Point(8, 4);
            this._allSeriesRadio.Name = "_allSeriesRadio";
            this._allSeriesRadio.Size = new System.Drawing.Size(92, 18);
            this._allSeriesRadio.TabIndex = 6;
            this._allSeriesRadio.TabStop = true;
            this._allSeriesRadio.Text = "ทุกชุดเอกสาร";
            this._allSeriesRadio.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this._printRangePageTextbox);
            this.panel2.Controls.Add(this._printSomePageRadio);
            this.panel2.Controls.Add(this._printAllPageRadio);
            this.panel2.Location = new System.Drawing.Point(8, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(370, 50);
            this.panel2.TabIndex = 9;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = global::SMLERPReportTool.Properties.Resources.information;
            this.pictureBox2.Location = new System.Drawing.Point(349, 26);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // _printRangePageTextbox
            // 
            this._printRangePageTextbox.Enabled = false;
            this._printRangePageTextbox.Location = new System.Drawing.Point(105, 23);
            this._printRangePageTextbox.Name = "_printRangePageTextbox";
            this._printRangePageTextbox.Size = new System.Drawing.Size(241, 22);
            this._printRangePageTextbox.TabIndex = 2;
            // 
            // _printSomePageRadio
            // 
            this._printSomePageRadio.AutoSize = true;
            this._printSomePageRadio.Font = new System.Drawing.Font("Tahoma", 9F);
            this._printSomePageRadio.Location = new System.Drawing.Point(10, 25);
            this._printSomePageRadio.Name = "_printSomePageRadio";
            this._printSomePageRadio.Size = new System.Drawing.Size(94, 18);
            this._printSomePageRadio.TabIndex = 1;
            this._printSomePageRadio.Text = "หน้าดังต่อไปนี้";
            this._printSomePageRadio.UseVisualStyleBackColor = true;
            this._printSomePageRadio.CheckedChanged += new System.EventHandler(this._printSomePageRadio_CheckedChanged);
            // 
            // _printAllPageRadio
            // 
            this._printAllPageRadio.AutoSize = true;
            this._printAllPageRadio.Checked = true;
            this._printAllPageRadio.Font = new System.Drawing.Font("Tahoma", 9F);
            this._printAllPageRadio.Location = new System.Drawing.Point(10, 2);
            this._printAllPageRadio.Name = "_printAllPageRadio";
            this._printAllPageRadio.Size = new System.Drawing.Size(62, 18);
            this._printAllPageRadio.TabIndex = 0;
            this._printAllPageRadio.TabStop = true;
            this._printAllPageRadio.Text = "ทุกหน้า";
            this._printAllPageRadio.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SMLERPReportTool.Properties.Resources.printer_information;
            this.pictureBox1.Location = new System.Drawing.Point(354, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._cancelButton.Image = global::SMLERPReportTool.Properties.Resources.error;
            this._cancelButton.Location = new System.Drawing.Point(368, 205);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(87, 23);
            this._cancelButton.TabIndex = 9;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _processButton
            // 
            this._processButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._processButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._processButton.Image = global::SMLERPReportTool.Properties.Resources.flash;
            this._processButton.Location = new System.Drawing.Point(271, 205);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(91, 23);
            this._processButton.TabIndex = 8;
            this._processButton.Text = "Process";
            this._processButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _formPrintOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(471, 289);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_formPrintOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ตัวเลือกการพิมพ์";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myPanel4.ResumeLayout(false);
            this._myPanel4.PerformLayout();
            this._myPanel6.ResumeLayout(false);
            this._myPanel5.ResumeLayout(false);
            this._myPanel5.PerformLayout();
            this._myPanel3.ResumeLayout(false);
            this._myPanel3.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this._printRangeGroupbox.ResumeLayout(false);
            this._panelIncludeDoc.ResumeLayout(false);
            this._panelIncludeDoc.PerformLayout();
            this._panelDocSeries.ResumeLayout(false);
            this._panelDocSeries.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.Button _cancelButton;
        public System.Windows.Forms.CheckBox _previewPrintCheck;
        public System.Windows.Forms.CheckBox _printCheck;
        public MyLib._myComboBox _printerCombo;
        public System.Windows.Forms.CheckBox _showagainCheck;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.Panel _myPanel4;
        private System.Windows.Forms.Panel _myPanel3;

        //private MyLib._myPanel _myPanel2;
        private System.Windows.Forms.Panel _myPanel2;

        private System.Windows.Forms.Panel _myPanel5;
        private System.Windows.Forms.Panel _myPanel6;
        public _selectFormScreen _myScreen1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _printRangePageTextbox;
        public System.Windows.Forms.RadioButton _printSomePageRadio;
        public System.Windows.Forms.RadioButton _printAllPageRadio;
        public System.Windows.Forms.CheckBox _includeDocSeriesCheckbox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel _panelDocSeries;
        public System.Windows.Forms.RadioButton _someSeriesRadio;
        public System.Windows.Forms.RadioButton _allSeriesRadio;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox _printDocSeriesNoTextBox;
        private System.Windows.Forms.Panel _panelIncludeDoc;
        public MyLib._myGroupBox _printRangeGroupbox;
    }
}