namespace SMLERPReport.cash_bank
{
    partial class Book_Bank_Balance
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
            this._report_cash_bank1 = new SMLERPReport.cash_bank._report_cash_bank();
            this.SuspendLayout();
            // 
            // _report_cash_bank1
            // 
            this._report_cash_bank1.Cash_bank = SMLERPReport.cash_bank._report_cash_bank._cash_bank_Enum.Book_Bank_Balance;
            this._report_cash_bank1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._report_cash_bank1.Location = new System.Drawing.Point(0, 0);
            this._report_cash_bank1.Name = "_report_cash_bank1";
            this._report_cash_bank1.Size = new System.Drawing.Size(985, 545);
            this._report_cash_bank1.TabIndex = 0;
            // 
            // Book_Bank_Balance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._report_cash_bank1);
            this.Name = "Book_Bank_Balance";
            this.Size = new System.Drawing.Size(985, 545);
            this.ResumeLayout(false);

        }

        #endregion

        private _report_cash_bank _report_cash_bank1;
    }
}
