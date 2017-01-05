namespace SMLInventoryControl
{
    partial class _labelPrintForm
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
            this._myManageBar = new System.Windows.Forms.ToolStrip();
            this._previewButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._startRowTextbox = new System.Windows.Forms.TextBox();
            this._startRowLabel = new System.Windows.Forms.Label();
            this._startColumnTextbox = new System.Windows.Forms.TextBox();
            this._startColumnLabel = new System.Windows.Forms.Label();
            this._printModeCheckbox = new MyLib._myCheckBox();
            this._formCombobox = new System.Windows.Forms.ComboBox();
            this._printerCombobox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._transport_labe_grid = new SMLTransportLabel._transport_label_grid();
            this._myManageBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageBar
            // 
            this._myManageBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton,
            this._printButton});
            this._myManageBar.Location = new System.Drawing.Point(0, 113);
            this._myManageBar.Name = "_myManageBar";
            this._myManageBar.Size = new System.Drawing.Size(584, 25);
            this._myManageBar.TabIndex = 4;
            this._myManageBar.Text = "toolStrip1";
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::SMLInventoryControl.Properties.Resources.printer;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Padding = new System.Windows.Forms.Padding(1);
            this._previewButton.ResourceName = "";
            this._previewButton.Size = new System.Drawing.Size(87, 22);
            this._previewButton.Text = "แสดงตัวอย่าง";
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLInventoryControl.Properties.Resources.printer;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "";
            this._printButton.Size = new System.Drawing.Size(52, 22);
            this._printButton.Text = "พิมพ์";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
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
            this._myPanel1.Size = new System.Drawing.Size(584, 113);
            this._myPanel1.TabIndex = 3;
            // 
            // _startRowTextbox
            // 
            this._startRowTextbox.Enabled = false;
            this._startRowTextbox.Location = new System.Drawing.Point(369, 86);
            this._startRowTextbox.Name = "_startRowTextbox";
            this._startRowTextbox.Size = new System.Drawing.Size(90, 22);
            this._startRowTextbox.TabIndex = 8;
            this._startRowTextbox.Text = "1";
            // 
            // _startRowLabel
            // 
            this._startRowLabel.AutoSize = true;
            this._startRowLabel.BackColor = System.Drawing.Color.Transparent;
            this._startRowLabel.Enabled = false;
            this._startRowLabel.Location = new System.Drawing.Point(216, 89);
            this._startRowLabel.Name = "_startRowLabel";
            this._startRowLabel.Size = new System.Drawing.Size(147, 14);
            this._startRowLabel.TabIndex = 7;
            this._startRowLabel.Text = "ตำแหน่งแถวเริ่มต้นแนวนอน :";
            // 
            // _startColumnTextbox
            // 
            this._startColumnTextbox.Enabled = false;
            this._startColumnTextbox.Location = new System.Drawing.Point(369, 60);
            this._startColumnTextbox.Name = "_startColumnTextbox";
            this._startColumnTextbox.Size = new System.Drawing.Size(90, 22);
            this._startColumnTextbox.TabIndex = 6;
            this._startColumnTextbox.Text = "1";
            // 
            // _startColumnLabel
            // 
            this._startColumnLabel.AutoSize = true;
            this._startColumnLabel.BackColor = System.Drawing.Color.Transparent;
            this._startColumnLabel.Enabled = false;
            this._startColumnLabel.Location = new System.Drawing.Point(224, 63);
            this._startColumnLabel.Name = "_startColumnLabel";
            this._startColumnLabel.Size = new System.Drawing.Size(139, 14);
            this._startColumnLabel.TabIndex = 5;
            this._startColumnLabel.Text = "ตำแหน่งแถวเริ่มต้นแนวตั้ง :";
            // 
            // _printModeCheckbox
            // 
            this._printModeCheckbox._isQuery = true;
            this._printModeCheckbox.AutoSize = true;
            this._printModeCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._printModeCheckbox.Location = new System.Drawing.Point(85, 62);
            this._printModeCheckbox.Name = "_printModeCheckbox";
            this._printModeCheckbox.ResourceName = "พิมพ์แบบสติกเกอร์ A4";
            this._printModeCheckbox.Size = new System.Drawing.Size(133, 18);
            this._printModeCheckbox.TabIndex = 4;
            this._printModeCheckbox.Text = "พิมพ์แบบสติกเกอร์ A4";
            this._printModeCheckbox.UseVisualStyleBackColor = false;
            // 
            // _formCombobox
            // 
            this._formCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._formCombobox.FormattingEnabled = true;
            this._formCombobox.Location = new System.Drawing.Point(85, 34);
            this._formCombobox.Name = "_formCombobox";
            this._formCombobox.Size = new System.Drawing.Size(415, 22);
            this._formCombobox.TabIndex = 3;
            // 
            // _printerCombobox
            // 
            this._printerCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._printerCombobox.FormattingEnabled = true;
            this._printerCombobox.Location = new System.Drawing.Point(85, 7);
            this._printerCombobox.Name = "_printerCombobox";
            this._printerCombobox.Size = new System.Drawing.Size(415, 22);
            this._printerCombobox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(38, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ฟอร์ม :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(29, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer :";
            // 
            // _transport_labe_grid
            // 
            this._transport_labe_grid._extraWordShow = true;
            this._transport_labe_grid._selectRow = -1;
            this._transport_labe_grid.AddRow = false;
            this._transport_labe_grid.BackColor = System.Drawing.SystemColors.Window;
            this._transport_labe_grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._transport_labe_grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._transport_labe_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transport_labe_grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._transport_labe_grid.Location = new System.Drawing.Point(0, 138);
            this._transport_labe_grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._transport_labe_grid.Name = "_transport_labe_grid";
            this._transport_labe_grid.Size = new System.Drawing.Size(584, 423);
            this._transport_labe_grid.TabIndex = 5;
            this._transport_labe_grid.TabStop = false;
            // 
            // _labelPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this._transport_labe_grid);
            this.Controls.Add(this._myManageBar);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "_labelPrintForm";
            this.Text = "_labelPrintForm";
            this._myManageBar.ResumeLayout(false);
            this._myManageBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLTransportLabel._transport_label_grid _transport_labe_grid;
        private System.Windows.Forms.ToolStrip _myManageBar;
        private MyLib.ToolStripMyButton _previewButton;
        private MyLib.ToolStripMyButton _printButton;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.TextBox _startRowTextbox;
        private System.Windows.Forms.Label _startRowLabel;
        private System.Windows.Forms.TextBox _startColumnTextbox;
        private System.Windows.Forms.Label _startColumnLabel;
        private MyLib._myCheckBox _printModeCheckbox;
        private System.Windows.Forms.ComboBox _formCombobox;
        private System.Windows.Forms.ComboBox _printerCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}