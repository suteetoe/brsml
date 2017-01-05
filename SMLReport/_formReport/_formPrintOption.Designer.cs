namespace SMLReport._formReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._previewPrintCheck = new System.Windows.Forms.CheckBox();
            this._printCheck = new System.Windows.Forms.CheckBox();
            this._printVatCheck = new System.Windows.Forms.CheckBox();
            this._showagainCheck = new System.Windows.Forms.CheckBox();
            this._printerCombo = new System.Windows.Forms.ComboBox();
            this._formCombo = new System.Windows.Forms.ComboBox();
            this._processButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "ฟอร์ม : ";
            // 
            // _previewPrintCheck
            // 
            this._previewPrintCheck.AutoSize = true;
            this._previewPrintCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._previewPrintCheck.Location = new System.Drawing.Point(90, 68);
            this._previewPrintCheck.Name = "_previewPrintCheck";
            this._previewPrintCheck.Size = new System.Drawing.Size(134, 18);
            this._previewPrintCheck.TabIndex = 2;
            this._previewPrintCheck.Text = "แสดงตัวอย่างก่อนพิมพ์";
            this._previewPrintCheck.UseVisualStyleBackColor = true;
            // 
            // _printCheck
            // 
            this._printCheck.AutoSize = true;
            this._printCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._printCheck.Location = new System.Drawing.Point(90, 92);
            this._printCheck.Name = "_printCheck";
            this._printCheck.Size = new System.Drawing.Size(47, 18);
            this._printCheck.TabIndex = 3;
            this._printCheck.Text = "พิมพ์";
            this._printCheck.UseVisualStyleBackColor = true;
            // 
            // _printVatCheck
            // 
            this._printVatCheck.AutoSize = true;
            this._printVatCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._printVatCheck.Location = new System.Drawing.Point(90, 116);
            this._printVatCheck.Name = "_printVatCheck";
            this._printVatCheck.Size = new System.Drawing.Size(144, 18);
            this._printVatCheck.TabIndex = 4;
            this._printVatCheck.Text = "พิมพ์ใบหักภาษี ณ ที่จ่าย";
            this._printVatCheck.UseVisualStyleBackColor = true;
            // 
            // _showagainCheck
            // 
            this._showagainCheck.AutoSize = true;
            this._showagainCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._showagainCheck.Location = new System.Drawing.Point(12, 153);
            this._showagainCheck.Name = "_showagainCheck";
            this._showagainCheck.Size = new System.Drawing.Size(124, 18);
            this._showagainCheck.TabIndex = 5;
            this._showagainCheck.Text = "ไม่ต้องการให้ถามอีก";
            this._showagainCheck.UseVisualStyleBackColor = true;
            // 
            // _printerCombo
            // 
            this._printerCombo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._printerCombo.FormattingEnabled = true;
            this._printerCombo.Location = new System.Drawing.Point(90, 12);
            this._printerCombo.Name = "_printerCombo";
            this._printerCombo.Size = new System.Drawing.Size(315, 22);
            this._printerCombo.TabIndex = 6;
            // 
            // _formCombo
            // 
            this._formCombo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._formCombo.FormattingEnabled = true;
            this._formCombo.Location = new System.Drawing.Point(90, 40);
            this._formCombo.Name = "_formCombo";
            this._formCombo.Size = new System.Drawing.Size(315, 22);
            this._formCombo.TabIndex = 7;
            // 
            // _processButton
            // 
            this._processButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._processButton.Image = global::SMLReport.Properties.Resources.flash;
            this._processButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._processButton.Location = new System.Drawing.Point(224, 148);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(90, 23);
            this._processButton.TabIndex = 8;
            this._processButton.Text = "Process";
            this._processButton.UseVisualStyleBackColor = true;
            // 
            // _cancelButton
            // 
            this._cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._cancelButton.Image = global::SMLReport.Resource16x16.delete2;
            this._cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cancelButton.Location = new System.Drawing.Point(320, 148);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(85, 23);
            this._cancelButton.TabIndex = 9;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _formPrintOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 176);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._processButton);
            this.Controls.Add(this._formCombo);
            this.Controls.Add(this._printerCombo);
            this.Controls.Add(this._showagainCheck);
            this.Controls.Add(this._printVatCheck);
            this.Controls.Add(this._printCheck);
            this.Controls.Add(this._previewPrintCheck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_formPrintOption";
            this.Text = "ตัวเลือกการพิมพ์";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox _previewPrintCheck;
        private System.Windows.Forms.CheckBox _printCheck;
        private System.Windows.Forms.CheckBox _printVatCheck;
        private System.Windows.Forms.CheckBox _showagainCheck;
        private System.Windows.Forms.ComboBox _printerCombo;
        private System.Windows.Forms.ComboBox _formCombo;
        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.Button _cancelButton;

    }
}