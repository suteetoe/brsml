namespace SMLERPAPARControl
{
    partial class _refDocControl
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
            this._grouper1 = new MyLib._grouper();
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._tsbProcess = new System.Windows.Forms.ToolStripButton();
            this._grouper1.SuspendLayout();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.Silver;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._toolBar);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(0, 0);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(3);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.Transparent;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 1;
            this._grouper1.Size = new System.Drawing.Size(787, 497);
            this._grouper1.TabIndex = 4;
            // 
            // _toolBar
            // 
            this._toolBar.BackgroundImage = global::SMLERPAPARControl.Properties.Resources.bt03;
            this._toolBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._toolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbProcess});
            this._toolBar.Location = new System.Drawing.Point(3, 469);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(781, 25);
            this._toolBar.TabIndex = 4;
            this._toolBar.Text = "toolStrip1";
            // 
            // _tsbProcess
            // 
            this._tsbProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._tsbProcess.Name = "_tsbProcess";
            this._tsbProcess.Size = new System.Drawing.Size(52, 22);
            this._tsbProcess.Text = "Process";
            // 
            // _refDocControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._grouper1);
            this.Name = "_refDocControl";
            this.Size = new System.Drawing.Size(787, 497);
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._grouper _grouper1;
        private System.Windows.Forms.ToolStrip _toolBar;
        public System.Windows.Forms.ToolStripButton _tsbProcess;

    }
}
