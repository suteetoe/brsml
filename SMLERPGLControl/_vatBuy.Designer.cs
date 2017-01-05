namespace SMLERPGLControl
{
	partial class _vatBuy
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
            this._manualVatCheckbox = new MyLib._myCheckBox();
            this._searchVatBuyButton = new MyLib._myButton();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _vatGrid
            // 
            this._vatGrid._extraWordShow = true;
            this._vatGrid._selectRow = -1;
            this._vatGrid.BackColor = System.Drawing.SystemColors.Window;
            this._vatGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._vatGrid.ColumnBackgroundAuto = false;
            this._vatGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._vatGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._vatGrid.Location = new System.Drawing.Point(0, 24);
            this._vatGrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._vatGrid.Name = "_vatGrid";
            this._vatGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._vatGrid.ShowTotal = true;
            this._vatGrid.Size = new System.Drawing.Size(656, 358);
            this._vatGrid.TabIndex = 8;
            this._vatGrid.TabStop = false;
            this._vatGrid.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._autoInputCheckBox);
            this._myFlowLayoutPanel2.Controls.Add(this._autoCalcVatCheckBox);
            this._myFlowLayoutPanel2.Controls.Add(this._manualVatCheckbox);
            this._myFlowLayoutPanel2.Controls.Add(this._searchVatBuyButton);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(656, 24);
            this._myFlowLayoutPanel2.TabIndex = 7;
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
            // _manualVatCheckbox
            // 
            this._manualVatCheckbox._isQuery = true;
            this._manualVatCheckbox.AutoSize = true;
            this._manualVatCheckbox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._manualVatCheckbox.ForeColor = System.Drawing.Color.Black;
            this._manualVatCheckbox.Location = new System.Drawing.Point(279, 3);
            this._manualVatCheckbox.Name = "_manualVatCheckbox";
            this._manualVatCheckbox.ResourceName = "บันทึกรายการภาษีเอง";
            this._manualVatCheckbox.Size = new System.Drawing.Size(132, 18);
            this._manualVatCheckbox.TabIndex = 3;
            this._manualVatCheckbox.TabStop = false;
            this._manualVatCheckbox.Text = "บันทึกรายการภาษีเอง";
            this._manualVatCheckbox.UseVisualStyleBackColor = true;
            // 
            // _searchVatBuyButton
            // 
            this._searchVatBuyButton._drawNewMethod = false;
            this._searchVatBuyButton.AutoSize = true;
            this._searchVatBuyButton.BackColor = System.Drawing.Color.Transparent;
            this._searchVatBuyButton.ButtonText = "ค้นหาใบกำกับภาษีไม่ถึงกำหนดชำระ";
            this._searchVatBuyButton.Location = new System.Drawing.Point(415, 0);
            this._searchVatBuyButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._searchVatBuyButton.myImage = global::SMLERPGLControl.Properties.Resources.flash;
            this._searchVatBuyButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._searchVatBuyButton.myUseVisualStyleBackColor = false;
            this._searchVatBuyButton.Name = "_searchVatBuyButton";
            this._searchVatBuyButton.ResourceName = "ค้นหาใบกำกับภาษีไม่ถึงกำหนดชำระ";
            this._searchVatBuyButton.Size = new System.Drawing.Size(218, 24);
            this._searchVatBuyButton.TabIndex = 2;
            this._searchVatBuyButton.Text = "ค้นหาใบกำกับภาษีไม่ถึงกำหนดชำระ";
            this._searchVatBuyButton.UseVisualStyleBackColor = false;
            this._searchVatBuyButton.Visible = false;
            // 
            // _vatBuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._vatGrid);
            this.Controls.Add(this._myFlowLayoutPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_vatBuy";
            this.Size = new System.Drawing.Size(656, 382);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myCheckBox _autoInputCheckBox;
        private MyLib._myCheckBox _autoCalcVatCheckBox;
        public MyLib._myGrid _vatGrid;
        public MyLib._myButton _searchVatBuyButton;
        public MyLib._myCheckBox _manualVatCheckbox;

    }
}
