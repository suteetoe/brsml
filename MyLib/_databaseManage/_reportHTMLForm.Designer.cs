namespace MyLib._databaseManage
{
    partial class _reportHTMLForm
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
            this._htmlWebBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonPrint = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _htmlWebBrowser
            // 
            this._htmlWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._htmlWebBrowser.Location = new System.Drawing.Point(0, 25);
            this._htmlWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._htmlWebBrowser.Name = "_htmlWebBrowser";
            this._htmlWebBrowser.Size = new System.Drawing.Size(635, 376);
            this._htmlWebBrowser.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(635, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonPrint
            // 
            this._buttonPrint.Image = global::MyLib.Properties.Resources.printer;
            this._buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrint.Name = "_buttonPrint";
            this._buttonPrint.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPrint.ResourceName = "";
            this._buttonPrint.Size = new System.Drawing.Size(52, 22);
            this._buttonPrint.Text = "พิมพ์";
            this._buttonPrint.Click += new System.EventHandler(this._buttonPrint_Click);
            // 
            // _reportHTMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 401);
            this.Controls.Add(this._htmlWebBrowser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_reportHTMLForm";
            this.Text = "_reportHTMLForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripMyButton _buttonPrint;
        public System.Windows.Forms.WebBrowser _htmlWebBrowser;
    }
}