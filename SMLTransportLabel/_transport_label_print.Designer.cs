namespace SMLTransportLabel
{
    partial class _transport_label_print
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
            this._myPanel1 = new MyLib._myPanel();
            this._startRowTextbox = new System.Windows.Forms.TextBox();
            this._startRowLabel = new MyLib._myLabel();
            this._startColumnTextbox = new System.Windows.Forms.TextBox();
            this._startColumnLabel = new MyLib._myLabel();
            this._printModeCheckbox = new MyLib._myCheckBox();
            this._formCombobox = new System.Windows.Forms.ComboBox();
            this._printerCombobox = new System.Windows.Forms.ComboBox();
            this.label2 = new MyLib._myLabel();
            this.label1 = new System.Windows.Forms.Label();
            this._myManageBar = new System.Windows.Forms.ToolStrip();
            this._previewButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this._clearButton = new MyLib.ToolStripMyButton();
            this._groupLabel = new MyLib.ToolStripMyLabel();
            this._groupList = new System.Windows.Forms.ToolStripComboBox();
            this._transport_labe_grid = new SMLTransportLabel._transport_label_grid();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myManageData = new MyLib._myManageData();
            this._myPanel1.SuspendLayout();
            this._myManageBar.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._startRowTextbox);
            this._myPanel1.Controls.Add(this._startRowLabel);
            this._myPanel1.Controls.Add(this._startColumnTextbox);
            this._myPanel1.Controls.Add(this._startColumnLabel);
            this._myPanel1.Controls.Add(this._printModeCheckbox);
            this._myPanel1.Controls.Add(this._formCombobox);
            this._myPanel1.Controls.Add(this._printerCombobox);
            this._myPanel1.Controls.Add(this.label2);
            this._myPanel1.Controls.Add(this.label1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(640, 114);
            this._myPanel1.TabIndex = 0;
            // 
            // _startRowTextbox
            // 
            this._startRowTextbox.Enabled = false;
            this._startRowTextbox.Location = new System.Drawing.Point(452, 83);
            this._startRowTextbox.Name = "_startRowTextbox";
            this._startRowTextbox.Size = new System.Drawing.Size(133, 22);
            this._startRowTextbox.TabIndex = 8;
            this._startRowTextbox.Text = "1";
            // 
            // _startRowLabel
            // 
            this._startRowLabel.AutoSize = true;
            this._startRowLabel.BackColor = System.Drawing.Color.Transparent;
            this._startRowLabel.Enabled = false;
            this._startRowLabel.Location = new System.Drawing.Point(299, 86);
            this._startRowLabel.Name = "_startRowLabel";
            this._startRowLabel.ResourceName = "ตำแหน่งแถวเริ่มต้นแนวนอน :";
            this._startRowLabel.Size = new System.Drawing.Size(147, 14);
            this._startRowLabel.TabIndex = 7;
            this._startRowLabel.Text = "ตำแหน่งแถวเริ่มต้นแนวนอน :";
            // 
            // _startColumnTextbox
            // 
            this._startColumnTextbox.Enabled = false;
            this._startColumnTextbox.Location = new System.Drawing.Point(155, 83);
            this._startColumnTextbox.Name = "_startColumnTextbox";
            this._startColumnTextbox.Size = new System.Drawing.Size(133, 22);
            this._startColumnTextbox.TabIndex = 6;
            this._startColumnTextbox.Text = "1";
            // 
            // _startColumnLabel
            // 
            this._startColumnLabel.AutoSize = true;
            this._startColumnLabel.BackColor = System.Drawing.Color.Transparent;
            this._startColumnLabel.Enabled = false;
            this._startColumnLabel.Location = new System.Drawing.Point(10, 86);
            this._startColumnLabel.Name = "_startColumnLabel";
            this._startColumnLabel.ResourceName = "ตำแหน่งแถวเริ่มต้นแนวตั้ง :";
            this._startColumnLabel.Size = new System.Drawing.Size(139, 14);
            this._startColumnLabel.TabIndex = 5;
            this._startColumnLabel.Text = "ตำแหน่งแถวเริ่มต้นแนวตั้ง :";
            // 
            // _printModeCheckbox
            // 
            this._printModeCheckbox._isQuery = true;
            this._printModeCheckbox.AutoSize = true;
            this._printModeCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._printModeCheckbox.Location = new System.Drawing.Point(155, 61);
            this._printModeCheckbox.Name = "_printModeCheckbox";
            this._printModeCheckbox.ResourceName = "พิมพ์แบบสติกเกอร์ A4";
            this._printModeCheckbox.Size = new System.Drawing.Size(133, 18);
            this._printModeCheckbox.TabIndex = 4;
            this._printModeCheckbox.Text = "พิมพ์แบบสติกเกอร์ A4";
            this._printModeCheckbox.UseVisualStyleBackColor = false;
            this._printModeCheckbox.CheckedChanged += new System.EventHandler(this._printModeCheckbox_CheckedChanged);
            // 
            // _formCombobox
            // 
            this._formCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._formCombobox.FormattingEnabled = true;
            this._formCombobox.Location = new System.Drawing.Point(155, 33);
            this._formCombobox.Name = "_formCombobox";
            this._formCombobox.Size = new System.Drawing.Size(430, 22);
            this._formCombobox.TabIndex = 3;
            // 
            // _printerCombobox
            // 
            this._printerCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._printerCombobox.FormattingEnabled = true;
            this._printerCombobox.Location = new System.Drawing.Point(155, 5);
            this._printerCombobox.Name = "_printerCombobox";
            this._printerCombobox.Size = new System.Drawing.Size(430, 22);
            this._printerCombobox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(108, 36);
            this.label2.Name = "label2";
            this.label2.ResourceName = "ฟอร์ม :";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ฟอร์ม :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(98, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer :";
            // 
            // _myManageBar
            // 
            this._myManageBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton,
            this._printButton,
            this._clearButton,
            this._groupLabel,
            this._groupList});
            this._myManageBar.Location = new System.Drawing.Point(0, 114);
            this._myManageBar.Name = "_myManageBar";
            this._myManageBar.Size = new System.Drawing.Size(640, 25);
            this._myManageBar.TabIndex = 1;
            this._myManageBar.Text = "toolStrip1";
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::SMLTransportLabel.Properties.Resources.view;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Padding = new System.Windows.Forms.Padding(1);
            this._previewButton.ResourceName = "แสดงตัวอย่าง";
            this._previewButton.Size = new System.Drawing.Size(87, 22);
            this._previewButton.Text = "แสดงตัวอย่าง";
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLTransportLabel.Properties.Resources.printer;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "พิมพ์";
            this._printButton.Size = new System.Drawing.Size(52, 22);
            this._printButton.Text = "พิมพ์";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::SMLTransportLabel.Properties.Resources.lightbulb_on;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Padding = new System.Windows.Forms.Padding(1);
            this._clearButton.ResourceName = "ล้างรายการ";
            this._clearButton.Size = new System.Drawing.Size(78, 22);
            this._clearButton.Text = "ล้างรายการ";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // _groupLabel
            // 
            this._groupLabel.Name = "_groupLabel";
            this._groupLabel.Padding = new System.Windows.Forms.Padding(1);
            this._groupLabel.ResourceName = "กลุ่ม";
            this._groupLabel.Size = new System.Drawing.Size(29, 22);
            this._groupLabel.Text = "กลุ่ม";
            // 
            // _groupList
            // 
            this._groupList.Name = "_groupList";
            this._groupList.Size = new System.Drawing.Size(121, 25);
            // 
            // _transport_labe_grid
            // 
            this._transport_labe_grid._extraWordShow = true;
            this._transport_labe_grid._selectRow = -1;
            this._transport_labe_grid.BackColor = System.Drawing.SystemColors.Window;
            this._transport_labe_grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._transport_labe_grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._transport_labe_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transport_labe_grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._transport_labe_grid.Location = new System.Drawing.Point(0, 139);
            this._transport_labe_grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._transport_labe_grid.Name = "_transport_labe_grid";
            this._transport_labe_grid.Size = new System.Drawing.Size(640, 629);
            this._transport_labe_grid.TabIndex = 2;
            this._transport_labe_grid.TabStop = false;
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(640, 25);
            this._myToolbar.TabIndex = 3;
            this._myToolbar.Text = "toolStrip2";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLTransportLabel.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myManageData
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Location = new System.Drawing.Point(0, 0);
            this._myManageData.Name = "_myManageData";
            this._myManageData.Size = new System.Drawing.Size(725, 789);
            this._myManageData.TabIndex = 4;
            this._myManageData.TabStop = false;

            this._myManageData._form2.Controls.Add(this._transport_labe_grid);
            this._myManageData._form2.Controls.Add(this._myManageBar);
            this._myManageData._form2.Controls.Add(this._myPanel1);

            // 
            // _transport_label_print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "_transport_label_print";
            this.Size = new System.Drawing.Size(725, 789);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myManageBar.ResumeLayout(false);
            this._myManageBar.PerformLayout();
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myLabel label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _formCombobox;
        private System.Windows.Forms.ComboBox _printerCombobox;
        private System.Windows.Forms.ToolStrip _myManageBar;
        private MyLib.ToolStripMyButton _previewButton;
        private MyLib.ToolStripMyButton _printButton;
        private MyLib.ToolStripMyButton _clearButton;
        private MyLib.ToolStripMyLabel _groupLabel;
        private System.Windows.Forms.ToolStripComboBox _groupList;
        private _transport_label_grid _transport_labe_grid;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myManageData _myManageData;
        private System.Windows.Forms.TextBox _startRowTextbox;
        private MyLib._myLabel _startRowLabel;
        private System.Windows.Forms.TextBox _startColumnTextbox;
        private MyLib._myLabel _startColumnLabel;
        private MyLib._myCheckBox _printModeCheckbox;
        //private _transport_label_grid _transport_label_grid1;
    }
}
