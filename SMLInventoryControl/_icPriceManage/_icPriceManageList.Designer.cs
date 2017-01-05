namespace SMLInventoryControl._icPriceManage
{
    partial class _icPriceManageList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icPriceManageList));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this.toolStripMyButton2 = new MyLib.ToolStripMyButton();
            this.toolStripMyButton3 = new MyLib.ToolStripMyButton();
            this._dock = new Crom.Controls.Docking.DockContainer();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMyButton1,
            this.toolStripMyButton2,
            this.toolStripMyButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(834, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMyButton1.Image")));
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.ResourceName = "";
            this.toolStripMyButton1.Size = new System.Drawing.Size(113, 22);
            this.toolStripMyButton1.Text = "เรียกตระกร้าราคา";
            // 
            // toolStripMyButton2
            // 
            this.toolStripMyButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMyButton2.Image")));
            this.toolStripMyButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton2.Name = "toolStripMyButton2";
            this.toolStripMyButton2.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton2.ResourceName = "";
            this.toolStripMyButton2.Size = new System.Drawing.Size(117, 22);
            this.toolStripMyButton2.Text = "บันทึกตระกร้าราคา";
            // 
            // toolStripMyButton3
            // 
            this.toolStripMyButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMyButton3.Image")));
            this.toolStripMyButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton3.Name = "toolStripMyButton3";
            this.toolStripMyButton3.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton3.ResourceName = "";
            this.toolStripMyButton3.Size = new System.Drawing.Size(55, 22);
            this.toolStripMyButton3.Text = "ปิดจอ";
            // 
            // _dock
            // 
            this._dock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this._dock.CanMoveByMouseFilledForms = true;
            this._dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dock.Location = new System.Drawing.Point(0, 25);
            this._dock.Name = "_dock";
            this._dock.Size = new System.Drawing.Size(834, 672);
            this._dock.TabIndex = 1;
            this._dock.TitleBarGradientColor1 = System.Drawing.SystemColors.Control;
            this._dock.TitleBarGradientColor2 = System.Drawing.Color.White;
            this._dock.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this._dock.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this._dock.TitleBarTextColor = System.Drawing.Color.Black;
            // 
            // _icPriceManageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dock);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_icPriceManageList";
            this.Size = new System.Drawing.Size(834, 697);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton toolStripMyButton1;
        private MyLib.ToolStripMyButton toolStripMyButton2;
        private MyLib.ToolStripMyButton toolStripMyButton3;
        private Crom.Controls.Docking.DockContainer _dock;
    }
}
