namespace SMLHTMLEditorTest
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._getHtml = new System.Windows.Forms.ToolStripButton();
            this.htmlwysiwyg1 = new HTMLwysiwygLib.htmlwysiwyg();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._getHtml});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1057, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _getHtml
            // 
            this._getHtml.Image = ((System.Drawing.Image)(resources.GetObject("_getHtml.Image")));
            this._getHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._getHtml.Name = "_getHtml";
            this._getHtml.Size = new System.Drawing.Size(81, 22);
            this._getHtml.Text = "Get HTML";
            this._getHtml.Click += new System.EventHandler(this._getHtml_Click);
            // 
            // htmlwysiwyg1
            // 
            this.htmlwysiwyg1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.htmlwysiwyg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlwysiwyg1.Location = new System.Drawing.Point(0, 25);
            this.htmlwysiwyg1.Name = "htmlwysiwyg1";
            //this.htmlwysiwyg1.ShowAlignCenterButton = true;
            //this.htmlwysiwyg1.ShowAlignLeftButton = true;
            //this.htmlwysiwyg1.ShowAlignRightButton = true;
            //this.htmlwysiwyg1.ShowBackColorButton = true;
            //this.htmlwysiwyg1.ShowBolButton = true;
            //this.htmlwysiwyg1.ShowBulletButton = true;
            //this.htmlwysiwyg1.ShowCopyButton = true;
            //this.htmlwysiwyg1.ShowCutButton = true;
            //this.htmlwysiwyg1.ShowFontFamilyButton = true;
            //this.htmlwysiwyg1.ShowFontSizeButton = true;
            //this.htmlwysiwyg1.ShowIndentButton = true;
            //this.htmlwysiwyg1.ShowItalicButton = true;
            //this.htmlwysiwyg1.ShowJustifyButton = true;
            //this.htmlwysiwyg1.ShowLinkButton = true;
            //this.htmlwysiwyg1.ShowNewButton = true;
            //this.htmlwysiwyg1.ShowOrderedListButton = true;
            //this.htmlwysiwyg1.ShowOutdentButton = true;
            //this.htmlwysiwyg1.ShowPasteButton = true;
            //this.htmlwysiwyg1.ShowPrintButton = true;
            //this.htmlwysiwyg1.ShowTxtBGButton = true;
            //this.htmlwysiwyg1.ShowTxtColorButton = true;
            //this.htmlwysiwyg1.ShowUnderlineButton = true;
            //this.htmlwysiwyg1.ShowUnlinkButton = true;
            this.htmlwysiwyg1.Size = new System.Drawing.Size(1057, 403);
            this.htmlwysiwyg1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 428);
            this.Controls.Add(this.htmlwysiwyg1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _getHtml;
        private HTMLwysiwygLib.htmlwysiwyg htmlwysiwyg1;

    }
}

