namespace SMLERPICReport
{
    partial class _report_ic_color_movement
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
            this._viewReport = new SMLReport._report._view();
            this.SuspendLayout();
            // 
            // _viewReport
            // 
            this._viewReport.BackColor = System.Drawing.Color.Azure;
            this._viewReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._viewReport.Location = new System.Drawing.Point(0, 0);
            this._viewReport.Name = "_viewReport";
            this._viewReport.Size = new System.Drawing.Size(1081, 574);
            this._viewReport.TabIndex = 1;
            // 
            // _report_ic_color_movement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._viewReport);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_report_ic_color_movement";
            this.Size = new System.Drawing.Size(1081, 574);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLReport._report._view _viewReport;
    }
}
