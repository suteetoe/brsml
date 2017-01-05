namespace SMLERPReport.ar
{
    partial class _condition_ar
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._screen_condition_ar1 = new SMLERPReport.ar._screen_condition_ar();
            this._grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grouper1
            // 
            this._grouper1.Controls.Add(this._screen_condition_ar1);
            this._grouper1.Size = new System.Drawing.Size(634, 456);
            this._grouper1.Controls.SetChildIndex(this._screen_condition_ar1, 0);
            // 
            // _screen_condition_ar1
            // 
            this._screen_condition_ar1._isChange = false;
            this._screen_condition_ar1._screenType = SMLERPReport.ar._screenConditionArType.ArBillingAndDetail;
            this._screen_condition_ar1.AutoSize = true;
            this._screen_condition_ar1.BackColor = System.Drawing.Color.Transparent;
            this._screen_condition_ar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen_condition_ar1.Location = new System.Drawing.Point(10, 25);
            this._screen_condition_ar1.Name = "_screen_condition_ar1";
            this._screen_condition_ar1.Size = new System.Drawing.Size(141, 383);
            this._screen_condition_ar1.TabIndex = 11;
            // 
            // _condition_ar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(634, 456);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_condition_ar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "_condition_ar";
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public _screen_condition_ar _screen_condition_ar1;


    }
}