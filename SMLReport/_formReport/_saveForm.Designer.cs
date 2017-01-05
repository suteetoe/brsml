namespace SMLReport._formReport
{
    partial class _saveForm
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
            this._buttonCancle = new System.Windows.Forms.Button();
            this._buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._saveFormCode = new System.Windows.Forms.TextBox();
            this._saveFormName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._formTypeCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _buttonCancle
            // 
            this._buttonCancle.Image = global::SMLReport.Resource16x16.error;
            this._buttonCancle.Location = new System.Drawing.Point(299, 91);
            this._buttonCancle.Name = "_buttonCancle";
            this._buttonCancle.Size = new System.Drawing.Size(75, 23);
            this._buttonCancle.TabIndex = 1;
            this._buttonCancle.Text = "Cancle";
            this._buttonCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonCancle.UseVisualStyleBackColor = true;
            this._buttonCancle.Click += new System.EventHandler(this._buttonCancle_Click);
            // 
            // _buttonSave
            // 
            this._buttonSave.AutoSize = true;
            this._buttonSave.Image = global::SMLReport.Resource16x16.disk_blue;
            this._buttonSave.Location = new System.Drawing.Point(218, 91);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(75, 23);
            this._buttonSave.TabIndex = 0;
            this._buttonSave.Text = "Save";
            this._buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonSave.UseVisualStyleBackColor = true;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Code : ";
            // 
            // _saveFormCode
            // 
            this._saveFormCode.Location = new System.Drawing.Point(62, 12);
            this._saveFormCode.Name = "_saveFormCode";
            this._saveFormCode.Size = new System.Drawing.Size(77, 20);
            this._saveFormCode.TabIndex = 3;
            // 
            // _saveFormName
            // 
            this._saveFormName.Location = new System.Drawing.Point(62, 38);
            this._saveFormName.Name = "_saveFormName";
            this._saveFormName.Size = new System.Drawing.Size(312, 20);
            this._saveFormName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Type : ";
            // 
            // _formTypeCombobox
            // 
            this._formTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._formTypeCombobox.FormattingEnabled = true;
            this._formTypeCombobox.Location = new System.Drawing.Point(62, 64);
            this._formTypeCombobox.Name = "_formTypeCombobox";
            this._formTypeCombobox.Size = new System.Drawing.Size(312, 21);
            this._formTypeCombobox.TabIndex = 7;
            // 
            // _saveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 120);
            this.ControlBox = false;
            this.Controls.Add(this._formTypeCombobox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._saveFormName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._saveFormCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonCancle);
            this.Controls.Add(this._buttonSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_saveForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save As ...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonSave;
        private System.Windows.Forms.Button _buttonCancle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _saveFormCode;
        public System.Windows.Forms.TextBox _saveFormName;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox _formTypeCombobox;
    }
}