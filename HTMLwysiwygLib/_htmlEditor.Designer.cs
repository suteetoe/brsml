namespace HTMLwysiwygLib
{
    partial class _htmlEditor
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
            this._browser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._previewButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _browser
            // 
            this._browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._browser.Location = new System.Drawing.Point(0, 25);
            this._browser.MinimumSize = new System.Drawing.Size(20, 20);
            this._browser.Name = "_browser";
            this._browser.Size = new System.Drawing.Size(713, 762);
            this._browser.TabIndex = 0;
            this._browser.Url = new System.Uri("http://www.smlsoft.com/editor/", System.UriKind.Absolute);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(713, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::HTMLwysiwygLib.Properties.Resources.document_view;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(68, 22);
            this._previewButton.Text = "Preview";
            // 
            // _htmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._browser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_htmlEditor";
            this.Size = new System.Drawing.Size(713, 787);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser _browser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton _previewButton;
    }
}
