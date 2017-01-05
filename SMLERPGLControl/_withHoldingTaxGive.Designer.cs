namespace SMLERPGLControl
{
	partial class _withHoldingTaxGive
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._listGrid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._detailGrid = new MyLib._myGrid();
            this._detailScreen = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._deleteButton = new MyLib._myButton();
            this._newButton = new MyLib._myButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._listGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._myPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(663, 438);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // _listGrid
            // 
            this._listGrid._extraWordShow = true;
            this._listGrid._selectRow = -1;
            this._listGrid.BackColor = System.Drawing.SystemColors.Window;
            this._listGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._listGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._listGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._listGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._listGrid.Location = new System.Drawing.Point(0, 0);
            this._listGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._listGrid.Name = "_listGrid";
            this._listGrid.Size = new System.Drawing.Size(221, 438);
            this._listGrid.TabIndex = 0;
            this._listGrid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._detailGrid);
            this._myPanel1.Controls.Add(this._detailScreen);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(438, 438);
            this._myPanel1.TabIndex = 3;
            // 
            // _detailGrid
            // 
            this._detailGrid._extraWordShow = true;
            this._detailGrid._selectRow = -1;
            this._detailGrid.BackColor = System.Drawing.SystemColors.Window;
            this._detailGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._detailGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._detailGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._detailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._detailGrid.Location = new System.Drawing.Point(0, 50);
            this._detailGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailGrid.Name = "_detailGrid";
            this._detailGrid.Size = new System.Drawing.Size(438, 362);
            this._detailGrid.TabIndex = 0;
            this._detailGrid.TabStop = false;
            // 
            // _detailScreen
            // 
            this._detailScreen._isChange = false;
            this._detailScreen.BackColor = System.Drawing.Color.Transparent;
            this._detailScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailScreen.Location = new System.Drawing.Point(0, 0);
            this._detailScreen.Name = "_detailScreen";
            this._detailScreen.Size = new System.Drawing.Size(438, 50);
            this._detailScreen.TabIndex = 1;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._deleteButton);
            this._myFlowLayoutPanel1.Controls.Add(this._newButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 412);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(438, 26);
            this._myFlowLayoutPanel1.TabIndex = 2;
            // 
            // _deleteButton
            // 
            this._deleteButton.AutoSize = true;
            this._deleteButton.BackColor = System.Drawing.Color.Transparent;
            this._deleteButton.ButtonText = "";
            this._deleteButton.Location = new System.Drawing.Point(382, 0);
            this._deleteButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._deleteButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._deleteButton.myUseVisualStyleBackColor = false;
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.Padding = new System.Windows.Forms.Padding(1);
            this._deleteButton.ResourceName = "ź";
            this._deleteButton.Size = new System.Drawing.Size(16, 24);
            this._deleteButton.TabIndex = 0;
            this._deleteButton.Text = "Delete";
            this._deleteButton.UseVisualStyleBackColor = false;
            // 
            // _newButton
            // 
            this._newButton.AutoSize = true;
            this._newButton.BackColor = System.Drawing.Color.Transparent;
            this._newButton.ButtonText = "";
            this._newButton.Location = new System.Drawing.Point(336, 0);
            this._newButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._newButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._newButton.myUseVisualStyleBackColor = false;
            this._newButton.Name = "_newButton";
            this._newButton.Padding = new System.Windows.Forms.Padding(1);
            this._newButton.ResourceName = "New";
            this._newButton.Size = new System.Drawing.Size(16, 24);
            this._newButton.TabIndex = 1;
            this._newButton.Text = "New";
            this._newButton.UseVisualStyleBackColor = false;
            // 
            // _withHoldingTaxGive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_withHoldingTaxGive";
            this.Size = new System.Drawing.Size(663, 438);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private MyLib._myGrid _listGrid;
		private MyLib._myPanel _myPanel1;
		private MyLib._myGrid _detailGrid;
		private MyLib._myScreen _detailScreen;
		private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
		private MyLib._myButton _deleteButton;
		private MyLib._myButton _newButton;
	}
}
