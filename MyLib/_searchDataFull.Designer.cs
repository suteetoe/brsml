namespace MyLib
{
	partial class _searchDataFull
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
            this._dataList = new MyLib._myDataList();
            this.SuspendLayout();
            // 
            // _dataList
            // 
            this._dataList._fullMode = false;
            this._dataList.BackColor = System.Drawing.Color.Transparent;
            this._dataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataList.Location = new System.Drawing.Point(0, 0);
            this._dataList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataList.Name = "_dataList";
            this._dataList.Size = new System.Drawing.Size(856, 399);
            this._dataList.TabIndex = 0;
            this._dataList.TabStop = false;
            // 
            // _searchDataFull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 399);
            this.Controls.Add(this._dataList);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "_searchDataFull";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "_searchDataFull";
            this.Load += new System.EventHandler(this._searchDataFull_Load);
            this.ResumeLayout(false);

		}

		#endregion

		public _myDataList _dataList;






	}
}