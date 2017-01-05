namespace SMLERPControl
{
    partial class _barcodeSearchForm
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
            this._labelBarcode = new System.Windows.Forms.Label();
            this._textBoxBarcode = new System.Windows.Forms.TextBox();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _labelBarcode
            // 
            this._labelBarcode.AutoSize = true;
            this._labelBarcode.Location = new System.Drawing.Point(93, 34);
            this._labelBarcode.Name = "_labelBarcode";
            this._labelBarcode.Size = new System.Drawing.Size(51, 14);
            this._labelBarcode.TabIndex = 0;
            this._labelBarcode.Text = "Barcode";
            // 
            // _textBoxBarcode
            // 
            this._textBoxBarcode.Location = new System.Drawing.Point(96, 9);
            this._textBoxBarcode.Name = "_textBoxBarcode";
            this._textBoxBarcode.Size = new System.Drawing.Size(279, 22);
            this._textBoxBarcode.TabIndex = 1;
            // 
            // _pictureBox
            // 
            this._pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBox.Image = global::SMLERPControl.Properties.Resources.barcode;
            this._pictureBox.Location = new System.Drawing.Point(12, 9);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(75, 39);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 2;
            this._pictureBox.TabStop = false;
            // 
            // _barcodeSearchForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(384, 63);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._textBoxBarcode);
            this.Controls.Add(this._labelBarcode);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_barcodeSearchForm";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pictureBox;
        public System.Windows.Forms.TextBox _textBoxBarcode;
        public System.Windows.Forms.Label _labelBarcode;
    }
}