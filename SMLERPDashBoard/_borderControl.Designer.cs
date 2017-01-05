namespace SMLERPDashBoard
{
    partial class _borderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._closePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._closePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _closePictureBox
            // 
            this._closePictureBox.BackColor = System.Drawing.Color.Transparent;
            this._closePictureBox.Image = global::SMLERPDashBoard.Properties.Resources.error;
            this._closePictureBox.Location = new System.Drawing.Point(496, 8);
            this._closePictureBox.Name = "_closePictureBox";
            this._closePictureBox.Size = new System.Drawing.Size(16, 16);
            this._closePictureBox.TabIndex = 0;
            this._closePictureBox.TabStop = false;
            // 
            // _borderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._closePictureBox);
            this.Name = "_borderControl";
            this.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.Size = new System.Drawing.Size(520, 437);
            ((System.ComponentModel.ISupportInitialize)(this._closePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox _closePictureBox;

    }
}
