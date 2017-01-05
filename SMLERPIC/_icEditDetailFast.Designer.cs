namespace SMLERPIC
{
    partial class _icEditDetailFast
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
            this._mainPanel = new MyLib._myPanel();
            this._icGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._resetButton = new MyLib.ToolStripMyButton();
            this._updateButton = new MyLib.ToolStripMyButton();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_description = new System.Windows.Forms.TabPage();
            this._icmainScreenDescripControl = new SMLInventoryControl._icmainScreenDescripControl();
            this.tab_group = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._icmainScreenDimesionControl = new SMLInventoryControl._icmainScreenDimesionControl();
            this._icmainScreenGroupControl = new SMLInventoryControl._icmainScreenGroupControl();
            this.tab_purchase_wh = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._icmainScreenPurchaseWh = new SMLInventoryControl._icmainScreenPurchaseWh();
            this._icmainScreenSaleWh = new SMLInventoryControl._icmainScreenSaleWh();
            this._icmainScreenOutWh = new SMLInventoryControl._icmainScreenOutWh();
            this.tab_group_status = new System.Windows.Forms.TabPage();
            this._icmainScreenStatus = new SMLInventoryControl._icmainScreenStatus();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myManageData = new MyLib._myManageData();
            this._mainPanel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_description.SuspendLayout();
            this.tab_group.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tab_purchase_wh.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tab_group_status.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel._switchTabAuto = false;
            this._mainPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._mainPanel.Controls.Add(this._icGrid);
            this._mainPanel.Controls.Add(this.toolStrip2);
            this._mainPanel.Controls.Add(this._myTabControl1);
            this._mainPanel.Controls.Add(this._myToolbar);
            this._mainPanel.CornerPicture = null;
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(931, 788);
            this._mainPanel.TabIndex = 0;
            // 
            // _icGrid
            // 
            this._icGrid._extraWordShow = true;
            this._icGrid._selectRow = -1;
            this._icGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icGrid.Location = new System.Drawing.Point(0, 314);
            this._icGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icGrid.Name = "_icGrid";
            this._icGrid.Size = new System.Drawing.Size(931, 474);
            this._icGrid.TabIndex = 4;
            this._icGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._resetButton,
            this._updateButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 289);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(931, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _resetButton
            // 
            this._resetButton.Image = global::SMLERPIC.Properties.Resources.lightbulb_on;
            this._resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._resetButton.Name = "_resetButton";
            this._resetButton.Padding = new System.Windows.Forms.Padding(1);
            this._resetButton.ResourceName = "ล้างข้อมูล";
            this._resetButton.Size = new System.Drawing.Size(71, 22);
            this._resetButton.Text = "ล้างข้อมูล";
            // 
            // _updateButton
            // 
            this._updateButton.Image = global::SMLERPIC.Properties.Resources.flash;
            this._updateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._updateButton.Name = "_updateButton";
            this._updateButton.Padding = new System.Windows.Forms.Padding(1);
            this._updateButton.ResourceName = "ปรับปรุงข้อมูล";
            this._updateButton.Size = new System.Drawing.Size(91, 22);
            this._updateButton.Text = "ปรับปรุงข้อมูล";
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_description);
            this._myTabControl1.Controls.Add(this.tab_group);
            this._myTabControl1.Controls.Add(this.tab_purchase_wh);
            this._myTabControl1.Controls.Add(this.tab_group_status);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(0, 25);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(931, 264);
            this._myTabControl1.TabIndex = 1;
            this._myTabControl1.TableName = "";
            // 
            // tab_description
            // 
            this.tab_description.Controls.Add(this._icmainScreenDescripControl);
            this.tab_description.Location = new System.Drawing.Point(4, 23);
            this.tab_description.Name = "tab_description";
            this.tab_description.Padding = new System.Windows.Forms.Padding(3);
            this.tab_description.Size = new System.Drawing.Size(923, 237);
            this.tab_description.TabIndex = 0;
            this.tab_description.Text = "1.tab_description";
            this.tab_description.UseVisualStyleBackColor = true;
            // 
            // _icmainScreenDescripControl
            // 
            this._icmainScreenDescripControl._isChange = false;
            this._icmainScreenDescripControl.AutoSize = true;
            this._icmainScreenDescripControl.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenDescripControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenDescripControl.Location = new System.Drawing.Point(3, 3);
            this._icmainScreenDescripControl.Name = "_icmainScreenDescripControl";
            this._icmainScreenDescripControl.Size = new System.Drawing.Size(917, 231);
            this._icmainScreenDescripControl.TabIndex = 0;
            // 
            // tab_group
            // 
            this.tab_group.Controls.Add(this.tableLayoutPanel1);
            this.tab_group.Location = new System.Drawing.Point(4, 23);
            this.tab_group.Name = "tab_group";
            this.tab_group.Padding = new System.Windows.Forms.Padding(3);
            this.tab_group.Size = new System.Drawing.Size(789, 237);
            this.tab_group.TabIndex = 1;
            this.tab_group.Text = "2.tab_group";
            this.tab_group.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this._icmainScreenDimesionControl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._icmainScreenGroupControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 231);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _icmainScreenDimesionControl
            // 
            this._icmainScreenDimesionControl._isChange = false;
            this._icmainScreenDimesionControl.AutoSize = true;
            this._icmainScreenDimesionControl.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenDimesionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenDimesionControl.Location = new System.Drawing.Point(394, 3);
            this._icmainScreenDimesionControl.Name = "_icmainScreenDimesionControl";
            this._icmainScreenDimesionControl.Size = new System.Drawing.Size(386, 225);
            this._icmainScreenDimesionControl.TabIndex = 0;
            // 
            // _icmainScreenGroupControl
            // 
            this._icmainScreenGroupControl._isChange = false;
            this._icmainScreenGroupControl.AutoSize = true;
            this._icmainScreenGroupControl.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenGroupControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenGroupControl.Location = new System.Drawing.Point(3, 3);
            this._icmainScreenGroupControl.Name = "_icmainScreenGroupControl";
            this._icmainScreenGroupControl.Size = new System.Drawing.Size(385, 225);
            this._icmainScreenGroupControl.TabIndex = 0;
            // 
            // tab_purchase_wh
            // 
            this.tab_purchase_wh.Controls.Add(this.tableLayoutPanel2);
            this.tab_purchase_wh.Location = new System.Drawing.Point(4, 23);
            this.tab_purchase_wh.Name = "tab_purchase_wh";
            this.tab_purchase_wh.Size = new System.Drawing.Size(789, 237);
            this.tab_purchase_wh.TabIndex = 2;
            this.tab_purchase_wh.Text = "3.tab_purchase_wh";
            this.tab_purchase_wh.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this._icmainScreenPurchaseWh, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._icmainScreenSaleWh, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._icmainScreenOutWh, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.80169F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.19831F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(789, 237);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // _icmainScreenPurchaseWh
            // 
            this._icmainScreenPurchaseWh._isChange = false;
            this._icmainScreenPurchaseWh.AutoSize = true;
            this._icmainScreenPurchaseWh.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenPurchaseWh.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenPurchaseWh.Location = new System.Drawing.Point(397, 3);
            this._icmainScreenPurchaseWh.Name = "_icmainScreenPurchaseWh";
            this._icmainScreenPurchaseWh.Size = new System.Drawing.Size(389, 67);
            this._icmainScreenPurchaseWh.TabIndex = 0;
            // 
            // _icmainScreenSaleWh
            // 
            this._icmainScreenSaleWh._isChange = false;
            this._icmainScreenSaleWh.AutoSize = true;
            this._icmainScreenSaleWh.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenSaleWh.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenSaleWh.Location = new System.Drawing.Point(3, 3);
            this._icmainScreenSaleWh.Name = "_icmainScreenSaleWh";
            this._icmainScreenSaleWh.Size = new System.Drawing.Size(388, 67);
            this._icmainScreenSaleWh.TabIndex = 1;
            // 
            // _icmainScreenOutWh
            // 
            this._icmainScreenOutWh._isChange = false;
            this._icmainScreenOutWh.AutoSize = true;
            this._icmainScreenOutWh.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenOutWh.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenOutWh.Location = new System.Drawing.Point(3, 76);
            this._icmainScreenOutWh.Name = "_icmainScreenOutWh";
            this._icmainScreenOutWh.Size = new System.Drawing.Size(388, 158);
            this._icmainScreenOutWh.TabIndex = 2;
            // 
            // tab_group_status
            // 
            this.tab_group_status.Controls.Add(this._icmainScreenStatus);
            this.tab_group_status.Location = new System.Drawing.Point(4, 23);
            this.tab_group_status.Name = "tab_group_status";
            this.tab_group_status.Size = new System.Drawing.Size(789, 237);
            this.tab_group_status.TabIndex = 3;
            this.tab_group_status.Text = "4.tab_group_status";
            this.tab_group_status.UseVisualStyleBackColor = true;
            // 
            // _icmainScreenStatus
            // 
            this._icmainScreenStatus._isChange = false;
            this._icmainScreenStatus.AutoSize = true;
            this._icmainScreenStatus.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainScreenStatus.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icmainScreenStatus.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenStatus.Name = "_icmainScreenStatus";
            this._icmainScreenStatus.Size = new System.Drawing.Size(789, 237);
            this._icmainScreenStatus.TabIndex = 0;
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(931, 25);
            this._myToolbar.TabIndex = 2;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPIC.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myManageData
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Location = new System.Drawing.Point(0, 0);
            this._myManageData.Name = "_myManageData";
            this._myManageData.Size = new System.Drawing.Size(1029, 809);
            this._myManageData.TabIndex = 1;
            this._myManageData.TabStop = false;
            this._myManageData._form2.Controls.Add(this._mainPanel);
            // 
            // _icEditDetailFast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "_icEditDetailFast";
            this.Size = new System.Drawing.Size(1029, 809);
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this._myTabControl1.ResumeLayout(false);
            this.tab_description.ResumeLayout(false);
            this.tab_description.PerformLayout();
            this.tab_group.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tab_purchase_wh.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tab_group_status.ResumeLayout(false);
            this.tab_group_status.PerformLayout();
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _mainPanel;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_description;
        private System.Windows.Forms.TabPage tab_group;
        private SMLInventoryControl._icmainScreenDescripControl _icmainScreenDescripControl;
        private SMLInventoryControl._icmainScreenDimesionControl _icmainScreenDimesionControl;
        private System.Windows.Forms.TabPage tab_purchase_wh;
        private SMLInventoryControl._icmainScreenOutWh _icmainScreenOutWh;
        private SMLInventoryControl._icmainScreenSaleWh _icmainScreenSaleWh;
        private SMLInventoryControl._icmainScreenPurchaseWh _icmainScreenPurchaseWh;
        private System.Windows.Forms.TabPage tab_group_status;
        private SMLInventoryControl._icmainScreenStatus _icmainScreenStatus;
        private MyLib._myGrid _icGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib.ToolStripMyButton _resetButton;
        private MyLib.ToolStripMyButton _updateButton;
        private MyLib._myManageData _myManageData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SMLInventoryControl._icmainScreenGroupControl _icmainScreenGroupControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
