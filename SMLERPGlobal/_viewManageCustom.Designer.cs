namespace _g
{
    partial class _viewManageCustom
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
            this._screen_view_manage_custom = new _g._screen_view_manage_custom();
            this._myManageData1 = new MyLib._myManageData();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._myGridTooblar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._previewButton = new MyLib.ToolStripMyButton();
            this._myPanel = new MyLib._myPanel();
            this._viewColumnGrid = new MyLib._myGrid();
            this._myToolBar.SuspendLayout();
            this._myGridTooblar.SuspendLayout();
            this._myPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _screen_view_manage_custom
            // 
            this._screen_view_manage_custom._isChange = false;
            this._screen_view_manage_custom.BackColor = System.Drawing.Color.Transparent;
            this._screen_view_manage_custom.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen_view_manage_custom.Location = new System.Drawing.Point(0, 0);
            this._screen_view_manage_custom.Name = "_screen_view_manage_custom";
            this._screen_view_manage_custom.Size = new System.Drawing.Size(392, 210);
            this._screen_view_manage_custom.TabIndex = 0;
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(629, 566);
            this._myManageData1.TabIndex = 1;
            this._myManageData1.TabStop = false;
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.White;
            this._myManageData1._form2.Controls.Add(this._myPanel);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(462, 418);
            this._myManageData1.TabIndex = 4;
            this._myManageData1.TabStop = false;
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(629, 25);
            this._myToolBar.TabIndex = 2;
            this._myToolBar.Text = "toolStrip1";
            // 
            // toolStripMyButton1
            // 
            this._saveButton.Image = global::_g.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "toolStripMyButton1";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก (F12)";
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(_saveButton_Click);
            // 
            // toolStripMyButton2
            // 
            this._closeButton.Image = global::_g.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "toolStripMyButton2";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(_closeButton_Click);
            // 
            // _myGridToolBar
            // 
            this._myGridTooblar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._previewButton});
            this._myGridTooblar.Location = new System.Drawing.Point(0, 0);
            this._myGridTooblar.Name = "_myGridTooblar";
            this._myGridTooblar.Size = new System.Drawing.Size(629, 25);
            this._myGridTooblar.TabIndex = 2;
            this._myGridTooblar.Text = "toolStrip1";
            // 
            // Preview Button
            // 
            this._previewButton.Image = global::_g.Properties.Resources.bell;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Padding = new System.Windows.Forms.Padding(1);
            this._previewButton.ResourceName = "ทดสอบการแก้ไข";
            this._previewButton.Size = new System.Drawing.Size(87, 22);
            this._previewButton.Text = "ทดสอบการแก้ไข";
            this._previewButton.Click += new System.EventHandler(_previewButton_Click);
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Controls.Add(this._viewColumnGrid);
            this._myPanel.Controls.Add(this._myGridTooblar);
            this._myPanel.Controls.Add(this._screen_view_manage_custom);
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.CornerPicture = null;
            this._myPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Location = new System.Drawing.Point(131, 47);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Size = new System.Drawing.Size(392, 482);
            this._myPanel.TabIndex = 3;
            // 
            // _viewColumnGrid
            // 
            this._viewColumnGrid._extraWordShow = true;
            this._viewColumnGrid._selectRow = -1;
            this._viewColumnGrid.BackColor = System.Drawing.SystemColors.Window;
            this._viewColumnGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._viewColumnGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._viewColumnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewColumnGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._viewColumnGrid.Location = new System.Drawing.Point(0, 188);
            this._viewColumnGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._viewColumnGrid.Name = "_viewColumnGrid";
            this._viewColumnGrid.Size = new System.Drawing.Size(392, 294);
            this._viewColumnGrid.TabIndex = 4;
            this._viewColumnGrid.TabStop = false;
            // 
            // _viewManageCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.Controls.Add(this._myPanel);
            //this.Controls.Add(this._myToolBar);
            this.Controls.Add(this._myManageData1);
            this.Name = "_viewManageCustom";
            this.Size = new System.Drawing.Size(629, 566);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myGridTooblar.ResumeLayout(false);
            this._myGridTooblar.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _screen_view_manage_custom _screen_view_manage_custom;
        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private System.Windows.Forms.ToolStrip _myGridTooblar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib.ToolStripMyButton _previewButton;
        private MyLib._myPanel _myPanel;
        private MyLib._myGrid _viewColumnGrid;

    }
}
