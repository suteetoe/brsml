namespace SMLInventoryControl
{
    partial class _icTransItemGridChangeQtyForm
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
            this._nameNumberBox = new MyLib._myNumberBox();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.AutoSize = true;
            this._cancelButton.BackColor = System.Drawing.Color.Transparent;
            this._cancelButton.ButtonText = "Cancel (Esc)";
            this._cancelButton.Location = new System.Drawing.Point(261, 9);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(87, 24);
            this._cancelButton.TabIndex = 5;
            this._cancelButton.TabStop = false;
            this._cancelButton.Text = "vistaButton1";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "Save (F12)";
            this._saveButton.Location = new System.Drawing.Point(354, 10);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(79, 24);
            this._saveButton.TabIndex = 4;
            this._saveButton.TabStop = false;
            this._saveButton.Text = "vistaButton1";
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // _nameNumberBox
            // 
            this._nameNumberBox._column = 0;
            this._nameNumberBox._default_color = System.Drawing.Color.Black;
            this._nameNumberBox._defaultBackGround = System.Drawing.Color.White;
            this._nameNumberBox._double = 0M;
            this._nameNumberBox._emtry = true;
            this._nameNumberBox._enterToTab = false;
            this._nameNumberBox._format = "";
            this._nameNumberBox._icon = true;
            this._nameNumberBox._iconNumber = 1;
            this._nameNumberBox._isChange = false;
            this._nameNumberBox._isQuery = true;
            this._nameNumberBox._isSearch = false;
            this._nameNumberBox._labelName = "";
            this._nameNumberBox._maxColumn = 0;
            this._nameNumberBox._maxValue = 0M;
            this._nameNumberBox._minValue = 0M;
            this._nameNumberBox._name = null;
            this._nameNumberBox._point = 0M;
            this._nameNumberBox._row = 0;
            this._nameNumberBox._rowCount = 0;
            this._nameNumberBox._textFirst = "";
            this._nameNumberBox._textLast = "";
            this._nameNumberBox._textSecond = "";
            this._nameNumberBox.BackColor = System.Drawing.Color.Transparent;
            this._nameNumberBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._nameNumberBox.ForeColor = System.Drawing.Color.Black;
            this._nameNumberBox.IsUpperCase = false;
            this._nameNumberBox.Location = new System.Drawing.Point(7, 10);
            this._nameNumberBox.Margin = new System.Windows.Forms.Padding(0);
            this._nameNumberBox.MaxLength = 0;
            this._nameNumberBox.Name = "_nameNumberBox";
            this._nameNumberBox.ShowIcon = true;
            this._nameNumberBox.Size = new System.Drawing.Size(249, 22);
            this._nameNumberBox.TabIndex = 6;
            // 
            // _icTransItemGridChangePriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 43);
            this.Controls.Add(this._nameNumberBox);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_icTransItemGridChangePriceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icTransItemGridChangePriceForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton _cancelButton;
        private MyLib.VistaButton _saveButton;
        public MyLib._myNumberBox _nameNumberBox;

    }
}