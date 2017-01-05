namespace SMLERPAPARControl._report_ar_ap
{
    partial class _view
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_view));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this._preview = new SMLERPAPARControl._report_ar_ap._frmPrintPreviewDialogControl();
            this.SuspendLayout();
            // 
            // _preview
            // 
            this._preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._preview.Document = null;
            this._preview.Location = new System.Drawing.Point(0, 0);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(913, 480);
            this._preview.TabIndex = 0;
            // 
            // _view
            // 
            this.ClientSize = new System.Drawing.Size(913, 480);
            this.Controls.Add(this._preview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.Name = "_view";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private _frmPrintPreviewDialogControl _preview;
    }
}