namespace SMLERPICInfo
{
    partial class _stkProfitChoiceForm
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
            this._select1Button = new MyLib.VistaButton();
            this._select2Button = new MyLib.VistaButton();
            this._selectCancelButton = new MyLib.VistaButton();
            this.SuspendLayout();
            // 
            // _select1Button
            // 
            this._select1Button.BackColor = System.Drawing.Color.Transparent;
            this._select1Button.ButtonText = "B1";
            this._select1Button.Location = new System.Drawing.Point(12, 12);
            this._select1Button.Name = "_select1Button";
            this._select1Button.Size = new System.Drawing.Size(100, 33);
            this._select1Button.TabIndex = 0;
            this._select1Button.UseVisualStyleBackColor = true;
            this._select1Button.Click += new System.EventHandler(this._selectDocumentButton_Click);
            // 
            // _select2Button
            // 
            this._select2Button.BackColor = System.Drawing.Color.Transparent;
            this._select2Button.ButtonText = "B2";
            this._select2Button.Location = new System.Drawing.Point(118, 12);
            this._select2Button.Name = "_select2Button";
            this._select2Button.Size = new System.Drawing.Size(100, 33);
            this._select2Button.TabIndex = 1;
            this._select2Button.UseVisualStyleBackColor = true;
            this._select2Button.Click += new System.EventHandler(this._selectArButton_Click);
            // 
            // _selectCancelButton
            // 
            this._selectCancelButton.BackColor = System.Drawing.Color.Transparent;
            this._selectCancelButton.ButtonText = "Cancel";
            this._selectCancelButton.Location = new System.Drawing.Point(224, 12);
            this._selectCancelButton.Name = "_selectCancelButton";
            this._selectCancelButton.Size = new System.Drawing.Size(100, 33);
            this._selectCancelButton.TabIndex = 2;
            this._selectCancelButton.Text = "Document";
            this._selectCancelButton.UseVisualStyleBackColor = true;
            this._selectCancelButton.Click += new System.EventHandler(this._selectCancelButton_Click);
            // 
            // _stkProfitChoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 58);
            this.ControlBox = false;
            this.Controls.Add(this._selectCancelButton);
            this.Controls.Add(this._select2Button);
            this.Controls.Add(this._select1Button);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_stkProfitChoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select";
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib.VistaButton _select1Button;
        private MyLib.VistaButton _select2Button;
        private MyLib.VistaButton _selectCancelButton;
    }
}