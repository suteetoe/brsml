namespace SMLERPCASHBANKReport._report_bank
{
    partial class _report_withdraw_cash
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
            this._report_cash_bank1 = new SMLERPCASHBANKReport._report_bank._report_cash_bank();
            this.SuspendLayout();
            // 
            // _report_cash_bank1
            // 
            this._report_cash_bank1.Bank_temp = SMLERPCASHBANKReport._report_bank._report_cash_bank._bank_enum.Withdraw_Cash;
            this._report_cash_bank1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._report_cash_bank1.Location = new System.Drawing.Point(0, 0);
            this._report_cash_bank1.Name = "_report_cash_bank1";
            this._report_cash_bank1.Size = new System.Drawing.Size(900, 424);
            this._report_cash_bank1.TabIndex = 0;
            // 
            // _report_withdraw_cash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._report_cash_bank1);
            this.Name = "_report_withdraw_cash";
            this.Size = new System.Drawing.Size(900, 424);
            this.ResumeLayout(false);

        }

        #endregion

        private _report_cash_bank _report_cash_bank1;
    }
}
