namespace SMLInventoryControl
{
    partial class _icTransItemGridChangeDiscountForm
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
            this._cancelButton = new MyLib.VistaButton();
            this._saveButton = new MyLib.VistaButton();
            this._nameTextBox = new MyLib._myTextBox();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.AutoSize = true;
            this._cancelButton.BackColor = System.Drawing.Color.Transparent;
            this._cancelButton.ButtonText = "Cancel (Esc)";
            this._cancelButton.Location = new System.Drawing.Point(191, 34);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(81, 24);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.TabStop = false;
            this._cancelButton.Text = "vistaButton1";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "Save (F12)";
            this._saveButton.Location = new System.Drawing.Point(284, 34);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(74, 24);
            this._saveButton.TabIndex = 4;
            this._saveButton.TabStop = false;
            this._saveButton.Text = "vistaButton1";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _nameTextBox
            // 
            this._nameTextBox._column = 0;
            this._nameTextBox._defaultBackGround = System.Drawing.Color.White;
            this._nameTextBox._emtry = true;
            this._nameTextBox._enterToTab = false;
            this._nameTextBox._icon = false;
            this._nameTextBox._iconNumber = 1;
            this._nameTextBox._isChange = false;
            this._nameTextBox._isQuery = true;
            this._nameTextBox._isSearch = false;
            this._nameTextBox._labelName = "";
            this._nameTextBox._maxColumn = 0;
            this._nameTextBox._name = null;
            this._nameTextBox._row = 0;
            this._nameTextBox._rowCount = 0;
            this._nameTextBox._textFirst = "";
            this._nameTextBox._textLast = "";
            this._nameTextBox._textSecond = "";
            this._nameTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._nameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._nameTextBox.ForeColor = System.Drawing.Color.Black;
            this._nameTextBox.IsUpperCase = false;
            this._nameTextBox.Location = new System.Drawing.Point(9, 9);
            this._nameTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._nameTextBox.MaxLength = 0;
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.ShowIcon = false;
            this._nameTextBox.Size = new System.Drawing.Size(353, 22);
            this._nameTextBox.TabIndex = 3;
            // 
            // _icTransItemGridChangeDiscountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 70);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._nameTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_icTransItemGridChangeDiscountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icTransItemGridChangeDiscountForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton _cancelButton;
        private MyLib.VistaButton _saveButton;
        public MyLib._myTextBox _nameTextBox;
    }
}