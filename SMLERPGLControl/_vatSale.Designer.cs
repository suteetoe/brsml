namespace SMLERPGLControl
{
	partial class _vatSale
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
            this._vatGrid = new MyLib._myGrid();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._autoInputCheckBox = new MyLib._myCheckBox();
            this._autoCalcVatCheckBox = new MyLib._myCheckBox();
            this._autoNumberCheckBox = new MyLib._myCheckBox();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _vatGrid
            // 
            this._vatGrid._extraWordShow = true;
            this._vatGrid._selectRow = -1;
            this._vatGrid.BackColor = System.Drawing.SystemColors.Window;
            this._vatGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._vatGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._vatGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._vatGrid.Location = new System.Drawing.Point(0, 24);
            this._vatGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._vatGrid.Name = "_vatGrid";
            this._vatGrid.ShowTotal = true;
            this._vatGrid.Size = new System.Drawing.Size(533, 396);
            this._vatGrid.TabIndex = 7;
            this._vatGrid.TabStop = false;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._autoInputCheckBox);
            this._myFlowLayoutPanel2.Controls.Add(this._autoCalcVatCheckBox);
            this._myFlowLayoutPanel2.Controls.Add(this._autoNumberCheckBox);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(533, 24);
            this._myFlowLayoutPanel2.TabIndex = 6;
            // 
            // _autoInputCheckBox
            // 
            this._autoInputCheckBox._isQuery = true;
            this._autoInputCheckBox.AutoSize = true;
            this._autoInputCheckBox.Checked = true;
            this._autoInputCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._autoInputCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._autoInputCheckBox.ForeColor = System.Drawing.Color.Black;
            this._autoInputCheckBox.Location = new System.Drawing.Point(3, 3);
            this._autoInputCheckBox.Name = "_autoInputCheckBox";
            this._autoInputCheckBox.ResourceName = "ช่วยบันทึกแบบต่อเนื่อง";
            this._autoInputCheckBox.Size = new System.Drawing.Size(139, 18);
            this._autoInputCheckBox.TabIndex = 0;
            this._autoInputCheckBox.TabStop = false;
            this._autoInputCheckBox.Text = "ช่วยบันทึกแบบต่อเนื่อง";
            this._autoInputCheckBox.UseVisualStyleBackColor = true;
            // 
            // _autoCalcVatCheckBox
            // 
            this._autoCalcVatCheckBox._isQuery = true;
            this._autoCalcVatCheckBox.AutoSize = true;
            this._autoCalcVatCheckBox.Checked = true;
            this._autoCalcVatCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._autoCalcVatCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._autoCalcVatCheckBox.ForeColor = System.Drawing.Color.Black;
            this._autoCalcVatCheckBox.Location = new System.Drawing.Point(148, 3);
            this._autoCalcVatCheckBox.Name = "_autoCalcVatCheckBox";
            this._autoCalcVatCheckBox.ResourceName = "คำนวณภาษีอัตโนมัติ";
            this._autoCalcVatCheckBox.Size = new System.Drawing.Size(125, 18);
            this._autoCalcVatCheckBox.TabIndex = 1;
            this._autoCalcVatCheckBox.TabStop = false;
            this._autoCalcVatCheckBox.Text = "คำนวณภาษีอัตโนมัติ";
            this._autoCalcVatCheckBox.UseVisualStyleBackColor = true;
            // 
            // _autoNumberCheckBox
            // 
            this._autoNumberCheckBox._isQuery = true;
            this._autoNumberCheckBox.AutoSize = true;
            this._autoNumberCheckBox.Checked = true;
            this._autoNumberCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._autoNumberCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._autoNumberCheckBox.ForeColor = System.Drawing.Color.Black;
            this._autoNumberCheckBox.Location = new System.Drawing.Point(279, 3);
            this._autoNumberCheckBox.Name = "_autoNumberCheckBox";
            this._autoNumberCheckBox.ResourceName = "สร้างเลขที่ใบกำกับภาษีแบบต่อเนื่อง";
            this._autoNumberCheckBox.Size = new System.Drawing.Size(201, 18);
            this._autoNumberCheckBox.TabIndex = 2;
            this._autoNumberCheckBox.TabStop = false;
            this._autoNumberCheckBox.Text = "สร้างเลขที่ใบกำกับภาษีแบบต่อเนื่อง";
            this._autoNumberCheckBox.UseVisualStyleBackColor = true;
            // 
            // _vatSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._vatGrid);
            this.Controls.Add(this._myFlowLayoutPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_vatSale";
            this.Size = new System.Drawing.Size(533, 420);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myCheckBox _autoInputCheckBox;
        private MyLib._myCheckBox _autoCalcVatCheckBox;
        private MyLib._myCheckBox _autoNumberCheckBox;
        public MyLib._myGrid _vatGrid;

    }
}
