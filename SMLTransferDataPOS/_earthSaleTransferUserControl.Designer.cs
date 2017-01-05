namespace SMLTransferDataPOS
{
    partial class _earthSaleTransferUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_earthSaleTransferUserControl));
            this._transGrid = new MyLib._myGrid();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._selectAllButton = new System.Windows.Forms.ToolStripButton();
            this._selectNoneButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this._saveButton = new MyLib.VistaButton();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._loadButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._sale_pos_screen_top = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _transGrid
            // 
            this._transGrid._extraWordShow = true;
            this._transGrid._selectRow = -1;
            this._transGrid.BackColor = System.Drawing.SystemColors.Window;
            this._transGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._transGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._transGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._transGrid.Location = new System.Drawing.Point(3, 28);
            this._transGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._transGrid.Name = "_transGrid";
            this._transGrid.Size = new System.Drawing.Size(742, 369);
            this._transGrid.TabIndex = 4;
            this._transGrid.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._transGrid);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(748, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "รายละเอียด";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllButton,
            this._selectNoneButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(742, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = ((System.Drawing.Image)(resources.GetObject("_selectAllButton.Image")));
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(75, 22);
            this._selectAllButton.Text = "Select All";
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _selectNoneButton
            // 
            this._selectNoneButton.Image = ((System.Drawing.Image)(resources.GetObject("_selectNoneButton.Image")));
            this._selectNoneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectNoneButton.Name = "_selectNoneButton";
            this._selectNoneButton.Size = new System.Drawing.Size(90, 22);
            this._selectNoneButton.Text = "Select None";
            this._selectNoneButton.Click += new System.EventHandler(this._selectNoneButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 427);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(748, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(748, 400);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(748, 400);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(748, 400);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก";
            this._saveButton.Location = new System.Drawing.Point(660, 3);
            this._saveButton.myImage = global::SMLTransferDataPOS.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(93, 26);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "vistaButton2";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._saveButton);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 471);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(756, 34);
            this._myFlowLayoutPanel2.TabIndex = 10;
            // 
            // _loadButton
            // 
            this._loadButton._drawNewMethod = false;
            this._loadButton.BackColor = System.Drawing.Color.Transparent;
            this._loadButton.ButtonText = "Load";
            this._loadButton.Location = new System.Drawing.Point(3, 3);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(108, 26);
            this._loadButton.TabIndex = 0;
            this._loadButton.Text = "vistaButton1";
            this._loadButton.UseVisualStyleBackColor = false;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(117, 3);
            this._closeButton.myImage = global::SMLTransferDataPOS.Properties.Resources.error;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(103, 26);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "vistaButton4";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _sale_pos_screen_top
            // 
            this._sale_pos_screen_top._isChange = false;
            this._sale_pos_screen_top.BackColor = System.Drawing.Color.Transparent;
            this._sale_pos_screen_top.Dock = System.Windows.Forms.DockStyle.Top;
            this._sale_pos_screen_top.Location = new System.Drawing.Point(0, 0);
            this._sale_pos_screen_top.Name = "_sale_pos_screen_top";
            this._sale_pos_screen_top.Size = new System.Drawing.Size(756, 12);
            this._sale_pos_screen_top.TabIndex = 8;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._loadButton);
            this._myFlowLayoutPanel1.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 12);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(756, 32);
            this._myFlowLayoutPanel1.TabIndex = 9;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(748, 400);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // _earthSaleTransferUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._myFlowLayoutPanel2);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this._sale_pos_screen_top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_earthSaleTransferUserControl";
            this.Size = new System.Drawing.Size(756, 505);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myGrid _transGrid;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private MyLib.VistaButton _saveButton;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib.VistaButton _loadButton;
        private MyLib.VistaButton _closeButton;
        private MyLib._myScreen _sale_pos_screen_top;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _selectAllButton;
        private System.Windows.Forms.ToolStripButton _selectNoneButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
    }
}
