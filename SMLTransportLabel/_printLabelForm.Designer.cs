namespace SMLTransportLabel
{
    partial class _printLabelForm
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
            this._myPanel1 = new MyLib._myPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._printButton = new System.Windows.Forms.Button();
            this._printPreviewButton = new System.Windows.Forms.Button();
            this._formComboBox = new MyLib._myComboBox();
            this.label2 = new MyLib._myLabel();
            this._printerCombo = new MyLib._myComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._myPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.pictureBox1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._formComboBox);
            this._myPanel1.Controls.Add(this.label2);
            this._myPanel1.Controls.Add(this._printerCombo);
            this._myPanel1.Controls.Add(this.label1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(398, 102);
            this._myPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SMLTransportLabel.Properties.Resources.printer_information;
            this.pictureBox1.Location = new System.Drawing.Point(14, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._printButton);
            this._myFlowLayoutPanel1.Controls.Add(this._printPreviewButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 70);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(398, 32);
            this._myFlowLayoutPanel1.TabIndex = 13;
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLTransportLabel.Properties.Resources.printer;
            this._printButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._printButton.Location = new System.Drawing.Point(316, 5);
            this._printButton.Name = "_printButton";
            this._printButton.Size = new System.Drawing.Size(75, 23);
            this._printButton.TabIndex = 0;
            this._printButton.Text = "Print";
            this._printButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._printButton.UseVisualStyleBackColor = true;
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _printPreviewButton
            // 
            this._printPreviewButton.Image = global::SMLTransportLabel.Properties.Resources.view;
            this._printPreviewButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._printPreviewButton.Location = new System.Drawing.Point(235, 5);
            this._printPreviewButton.Name = "_printPreviewButton";
            this._printPreviewButton.Size = new System.Drawing.Size(75, 23);
            this._printPreviewButton.TabIndex = 1;
            this._printPreviewButton.Text = "Preview";
            this._printPreviewButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._printPreviewButton.UseVisualStyleBackColor = true;
            this._printPreviewButton.Click += new System.EventHandler(this._printPreviewButton_Click);
            // 
            // _formComboBox
            // 
            this._formComboBox._isQuery = true;
            this._formComboBox._maxColumn = 1;
            this._formComboBox.BackColor = System.Drawing.Color.White;
            this._formComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._formComboBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._formComboBox.FormattingEnabled = true;
            this._formComboBox.Location = new System.Drawing.Point(129, 40);
            this._formComboBox.Name = "_formComboBox";
            this._formComboBox.Size = new System.Drawing.Size(253, 22);
            this._formComboBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 43);
            this.label2.Name = "label2";
            this.label2.ResourceName = "ฟอร์ม :";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "ฟอร์ม :";
            // 
            // _printerCombo
            // 
            this._printerCombo._isQuery = true;
            this._printerCombo._maxColumn = 1;
            this._printerCombo.BackColor = System.Drawing.Color.White;
            this._printerCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._printerCombo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._printerCombo.FormattingEnabled = true;
            this._printerCombo.Location = new System.Drawing.Point(129, 12);
            this._printerCombo.Name = "_printerCombo";
            this._printerCombo.Size = new System.Drawing.Size(253, 22);
            this._printerCombo.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Printer :";
            // 
            // _printLabelForm
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 102);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_printLabelForm";
            this.ResourceName = "พิมพ์ฉลากขนส่ง";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "พิมพ์ฉลากขนส่ง";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        public MyLib._myComboBox _printerCombo;
        private System.Windows.Forms.Label label1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        public MyLib._myComboBox _formComboBox;
        private MyLib._myLabel label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button _printButton;
        private System.Windows.Forms.Button _printPreviewButton;
    }
}