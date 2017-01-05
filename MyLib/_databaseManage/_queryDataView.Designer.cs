namespace MyLib._databaseManage
{
	partial class _queryDataView
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
            this._resultLabel = new System.Windows.Forms.Label();
            this._buttonProcess = new MyLib._myButton();
            this._queryLabel1 = new System.Windows.Forms.Label();
            this._textBoxQuery = new System.Windows.Forms.TextBox();
            this._buttonExit = new MyLib._myButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myPanel1 = new MyLib._myPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _resultLabel
            // 
            this._resultLabel.AutoSize = true;
            this._resultLabel.BackColor = System.Drawing.Color.Transparent;
            this._resultLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._resultLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultLabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this._resultLabel.Location = new System.Drawing.Point(5, 126);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._resultLabel.Size = new System.Drawing.Size(50, 24);
            this._resultLabel.TabIndex = 2;
            this._resultLabel.Text = "Result:";
            // 
            // _buttonProcess
            // 
            this._buttonProcess.AutoSize = true;
            this._buttonProcess.BackColor = System.Drawing.Color.Transparent;
            this._buttonProcess.ButtonText = "ประมวลผล";
            this._buttonProcess.Location = new System.Drawing.Point(431, 4);
            this._buttonProcess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._buttonProcess.myImage = global::MyLib.Resource16x16.view;
            this._buttonProcess.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonProcess.myUseVisualStyleBackColor = false;
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.ResourceName = "process";
            this._buttonProcess.Size = new System.Drawing.Size(91, 24);
            this._buttonProcess.TabIndex = 4;
            this._buttonProcess.Text = "Start";
            this._buttonProcess.UseVisualStyleBackColor = false;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _queryLabel1
            // 
            this._queryLabel1.AutoSize = true;
            this._queryLabel1.BackColor = System.Drawing.Color.Transparent;
            this._queryLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._queryLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryLabel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this._queryLabel1.Location = new System.Drawing.Point(5, 5);
            this._queryLabel1.Name = "_queryLabel1";
            this._queryLabel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._queryLabel1.Size = new System.Drawing.Size(47, 24);
            this._queryLabel1.TabIndex = 0;
            this._queryLabel1.Text = "Query:";
            // 
            // _textBoxQuery
            // 
            this._textBoxQuery.BackColor = System.Drawing.Color.White;
            this._textBoxQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this._textBoxQuery.Location = new System.Drawing.Point(5, 29);
            this._textBoxQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._textBoxQuery.Multiline = true;
            this._textBoxQuery.Name = "_textBoxQuery";
            this._textBoxQuery.Size = new System.Drawing.Size(636, 65);
            this._textBoxQuery.TabIndex = 1;
            this._textBoxQuery.Text = "SELECT   ";
            // 
            // _buttonExit
            // 
            this._buttonExit.AutoSize = true;
            this._buttonExit.BackColor = System.Drawing.Color.Transparent;
            this._buttonExit.ButtonText = "close_screen";
            this._buttonExit.Location = new System.Drawing.Point(528, 4);
            this._buttonExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._buttonExit.myImage = global::MyLib.Resource16x16.error;
            this._buttonExit.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonExit.myUseVisualStyleBackColor = false;
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.ResourceName = "close_screen";
            this._buttonExit.Size = new System.Drawing.Size(105, 24);
            this._buttonExit.TabIndex = 6;
            this._buttonExit.Text = "Exit";
            this._buttonExit.UseVisualStyleBackColor = false;
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Ivory;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(636, 463);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.Text = "dataGridView1";
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonExit);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonProcess);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 94);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(636, 32);
            this._myFlowLayoutPanel1.TabIndex = 7;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.dataGridView1);
            this._myPanel1.Controls.Add(this._resultLabel);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._textBoxQuery);
            this._myPanel1.Controls.Add(this._queryLabel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(6, 6);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(646, 618);
            this._myPanel1.TabIndex = 8;
            // 
            // _queryDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_queryDataView";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(658, 630);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label _resultLabel;
        private _myButton  _buttonProcess;
		private System.Windows.Forms.Label _queryLabel1;
        private System.Windows.Forms.TextBox _textBoxQuery;
		private _myButton _buttonExit;
		private System.Windows.Forms.DataGridView dataGridView1;
        private _myFlowLayoutPanel _myFlowLayoutPanel1;
        private _myPanel _myPanel1;
	}
}
