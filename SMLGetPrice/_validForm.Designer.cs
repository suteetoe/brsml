namespace SMLGetPrice
{
    partial class _validForm
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
            this._displayTextBox = new System.Windows.Forms.TextBox();
            this._inputTextBox = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "กรุณาบันทึกเลขที่เห็น เพื่อความปลอดภัย";
            // 
            // _displayTextBox
            // 
            this._displayTextBox.Location = new System.Drawing.Point(108, 58);
            this._displayTextBox.Name = "_displayTextBox";
            this._displayTextBox.ReadOnly = true;
            this._displayTextBox.Size = new System.Drawing.Size(169, 22);
            this._displayTextBox.TabIndex = 2;
            this._displayTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _inputTextBox
            // 
            this._inputTextBox.Location = new System.Drawing.Point(108, 97);
            this._inputTextBox.Name = "_inputTextBox";
            this._inputTextBox.Size = new System.Drawing.Size(169, 22);
            this._inputTextBox.TabIndex = 0;
            this._inputTextBox.Text = "1234";
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(159, 149);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _validForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 184);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._inputTextBox);
            this.Controls.Add(this._displayTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_validForm";
            this.Text = "ยืนยันรหัส";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _displayTextBox;
        private System.Windows.Forms.TextBox _inputTextBox;
        private System.Windows.Forms.Button _okButton;
    }
}