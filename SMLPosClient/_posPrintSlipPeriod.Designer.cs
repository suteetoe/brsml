namespace SMLPosClient
{
    partial class _posPrintSlipPeriod
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
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonProcess = new System.Windows.Forms.Button();
            this._textBoxPassword = new System.Windows.Forms.TextBox();
            this._textBoxUserCode = new System.Windows.Forms.TextBox();
            this._textBoxRemark = new System.Windows.Forms.TextBox();
            this._frombillTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._toBillTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._managerPasswordTextbox = new System.Windows.Forms.TextBox();
            this._managerUserTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(125, 170);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(191, 27);
            this._buttonCancel.TabIndex = 21;
            this._buttonCancel.Text = "ไม่ต้องการพิมพ์ใบกำกับภาษี";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Location = new System.Drawing.Point(323, 170);
            this._buttonProcess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(190, 27);
            this._buttonProcess.TabIndex = 22;
            this._buttonProcess.Text = "เริ่มพิมพ์ใบกำกับภาษีอย่างย่อ";
            this._buttonProcess.UseVisualStyleBackColor = true;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _textBoxPassword
            // 
            this._textBoxPassword.Location = new System.Drawing.Point(146, 91);
            this._textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._textBoxPassword.Name = "_textBoxPassword";
            this._textBoxPassword.PasswordChar = '*';
            this._textBoxPassword.Size = new System.Drawing.Size(367, 22);
            this._textBoxPassword.TabIndex = 17;
            // 
            // _textBoxUserCode
            // 
            this._textBoxUserCode.Enabled = false;
            this._textBoxUserCode.Location = new System.Drawing.Point(146, 65);
            this._textBoxUserCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._textBoxUserCode.Name = "_textBoxUserCode";
            this._textBoxUserCode.Size = new System.Drawing.Size(367, 22);
            this._textBoxUserCode.TabIndex = 16;
            // 
            // _textBoxRemark
            // 
            this._textBoxRemark.Location = new System.Drawing.Point(146, 169);
            this._textBoxRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._textBoxRemark.Name = "_textBoxRemark";
            this._textBoxRemark.Size = new System.Drawing.Size(367, 22);
            this._textBoxRemark.TabIndex = 20;
            this._textBoxRemark.Visible = false;
            // 
            // _frombillTextbox
            // 
            this._frombillTextbox.Location = new System.Drawing.Point(146, 13);
            this._frombillTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._frombillTextbox.Name = "_frombillTextbox";
            this._frombillTextbox.Size = new System.Drawing.Size(367, 22);
            this._frombillTextbox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "รหัสผ่าน :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "รหัสพนักงาน :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "หมายเหตุ :";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "ตั้งแต่เลขที่ใบกำกับภาษี :";
            // 
            // _toBillTextbox
            // 
            this._toBillTextbox.Location = new System.Drawing.Point(146, 39);
            this._toBillTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._toBillTextbox.Name = "_toBillTextbox";
            this._toBillTextbox.Size = new System.Drawing.Size(367, 22);
            this._toBillTextbox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 14);
            this.label5.TabIndex = 20;
            this.label5.Text = "ถึงเลขที่ใบกำกับภาษี :";
            // 
            // _managerPasswordTextbox
            // 
            this._managerPasswordTextbox.Location = new System.Drawing.Point(146, 143);
            this._managerPasswordTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._managerPasswordTextbox.Name = "_managerPasswordTextbox";
            this._managerPasswordTextbox.PasswordChar = '*';
            this._managerPasswordTextbox.Size = new System.Drawing.Size(367, 22);
            this._managerPasswordTextbox.TabIndex = 19;
            // 
            // _managerUserTextbox
            // 
            this._managerUserTextbox.Location = new System.Drawing.Point(146, 117);
            this._managerUserTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._managerUserTextbox.Name = "_managerUserTextbox";
            this._managerUserTextbox.Size = new System.Drawing.Size(367, 22);
            this._managerUserTextbox.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 14);
            this.label6.TabIndex = 23;
            this.label6.Text = "รหัสผ่านผู้จัดการ :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(69, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 14);
            this.label7.TabIndex = 22;
            this.label7.Text = "รหัสผู้จัดการ :";
            // 
            // _posPrintSlipPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 203);
            this.Controls.Add(this._managerPasswordTextbox);
            this.Controls.Add(this._managerUserTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._toBillTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonProcess);
            this.Controls.Add(this._textBoxPassword);
            this.Controls.Add(this._textBoxUserCode);
            this.Controls.Add(this._textBoxRemark);
            this.Controls.Add(this._frombillTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_posPrintSlipPeriod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "พิมพ์ใบเสร็จตามช่วง";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonProcess;
        private System.Windows.Forms.TextBox _textBoxPassword;
        private System.Windows.Forms.TextBox _textBoxUserCode;
        private System.Windows.Forms.TextBox _textBoxRemark;
        private System.Windows.Forms.TextBox _frombillTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _toBillTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _managerPasswordTextbox;
        private System.Windows.Forms.TextBox _managerUserTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}