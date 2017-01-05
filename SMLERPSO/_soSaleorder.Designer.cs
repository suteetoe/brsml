namespace SMLERPSO
{
    partial class _soSaleorder
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
            this._ictrans._transControlType = _g.g._transControlTypeEnum.ขาย_สั่งขาย;
            this._ictrans.Location = new System.Drawing.Point(0, 0);
            this._ictrans.Name = "_ictrans";
            this._ictrans.Size = new System.Drawing.Size(944, 647);
            this._ictrans.TabIndex = 0;
            // 
            // _soSaleorder
            // 
            this.Controls.Add(this._ictrans);
            this.Name = "_soSaleorder";
            this.Size = new System.Drawing.Size(944, 647);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLInventoryControl._icTransControl _ictrans;
       

    }
}
