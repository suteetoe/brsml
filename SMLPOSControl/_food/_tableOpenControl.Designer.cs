namespace SMLPOSControl._food
{
    partial class _tableOpenControl
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
            this._customerCountTextbox = new System.Windows.Forms.TextBox();
            this._saveButton = new MyLib.VistaButton();
            this.SuspendLayout();
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(24, 16);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "จำนวนลูกค้า :";
            this._myLabel1.Size = new System.Drawing.Size(75, 14);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "จำนวนลูกค้า :";
            // 
            // _customerCountTextbox
            // 
            this._customerCountTextbox.Location = new System.Drawing.Point(105, 13);
            this._customerCountTextbox.Name = "_customerCountTextbox";
            this._customerCountTextbox.Size = new System.Drawing.Size(221, 22);
            this._customerCountTextbox.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "ตกลง";
            this._saveButton.Location = new System.Drawing.Point(226, 41);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(100, 32);
            this._saveButton.TabIndex = 3;
            this._saveButton.Text = "vistaButton2";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _tableOpenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 83);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._customerCountTextbox);
            this.Controls.Add(this._myLabel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_tableOpenControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เปิดโต๊ะ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton _saveButton;
        public System.Windows.Forms.TextBox _customerCountTextbox;
        public MyLib._myLabel _myLabel1;
    }
}