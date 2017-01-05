namespace SMLPPControl
{
    partial class _shipmentDetailControl
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
            this._toolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._printButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._mainPanel = new MyLib._myPanel();
            this._myTab = new MyLib._myTabControl();
            this.tab_description = new MyLib._myTabPage();
            this._shipmentGrid = new SMLPPControl._shipmentDetailGrid();
            this._screenButtom = new SMLPPControl._shipmentScreenButtom();
            this._screenTop = new SMLPPControl._shipmentScreenTop();
            this._toolbar.SuspendLayout();
            this._mainPanel.SuspendLayout();
            this._myTab.SuspendLayout();
            this.tab_description.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolbar
            // 
            this._toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._printButton,
            this._closeButton});
            this._toolbar.Location = new System.Drawing.Point(0, 0);
            this._toolbar.Name = "_toolbar";
            this._toolbar.Size = new System.Drawing.Size(591, 25);
            this._toolbar.TabIndex = 2;
            this._toolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLPPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทึก";
            // 
            // _printButton
            // 
            this._printButton.Image = global::SMLPPControl.Properties.Resources.printer;
            this._printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._printButton.Name = "_printButton";
            this._printButton.Padding = new System.Windows.Forms.Padding(1);
            this._printButton.ResourceName = "พิมพ์";
            this._printButton.Size = new System.Drawing.Size(52, 22);
            this._printButton.Text = "พิมพ์";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLPPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Size = new System.Drawing.Size(74, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _mainPanel
            // 
            this._mainPanel._switchTabAuto = false;
            this._mainPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._mainPanel.Controls.Add(this._myTab);
            this._mainPanel.Controls.Add(this._screenTop);
            this._mainPanel.CornerPicture = null;
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._mainPanel.Location = new System.Drawing.Point(0, 25);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Padding = new System.Windows.Forms.Padding(3);
            this._mainPanel.Size = new System.Drawing.Size(591, 641);
            this._mainPanel.TabIndex = 3;
            // 
            // _myTab
            // 
            this._myTab.Controls.Add(this.tab_description);
            this._myTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTab.Location = new System.Drawing.Point(3, 3);
            this._myTab.Multiline = true;
            this._myTab.Name = "_myTab";
            this._myTab.SelectedIndex = 0;
            this._myTab.Size = new System.Drawing.Size(585, 635);
            this._myTab.TabIndex = 3;
            this._myTab.TableName = "pp_shipment";
            // 
            // tab_description
            // 
            this.tab_description.Controls.Add(this._shipmentGrid);
            this.tab_description.Controls.Add(this._screenButtom);
            this.tab_description.Location = new System.Drawing.Point(4, 23);
            this.tab_description.Name = "tab_description";
            this.tab_description.Size = new System.Drawing.Size(577, 608);
            this.tab_description.TabIndex = 0;
            this.tab_description.Text = "1.tab_description";
            this.tab_description.UseVisualStyleBackColor = true;
            // 
            // _shipmentGrid
            // 
            this._shipmentGrid._extraWordShow = true;
            this._shipmentGrid._selectRow = -1;
            this._shipmentGrid.BackColor = System.Drawing.SystemColors.Window;
            this._shipmentGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._shipmentGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._shipmentGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._shipmentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._shipmentGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._shipmentGrid.Location = new System.Drawing.Point(0, 0);
            this._shipmentGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._shipmentGrid.Name = "_shipmentGrid";
            this._shipmentGrid.Size = new System.Drawing.Size(577, 598);
            this._shipmentGrid.TabIndex = 0;
            this._shipmentGrid.TabStop = false;
            this._shipmentGrid.transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;
            // 
            // _screenButtom
            // 
            this._screenButtom._isChange = false;
            this._screenButtom.BackColor = System.Drawing.Color.Transparent;
            this._screenButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._screenButtom.Location = new System.Drawing.Point(0, 598);
            this._screenButtom.Name = "_screenButtom";
            this._screenButtom.Size = new System.Drawing.Size(577, 10);
            this._screenButtom.TabIndex = 1;
            this._screenButtom.transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;
            this._screenButtom.Visible = false;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(3, 3);
            this._screenTop.Margin = new System.Windows.Forms.Padding(0);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(585, 0);
            this._screenTop.TabIndex = 0;
            this._screenTop.transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;
            // 
            // _shipmentDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainPanel);
            this.Controls.Add(this._toolbar);
            this.Name = "_shipmentDetailControl";
            this.Size = new System.Drawing.Size(591, 666);
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this._mainPanel.ResumeLayout(false);
            this._myTab.ResumeLayout(false);
            this.tab_description.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MyLib._myTabPage tab_description;
        private MyLib._myPanel _mainPanel;
        public MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        public _shipmentScreenTop _screenTop;
        public _shipmentDetailGrid _shipmentGrid;
        public MyLib._myTabControl _myTab;
        public _shipmentScreenButtom _screenButtom;
        public MyLib.ToolStripMyButton _printButton;
        public System.Windows.Forms.ToolStrip _toolbar;
    }
}
