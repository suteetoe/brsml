namespace SMLERPTemplate
{
    partial class _seletMenuGroupForm
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
            this._listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _listBox
            // 
            this._listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBox.FormattingEnabled = true;
            this._listBox.ItemHeight = 15;
            this._listBox.Location = new System.Drawing.Point(0, 0);
            this._listBox.Name = "_listBox";
            this._listBox.Size = new System.Drawing.Size(227, 269);
            this._listBox.TabIndex = 0;
            // 
            // _seletMenuGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 269);
            this.Controls.Add(this._listBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_seletMenuGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Menu Group";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox _listBox;

    }
}