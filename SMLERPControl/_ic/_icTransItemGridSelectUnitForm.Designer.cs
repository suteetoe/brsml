namespace SMLERPControl._ic
{
    partial class _icTransItemGridSelectUnitForm
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
            this.components = new System.ComponentModel.Container();
            this._icmainGridUnit = new SMLERPControl._ic._icmainGridUnitControl();
            this.SuspendLayout();
            // 
            // _icmainGridUnit
            // 
            this._icmainGridUnit._extraWordShow = true;
            this._icmainGridUnit.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridUnit.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridUnit.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridUnit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridUnit.IsEdit = false;
            this._icmainGridUnit.Location = new System.Drawing.Point(0, 0);
            this._icmainGridUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridUnit.Name = "_icmainGridUnit";
            this._icmainGridUnit.Size = new System.Drawing.Size(823, 243);
            this._icmainGridUnit.TabIndex = 0;
            this._icmainGridUnit.TabStop = false;
            // 
            // _icTransItemGridSelectUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 243);
            this.Controls.Add(this._icmainGridUnit);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_icTransItemGridSelectUnitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icTransItemGridSelectUnitForm";
            this.ResumeLayout(false);

        }

        #endregion

        public SMLERPControl._ic._icmainGridUnitControl _icmainGridUnit;

    }
}