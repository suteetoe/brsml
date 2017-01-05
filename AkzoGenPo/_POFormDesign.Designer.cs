namespace AkzoGenPo
{
    partial class _POFormDesign
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
            this._podesign1 = new AkzoGenPo._podesign();
            this.SuspendLayout();
            // 
            // _podesign1
            // 
            this._podesign1.BackColor = System.Drawing.SystemColors.Window;
            this._podesign1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._podesign1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._podesign1.FormCode = null;
            this._podesign1.FormName = null;
            this._podesign1.Location = new System.Drawing.Point(0, 0);
            this._podesign1.Margin = new System.Windows.Forms.Padding(0);
            this._podesign1.Name = "_podesign1";
            this._podesign1.Size = new System.Drawing.Size(1124, 802);
            this._podesign1.TabIndex = 0;
            // 
            // _POFormDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 802);
            this.Controls.Add(this._podesign1);
            this.Name = "_POFormDesign";
            this.Text = "Design PO Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private _podesign _podesign1;
    }
}