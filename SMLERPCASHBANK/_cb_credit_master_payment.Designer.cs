﻿namespace SMLERPCASHBANK
{
    partial class _cb_credit_master_payment
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
            this._creditMasterControl1 = new SMLERPControl._bank._creditMasterControl();
            this.SuspendLayout();
            // 
            // _creditMasterControl1
            // 
            this._creditMasterControl1.AutoSize = true;
            this._creditMasterControl1.creditMasterControlType = SMLERPControl._bank._creditMasterControlTypeEnum.credit_master_payment;
            this._creditMasterControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._creditMasterControl1.Location = new System.Drawing.Point(0, 0);
            this._creditMasterControl1.Name = "_creditMasterControl1";
            this._creditMasterControl1.Size = new System.Drawing.Size(924, 751);
            this._creditMasterControl1.TabIndex = 0;
            // 
            // _cb_credit_master_payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._creditMasterControl1);
            this.Name = "_cb_credit_master_payment";
            this.Size = new System.Drawing.Size(924, 751);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPControl._bank._creditMasterControl _creditMasterControl1;
    }
}
