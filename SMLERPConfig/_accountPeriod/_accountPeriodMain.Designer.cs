namespace SMLERPConfig._accountPeriod
{
	partial class _accountPeriod
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_accountPeriod));
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._buttonSave = new MyLib._myButton();
            this._buttonClose = new MyLib._myButton();
            this._buttonAuto = new MyLib._myButton();
            this._myLabel1 = new MyLib._myLabel();
            this._myGrid1 = new MyLib._myGrid();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonSave);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonClose);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonAuto);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 488);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(557, 35);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _buttonSave
            // 
            this._buttonSave._drawNewMethod = false;
            this._buttonSave.AutoSize = true;
            this._buttonSave.BackColor = System.Drawing.Color.Transparent;
            this._buttonSave.ButtonText = "บันทึก";
            this._buttonSave.Location = new System.Drawing.Point(489, 4);
            this._buttonSave.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonSave.myImage = global::SMLERPConfig.Resource16x16.disk_blue;
            this._buttonSave.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonSave.myUseVisualStyleBackColor = false;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this._buttonSave.ResourceName = "บันทึก";
            this._buttonSave.Size = new System.Drawing.Size(67, 24);
            this._buttonSave.TabIndex = 0;
            this._buttonSave.Text = "บันทึก";
            this._buttonSave.UseVisualStyleBackColor = false;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose._drawNewMethod = false;
            this._buttonClose.AutoSize = true;
            this._buttonClose.BackColor = System.Drawing.Color.Transparent;
            this._buttonClose.ButtonText = "ปิดหน้าจอ";
            this._buttonClose.Location = new System.Drawing.Point(400, 4);
            this._buttonClose.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonClose.myImage = global::SMLERPConfig.Resource16x16.error;
            this._buttonClose.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonClose.myUseVisualStyleBackColor = false;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(87, 24);
            this._buttonClose.TabIndex = 1;
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.UseVisualStyleBackColor = false;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _buttonAuto
            // 
            this._buttonAuto._drawNewMethod = false;
            this._buttonAuto.AutoSize = true;
            this._buttonAuto.BackColor = System.Drawing.Color.Transparent;
            this._buttonAuto.ButtonText = "กำหนดงวดอัตโนมัติ";
            this._buttonAuto.Location = new System.Drawing.Point(262, 4);
            this._buttonAuto.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonAuto.myImage = global::SMLERPConfig.Resource16x16.lightbulb_on;
            this._buttonAuto.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonAuto.myUseVisualStyleBackColor = false;
            this._buttonAuto.Name = "_buttonAuto";
            this._buttonAuto.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this._buttonAuto.ResourceName = "กำหนดงวดอัตโนมัติ";
            this._buttonAuto.Size = new System.Drawing.Size(136, 24);
            this._buttonAuto.TabIndex = 2;
            this._buttonAuto.Text = "กำหนดงวดอัตโนมัติ";
            this._buttonAuto.UseVisualStyleBackColor = false;
            this._buttonAuto.Click += new System.EventHandler(this._buttonAuto_Click);
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myLabel1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myLabel1.ForeColor = System.Drawing.Color.White;
            this._myLabel1.Location = new System.Drawing.Point(5, 5);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "งวดบัญชี";
            this._myLabel1.Size = new System.Drawing.Size(98, 25);
            this._myLabel1.TabIndex = 2;
            this._myLabel1.Text = "งวดบัญชี";
            // 
            // _myGrid1
            // 
            this._myGrid1._extraWordShow = true;
            this._myGrid1._selectRow = -1;
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(5, 30);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(557, 458);
            this._myGrid1.TabIndex = 4;
            this._myGrid1.TabStop = false;
            // 
            // _accountPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 528);
            this.Controls.Add(this._myGrid1);
            this.Controls.Add(this._myLabel1);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_accountPeriod";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "_accountPeriod";
            this.Load += new System.EventHandler(this._accountPeriod_Load);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myLabel _myLabel1;
		private MyLib._myButton _buttonSave;
		private MyLib._myButton _buttonClose;
        private MyLib._myButton _buttonAuto;
        private MyLib._myGrid _myGrid1;
	}
}