namespace SMLInventoryControl
{
    partial class _selectBillFromPosForm 
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
            this._textBoxBillNo = new System.Windows.Forms.TextBox();
            this._buttonProcess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่ใบกำกับภาษี :";
            // 
            // _textBoxBillNo
            // 
            this._textBoxBillNo.Location = new System.Drawing.Point(115, 11);
            this._textBoxBillNo.Name = "_textBoxBillNo";
            this._textBoxBillNo.Size = new System.Drawing.Size(315, 22);
            this._textBoxBillNo.TabIndex = 4;
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Location = new System.Drawing.Point(342, 39);
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(88, 25);
            this._buttonProcess.TabIndex = 8;
            this._buttonProcess.Text = "เรียกรายการ";
            this._buttonProcess.UseVisualStyleBackColor = true;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _selectBillFromPosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 72);
            this.Controls.Add(this._buttonProcess);
            this.Controls.Add(this._textBoxBillNo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_selectBillFromPosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "พิมพ์ใบกำกับภาษีอย่างเต็ม";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxBillNo;
        private System.Windows.Forms.Button _buttonProcess;
        public System.Windows.Forms.Label label1;
    }
}