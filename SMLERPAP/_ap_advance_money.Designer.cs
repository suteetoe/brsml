namespace SMLERPAP
{
    partial class _ap_advance_money
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
            this._ap_ar_deposit_control1 = new SMLERPAPARControl._ap_ar_deposit_control();
            this.SuspendLayout();
            // 
            // _ap_ar_deposit_control1
            // 
            this._ap_ar_deposit_control1.ApArDepositControlFlag = SMLERPAPARControl._ApArDepositControlFlagEnum.ap_advance;
            this._ap_ar_deposit_control1.ApArDepositControlType = SMLERPAPARControl._ApArDepositControlTypeEnum.ap;
            this._ap_ar_deposit_control1.AutoSize = true;
            this._ap_ar_deposit_control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ap_ar_deposit_control1.Location = new System.Drawing.Point(0, 0);
            this._ap_ar_deposit_control1.Name = "_ap_ar_deposit_control1";
            this._ap_ar_deposit_control1.Size = new System.Drawing.Size(765, 622);
            this._ap_ar_deposit_control1.TabIndex = 0;
            // 
            // _ap_advance_money
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ap_ar_deposit_control1);
            this.Name = "_ap_advance_money";
            this.Size = new System.Drawing.Size(765, 622);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPAPARControl._ap_ar_deposit_control _ap_ar_deposit_control1;
    }
}
