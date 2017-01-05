namespace SMLERPReport.ap
{
    partial class Payable_by_Currency
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
            this._report_ap1 = new SMLERPReport.ap._report_ap();
            this.SuspendLayout();
            // 
            // _report_ap1
            // 
            this._report_ap1._apType = SMLERPReport.ap._report_ap._apEnum.Payable_by_Currency;
            this._report_ap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._report_ap1.Location = new System.Drawing.Point(0, 0);
            this._report_ap1.Name = "_report_ap1";
            this._report_ap1.Size = new System.Drawing.Size(843, 403);
            this._report_ap1.TabIndex = 0;
            // 
            // Payable_by_Currency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._report_ap1);
            this.Name = "Payable_by_Currency";
            this.Size = new System.Drawing.Size(843, 403);
            this.ResumeLayout(false);

        }

        #endregion

        private _report_ap _report_ap1;
    }
}
