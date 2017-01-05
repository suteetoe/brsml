namespace SMLERPCASHBANK
{
    partial class _cb_petty_cash
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
            this._pettyCashMasterControl1 = new SMLERPControl._bank._pettyCashMasterControl();
            this.SuspendLayout();
            // 
            // _pettyCashMasterControl1
            // 
            this._pettyCashMasterControl1.AutoSize = true;
            this._pettyCashMasterControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pettyCashMasterControl1.Location = new System.Drawing.Point(0, 0);
            this._pettyCashMasterControl1.Name = "_pettyCashMasterControl1";
            this._pettyCashMasterControl1.pettyCashMasterControlType = SMLERPControl._bank._pettyCashMasterControlTypeEnum.pettycash_master;
            this._pettyCashMasterControl1.Size = new System.Drawing.Size(728, 605);
            this._pettyCashMasterControl1.TabIndex = 0;
            // 
            // _cb_petty_cash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pettyCashMasterControl1);
            this.Name = "_cb_petty_cash";
            this.Size = new System.Drawing.Size(728, 605);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPControl._bank._pettyCashMasterControl _pettyCashMasterControl1;
    }
}
