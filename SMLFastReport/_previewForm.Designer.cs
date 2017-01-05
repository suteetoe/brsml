namespace SMLFastReport
{
    partial class _previewForm
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
            this._view = new SMLReport._report._view();
            this.SuspendLayout();
            // 
            // _view
            // 
            this._view.BackColor = System.Drawing.Color.Azure;
            this._view.Dock = System.Windows.Forms.DockStyle.Fill;
            this._view.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._view.Location = new System.Drawing.Point(0, 0);
            this._view.Name = "_view";
            this._view.Size = new System.Drawing.Size(859, 585);
            this._view.TabIndex = 0;
            // 
            // _previewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 585);
            this.Controls.Add(this._view);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_previewForm";
            this.Text = "Preview";
            this.ResumeLayout(false);

        }

        #endregion

        public SMLReport._report._view _view;

    }
}