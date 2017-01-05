namespace SMLInventoryControl
{
    partial class _icTransItemGridChangeNameForm
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
            this._saveButton = new MyLib.VistaButton();
            this._cancelButton = new MyLib.VistaButton();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "Save (F12)";
            this._saveButton.Location = new System.Drawing.Point(466, 148);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(79, 24);
            this._saveButton.TabIndex = 1;
            this._saveButton.TabStop = false;
            this._saveButton.Text = "Save (F12)";
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // _cancelButton
            // 
            this._cancelButton._drawNewMethod = false;
            this._cancelButton.AutoSize = true;
            this._cancelButton.BackColor = System.Drawing.Color.Transparent;
            this._cancelButton.ButtonText = "Cancel (Esc)";
            this._cancelButton.Location = new System.Drawing.Point(373, 148);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(87, 24);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.TabStop = false;
            this._cancelButton.Text = "Cancel (Esc)";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Location = new System.Drawing.Point(12, 13);
            this._nameTextBox.Multiline = true;
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.Size = new System.Drawing.Size(533, 129);
            this._nameTextBox.TabIndex = 3;
            // 
            // _icTransItemGridChangeNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 184);
            this.Controls.Add(this._nameTextBox);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_icTransItemGridChangeNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton _saveButton;
        private MyLib.VistaButton _cancelButton;
        public System.Windows.Forms.TextBox _nameTextBox;
    }
}