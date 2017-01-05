namespace _g
{
    partial class _docCancelForm
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
            this._textDocNo = new System.Windows.Forms.TextBox();
            this._comboCancelConfirm = new System.Windows.Forms.ComboBox();
            this._myLabel5 = new MyLib._myLabel();
            this._myLabel4 = new MyLib._myLabel();
            this._myLabel3 = new MyLib._myLabel();
            this.vistaButton2 = new MyLib.VistaButton();
            this.vistaButton1 = new MyLib.VistaButton();
            this._myLabel1 = new MyLib._myLabel();
            this._cancelReasonTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _textDocNo
            // 
            this._textDocNo.Enabled = false;
            this._textDocNo.Location = new System.Drawing.Point(237, 43);
            this._textDocNo.Name = "_textDocNo";
            this._textDocNo.Size = new System.Drawing.Size(192, 22);
            this._textDocNo.TabIndex = 7;
            // 
            // _comboCancelConfirm
            // 
            this._comboCancelConfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboCancelConfirm.FormattingEnabled = true;
            this._comboCancelConfirm.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this._comboCancelConfirm.Location = new System.Drawing.Point(237, 71);
            this._comboCancelConfirm.Name = "_comboCancelConfirm";
            this._comboCancelConfirm.Size = new System.Drawing.Size(192, 22);
            this._comboCancelConfirm.TabIndex = 1;
            // 
            // _myLabel5
            // 
            this._myLabel5.Location = new System.Drawing.Point(15, 72);
            this._myLabel5.Name = "_myLabel5";
            this._myLabel5.ResourceName = "ท่านต้องการยกเลิกเอกสารนี้จริงหรือไม่ :";
            this._myLabel5.Size = new System.Drawing.Size(216, 19);
            this._myLabel5.TabIndex = 6;
            this._myLabel5.Text = "ท่านต้องการยกเลิกเอกสารนี้จริงหรือไม่ :";
            this._myLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _myLabel4
            // 
            this._myLabel4.Location = new System.Drawing.Point(15, 44);
            this._myLabel4.Name = "_myLabel4";
            this._myLabel4.ResourceName = "เอกสารที่ต้องการยกเลิก :";
            this._myLabel4.Size = new System.Drawing.Size(216, 19);
            this._myLabel4.TabIndex = 5;
            this._myLabel4.Text = "เอกสารที่ต้องการยกเลิก :";
            this._myLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _myLabel3
            // 
            this._myLabel3.Location = new System.Drawing.Point(17, 9);
            this._myLabel3.Name = "_myLabel3";
            this._myLabel3.ResourceName = "ต้องการยกเลิกเอกสารดังต่อไปนี้หรือไม่ กรุณาตรวจสอบให้แน่ใจอีกครั้งก่อนยกเลิก เพือ" +
    "ให้แน่ใจกรุณาเลือกคำว่า YES ในช่องยืนยัน ก่อนกดที่ปุ่มตกลง";
            this._myLabel3.Size = new System.Drawing.Size(415, 40);
            this._myLabel3.TabIndex = 4;
            this._myLabel3.Text = "ต้องการยกเลิกเอกสารดังต่อไปนี้หรือไม่ กรุณาตรวจสอบให้แน่ใจอีกครั้งก่อนยกเลิก เพือ" +
    "ให้แน่ใจกรุณาเลือกคำว่า YES ในช่องยืนยัน ก่อนกดที่ปุ่มตกลง";
            // 
            // vistaButton2
            // 
            this.vistaButton2._drawNewMethod = false;
            this.vistaButton2.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton2.ButtonText = "ยกเลิก";
            this.vistaButton2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.vistaButton2.Location = new System.Drawing.Point(315, 174);
            this.vistaButton2.Name = "vistaButton2";
            this.vistaButton2.Size = new System.Drawing.Size(117, 28);
            this.vistaButton2.TabIndex = 2;
            this.vistaButton2.Text = "vistaButton2";
            this.vistaButton2.UseVisualStyleBackColor = false;
            // 
            // vistaButton1
            // 
            this.vistaButton1._drawNewMethod = false;
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "ตกลง";
            this.vistaButton1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.vistaButton1.Location = new System.Drawing.Point(192, 174);
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(117, 28);
            this.vistaButton1.TabIndex = 3;
            this.vistaButton1.Text = "vistaButton1";
            this.vistaButton1.UseVisualStyleBackColor = false;
            // 
            // _myLabel1
            // 
            this._myLabel1.Location = new System.Drawing.Point(9, 91);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "เหตุผลการยกเลิก :";
            this._myLabel1.Size = new System.Drawing.Size(133, 19);
            this._myLabel1.TabIndex = 8;
            this._myLabel1.Text = "เหตุผลการยกเลิก :";
            this._myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cancelReasonTextbox
            // 
            this._cancelReasonTextbox.Location = new System.Drawing.Point(12, 113);
            this._cancelReasonTextbox.Multiline = true;
            this._cancelReasonTextbox.Name = "_cancelReasonTextbox";
            this._cancelReasonTextbox.Size = new System.Drawing.Size(420, 55);
            this._cancelReasonTextbox.TabIndex = 9;
            // 
            // _docCancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 209);
            this.Controls.Add(this._cancelReasonTextbox);
            this.Controls.Add(this._myLabel1);
            this.Controls.Add(this._comboCancelConfirm);
            this.Controls.Add(this._textDocNo);
            this.Controls.Add(this._myLabel5);
            this.Controls.Add(this._myLabel4);
            this.Controls.Add(this._myLabel3);
            this.Controls.Add(this.vistaButton2);
            this.Controls.Add(this.vistaButton1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_docCancelForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancel Document";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton vistaButton1;
        private MyLib.VistaButton vistaButton2;
        private MyLib._myLabel _myLabel3;
        private MyLib._myLabel _myLabel4;
        private MyLib._myLabel _myLabel5;
        private System.Windows.Forms.TextBox _textDocNo;
        public System.Windows.Forms.ComboBox _comboCancelConfirm;
        private MyLib._myLabel _myLabel1;
        public System.Windows.Forms.TextBox _cancelReasonTextbox;
    }
}