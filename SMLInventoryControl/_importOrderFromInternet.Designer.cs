namespace SMLInventoryControl
{
    partial class _importOrderFromInternet
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
            this.label1 = new System.Windows.Forms.Label();
            this._orderNumberTextbox = new System.Windows.Forms.TextBox();
            this._importButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Number :";
            // 
            // _orderNumberTextbox
            // 
            this._orderNumberTextbox.Location = new System.Drawing.Point(97, 12);
            this._orderNumberTextbox.Name = "_orderNumberTextbox";
            this._orderNumberTextbox.Size = new System.Drawing.Size(169, 20);
            this._orderNumberTextbox.TabIndex = 1;
            // 
            // _importButton
            // 
            this._importButton.Location = new System.Drawing.Point(191, 38);
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(75, 23);
            this._importButton.TabIndex = 2;
            this._importButton.Text = "Import";
            this._importButton.UseVisualStyleBackColor = true;
            // 
            // _importOrderFromInternet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 69);
            this.Controls.Add(this._importButton);
            this.Controls.Add(this._orderNumberTextbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_importOrderFromInternet";
            this.Text = "Import Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox _orderNumberTextbox;
        public System.Windows.Forms.Button _importButton;
    }
}