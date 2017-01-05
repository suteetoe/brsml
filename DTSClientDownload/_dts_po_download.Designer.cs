namespace DTSClientDownload
{
    partial class _dts_po_download
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
            this._transDownloadControl1 = new DTSClientDownload._transDownloadControl();
            this.SuspendLayout();
            // 
            // _transDownloadControl1
            // 
            this._transDownloadControl1._screen_type = DTSClientDownload._screenDownloadEnum.PO;
            this._transDownloadControl1.BackColor = System.Drawing.Color.Transparent;
            this._transDownloadControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transDownloadControl1.Location = new System.Drawing.Point(0, 0);
            this._transDownloadControl1.Name = "_transDownloadControl1";
            this._transDownloadControl1.Size = new System.Drawing.Size(774, 774);
            this._transDownloadControl1.TabIndex = 0;
            // 
            // _dts_po_download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
            this.Controls.Add(this._transDownloadControl1);
            this.Name = "_dts_po_download";
            this.Size = new System.Drawing.Size(774, 774);
            this.ResumeLayout(false);

        }

        #endregion

        private _transDownloadControl _transDownloadControl1;

    }
}
