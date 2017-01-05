namespace SMLSyncCheckingTools
{
    partial class _updateQueryForm
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
            this._queryTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _queryTextbox
            // 
            this._queryTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryTextbox.Location = new System.Drawing.Point(0, 0);
            this._queryTextbox.Multiline = true;
            this._queryTextbox.Name = "_queryTextbox";
            this._queryTextbox.Size = new System.Drawing.Size(742, 740);
            this._queryTextbox.TabIndex = 0;
            // 
            // _updateQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 740);
            this.Controls.Add(this._queryTextbox);
            this.Name = "_updateQueryForm";
            this.Text = "_updateQueryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox _queryTextbox;
    }
}