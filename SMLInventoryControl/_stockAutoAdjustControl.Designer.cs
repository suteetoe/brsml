namespace SMLInventoryControl
{
    partial class _stockAutoAdjustControl
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
            this._conditionScreen = new MyLib._myScreen();
            this._selectGrid = new MyLib._myGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._loadButton = new MyLib.VistaButton();
            this._selectDocFormatButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._checkButton = new MyLib.VistaButton();
            this._productNotFoundButton = new MyLib.VistaButton();
            this._processButton = new MyLib.VistaButton();
            this._stopButton = new MyLib.VistaButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._checkGrid = new MyLib._myGrid();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(1001, 0);
            this._conditionScreen.TabIndex = 1;
            // 
            // _selectGrid
            // 
            this._selectGrid._extraWordShow = true;
            this._selectGrid._selectRow = -1;
            this._selectGrid.BackColor = System.Drawing.SystemColors.Window;
            this._selectGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._selectGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._selectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._selectGrid.IsEdit = false;
            this._selectGrid.Location = new System.Drawing.Point(0, 0);
            this._selectGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._selectGrid.Name = "_selectGrid";
            this._selectGrid.Size = new System.Drawing.Size(999, 251);
            this._selectGrid.TabIndex = 2;
            this._selectGrid.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this._conditionScreen);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 30);
            this.panel1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._loadButton);
            this.flowLayoutPanel1.Controls.Add(this._selectDocFormatButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1001, 30);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // _loadButton
            // 
            this._loadButton.AutoSize = true;
            this._loadButton.BackColor = System.Drawing.Color.Transparent;
            this._loadButton.ButtonText = "Load";
            this._loadButton.Location = new System.Drawing.Point(3, 3);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(46, 24);
            this._loadButton.TabIndex = 0;
            this._loadButton.Text = "vistaButton1";
            this._loadButton.UseVisualStyleBackColor = false;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // _selectDocFormatButton
            // 
            this._selectDocFormatButton.AutoSize = true;
            this._selectDocFormatButton.BackColor = System.Drawing.Color.Transparent;
            this._selectDocFormatButton.ButtonText = "เลือกรูปแบบเอกสาร";
            this._selectDocFormatButton.Location = new System.Drawing.Point(95, 3);
            this._selectDocFormatButton.Name = "_selectDocFormatButton";
            this._selectDocFormatButton.Size = new System.Drawing.Size(120, 24);
            this._selectDocFormatButton.TabIndex = 1;
            this._selectDocFormatButton.Text = "vistaButton1";
            this._selectDocFormatButton.UseVisualStyleBackColor = false;
            this._selectDocFormatButton.Click += new System.EventHandler(this._selectDocFormatButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(221, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(48, 24);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "vistaButton1";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this._checkButton);
            this.flowLayoutPanel2.Controls.Add(this._productNotFoundButton);
            this.flowLayoutPanel2.Controls.Add(this._processButton);
            this.flowLayoutPanel2.Controls.Add(this._stopButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 594);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1001, 30);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // _checkButton
            // 
            this._checkButton.AutoSize = true;
            this._checkButton.BackColor = System.Drawing.Color.Transparent;
            this._checkButton.ButtonText = "ตรวจสอบสินค้าซ้ำ";
            this._checkButton.Location = new System.Drawing.Point(3, 3);
            this._checkButton.Name = "_checkButton";
            this._checkButton.Size = new System.Drawing.Size(111, 24);
            this._checkButton.TabIndex = 2;
            this._checkButton.Text = "vistaButton1";
            this._checkButton.UseVisualStyleBackColor = false;
            this._checkButton.Click += new System.EventHandler(this._checkButton_Click);
            // 
            // _productNotFoundButton
            // 
            this._productNotFoundButton.AutoSize = true;
            this._productNotFoundButton.BackColor = System.Drawing.Color.Transparent;
            this._productNotFoundButton.ButtonText = "สินค้าที่ไม่มีการตรวจนับและมียอดคงเหลือ";
            this._productNotFoundButton.Location = new System.Drawing.Point(120, 3);
            this._productNotFoundButton.Name = "_productNotFoundButton";
            this._productNotFoundButton.Size = new System.Drawing.Size(229, 24);
            this._productNotFoundButton.TabIndex = 3;
            this._productNotFoundButton.Text = "vistaButton1";
            this._productNotFoundButton.UseVisualStyleBackColor = false;
            this._productNotFoundButton.Click += new System.EventHandler(this._productNotFoundButton_Click);
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(355, 3);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(61, 24);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "vistaButton1";
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _stopButton
            // 
            this._stopButton.AutoSize = true;
            this._stopButton.BackColor = System.Drawing.Color.Transparent;
            this._stopButton.ButtonText = "Stop";
            this._stopButton.Location = new System.Drawing.Point(447, 3);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(44, 24);
            this._stopButton.TabIndex = 1;
            this._stopButton.Text = "vistaButton1";
            this._stopButton.UseVisualStyleBackColor = false;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._selectGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._checkGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1001, 564);
            this.splitContainer1.SplitterDistance = 253;
            this.splitContainer1.TabIndex = 5;
            // 
            // _checkGrid
            // 
            this._checkGrid._extraWordShow = true;
            this._checkGrid._selectRow = -1;
            this._checkGrid.BackColor = System.Drawing.SystemColors.Window;
            this._checkGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._checkGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._checkGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._checkGrid.IsEdit = false;
            this._checkGrid.Location = new System.Drawing.Point(0, 0);
            this._checkGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._checkGrid.Name = "_checkGrid";
            this._checkGrid.Size = new System.Drawing.Size(999, 305);
            this._checkGrid.TabIndex = 3;
            this._checkGrid.TabStop = false;
            // 
            // _stockAutoAdjustControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_stockAutoAdjustControl";
            this.Size = new System.Drawing.Size(1001, 624);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myScreen _conditionScreen;
        private MyLib._myGrid _selectGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _loadButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _stopButton;
        private MyLib.VistaButton _selectDocFormatButton;
        private MyLib.VistaButton _checkButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _checkGrid;
        private MyLib.VistaButton _closeButton;
        private MyLib.VistaButton _productNotFoundButton;
    }
}
