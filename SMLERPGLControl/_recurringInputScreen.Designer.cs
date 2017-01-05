namespace SMLERPGLControl
{
    partial class _recurringInputScreen
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
            this._myLabel1 = new MyLib._myLabel();
            this._codeTextBox = new MyLib._myTextBox();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._saveButton = new MyLib._myButton();
            this._cancelButton = new MyLib._myButton();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(7, 11);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "กรุณาบันทึกรหัสที่ต้องการ";
            this._myLabel1.Size = new System.Drawing.Size(132, 13);
            this._myLabel1.TabIndex = 3;
            this._myLabel1.Text = "กรุณาบันทึกรหัสที่ต้องการ :";
            this._myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _codeTextBox
            // 
            this._codeTextBox._column = 0;
            this._codeTextBox._defaultBackGround = System.Drawing.Color.White;
            this._codeTextBox._emtry = true;
            this._codeTextBox._enterToTab = true;
            this._codeTextBox._icon = false;
            this._codeTextBox._isQuery = true;
            this._codeTextBox._isSearch = false;
            this._codeTextBox._labelName = "";
            this._codeTextBox._maxColumn = 0;
            this._codeTextBox._name = null;
            this._codeTextBox._row = 0;
            this._codeTextBox._rowCount = 0;
            this._codeTextBox._textFirst = "";
            this._codeTextBox._textLast = "";
            this._codeTextBox._textSecond = "";
            this._codeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._codeTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._codeTextBox.ForeColor = System.Drawing.Color.Black;
            this._codeTextBox._iconNumber = 1;
            this._codeTextBox.Location = new System.Drawing.Point(140, 9);
            this._codeTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._codeTextBox.MaxLength = 0;
            this._codeTextBox.Name = "_codeTextBox";
            this._codeTextBox.ShowIcon = false;
            this._codeTextBox.Size = new System.Drawing.Size(143, 21);
            this._codeTextBox.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._saveButton);
            this._myFlowLayoutPanel1.Controls.Add(this._cancelButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(292, 31);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.AutoSize = true;
            this._saveButton.Location = new System.Drawing.Point(196, 3);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.Size = new System.Drawing.Size(93, 25);
            this._saveButton.TabIndex = 1;
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.myUseVisualStyleBackColor = true;
            // 
            // _cancelButton
            // 
            this._cancelButton.AutoSize = true;
            this._cancelButton.Location = new System.Drawing.Point(112, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Padding = new System.Windows.Forms.Padding(1);
            this._cancelButton.Size = new System.Drawing.Size(78, 25);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "ยกเลิก (Esc)";
            this._cancelButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.myUseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _recurringInputScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(292, 71);
            this.Controls.Add(this._myLabel1);
            this.Controls.Add(this._codeTextBox);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_recurringInputScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this._recurringInputScreen_Load);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myLabel _myLabel1;
        public MyLib._myTextBox _codeTextBox;
        public MyLib._myButton _saveButton;
        public MyLib._myButton _cancelButton;
    }
}