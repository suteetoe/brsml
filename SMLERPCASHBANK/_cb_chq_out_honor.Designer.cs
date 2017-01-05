namespace SMLERPCASHBANK
{
    partial class _cb_chq_out_honor
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
            this._bankControl1 = new SMLERPControl._bank._bankControl();
            this.SuspendLayout();
            // 
            // _bankControl1
            // 
            this._bankControl1.BankControlType = SMLERPControl._bank._bankControlTypeEnum.out_honor;
            this._bankControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bankControl1.Location = new System.Drawing.Point(0, 0);
            this._bankControl1.Name = "_bankControl1";
            this._bankControl1.Size = new System.Drawing.Size(688, 426);
            this._bankControl1.TabIndex = 0;
            this._bankControl1.TransType = SMLERPControl._bank._TransTypeEnum.Checks;
            // 
            // _cb_chq_out_honor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._bankControl1);
            this.Name = "_cb_chq_out_honor";
            this.Size = new System.Drawing.Size(688, 426);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLERPControl._bank._bankControl _bankControl1;
    }
}
