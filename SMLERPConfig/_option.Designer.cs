namespace SMLERPConfig
{
    partial class _option
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
            this._myPanel1 = new System.Windows.Forms.Panel();
            this._glOption = new SMLERPConfig._optionGlScreen();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_gl = new System.Windows.Forms.TabPage();
            this.tab_item = new System.Windows.Forms.TabPage();
            this._itemOption = new SMLERPConfig._optionItemScreen();
            this.tab_saleorder = new System.Windows.Forms.TabPage();
            this._saleOption = new SMLERPConfig._optionSaleOrderScreen();
            this.tab_sync = new System.Windows.Forms.TabPage();
            this._optionSync = new SMLERPConfig._optionSyncScreen();
            this.tab_mis = new System.Windows.Forms.TabPage();
            this._dashboardConfigScreen1 = new SMLERPConfig._dashboardConfigScreen();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._myPanel1.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_gl.SuspendLayout();
            this.tab_item.SuspendLayout();
            this.tab_saleorder.SuspendLayout();
            this.tab_sync.SuspendLayout();
            this.tab_mis.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1.AutoScroll = true;
            this._myPanel1.Controls.Add(this._glOption);
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.Location = new System.Drawing.Point(3, 3);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(700, 578);
            this._myPanel1.TabIndex = 6;
            // 
            // _glOption
            // 
            this._glOption._isChange = false;
            this._glOption.AutoSize = true;
            this._glOption.BackColor = System.Drawing.Color.Transparent;
            this._glOption.Dock = System.Windows.Forms.DockStyle.Top;
            this._glOption.Location = new System.Drawing.Point(5, 5);
            this._glOption.Name = "_glOption";
            this._glOption.Size = new System.Drawing.Size(673, 606);
            this._glOption.TabIndex = 0;
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_gl);
            this._myTabControl1.Controls.Add(this.tab_item);
            this._myTabControl1.Controls.Add(this.tab_saleorder);
            this._myTabControl1.Controls.Add(this.tab_sync);
            this._myTabControl1.Controls.Add(this.tab_mis);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(0, 25);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.ShowTabNumber = true;
            this._myTabControl1.Size = new System.Drawing.Size(714, 611);
            this._myTabControl1.TabIndex = 7;
            this._myTabControl1.TableName = "erp_option";
            this._myTabControl1.TabStop = false;
            // 
            // tab_gl
            // 
            this.tab_gl.Controls.Add(this._myPanel1);
            this.tab_gl.Location = new System.Drawing.Point(4, 23);
            this.tab_gl.Name = "tab_gl";
            this.tab_gl.Padding = new System.Windows.Forms.Padding(3);
            this.tab_gl.Size = new System.Drawing.Size(706, 584);
            this.tab_gl.TabIndex = 0;
            this.tab_gl.Text = "1.tab_gl";
            this.tab_gl.UseVisualStyleBackColor = true;
            // 
            // tab_item
            // 
            this.tab_item.Controls.Add(this.panel1);
            this.tab_item.Location = new System.Drawing.Point(4, 23);
            this.tab_item.Name = "tab_item";
            this.tab_item.Size = new System.Drawing.Size(706, 584);
            this.tab_item.TabIndex = 1;
            this.tab_item.Text = "2.tab_item";
            this.tab_item.UseVisualStyleBackColor = true;
            // 
            // _itemOption
            // 
            this._itemOption._isChange = false;
            this._itemOption.AutoSize = true;
            this._itemOption.BackColor = System.Drawing.Color.Transparent;
            this._itemOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemOption.Location = new System.Drawing.Point(0, 0);
            this._itemOption.Name = "_itemOption";
            this._itemOption.Size = new System.Drawing.Size(706, 584);
            this._itemOption.TabIndex = 0;
            // 
            // tab_saleorder
            // 
            this.tab_saleorder.Controls.Add(this._saleOption);
            this.tab_saleorder.Location = new System.Drawing.Point(4, 23);
            this.tab_saleorder.Name = "tab_saleorder";
            this.tab_saleorder.Size = new System.Drawing.Size(706, 584);
            this.tab_saleorder.TabIndex = 3;
            this.tab_saleorder.Text = "3.tab_saleorder";
            this.tab_saleorder.UseVisualStyleBackColor = true;
            // 
            // _saleOption
            // 
            this._saleOption._isChange = false;
            this._saleOption.BackColor = System.Drawing.Color.Transparent;
            this._saleOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleOption.Location = new System.Drawing.Point(0, 0);
            this._saleOption.Name = "_saleOption";
            this._saleOption.Size = new System.Drawing.Size(706, 584);
            this._saleOption.TabIndex = 0;
            // 
            // tab_sync
            // 
            this.tab_sync.Controls.Add(this._optionSync);
            this.tab_sync.Location = new System.Drawing.Point(4, 23);
            this.tab_sync.Name = "tab_sync";
            this.tab_sync.Size = new System.Drawing.Size(706, 584);
            this.tab_sync.TabIndex = 2;
            this.tab_sync.Text = "4.tab_sync";
            this.tab_sync.UseVisualStyleBackColor = true;
            // 
            // _optionSync
            // 
            this._optionSync._isChange = false;
            this._optionSync.BackColor = System.Drawing.Color.Transparent;
            this._optionSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this._optionSync.Location = new System.Drawing.Point(0, 0);
            this._optionSync.Name = "_optionSync";
            this._optionSync.Size = new System.Drawing.Size(706, 584);
            this._optionSync.TabIndex = 0;
            // 
            // tab_mis
            // 
            this.tab_mis.Controls.Add(this._dashboardConfigScreen1);
            this.tab_mis.Location = new System.Drawing.Point(4, 23);
            this.tab_mis.Name = "tab_mis";
            this.tab_mis.Size = new System.Drawing.Size(706, 584);
            this.tab_mis.TabIndex = 4;
            this.tab_mis.Text = "5.tab_mis";
            this.tab_mis.UseVisualStyleBackColor = true;
            // 
            // _dashboardConfigScreen1
            // 
            this._dashboardConfigScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dashboardConfigScreen1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dashboardConfigScreen1.Location = new System.Drawing.Point(0, 0);
            this._dashboardConfigScreen1.Name = "_dashboardConfigScreen1";
            this._dashboardConfigScreen1.Size = new System.Drawing.Size(706, 584);
            this._dashboardConfigScreen1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPConfig.Resource16x16.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(714, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPConfig.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Size = new System.Drawing.Size(112, 22);
            this._buttonSave.Text = "บันทึกข้อมูล (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPConfig.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(74, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this._itemOption);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(706, 584);
            this.panel1.TabIndex = 1;
            // 
            // _option
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(714, 636);
            this.Controls.Add(this._myTabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "_option";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_option";
            this.Load += new System.EventHandler(this._option_Load);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myTabControl1.ResumeLayout(false);
            this.tab_gl.ResumeLayout(false);
            this.tab_item.ResumeLayout(false);
            this.tab_saleorder.ResumeLayout(false);
            this.tab_sync.ResumeLayout(false);
            this.tab_mis.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _myPanel1;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_gl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton _buttonClose;
        private _optionGlScreen _glOption;
        private System.Windows.Forms.TabPage tab_item;
        private _optionItemScreen _itemOption;
        private System.Windows.Forms.TabPage tab_sync;
        private _optionSyncScreen _optionSync;
        private System.Windows.Forms.TabPage tab_saleorder;
        private _optionSaleOrderScreen _saleOption;
        private System.Windows.Forms.TabPage tab_mis;
        private _dashboardConfigScreen _dashboardConfigScreen1;
        private System.Windows.Forms.Panel panel1;
    }
}