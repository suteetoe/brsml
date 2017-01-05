namespace SMLReport._design
{
    partial class _linkList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_linkList));
            this._vScrollBar = new System.Windows.Forms.VScrollBar();
            this._hScrollBar = new System.Windows.Forms.HScrollBar();
            this._panel = new System.Windows.Forms.Panel();
            this._printDocument = new System.Drawing.Printing.PrintDocument();
            this._printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this._pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.SuspendLayout();
            // 
            // _vScrollBar
            // 
            this._vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this._vScrollBar.Location = new System.Drawing.Point(459, 0);
            this._vScrollBar.Name = "_vScrollBar";
            this._vScrollBar.Size = new System.Drawing.Size(17, 300);
            this._vScrollBar.TabIndex = 1;
            // 
            // _hScrollBar
            // 
            this._hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._hScrollBar.Location = new System.Drawing.Point(0, 300);
            this._hScrollBar.Name = "_hScrollBar";
            this._hScrollBar.Size = new System.Drawing.Size(476, 17);
            this._hScrollBar.SmallChange = 10;
            this._hScrollBar.TabIndex = 2;
            // 
            // _panel
            // 
            this._panel.BackColor = System.Drawing.Color.White;
            this._panel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._panel.Location = new System.Drawing.Point(24, 12);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(358, 242);
            this._panel.TabIndex = 0;
            // 
            // _printPreviewDialog
            // 
            this._printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this._printPreviewDialog.Document = this._printDocument;
            this._printPreviewDialog.Enabled = true;
            this._printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("_printPreviewDialog.Icon")));
            this._printPreviewDialog.Name = "_printPreviewDialog";
            this._printPreviewDialog.Visible = false;
            // 
            // _pageSetupDialog
            // 
            this._pageSetupDialog.Document = this._printDocument;
            // 
            // _linkList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._vScrollBar);
            this.Controls.Add(this._hScrollBar);
            this.Controls.Add(this._panel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_linkList";
            this.Size = new System.Drawing.Size(476, 317);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar _vScrollBar;
        private System.Windows.Forms.HScrollBar _hScrollBar;
        public System.Windows.Forms.Panel _panel;
        private System.Drawing.Printing.PrintDocument _printDocument;
        private System.Windows.Forms.PrintPreviewDialog _printPreviewDialog;
        private System.Windows.Forms.PageSetupDialog _pageSetupDialog;
    }
}
