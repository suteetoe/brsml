namespace SMLERPGL._display
{
    partial class _glListDrillDown
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
            this._glList1 = new SMLERPGL._display._glList();
            this.SuspendLayout();
            // 
            // _glList1
            // 
            this._glList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glList1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glList1.Location = new System.Drawing.Point(0, 0);
            this._glList1.Name = "_glList1";
            this._glList1.Size = new System.Drawing.Size(667, 575);
            this._glList1.TabIndex = 0;
            // 
            // _glListDrillDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 575);
            this.Controls.Add(this._glList1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_glListDrillDown";
            this.Text = "_glListDrillDown";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public _glList _glList1;

    }
}