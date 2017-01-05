namespace SMLPOSControl._designer._object
{
    partial class _propertyForm
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
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Location = new System.Drawing.Point(0, 0);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(274, 537);
            this._propertyGrid.TabIndex = 0;
            // 
            // _propertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 537);
            this.Controls.Add(this._propertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_propertyForm";
            this.ShowInTaskbar = false;
            this.Text = "Property";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PropertyGrid _propertyGrid;

    }
}