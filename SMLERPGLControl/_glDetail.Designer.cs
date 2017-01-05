namespace SMLERPGLControl
{
	partial class _glDetail
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
            this._glDetailGrid = new MyLib._myGrid();
            this._totalFlowLayoutPanel = new MyLib._myFlowLayoutPanel();
            this._total_amount = new MyLib._myNumberBox();
            this.difference_debit_credit = new MyLib._myLabel();
            this._autoTabCheckBox = new MyLib._myCheckBox();
            this._totalFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _glDetailGrid
            // 
            this._glDetailGrid._extraWordShow = true;
            this._glDetailGrid._selectRow = -1;
            this._glDetailGrid.AllowDrop = true;
            this._glDetailGrid.BackColor = System.Drawing.SystemColors.Window;
            this._glDetailGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._glDetailGrid.ColumnBackgroundAuto = false;
            this._glDetailGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._glDetailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetailGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetailGrid.Location = new System.Drawing.Point(0, 0);
            this._glDetailGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetailGrid.Name = "_glDetailGrid";
            this._glDetailGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._glDetailGrid.ShowTotal = true;
            this._glDetailGrid.Size = new System.Drawing.Size(788, 307);
            this._glDetailGrid.TabIndex = 4;
            this._glDetailGrid.TabStop = false;
            // 
            // _totalFlowLayoutPanel
            // 
            this._totalFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._totalFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this._totalFlowLayoutPanel.Controls.Add(this._total_amount);
            this._totalFlowLayoutPanel.Controls.Add(this.difference_debit_credit);
            this._totalFlowLayoutPanel.Controls.Add(this._autoTabCheckBox);
            this._totalFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._totalFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._totalFlowLayoutPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._totalFlowLayoutPanel.Location = new System.Drawing.Point(0, 307);
            this._totalFlowLayoutPanel.Name = "_totalFlowLayoutPanel";
            this._totalFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(2);
            this._totalFlowLayoutPanel.Size = new System.Drawing.Size(788, 27);
            this._totalFlowLayoutPanel.TabIndex = 3;
            // 
            // _total_amount
            // 
            this._total_amount._column = 0;
            this._total_amount._default_color = System.Drawing.Color.Black;
            this._total_amount._defaultBackGround = System.Drawing.Color.White;
            this._total_amount._double = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._total_amount._emtry = true;
            this._total_amount._enterToTab = false;
            this._total_amount._format = "";
            this._total_amount._hiddenNumberValue = false;
            this._total_amount._icon = true;
            this._total_amount._iconNumber = 1;
            this._total_amount._isChange = false;
            this._total_amount._isGetData = false;
            this._total_amount._isQuery = true;
            this._total_amount._isSearch = false;
            this._total_amount._isTime = false;
            this._total_amount._labelName = "";
            this._total_amount._maxColumn = 0;
            this._total_amount._maxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._total_amount._minValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._total_amount._name = null;
            this._total_amount._point = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._total_amount._row = 0;
            this._total_amount._rowCount = 0;
            this._total_amount._textFirst = "";
            this._total_amount._textLast = "";
            this._total_amount._textSecond = "";
            this._total_amount._upperCase = false;
            this._total_amount.AutoSize = true;
            this._total_amount.BackColor = System.Drawing.Color.Transparent;
            this._total_amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._total_amount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._total_amount.Enabled = false;
            this._total_amount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._total_amount.ForeColor = System.Drawing.Color.Black;
            this._total_amount.IsUpperCase = false;
            this._total_amount.Location = new System.Drawing.Point(204, 2);
            this._total_amount.Margin = new System.Windows.Forms.Padding(0);
            this._total_amount.MaxLength = 0;
            this._total_amount.Name = "_total_amount";
            this._total_amount.ShowIcon = true;
            this._total_amount.Size = new System.Drawing.Size(580, 22);
            this._total_amount.TabIndex = 1;
            this._total_amount.TabStop = false;
            // 
            // difference_debit_credit
            // 
            this.difference_debit_credit.AutoSize = true;
            this.difference_debit_credit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.difference_debit_credit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.difference_debit_credit.Location = new System.Drawing.Point(66, 2);
            this.difference_debit_credit.Name = "difference_debit_credit";
            this.difference_debit_credit.ResourceName = "";
            this.difference_debit_credit.Size = new System.Drawing.Size(135, 24);
            this.difference_debit_credit.TabIndex = 0;
            this.difference_debit_credit.Text = "difference_debit_credit";
            this.difference_debit_credit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _autoTabCheckBox
            // 
            this._autoTabCheckBox._isQuery = true;
            this._autoTabCheckBox.AutoSize = true;
            this._autoTabCheckBox.Checked = true;
            this._autoTabCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._autoTabCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this._autoTabCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._autoTabCheckBox.ForeColor = System.Drawing.Color.Black;
            this._autoTabCheckBox.Location = new System.Drawing.Point(668, 29);
            this._autoTabCheckBox.Name = "_autoTabCheckBox";
            this._autoTabCheckBox.ResourceName = null;
            this._autoTabCheckBox.Size = new System.Drawing.Size(113, 18);
            this._autoTabCheckBox.TabIndex = 0;
            this._autoTabCheckBox.TabStop = false;
            this._autoTabCheckBox.Text = "เลื่อนช่องอัตโนมัติ";
            this._autoTabCheckBox.UseVisualStyleBackColor = true;
            // 
            // _glDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._glDetailGrid);
            this.Controls.Add(this._totalFlowLayoutPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_glDetail";
            this.Size = new System.Drawing.Size(788, 334);
            this._totalFlowLayoutPanel.ResumeLayout(false);
            this._totalFlowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        public MyLib._myFlowLayoutPanel _totalFlowLayoutPanel;
        public MyLib._myLabel difference_debit_credit;
        public MyLib._myNumberBox _total_amount;
        public MyLib._myGrid _glDetailGrid;
        private MyLib._myCheckBox _autoTabCheckBox;
	}
}
