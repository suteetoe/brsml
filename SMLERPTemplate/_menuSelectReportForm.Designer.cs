namespace SMLERPTemplate
{
    partial class _menuSelectReportForm
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
            this._grid = new MyLib._myDataList();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid._extraWhere = "";
            this._grid._multiSelect = false;
            this._grid.BackColor = System.Drawing.Color.WhiteSmoke;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Margin = new System.Windows.Forms.Padding(0);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(729, 465);
            this._grid.TabIndex = 1;
            this._grid.TabStop = false;
            // 
            // _menuSelectReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 465);
            this.Controls.Add(this._grid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_menuSelectReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Report";
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myDataList _grid;

    }
}