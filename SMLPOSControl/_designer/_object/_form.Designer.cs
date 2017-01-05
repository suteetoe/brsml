namespace SMLPOSControl._designer._object
{
    partial class _form
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
            this._formlayout = new System.Windows.Forms.Panel();
            this._drawPanel = new SMLPOSControl._designer._drawPanel();
            this.SuspendLayout();
            // 
            // _formlayout
            // 
            this._formlayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formlayout.Location = new System.Drawing.Point(0, 0);
            this._formlayout.Name = "_formlayout";
            this._formlayout.Size = new System.Drawing.Size(425, 281);
            this._formlayout.TabIndex = 2;
            // 
            // _drawPanel
            // 
            this._drawPanel._activeTool = SMLReport._design._drawToolType.Pointer;
            this._drawPanel._drawNetRectangle = false;
            this._drawPanel._drawScale = 1F;
            this._drawPanel._netRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this._drawPanel._showHighlightTextField = false;
            this._drawPanel.BackColor = System.Drawing.Color.Transparent;
            this._drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._drawPanel.Location = new System.Drawing.Point(0, 0);
            this._drawPanel.Name = "_drawPanel";
            this._drawPanel.Size = new System.Drawing.Size(425, 281);
            this._drawPanel.TabIndex = 1;
            // 
            // _form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(425, 281);
            this.Controls.Add(this._drawPanel);
            this.Controls.Add(this._formlayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "_form";
            this.Text = "_form";
            this.ResumeLayout(false);

        }

        #endregion

        public _drawPanel _drawPanel;
        private System.Windows.Forms.Panel _formlayout;
    }
}