namespace SMLERPControl
{
	partial class _chqReceive
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
            this._receiveChqGrid = new MyLib._myGrid();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._newChqButton = new MyLib._myButton();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _receiveChqGrid
            // 
            this._receiveChqGrid._extraWordShow = true;
            this._receiveChqGrid.BackColor = System.Drawing.SystemColors.Window;
            this._receiveChqGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._receiveChqGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._receiveChqGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._receiveChqGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiveChqGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._receiveChqGrid.Location = new System.Drawing.Point(0, 26);
            this._receiveChqGrid.Margin = new System.Windows.Forms.Padding(0);
            this._receiveChqGrid.Name = "_receiveChqGrid";
            this._receiveChqGrid.Size = new System.Drawing.Size(596, 413);
            this._receiveChqGrid.TabIndex = 5;
            this._receiveChqGrid.TabStop = false;
            this._receiveChqGrid.Load += new System.EventHandler(this._receiveChqGrid_Load);
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.LavenderBlush;
            this._myFlowLayoutPanel1.Controls.Add(this._newChqButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(596, 26);
            this._myFlowLayoutPanel1.TabIndex = 6;
            // 
            // _newChqButton
            // 
            this._newChqButton.AutoSize = true;
            this._newChqButton.BackColor = System.Drawing.Color.Transparent;
            this._newChqButton.ButtonText = "";
            this._newChqButton.Location = new System.Drawing.Point(1, 0);
            this._newChqButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._newChqButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._newChqButton.myUseVisualStyleBackColor = true;
            this._newChqButton.Name = "_newChqButton";
            this._newChqButton.ResourceName = "สร้างเช็ครับใบใหม่";
            this._newChqButton.Padding = new System.Windows.Forms.Padding(1);
            this._newChqButton.Size = new System.Drawing.Size(16, 24);
            this._newChqButton.TabIndex = 0;
            this._newChqButton.Text = "สร้างเช็ครับใบใหม่";
            this._newChqButton.UseVisualStyleBackColor = false;
            this._newChqButton.Click += new System.EventHandler(this._newChqButton_Click);
            // 
            // _chqReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._receiveChqGrid);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_chqReceive";
            this.Size = new System.Drawing.Size(596, 439);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myButton _newChqButton;
        public MyLib._myGrid _receiveChqGrid;
	}
}
