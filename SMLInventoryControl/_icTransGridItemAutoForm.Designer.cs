namespace SMLInventoryControl
{
    partial class _icTransGridItemAutoForm
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
            this._barcodeTextBox = new System.Windows.Forms.TextBox();
            this._lastBarcodeLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Barcode :";
            // 
            // _barcodeTextBox
            // 
            this._barcodeTextBox.Location = new System.Drawing.Point(174, 15);
            this._barcodeTextBox.Name = "_barcodeTextBox";
            this._barcodeTextBox.Size = new System.Drawing.Size(278, 22);
            this._barcodeTextBox.TabIndex = 1;
            // 
            // _lastBarcodeLabel
            // 
            this._lastBarcodeLabel.AutoSize = true;
            this._lastBarcodeLabel.Location = new System.Drawing.Point(171, 47);
            this._lastBarcodeLabel.Name = "_lastBarcodeLabel";
            this._lastBarcodeLabel.Size = new System.Drawing.Size(19, 14);
            this._lastBarcodeLabel.TabIndex = 2;
            this._lastBarcodeLabel.Text = "...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::SMLInventoryControl.Properties.Resources.barcode;
            this.pictureBox1.Location = new System.Drawing.Point(9, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // _icTransGridItemAutoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(462, 70);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._lastBarcodeLabel);
            this.Controls.Add(this._barcodeTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_icTransGridItemAutoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Input by Barcode";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox _barcodeTextBox;
        private System.Windows.Forms.Label _lastBarcodeLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}