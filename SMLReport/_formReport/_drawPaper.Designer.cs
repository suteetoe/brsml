namespace SMLReport._formReport
{
    partial class _drawPaper
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
            this._area = new SMLReport._design._drawPanel();
            this.SuspendLayout();
            // 
            // _area
            // 
            this._area._activeTool = SMLReport._design._drawToolType.Pointer;
            this._area._drawNetRectangle = false;
            this._area._drawScale = 1F;
            this._area._netRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this._area.BackColor = System.Drawing.Color.White;
            this._area.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._area.Location = new System.Drawing.Point(3, 3);
            this._area.Name = "_area";
            this._area.Size = new System.Drawing.Size(366, 361);
            this._area.TabIndex = 0;
            // 
            // _drawPaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._area);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_drawPaper";
            this.Size = new System.Drawing.Size(369, 387);
            this.ResumeLayout(false);

        }

        #endregion

        public _design._drawPanel _area;




    }
}
