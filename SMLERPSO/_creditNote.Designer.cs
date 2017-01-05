namespace SMLERPSO
{
    partial class _creditNote
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
            this._ictrans = new SMLInventoryControl._icTransControl();
            this.SuspendLayout();
            // 
            // _ictrans
            // 
            this._ictrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ictrans.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ictrans._transControlType = _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้;
            this._ictrans.Location = new System.Drawing.Point(0, 0);
            this._ictrans.Name = "_ictrans";
            this._ictrans.Size = new System.Drawing.Size(1179, 625);
            this._ictrans.TabIndex = 1;
            // 
            // _creditNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ictrans);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_creditNote";
            this.Size = new System.Drawing.Size(1179, 625);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLInventoryControl._icTransControl _ictrans;
    }
}
