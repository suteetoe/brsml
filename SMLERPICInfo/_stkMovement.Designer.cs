namespace SMLERPICInfo
{
    partial class _stkMovement
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
            this._resultGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._stkBalanceWareHouse = new SMLERPICInfo._stkBalanceWareHouseGrid();
            this._stkBalanceLocation = new SMLERPICInfo._stkBalanceLocationGrid();
            this._conditionScreen = new SMLERPICInfo._stkMovementConditionScreen();
            this._typeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(1125, 283);
            this._resultGrid.TabIndex = 2;
            this._resultGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton,
            this.toolStripSeparator1,
            this._typeComboBox,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 67);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1127, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPICInfo.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPICInfo.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 92);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._resultGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1127, 551);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.TabIndex = 4;
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
            this.splitContainer2.Panel1.Controls.Add(this._stkBalanceWareHouse);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._stkBalanceLocation);
            this.splitContainer2.Size = new System.Drawing.Size(1125, 260);
            this.splitContainer2.SplitterDistance = 121;
            this.splitContainer2.TabIndex = 3;
            // 
            // _stkBalanceWareHouse
            // 
            this._stkBalanceWareHouse._extraWordShow = true;
            this._stkBalanceWareHouse._selectRow = -1;
            this._stkBalanceWareHouse.BackColor = System.Drawing.SystemColors.Window;
            this._stkBalanceWareHouse.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._stkBalanceWareHouse.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._stkBalanceWareHouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stkBalanceWareHouse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._stkBalanceWareHouse.Location = new System.Drawing.Point(0, 0);
            this._stkBalanceWareHouse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._stkBalanceWareHouse.Name = "_stkBalanceWareHouse";
            this._stkBalanceWareHouse.Size = new System.Drawing.Size(1125, 121);
            this._stkBalanceWareHouse.TabIndex = 0;
            this._stkBalanceWareHouse.TabStop = false;
            // 
            // _stkBalanceLocation
            // 
            this._stkBalanceLocation._extraWordShow = true;
            this._stkBalanceLocation._selectRow = -1;
            this._stkBalanceLocation.BackColor = System.Drawing.SystemColors.Window;
            this._stkBalanceLocation.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._stkBalanceLocation.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._stkBalanceLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stkBalanceLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._stkBalanceLocation.Location = new System.Drawing.Point(0, 0);
            this._stkBalanceLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._stkBalanceLocation.Name = "_stkBalanceLocation";
            this._stkBalanceLocation.ShowTotal = true;
            this._stkBalanceLocation.Size = new System.Drawing.Size(1125, 135);
            this._stkBalanceLocation.TabIndex = 0;
            this._stkBalanceLocation.TabStop = false;
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Font = new System.Drawing.Font("Tahoma", 9F);
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(1127, 67);
            this._conditionScreen.TabIndex = 0;
            // 
            // _typeComboBox
            // 
            this._typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._typeComboBox.Items.AddRange(new object[] {
            "ตามสินค้า",
            "ตามคลังสินค้า",
            "ตามที่เก็บสินค้า"});
            this._typeComboBox.Name = "_typeComboBox";
            this._typeComboBox.Size = new System.Drawing.Size(221, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _stkMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._conditionScreen);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_stkMovement";
            this.Size = new System.Drawing.Size(1127, 643);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _stkMovementConditionScreen _conditionScreen;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private _stkBalanceWareHouseGrid _stkBalanceWareHouse;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private _stkBalanceLocationGrid _stkBalanceLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox _typeComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}
