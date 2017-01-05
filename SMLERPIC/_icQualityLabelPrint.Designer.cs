namespace SMLERPIC
{
    partial class _icQualityLabelPrint
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
            this._printGrid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._formCombobox = new System.Windows.Forms.ComboBox();
            this._printerCombobox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._myManageBar = new System.Windows.Forms.ToolStrip();
            this._previewButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._myManageBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _printGrid
            // 
            this._printGrid._extraWordShow = true;
            this._printGrid._selectRow = -1;
            this._printGrid.BackColor = System.Drawing.SystemColors.Window;
            this._printGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._printGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._printGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._printGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._printGrid.Location = new System.Drawing.Point(0, 96);
            this._printGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._printGrid.Name = "_printGrid";
            this._printGrid.Size = new System.Drawing.Size(751, 493);
            this._printGrid.TabIndex = 1;
            this._printGrid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._formCombobox);
            this._myPanel1.Controls.Add(this._printerCombobox);
            this._myPanel1.Controls.Add(this.label2);
            this._myPanel1.Controls.Add(this.label1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(751, 71);
            this._myPanel1.TabIndex = 4;
            // 
            // _formCombobox
            // 
            this._formCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._formCombobox.FormattingEnabled = true;
            this._formCombobox.Location = new System.Drawing.Point(99, 37);
            this._formCombobox.Name = "_formCombobox";
            this._formCombobox.Size = new System.Drawing.Size(483, 22);
            this._formCombobox.TabIndex = 3;
            // 
            // _printerCombobox
            // 
            this._printerCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._printerCombobox.FormattingEnabled = true;
            this._printerCombobox.Location = new System.Drawing.Point(99, 8);
            this._printerCombobox.Name = "_printerCombobox";
            this._printerCombobox.Size = new System.Drawing.Size(483, 22);
            this._printerCombobox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(44, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ฟอร์ม :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(34, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer :";
            // 
            // _myManageBar
            // 
            this._myManageBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton,
            this._printButton});
            this._myManageBar.Location = new System.Drawing.Point(0, 71);
            this._myManageBar.Name = "_myManageBar";
            this._myManageBar.Size = new System.Drawing.Size(751, 25);
            this._myManageBar.TabIndex = 5;
            this._myManageBar.Text = "toolStrip1";
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::SMLERPIC.Properties.Resources.printer1;
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
            this._printButton.Image = global::SMLERPIC.Properties.Resources.printer1;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "";
            this._printButton.Size = new System.Drawing.Size(52, 22);
            this._printButton.Text = "พิมพ์";
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _icQualityLabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 589);
            this.Controls.Add(this._printGrid);
            this.Controls.Add(this._myManageBar);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icQualityLabelPrint";
            this.Text = "_icQualityLabelPrint";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myManageBar.ResumeLayout(false);
            this._myManageBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MyLib._myGrid _printGrid;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ComboBox _formCombobox;
        private System.Windows.Forms.ComboBox _printerCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip _myManageBar;
        private MyLib.ToolStripMyButton _previewButton;
        private MyLib.ToolStripMyButton _printButton;
    }
}