namespace MyLib._databaseManage
{
	partial class _databaseStruct
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
            this._myPanel1 = new MyLib._myPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._myGridTable = new MyLib._myGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._myGridField = new MyLib._myGrid();
            this._myGridIndex = new MyLib._myGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._exitButton = new MyLib._myButton();
            this._myPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.splitContainer1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(5, 30);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(738, 570);
            this._myPanel1.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._myGridTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(738, 570);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 0;
            // 
            // _myGridTable
            // 
            this._myGridTable._extraWordShow = true;
            this._myGridTable._selectRow = -1;
            this._myGridTable.BackColor = System.Drawing.SystemColors.Window;
            this._myGridTable.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGridTable.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridTable.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridTable.Location = new System.Drawing.Point(0, 0);
            this._myGridTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridTable.Name = "_myGridTable";
            this._myGridTable.Size = new System.Drawing.Size(246, 570);
            this._myGridTable.TabIndex = 0;
            this._myGridTable.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._myGridField);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._myGridIndex);
            this.splitContainer2.Size = new System.Drawing.Size(488, 570);
            this.splitContainer2.SplitterDistance = 385;
            this.splitContainer2.TabIndex = 0;
            // 
            // _myGridField
            // 
            this._myGridField._extraWordShow = true;
            this._myGridField._selectRow = -1;
            this._myGridField.BackColor = System.Drawing.SystemColors.Window;
            this._myGridField.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGridField.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGridField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridField.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridField.Location = new System.Drawing.Point(0, 0);
            this._myGridField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridField.Name = "_myGridField";
            this._myGridField.Size = new System.Drawing.Size(488, 385);
            this._myGridField.TabIndex = 1;
            this._myGridField.TabStop = false;
            // 
            // _myGridIndex
            // 
            this._myGridIndex._extraWordShow = true;
            this._myGridIndex._selectRow = -1;
            this._myGridIndex.BackColor = System.Drawing.SystemColors.Window;
            this._myGridIndex.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGridIndex.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGridIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridIndex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridIndex.Location = new System.Drawing.Point(0, 0);
            this._myGridIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridIndex.Name = "_myGridIndex";
            this._myGridIndex.Size = new System.Drawing.Size(488, 181);
            this._myGridIndex.TabIndex = 2;
            this._myGridIndex.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label3.Size = new System.Drawing.Size(37, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Table:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label2.Size = new System.Drawing.Size(36, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Index:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label1.Size = new System.Drawing.Size(32, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Field:";
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._exitButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 561);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(738, 34);
            this._myFlowLayoutPanel1.TabIndex = 6;
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(5, 5);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(738, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::MyLib.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _exitButton
            // 
            this._exitButton.AutoSize = true;
            this._exitButton.BackColor = System.Drawing.Color.Transparent;
            this._exitButton.ButtonText = "ปิดหน้าจอ";
            this._exitButton.Location = new System.Drawing.Point(654, 5);
            this._exitButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._exitButton.myImage = global::MyLib.Resource16x16.error;
            this._exitButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._exitButton.myUseVisualStyleBackColor = false;
            this._exitButton.Name = "_exitButton";
            this._exitButton.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this._exitButton.ResourceName = "screen_close";
            this._exitButton.Size = new System.Drawing.Size(78, 24);
            this._exitButton.TabIndex = 5;
            this._exitButton.UseVisualStyleBackColor = false;
            this._exitButton.Click += new System.EventHandler(this._exitButton_Click);
            // 
            // _databaseStruct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_databaseStruct";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(748, 605);
            this.Load += new System.EventHandler(this._databaseStruct_Load);
            this._myPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
        private _myPanel _myPanel1;
        private System.Windows.Forms.Label label1;
        private _myButton _exitButton;
        public _myFlowLayoutPanel _myFlowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private _myGrid _myGridTable;
        private _myGrid _myGridField;
        private _myGrid _myGridIndex;
        public System.Windows.Forms.SplitContainer splitContainer2;
        public System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _closeButton;
	}
}
