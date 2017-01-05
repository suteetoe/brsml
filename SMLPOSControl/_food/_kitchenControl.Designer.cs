namespace SMLPOSControl._food
{
    partial class _kitchenControl
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
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._panel = new MyLib._myPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._myTabControl = new MyLib._myTabControl();
            this.tab_detail = new MyLib._myTabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._orderComputerGrid = new MyLib._myGrid();
            this._itemGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._deleteRowButton = new System.Windows.Forms.ToolStripButton();
            this._restartButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._itemGroup = new System.Windows.Forms.ToolStripComboBox();
            this._getFoodButton = new System.Windows.Forms.ToolStripButton();
            this._getDrinkButton = new System.Windows.Forms.ToolStripButton();
            this.tab_packing_pos = new System.Windows.Forms.TabPage();
            this._pos_id_grid = new MyLib._myGrid();
            this.tab_print_option = new MyLib._myTabPage();
            this._myPrintOptionScreen = new MyLib._myScreen();
            this.tab_print_copy = new System.Windows.Forms.TabPage();
            this._gridPrintCopy = new SMLPOSControl._food._gridPrintCopy();
            this.tab_cancel_copy = new System.Windows.Forms.TabPage();
            this._gridCancelCopy = new SMLPOSControl._food._gridPrintCopy();
            this._itemLevelPanel = new System.Windows.Forms.Panel();
            this._myScreen1 = new MyLib._myScreen();
            this._toolStrip.SuspendLayout();
            this._panel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._myTabControl.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tab_packing_pos.SuspendLayout();
            this.tab_print_option.SuspendLayout();
            this.tab_print_copy.SuspendLayout();
            this.tab_cancel_copy.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(1045, 25);
            this._toolStrip.TabIndex = 5;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(110, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(72, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _panel
            // 
            this._panel._switchTabAuto = false;
            this._panel.BackColor = System.Drawing.Color.Transparent;
            this._panel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Controls.Add(this.splitContainer1);
            this._panel.Controls.Add(this._myScreen1);
            this._panel.CornerPicture = null;
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(1045, 624);
            this._panel.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._myTabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._itemLevelPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1045, 624);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 1;
            // 
            // _myTabControl
            // 
            this._myTabControl.Controls.Add(this.tab_detail);
            this._myTabControl.Controls.Add(this.tab_packing_pos);
            this._myTabControl.Controls.Add(this.tab_print_option);
            this._myTabControl.Controls.Add(this.tab_print_copy);
            this._myTabControl.Controls.Add(this.tab_cancel_copy);
            this._myTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl.Location = new System.Drawing.Point(0, 0);
            this._myTabControl.Multiline = true;
            this._myTabControl.Name = "_myTabControl";
            this._myTabControl.SelectedIndex = 0;
            this._myTabControl.Size = new System.Drawing.Size(1045, 200);
            this._myTabControl.TabIndex = 0;
            this._myTabControl.TableName = "kitchen_master";
            // 
            // tab_detail
            // 
            this.tab_detail.BackColor = System.Drawing.Color.White;
            this.tab_detail.Controls.Add(this.splitContainer2);
            this.tab_detail.Location = new System.Drawing.Point(4, 23);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Size = new System.Drawing.Size(1037, 173);
            this.tab_detail.TabIndex = 0;
            this.tab_detail.Text = "tab_detail";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._orderComputerGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._itemGrid);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer2.Size = new System.Drawing.Size(1037, 173);
            this.splitContainer2.SplitterDistance = 395;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // _orderComputerGrid
            // 
            this._orderComputerGrid._extraWordShow = true;
            this._orderComputerGrid._selectRow = -1;
            this._orderComputerGrid.BackColor = System.Drawing.SystemColors.Window;
            this._orderComputerGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._orderComputerGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._orderComputerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderComputerGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._orderComputerGrid.Location = new System.Drawing.Point(0, 0);
            this._orderComputerGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._orderComputerGrid.Name = "_orderComputerGrid";
            this._orderComputerGrid.Size = new System.Drawing.Size(393, 171);
            this._orderComputerGrid.TabIndex = 0;
            this._orderComputerGrid.TabStop = false;
            // 
            // _itemGrid
            // 
            this._itemGrid._extraWordShow = true;
            this._itemGrid._selectRow = -1;
            this._itemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._itemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._itemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemGrid.Location = new System.Drawing.Point(0, 0);
            this._itemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._itemGrid.Name = "_itemGrid";
            this._itemGrid.Size = new System.Drawing.Size(635, 146);
            this._itemGrid.TabIndex = 1;
            this._itemGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._deleteRowButton,
            this._restartButton,
            this.toolStripSeparator1,
            this._itemGroup,
            this._getFoodButton,
            this._getDrinkButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 146);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(635, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _deleteRowButton
            // 
            this._deleteRowButton.Image = global::SMLPOSControl.Properties.Resources.delete2;
            this._deleteRowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deleteRowButton.Name = "_deleteRowButton";
            this._deleteRowButton.Size = new System.Drawing.Size(71, 22);
            this._deleteRowButton.Text = "ลบบรรทัด";
            this._deleteRowButton.Click += new System.EventHandler(this._deleteRowButton_Click);
            // 
            // _restartButton
            // 
            this._restartButton.Image = global::SMLPOSControl.Properties.Resources.garbage_empty;
            this._restartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._restartButton.Name = "_restartButton";
            this._restartButton.Size = new System.Drawing.Size(59, 22);
            this._restartButton.Text = "เริ่มใหม่";
            this._restartButton.Click += new System.EventHandler(this._restartButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _itemGroup
            // 
            this._itemGroup.Name = "_itemGroup";
            this._itemGroup.Size = new System.Drawing.Size(121, 25);
            // 
            // _getFoodButton
            // 
            this._getFoodButton.Image = global::SMLPOSControl.Properties.Resources.export2;
            this._getFoodButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._getFoodButton.Name = "_getFoodButton";
            this._getFoodButton.Size = new System.Drawing.Size(100, 22);
            this._getFoodButton.Text = "ดึงรายการอาหาร";
            this._getFoodButton.Click += new System.EventHandler(this._getFoodButton_Click);
            // 
            // _getDrinkButton
            // 
            this._getDrinkButton.Image = global::SMLPOSControl.Properties.Resources.import1;
            this._getDrinkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._getDrinkButton.Name = "_getDrinkButton";
            this._getDrinkButton.Size = new System.Drawing.Size(79, 22);
            this._getDrinkButton.Text = "ดึงเครื่องดื่ม";
            this._getDrinkButton.Click += new System.EventHandler(this._getDrinkButton_Click);
            // 
            // tab_packing_pos
            // 
            this.tab_packing_pos.Controls.Add(this._pos_id_grid);
            this.tab_packing_pos.Location = new System.Drawing.Point(4, 23);
            this.tab_packing_pos.Name = "tab_packing_pos";
            this.tab_packing_pos.Size = new System.Drawing.Size(1037, 173);
            this.tab_packing_pos.TabIndex = 2;
            this.tab_packing_pos.Text = "tab_packing_pos";
            this.tab_packing_pos.UseVisualStyleBackColor = true;
            // 
            // _pos_id_grid
            // 
            this._pos_id_grid._extraWordShow = true;
            this._pos_id_grid._selectRow = -1;
            this._pos_id_grid.BackColor = System.Drawing.SystemColors.Window;
            this._pos_id_grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._pos_id_grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._pos_id_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pos_id_grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._pos_id_grid.Location = new System.Drawing.Point(0, 0);
            this._pos_id_grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pos_id_grid.Name = "_pos_id_grid";
            this._pos_id_grid.Size = new System.Drawing.Size(1037, 173);
            this._pos_id_grid.TabIndex = 1;
            this._pos_id_grid.TabStop = false;
            // 
            // tab_print_option
            // 
            this.tab_print_option.BackColor = System.Drawing.Color.White;
            this.tab_print_option.Controls.Add(this._myPrintOptionScreen);
            this.tab_print_option.Location = new System.Drawing.Point(4, 23);
            this.tab_print_option.Name = "tab_print_option";
            this.tab_print_option.Size = new System.Drawing.Size(1037, 173);
            this.tab_print_option.TabIndex = 1;
            this.tab_print_option.Text = "tab_print_option";
            // 
            // _myPrintOptionScreen
            // 
            this._myPrintOptionScreen._isChange = false;
            this._myPrintOptionScreen.AutoSize = true;
            this._myPrintOptionScreen.BackColor = System.Drawing.Color.Transparent;
            this._myPrintOptionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPrintOptionScreen.Location = new System.Drawing.Point(0, 0);
            this._myPrintOptionScreen.Name = "_myPrintOptionScreen";
            this._myPrintOptionScreen.Size = new System.Drawing.Size(1037, 0);
            this._myPrintOptionScreen.TabIndex = 0;
            // 
            // tab_print_copy
            // 
            this.tab_print_copy.BackColor = System.Drawing.Color.White;
            this.tab_print_copy.Controls.Add(this._gridPrintCopy);
            this.tab_print_copy.Location = new System.Drawing.Point(4, 23);
            this.tab_print_copy.Name = "tab_print_copy";
            this.tab_print_copy.Size = new System.Drawing.Size(1037, 173);
            this.tab_print_copy.TabIndex = 1;
            this.tab_print_copy.Text = "tab_print_copy";
            // 
            // _gridPrintCopy
            // 
            this._gridPrintCopy._extraWordShow = true;
            this._gridPrintCopy._selectRow = -1;
            this._gridPrintCopy.BackColor = System.Drawing.SystemColors.Window;
            this._gridPrintCopy.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridPrintCopy.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridPrintCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPrintCopy.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridPrintCopy.gridType = SMLPOSControl._food.printcopyGridType.Order;
            this._gridPrintCopy.Location = new System.Drawing.Point(0, 0);
            this._gridPrintCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridPrintCopy.Name = "_gridPrintCopy";
            this._gridPrintCopy.Size = new System.Drawing.Size(1037, 173);
            this._gridPrintCopy.TabIndex = 1;
            this._gridPrintCopy.TabStop = false;
            // 
            // tab_cancel_copy
            // 
            this.tab_cancel_copy.Controls.Add(this._gridCancelCopy);
            this.tab_cancel_copy.Location = new System.Drawing.Point(4, 23);
            this.tab_cancel_copy.Name = "tab_cancel_copy";
            this.tab_cancel_copy.Size = new System.Drawing.Size(1037, 173);
            this.tab_cancel_copy.TabIndex = 3;
            this.tab_cancel_copy.Text = "tab_cancel_copy";
            this.tab_cancel_copy.UseVisualStyleBackColor = true;
            // 
            // _gridCancelCopy
            // 
            this._gridCancelCopy._extraWordShow = true;
            this._gridCancelCopy._selectRow = -1;
            this._gridCancelCopy.BackColor = System.Drawing.SystemColors.Window;
            this._gridCancelCopy.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridCancelCopy.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridCancelCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCancelCopy.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridCancelCopy.gridType = SMLPOSControl._food.printcopyGridType.Cancel;
            this._gridCancelCopy.Location = new System.Drawing.Point(0, 0);
            this._gridCancelCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridCancelCopy.Name = "_gridCancelCopy";
            this._gridCancelCopy.Size = new System.Drawing.Size(1037, 173);
            this._gridCancelCopy.TabIndex = 2;
            this._gridCancelCopy.TabStop = false;
            // 
            // _itemLevelPanel
            // 
            this._itemLevelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemLevelPanel.Location = new System.Drawing.Point(0, 0);
            this._itemLevelPanel.Name = "_itemLevelPanel";
            this._itemLevelPanel.Size = new System.Drawing.Size(1045, 420);
            this._itemLevelPanel.TabIndex = 0;
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.AutoSize = true;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen1.Location = new System.Drawing.Point(0, 0);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(1045, 0);
            this._myScreen1.TabIndex = 0;
            // 
            // _kitchenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_kitchenControl";
            this.Size = new System.Drawing.Size(1045, 649);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._panel.ResumeLayout(false);
            this._panel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._myTabControl.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tab_packing_pos.ResumeLayout(false);
            this.tab_print_option.ResumeLayout(false);
            this.tab_print_option.PerformLayout();
            this.tab_print_copy.ResumeLayout(false);
            this.tab_cancel_copy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myPanel _panel;
        public MyLib._myScreen _myScreen1;
        public MyLib._myScreen _myPrintOptionScreen;
        public System.Windows.Forms.ToolStripButton _closeButton;
        public System.Windows.Forms.ToolStripButton _saveButton;
        public System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel _itemLevelPanel;
        public MyLib._myGrid _orderComputerGrid;
        public MyLib._myGrid _itemGrid;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton _deleteRowButton;
        public System.Windows.Forms.ToolStripButton _restartButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton _getFoodButton;
        public System.Windows.Forms.ToolStripButton _getDrinkButton;
        private MyLib._myTabControl _myTabControl;
        private MyLib._myTabPage tab_detail;
        private MyLib._myTabPage tab_print_option;
        private System.Windows.Forms.ToolStripComboBox _itemGroup;
        private System.Windows.Forms.TabPage tab_packing_pos;
        public MyLib._myGrid _pos_id_grid;
        public _gridPrintCopy _gridPrintCopy;
        private System.Windows.Forms.TabPage tab_print_copy;
        private System.Windows.Forms.TabPage tab_cancel_copy;
        public _gridPrintCopy _gridCancelCopy;
    }
}
