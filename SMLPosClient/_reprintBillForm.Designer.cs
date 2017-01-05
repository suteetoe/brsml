namespace SMLPosClient
{
    partial class _reprintBillForm 
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._textBoxBillNo = new System.Windows.Forms.TextBox();
            this._textBoxRemark = new System.Windows.Forms.TextBox();
            this._textBoxUserCode = new System.Windows.Forms.TextBox();
            this._textBoxPassword = new System.Windows.Forms.TextBox();
            this._buttonProcess = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonBillSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่ใบกำกับภาษี :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "หมายเหตุ :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "รหัสพนักงาน :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "รหัสผ่าน :";
            // 
            // _textBoxBillNo
            // 
            this._textBoxBillNo.Location = new System.Drawing.Point(162, 11);
            this._textBoxBillNo.Name = "_textBoxBillNo";
            this._textBoxBillNo.Size = new System.Drawing.Size(315, 22);
            this._textBoxBillNo.TabIndex = 4;
            // 
            // _textBoxRemark
            // 
            this._textBoxRemark.Location = new System.Drawing.Point(162, 38);
            this._textBoxRemark.Name = "_textBoxRemark";
            this._textBoxRemark.Size = new System.Drawing.Size(315, 22);
            this._textBoxRemark.TabIndex = 5;
            // 
            // _textBoxUserCode
            // 
            this._textBoxUserCode.Location = new System.Drawing.Point(162, 65);
            this._textBoxUserCode.Name = "_textBoxUserCode";
            this._textBoxUserCode.Size = new System.Drawing.Size(315, 22);
            this._textBoxUserCode.TabIndex = 6;
            // 
            // _textBoxPassword
            // 
            this._textBoxPassword.Location = new System.Drawing.Point(162, 92);
            this._textBoxPassword.Name = "_textBoxPassword";
            this._textBoxPassword.PasswordChar = '*';
            this._textBoxPassword.Size = new System.Drawing.Size(315, 22);
            this._textBoxPassword.TabIndex = 7;
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Location = new System.Drawing.Point(315, 120);
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(163, 25);
            this._buttonProcess.TabIndex = 8;
            this._buttonProcess.Text = "เริ่มพิมพ์ใบกำกับภาษีอย่างย่อ";
            this._buttonProcess.UseVisualStyleBackColor = true;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(146, 120);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(164, 25);
            this._buttonCancel.TabIndex = 9;
            this._buttonCancel.Text = "ไม่ต้องการพิมพ์ใบกำกับภาษี";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // _buttonBillSearch
            // 
            this._buttonBillSearch.Location = new System.Drawing.Point(7, 120);
            this._buttonBillSearch.Name = "_buttonBillSearch";
            this._buttonBillSearch.Size = new System.Drawing.Size(135, 25);
            this._buttonBillSearch.TabIndex = 10;
            this._buttonBillSearch.Text = "ค้นหาใบกำกับภาษี (F2)";
            this._buttonBillSearch.UseVisualStyleBackColor = true;
            this._buttonBillSearch.Click += new System.EventHandler(this._buttonBillSearch_Click);
            // 
            // _reprintBillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 152);
            this.Controls.Add(this._buttonBillSearch);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonProcess);
            this.Controls.Add(this._textBoxPassword);
            this.Controls.Add(this._textBoxUserCode);
            this.Controls.Add(this._textBoxRemark);
            this.Controls.Add(this._textBoxBillNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_reprintBillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "พิมพ์ใบกำกับภาษีอย่างย่อใหม่";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _textBoxBillNo;
        private System.Windows.Forms.TextBox _textBoxRemark;
        private System.Windows.Forms.TextBox _textBoxUserCode;
        private System.Windows.Forms.TextBox _textBoxPassword;
        private System.Windows.Forms.Button _buttonProcess;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonBillSearch;
    }
}