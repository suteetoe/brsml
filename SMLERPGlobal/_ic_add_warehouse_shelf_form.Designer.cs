namespace _g
{
    partial class _ic_add_warehouse_shelf_form
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
            this._descTextBox = new System.Windows.Forms.TextBox();
            this._processButton = new MyLib.VistaButton();
            this._statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _descTextBox
            // 
            this._descTextBox.Location = new System.Drawing.Point(13, 13);
            this._descTextBox.Multiline = true;
            this._descTextBox.Name = "_descTextBox";
            this._descTextBox.Size = new System.Drawing.Size(343, 126);
            this._descTextBox.TabIndex = 0;
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(295, 145);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(61, 24);
            this._processButton.TabIndex = 1;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Location = new System.Drawing.Point(12, 150);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(0, 14);
            this._statusLabel.TabIndex = 2;
            // 
            // _ic_add_warehouse_shelf_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 182);
            this.Controls.Add(this._statusLabel);
            this.Controls.Add(this._processButton);
            this.Controls.Add(this._descTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "_ic_add_warehouse_shelf_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Warehouse and Shelf Auto";
            this.Load += new System.EventHandler(this._ic_add_warehouse_shelf_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _descTextBox;
        private MyLib.VistaButton _processButton;
        private System.Windows.Forms.Label _statusLabel;
    }
}