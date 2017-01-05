namespace SMLPOSControl._food
{
    partial class _chefListControl
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
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel = new MyLib._myPanel();
            this._screenTop = new MyLib._myScreen();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_chefmasteritem = new System.Windows.Forms.TabPage();
            this._itemGrid = new MyLib._myGrid();
            this._myManageData = new MyLib._myManageData();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._selectAllButton = new MyLib.ToolStripMyButton();
            this._disselectButton = new MyLib.ToolStripMyButton();
            this._myToolbar.SuspendLayout();
            this._myPanel.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_chefmasteritem.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(715, 25);
            this._myToolbar.TabIndex = 0;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก (F12)";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Controls.Add(this._screenTop);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Size = new System.Drawing.Size(715, 80);
            this._myPanel.TabIndex = 1;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenTop.Location = new System.Drawing.Point(0, 0);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(715, 80);
            this._screenTop.TabIndex = 0;
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_chefmasteritem);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(0, 105);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(715, 572);
            this._myTabControl1.TabIndex = 2;
            this._myTabControl1.TableName = "chef_master";
            // 
            // tab_chefmasteritem
            // 
            this.tab_chefmasteritem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab_chefmasteritem.Controls.Add(this._itemGrid);
            this.tab_chefmasteritem.Controls.Add(this.toolStrip1);
            this.tab_chefmasteritem.Location = new System.Drawing.Point(4, 23);
            this.tab_chefmasteritem.Margin = new System.Windows.Forms.Padding(0);
            this.tab_chefmasteritem.Name = "tab_chefmasteritem";
            this.tab_chefmasteritem.Padding = new System.Windows.Forms.Padding(3);
            this.tab_chefmasteritem.Size = new System.Drawing.Size(707, 545);
            this.tab_chefmasteritem.TabIndex = 0;
            this.tab_chefmasteritem.Text = "tab_chefmasteritem";
            this.tab_chefmasteritem.UseVisualStyleBackColor = true;
            // 
            // _itemGrid
            // 
            this._itemGrid._extraWordShow = true;
            this._itemGrid._selectRow = -1;
            this._itemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._itemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._itemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._itemGrid.Location = new System.Drawing.Point(3, 28);
            this._itemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._itemGrid.Name = "_itemGrid";
            this._itemGrid.Size = new System.Drawing.Size(699, 512);
            this._itemGrid.TabIndex = 0;
            this._itemGrid.TabStop = false;
            // 
            // _myManageData
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Location = new System.Drawing.Point(0, 0);
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Name = "_myManageData";
            this._myManageData.Size = new System.Drawing.Size(335, 564);
            this._myManageData.TabIndex = 1;
            this._myManageData.TabStop = false;

            this._myManageData._form2.Controls.Add(this._myTabControl1);
            this._myManageData._form2.Controls.Add(this._myPanel);
            this._myManageData._form2.Controls.Add(this._myToolbar);

            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllButton,
            this._disselectButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(699, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::SMLPOSControl.Properties.Resources.preferences1;
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectAllButton.ResourceName = "เลือกทั้งหมด";
            this._selectAllButton.Size = new System.Drawing.Size(86, 22);
            this._selectAllButton.Text = "เลือกทั้งหมด";
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _disselectButton
            // 
            this._disselectButton.Image = global::SMLPOSControl.Properties.Resources.delete2;
            this._disselectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._disselectButton.Name = "_disselectButton";
            this._disselectButton.Padding = new System.Windows.Forms.Padding(1);
            this._disselectButton.ResourceName = "ละเว้นทั้งหมด";
            this._disselectButton.Size = new System.Drawing.Size(92, 22);
            this._disselectButton.Text = "ละเว้นทั้งหมด";
            this._disselectButton.Click += new System.EventHandler(this._disselectButton_Click);
            // 
            // _chefListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_chefListControl";
            this.Size = new System.Drawing.Size(715, 677);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this._myTabControl1.ResumeLayout(false);
            this.tab_chefmasteritem.ResumeLayout(false);
            this.tab_chefmasteritem.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_chefmasteritem;
        private MyLib._myGrid _itemGrid;
        private MyLib._myScreen _screenTop;
        private MyLib._myManageData _myManageData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _selectAllButton;
        private MyLib.ToolStripMyButton _disselectButton;
    }
}
