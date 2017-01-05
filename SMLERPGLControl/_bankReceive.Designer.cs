namespace SMLERPGLControl
{
	partial class _bankReceive
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
            this.components = new System.ComponentModel.Container();
            this._bankGrid = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _bankGrid
            // 
            this._bankGrid.BackColor = System.Drawing.SystemColors.Window;
            this._bankGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bankGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._bankGrid.Location = new System.Drawing.Point(0, 0);
            this._bankGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._bankGrid.Name = "_bankGrid";
            this._bankGrid.Size = new System.Drawing.Size(375, 319);
            this._bankGrid.TabIndex = 0;
            this._bankGrid.TabStop = false;
            this._bankGrid.Load += new System.EventHandler(this._myGrid1_Load);
            // 
            // _bankReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._bankGrid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_bankReceive";
            this.Size = new System.Drawing.Size(375, 319);
            this.ResumeLayout(false);

		}

		#endregion

        public MyLib._myGrid _bankGrid;

    }
}
