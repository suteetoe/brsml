namespace Calendar
{
    partial class _dayControl
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
            this._dayTitle = new System.Windows.Forms.Label();
            this._flowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // _dayTitle
            // 
            this._dayTitle.AutoSize = true;
            this._dayTitle.BackColor = System.Drawing.Color.Transparent;
            this._dayTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._dayTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dayTitle.ForeColor = System.Drawing.Color.Blue;
            this._dayTitle.Location = new System.Drawing.Point(0, 0);
            this._dayTitle.Name = "_dayTitle";
            this._dayTitle.Size = new System.Drawing.Size(40, 19);
            this._dayTitle.TabIndex = 0;
            this._dayTitle.Text = "Day";
            // 
            // _flowLayout
            // 
            this._flowLayout.BackColor = System.Drawing.Color.Transparent;
            this._flowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayout.Location = new System.Drawing.Point(0, 19);
            this._flowLayout.Name = "_flowLayout";
            this._flowLayout.Size = new System.Drawing.Size(250, 211);
            this._flowLayout.TabIndex = 1;
            // 
            // _dayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._flowLayout);
            this.Controls.Add(this._dayTitle);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_dayControl";
            this.Size = new System.Drawing.Size(250, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _dayTitle;
        public System.Windows.Forms.FlowLayoutPanel _flowLayout;
    }
}
