namespace MyLib
{
    partial class _myTextBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_myTextBox));
            this.textBox = new System.Windows.Forms.TextBox();
            this._iconSearch = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._iconSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox.ForeColor = System.Drawing.Color.Black;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Margin = new System.Windows.Forms.Padding(0);
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(92, 21);
            this.textBox.TabIndex = 0;
            // 
            // _iconSearch
            // 
            this._iconSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_iconSearch.BackgroundImage")));
            this._iconSearch.Location = new System.Drawing.Point(92, 0);
            this._iconSearch.Margin = new System.Windows.Forms.Padding(0);
            this._iconSearch.Name = "_iconSearch";
            this._iconSearch.Size = new System.Drawing.Size(18, 21);
            this._iconSearch.TabIndex = 1;
            this._iconSearch.TabStop = false;
            this._iconSearch.Click += new System.EventHandler(this.IconSearch_Click);
            // 
            // _myTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this._iconSearch);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "_myTextBox";
            this.Size = new System.Drawing.Size(143, 21);
            this.Load += new System.EventHandler(this._myTextBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this._iconSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.PictureBox _iconSearch;
    }
}
