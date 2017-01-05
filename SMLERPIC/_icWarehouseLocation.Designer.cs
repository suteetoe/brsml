namespace SMLERPIC
{
    partial class _icWarehouseLocation
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
            this._panel = new System.Windows.Forms.Panel();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._icmainScreenTopControl1 = new SMLERPControl._icmainScreenTopControl();
            this._icmainGridWarehouseLocationControl1 = new SMLInventoryControl._icmainGridWarehouseLocationControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._getWareHouseAndLocationButton = new System.Windows.Forms.ToolStripButton();
            this._clearButton = new System.Windows.Forms.ToolStripButton();
            this._myManageData1 = new MyLib._myManageData();
            this._toolStrip.SuspendLayout();
            this._panel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(1070, 25);
            this._toolStrip.TabIndex = 0;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _panel
            // 
            this._panel.Controls.Add(this._icmainGridWarehouseLocationControl1);
            this._panel.Controls.Add(this.toolStrip1);
            this._panel.Controls.Add(this._icmainScreenTopControl1);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(1070, 783);
            this._panel.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทึก";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
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
            // _icmainScreenTopControl1
            // 
            this._icmainScreenTopControl1._isChange = false;
            this._icmainScreenTopControl1.AutoSize = true;
            this._icmainScreenTopControl1.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTopControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTopControl1.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenTopControl1.Name = "_icmainScreenTopControl1";
            this._icmainScreenTopControl1.Size = new System.Drawing.Size(1070, 182);
            this._icmainScreenTopControl1.TabIndex = 0;
            // 
            // _icmainGridWarehouseLocationControl1
            // 
            this._icmainGridWarehouseLocationControl1._extraWordShow = true;
            this._icmainGridWarehouseLocationControl1._selectRow = -1;
            this._icmainGridWarehouseLocationControl1.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridWarehouseLocationControl1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridWarehouseLocationControl1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridWarehouseLocationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridWarehouseLocationControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icmainGridWarehouseLocationControl1.Location = new System.Drawing.Point(0, 207);
            this._icmainGridWarehouseLocationControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridWarehouseLocationControl1.Name = "_icmainGridWarehouseLocationControl1";
            this._icmainGridWarehouseLocationControl1.Size = new System.Drawing.Size(1070, 576);
            this._icmainGridWarehouseLocationControl1.TabIndex = 1;
            this._icmainGridWarehouseLocationControl1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._getWareHouseAndLocationButton,
            this._clearButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 182);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1070, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _getWareHouseAndLocationButton
            // 
            this._getWareHouseAndLocationButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._getWareHouseAndLocationButton.Image = global::SMLERPIC.Properties.Resources.replace2;
            this._getWareHouseAndLocationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._getWareHouseAndLocationButton.Name = "_getWareHouseAndLocationButton";
            this._getWareHouseAndLocationButton.Size = new System.Drawing.Size(161, 22);
            this._getWareHouseAndLocationButton.Text = "All WareHouse and Shelf";
            this._getWareHouseAndLocationButton.Click += new System.EventHandler(this._getWareHouseAndLocationButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::SMLERPIC.Properties.Resources.delete;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(53, 22);
            this._clearButton.Text = "Clear";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 25);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(1070, 783);
            this._myManageData1.TabIndex = 7;
            this._myManageData1.TabStop = false;

            this._myManageData1._form2.Controls.Add(this._panel);
            this._myManageData1._form2.Controls.Add(this._toolStrip);
            // 
            // _icWarehouseLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            //this.Controls.Add(this._panel);
            //this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icWarehouseLocation";
            this.Size = new System.Drawing.Size(1070, 808);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._panel.ResumeLayout(false);
            this._panel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStrip;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.Panel _panel;
        private SMLERPControl._icmainScreenTopControl _icmainScreenTopControl1;
        private SMLInventoryControl._icmainGridWarehouseLocationControl _icmainGridWarehouseLocationControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _getWareHouseAndLocationButton;
        private System.Windows.Forms.ToolStripButton _clearButton;
        private MyLib._myManageData _myManageData1;
    }
}
