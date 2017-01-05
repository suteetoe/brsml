namespace SMLPosClient
{
    partial class _printVatFull
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
            this._docNoLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._textBoxBillNo = new System.Windows.Forms.TextBox();
            this._textBoxRemark = new System.Windows.Forms.TextBox();
            this._textBoxUserCode = new System.Windows.Forms.TextBox();
            this._textBoxPassword = new System.Windows.Forms.TextBox();
            this._buttonProcess = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._textBoxCustCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._textBoxCustName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._textBoxCustAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._buttonCustSearch = new System.Windows.Forms.Button();
            this._buttonCustAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _docNoLabel
            // 
            this._docNoLabel.Location = new System.Drawing.Point(2, 14);
            this._docNoLabel.Name = "_docNoLabel";
            this._docNoLabel.Size = new System.Drawing.Size(162, 19);
            this._docNoLabel.TabIndex = 12;
            this._docNoLabel.Text = "เลขที่ใบกำกับภาษีแบบย่อ :";
            this._docNoLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "หมายเหตุ :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "รหัสพนักงาน :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "รหัสผ่าน :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _textBoxBillNo
            // 
            this._textBoxBillNo.Location = new System.Drawing.Point(170, 11);
            this._textBoxBillNo.Name = "_textBoxBillNo";
            this._textBoxBillNo.Size = new System.Drawing.Size(363, 22);
            this._textBoxBillNo.TabIndex = 0;
            // 
            // _textBoxRemark
            // 
            this._textBoxRemark.Location = new System.Drawing.Point(170, 179);
            this._textBoxRemark.Name = "_textBoxRemark";
            this._textBoxRemark.Size = new System.Drawing.Size(363, 22);
            this._textBoxRemark.TabIndex = 4;
            // 
            // _textBoxUserCode
            // 
            this._textBoxUserCode.Location = new System.Drawing.Point(170, 206);
            this._textBoxUserCode.Name = "_textBoxUserCode";
            this._textBoxUserCode.Size = new System.Drawing.Size(363, 22);
            this._textBoxUserCode.TabIndex = 5;
            // 
            // _textBoxPassword
            // 
            this._textBoxPassword.Location = new System.Drawing.Point(170, 233);
            this._textBoxPassword.Name = "_textBoxPassword";
            this._textBoxPassword.PasswordChar = '*';
            this._textBoxPassword.Size = new System.Drawing.Size(363, 22);
            this._textBoxPassword.TabIndex = 6;
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Location = new System.Drawing.Point(383, 265);
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(150, 25);
            this._buttonProcess.TabIndex = 7;
            this._buttonProcess.Text = "พิมพ์ใบกำกับภาษีแบบเต็ม";
            this._buttonProcess.UseVisualStyleBackColor = true;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(316, 265);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(61, 25);
            this._buttonCancel.TabIndex = 8;
            this._buttonCancel.Text = "ยกเลิก";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // _textBoxCustCode
            // 
            this._textBoxCustCode.Location = new System.Drawing.Point(170, 38);
            this._textBoxCustCode.Name = "_textBoxCustCode";
            this._textBoxCustCode.ReadOnly = true;
            this._textBoxCustCode.Size = new System.Drawing.Size(363, 22);
            this._textBoxCustCode.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "รหัสลูกค้า :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _textBoxCustName
            // 
            this._textBoxCustName.Location = new System.Drawing.Point(170, 64);
            this._textBoxCustName.Name = "_textBoxCustName";
            this._textBoxCustName.ReadOnly = true;
            this._textBoxCustName.Size = new System.Drawing.Size(363, 22);
            this._textBoxCustName.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(107, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "ชื่อลูกค้า :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _textBoxCustAddress
            // 
            this._textBoxCustAddress.Location = new System.Drawing.Point(170, 90);
            this._textBoxCustAddress.Multiline = true;
            this._textBoxCustAddress.Name = "_textBoxCustAddress";
            this._textBoxCustAddress.ReadOnly = true;
            this._textBoxCustAddress.Size = new System.Drawing.Size(363, 84);
            this._textBoxCustAddress.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 14);
            this.label7.TabIndex = 15;
            this.label7.Text = "ที่อยู่ลูกค้า :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _buttonCustSearch
            // 
            this._buttonCustSearch.Location = new System.Drawing.Point(63, 265);
            this._buttonCustSearch.Name = "_buttonCustSearch";
            this._buttonCustSearch.Size = new System.Drawing.Size(101, 25);
            this._buttonCustSearch.TabIndex = 9;
            this._buttonCustSearch.Text = "ค้นหาลูกค้า (F2)";
            this._buttonCustSearch.UseVisualStyleBackColor = true;
            this._buttonCustSearch.Click += new System.EventHandler(this._buttonCustSearch_Click);
            // 
            // _buttonCustAdd
            // 
            this._buttonCustAdd.Location = new System.Drawing.Point(170, 265);
            this._buttonCustAdd.Name = "_buttonCustAdd";
            this._buttonCustAdd.Size = new System.Drawing.Size(129, 25);
            this._buttonCustAdd.TabIndex = 18;
            this._buttonCustAdd.Text = "เพิ่มลูกค้าใหม่ (F3)";
            this._buttonCustAdd.UseVisualStyleBackColor = true;
            this._buttonCustAdd.Click += new System.EventHandler(this._buttonCustAdd_Click);
            // 
            // _printVatFull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(545, 302);
            this.Controls.Add(this._buttonCustAdd);
            this.Controls.Add(this._buttonCustSearch);
            this.Controls.Add(this._textBoxCustAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._textBoxCustName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._textBoxCustCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonProcess);
            this.Controls.Add(this._textBoxPassword);
            this.Controls.Add(this._textBoxUserCode);
            this.Controls.Add(this._textBoxRemark);
            this.Controls.Add(this._textBoxBillNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._docNoLabel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_printVatFull";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "พิมพ์ใบกำกับภาษีแบบเต็ม";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _docNoLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _textBoxBillNo;
        private System.Windows.Forms.TextBox _textBoxRemark;
        private System.Windows.Forms.TextBox _textBoxUserCode;
        private System.Windows.Forms.TextBox _textBoxPassword;
        private System.Windows.Forms.Button _buttonProcess;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.TextBox _textBoxCustCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _textBoxCustName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _textBoxCustAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button _buttonCustSearch;
        private System.Windows.Forms.Button _buttonCustAdd;
    }
}