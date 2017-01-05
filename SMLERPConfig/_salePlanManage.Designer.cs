namespace SMLERPConfig
{
    partial class _salePlanManage
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
            this._gridSale = new SMLERPConfig._gridSalePlanDetail();
            this._screenTop = new SMLERPConfig._sale_plan_screentop();
            this.panel1 = new MyLib._myPanel();
            this._myManageData = new MyLib._myManageData();
            this._myToolbar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(624, 25);
            this._myToolbar.TabIndex = 0;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPConfig.Resource16x16.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPConfig.Resource16x16.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _gridSale
            // 
            this._gridSale._extraWordShow = true;
            this._gridSale._selectRow = -1;
            this._gridSale.BackColor = System.Drawing.SystemColors.Window;
            this._gridSale.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridSale.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridSale.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridSale.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridSale.Location = new System.Drawing.Point(0, 138);
            this._gridSale.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridSale.Name = "_gridSale";
            this._gridSale.Size = new System.Drawing.Size(624, 512);
            this._gridSale.TabIndex = 2;
            this._gridSale.TabStop = false;
            // 
            // _screenTop
            // 
            this._screenTop._isChange = false;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Font = new System.Drawing.Font("Tahoma", 9F);
            this._screenTop.Location = new System.Drawing.Point(0, 0);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(624, 138);
            this._screenTop.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1._switchTabAuto = false;
            this.panel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this._gridSale);
            this.panel1.Controls.Add(this._screenTop);
            this.panel1.CornerPicture = null;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 650);
            this.panel1.TabIndex = 3;
            // 
            // _myManageData1
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Location = new System.Drawing.Point(0, 25);
            this._myManageData.Name = "_myManageData1";
            this._myManageData.Size = new System.Drawing.Size(624, 650);
            this._myManageData.TabIndex = 3;
            this._myManageData.TabStop = false;

            this._myManageData._form2.Controls.Add(this.panel1);
            this._myManageData._form2.Controls.Add(this._myToolbar);

            // 
            // _salePlanManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData);
            this.Name = "_salePlanManage";
            this.Size = new System.Drawing.Size(624, 675);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private _sale_plan_screentop _screenTop;
        private _gridSalePlanDetail _gridSale;
        private MyLib._myPanel panel1;
        private MyLib._myManageData _myManageData;
    }
}
