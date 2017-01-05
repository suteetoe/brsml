namespace SMLERPAP
{
    partial class _ap_debt_billing
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
            this._ap_screen = new SMLERPAPARControl._ar_ap_trans();
            this.SuspendLayout();
            // 
            // _ap_screen
            // 
            this._ap_screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ap_screen.Location = new System.Drawing.Point(0, 0);
            this._ap_screen.Name = "_ap_screen";
            this._ap_screen.Size = new System.Drawing.Size(1056, 604);
            this._ap_screen.TabIndex = 0;
            this._ap_screen._transControlType = _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้;
            // 
            // _ap_debt_billing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ap_screen);
            this.Name = "_ap_debt_billing";
            this.Size = new System.Drawing.Size(1056, 604);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLERPAPARControl._ar_ap_trans _ap_screen;
    }
}
