﻿namespace SMLERPAR
{
    partial class _ar_pay_bill
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
            this._ar_ap_trans1 = new SMLERPAPARControl._ar_ap_trans();
            this.SuspendLayout();
            // 
            // _ar_ap_trans1
            // 
            this._ar_ap_trans1.AutoSize = true;
            this._ar_ap_trans1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ar_ap_trans1.Location = new System.Drawing.Point(0, 0);
            this._ar_ap_trans1.Name = "_ar_ap_trans1";
            this._ar_ap_trans1.Size = new System.Drawing.Size(1000, 600);
            this._ar_ap_trans1.TabIndex = 0;
            this._ar_ap_trans1._transControlType = _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล;
            // 
            // _ar_pay_bill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ar_ap_trans1);
            this.Name = "_ar_pay_bill";
            this.Size = new System.Drawing.Size(1000, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPAPARControl._ar_ap_trans _ar_ap_trans1;
    }
}
