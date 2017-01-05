namespace SMLInventoryControl
{
    partial class _icTransExtraControl
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
            this._myPanel1 = new MyLib._myPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._ictransExtraControlScreenTop = new SMLInventoryControl._ictransExtraControlScreenTop();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonConfirm = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.groupBox1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(1);
            this._myPanel1.Size = new System.Drawing.Size(462, 202);
            this._myPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this._ictransExtraControlScreenTop);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // _ictransExtraControlScreenTop
            // 
            this._ictransExtraControlScreenTop._isChange = false;
            this._ictransExtraControlScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._ictransExtraControlScreenTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ictransExtraControlScreenTop.Location = new System.Drawing.Point(3, 16);
            this._ictransExtraControlScreenTop.Name = "_ictransExtraControlScreenTop";
            this._ictransExtraControlScreenTop.Size = new System.Drawing.Size(454, 181);
            this._ictransExtraControlScreenTop.TabIndex = 0;
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonConfirm,
            this._buttonClose});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(462, 25);
            this._myToolBar.TabIndex = 1;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _buttonConfirm
            // 
            this._buttonConfirm.Image = global::SMLInventoryControl.Properties.Resources.filesave;
            this._buttonConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonConfirm.Name = "_buttonConfirm";
            this._buttonConfirm.ResourceName = "ยืนยัน";
            this._buttonConfirm.Padding = new System.Windows.Forms.Padding(1);
            this._buttonConfirm.Size = new System.Drawing.Size(59, 22);
            this._buttonConfirm.Text = "ยืนยัน";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLInventoryControl.Properties.Resources.exit;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.Size = new System.Drawing.Size(43, 22);
            this._buttonClose.Text = "ปิด";
            // 
            // _icTransExtraControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 227);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_icTransExtraControl";
            this.Text = "_ictransExtraControl";
            this._myPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonClose;
        public MyLib.ToolStripMyButton _buttonConfirm;
        public _ictransExtraControlScreenTop _ictransExtraControlScreenTop;
    }
}