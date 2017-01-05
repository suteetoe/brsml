namespace SMLERPAR
{
    partial class ar_point
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
            this._ar_point_balance1 = new SMLERPControl._customer._ar_point_balance();
            this.SuspendLayout();
            // 
            // _ar_point_balance1
            // 
            this._ar_point_balance1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ar_point_balance1.Location = new System.Drawing.Point(0, 0);
            this._ar_point_balance1.Name = "_ar_point_balance1";
            this._ar_point_balance1.Size = new System.Drawing.Size(574, 561);
            this._ar_point_balance1.TabIndex = 0;
            // 
            // ar_point
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ar_point_balance1);
            this.Name = "ar_point";
            this.Size = new System.Drawing.Size(574, 561);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLERPControl._customer._ar_point_balance _ar_point_balance1;
    }
}
