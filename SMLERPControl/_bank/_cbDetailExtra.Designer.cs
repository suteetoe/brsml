namespace SMLERPControl._bank
{
    partial class _cbDetailExtra
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonConfirm = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._cbDetailExtraTopScreen = new SMLERPControl._bank._cbDetailExtraTopScreen();
            this.toolStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonConfirm,
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(684, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonConfirm
            // 
            this._buttonConfirm.Image = global::SMLERPControl.Properties.Resources.check;
            this._buttonConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonConfirm.Name = "_buttonConfirm";
            this._buttonConfirm.ResourceName = "ยืนยันและปิดหน้าจอ";
            this._buttonConfirm.Padding = new System.Windows.Forms.Padding(1);
            this._buttonConfirm.Size = new System.Drawing.Size(124, 22);
            this._buttonConfirm.Text = "ยืนยันและปิดหน้าจอ";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPControl.Properties.Resources.delete;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.ResourceName = "ยกเลิกและปิดหน้าจอ";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.Size = new System.Drawing.Size(126, 22);
            this._buttonClose.Text = "ยกเลิกและปิดหน้าจอ";
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._cbDetailExtraTopScreen);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(684, 255);
            this._myPanel1.TabIndex = 5;
            // 
            // _cbDetailExtraTopScreen
            // 
            this._cbDetailExtraTopScreen._isChange = false;
            this._cbDetailExtraTopScreen.BackColor = System.Drawing.Color.Transparent;
            this._cbDetailExtraTopScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cbDetailExtraTopScreen.Location = new System.Drawing.Point(0, 0);
            this._cbDetailExtraTopScreen.Name = "_cbDetailExtraTopScreen";
            this._cbDetailExtraTopScreen.Size = new System.Drawing.Size(684, 255);
            this._cbDetailExtraTopScreen.TabIndex = 0;
            // 
            // _cbDetailExtra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 280);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_cbDetailExtra";
            this.Load += new System.EventHandler(this._cbDetailExtra_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib.ToolStripMyButton _buttonConfirm;
        private MyLib.ToolStripMyButton _buttonClose;
        private MyLib._myPanel _myPanel1;
        public _cbDetailExtraTopScreen _cbDetailExtraTopScreen;

    }
}
