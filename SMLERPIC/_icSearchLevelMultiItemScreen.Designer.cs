namespace SMLERPIC
{
    partial class _icSearchLevelMultiItemScreen
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
            this._icSearchLevelMuitiItemScreenTopScreen1 = new SMLERPIC._icSearchLevelMuitiItemScreenTopScreen();
            this._selectedGid = new MyLib._myGrid();
            this._panel = new MyLib._myPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._resetGridButton = new MyLib.ToolStripMyButton();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._resetLevelButton = new MyLib.ToolStripMyButton();
            this._panel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _icSearchLevelMuitiItemScreenTopScreen1
            // 
            this._icSearchLevelMuitiItemScreenTopScreen1._isChange = false;
            this._icSearchLevelMuitiItemScreenTopScreen1.BackColor = System.Drawing.Color.Transparent;
            this._icSearchLevelMuitiItemScreenTopScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._icSearchLevelMuitiItemScreenTopScreen1.Location = new System.Drawing.Point(0, 25);
            this._icSearchLevelMuitiItemScreenTopScreen1.Name = "_icSearchLevelMuitiItemScreenTopScreen1";
            this._icSearchLevelMuitiItemScreenTopScreen1.Size = new System.Drawing.Size(489, 116);
            this._icSearchLevelMuitiItemScreenTopScreen1.TabIndex = 0;
            // 
            // _selectedGid
            // 
            this._selectedGid._extraWordShow = true;
            this._selectedGid._selectRow = -1;
            this._selectedGid.BackColor = System.Drawing.SystemColors.Window;
            this._selectedGid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._selectedGid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._selectedGid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectedGid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._selectedGid.Location = new System.Drawing.Point(0, 0);
            this._selectedGid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._selectedGid.Name = "_selectedGid";
            this._selectedGid.Size = new System.Drawing.Size(489, 324);
            this._selectedGid.TabIndex = 1;
            this._selectedGid.TabStop = false;
            // 
            // _panel
            // 
            this._panel._switchTabAuto = false;
            this._panel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Controls.Add(this._selectedGid);
            this._panel.CornerPicture = null;
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Location = new System.Drawing.Point(0, 166);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(489, 324);
            this._panel.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._resetGridButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 141);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(489, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _resetGridButton
            // 
            this._resetGridButton.Image = global::SMLERPIC.Properties.Resources.lightbulb_on;
            this._resetGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._resetGridButton.Name = "_resetGridButton";
            this._resetGridButton.Padding = new System.Windows.Forms.Padding(1);
            this._resetGridButton.ResourceName = "ล้างรายการ";
            this._resetGridButton.Size = new System.Drawing.Size(78, 22);
            this._resetGridButton.Text = "ล้างรายการ";
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton,
            this._resetLevelButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(489, 25);
            this._toolStrip.TabIndex = 3;
            this._toolStrip.Text = "toolStrip2";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทีก";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทีก";
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
            // 
            // _resetLevelButton
            // 
            this._resetLevelButton.Image = global::SMLERPIC.Properties.Resources.lightbulb_on;
            this._resetLevelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._resetLevelButton.Name = "_resetLevelButton";
            this._resetLevelButton.Padding = new System.Windows.Forms.Padding(1);
            this._resetLevelButton.ResourceName = "ล้างลำดับการค้นหา";
            this._resetLevelButton.Size = new System.Drawing.Size(113, 22);
            this._resetLevelButton.Text = "ล้างลำดับการค้นหา";
            // 
            // _icSearchLevelMultiItemScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._icSearchLevelMuitiItemScreenTopScreen1);
            this.Controls.Add(this._toolStrip);
            this.Name = "_icSearchLevelMultiItemScreen";
            this.Size = new System.Drawing.Size(489, 490);
            this._panel.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myPanel _panel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib._myGrid _selectedGid;
        public MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib.ToolStripMyButton _resetGridButton;
        public MyLib.ToolStripMyButton _resetLevelButton;
        public _icSearchLevelMuitiItemScreenTopScreen _icSearchLevelMuitiItemScreenTopScreen1;
        public System.Windows.Forms.ToolStrip _toolStrip;

    }
}
