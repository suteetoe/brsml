namespace SMLERPCASHBANK
{
    partial class _cb_chq_in_receive_master
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
            this._chqListControl1 = new SMLERPControl._bank._chqListControl();
            this.SuspendLayout();
            // 
            // _chqListControl1
            // 
            this._chqListControl1.chqListControlType = SMLERPControl._bank._chqListControlTypeEnum.ทะเบียนเช็ครับ;
            this._chqListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chqListControl1.Location = new System.Drawing.Point(0, 0);
            this._chqListControl1.Name = "_chqListControl1";
            this._chqListControl1.Size = new System.Drawing.Size(787, 644);
            this._chqListControl1.TabIndex = 0;
            // 
            // _cb_chq_in_receive_master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._chqListControl1);
            this.Name = "_cb_chq_in_receive_master";
            this.Size = new System.Drawing.Size(787, 644);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLERPControl._bank._chqListControl _chqListControl1;
    }
}
