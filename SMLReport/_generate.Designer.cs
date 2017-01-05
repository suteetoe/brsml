namespace SMLReport
{
    partial class _generate
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
            this._viewControl = new SMLReport._report._view();
            this.SuspendLayout();
            // 
            // _viewControl
            // 
            this._viewControl.BackColor = System.Drawing.Color.Azure;
            this._viewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._viewControl.Location = new System.Drawing.Point(0, 0);
            this._viewControl.Name = "_viewControl";
            this._viewControl.Size = new System.Drawing.Size(884, 611);
            this._viewControl.TabIndex = 0;
            // 
            // _generate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._viewControl);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_generate";
            this.Size = new System.Drawing.Size(884, 611);
            this.ResumeLayout(false);

        }

        #endregion

        public SMLReport._report._view _viewControl;

    }
}
