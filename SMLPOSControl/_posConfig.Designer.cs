namespace SMLPOSControl
{
    partial class _posConfig
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
            this.components = new System.ComponentModel.Container();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myPanel1 = new MyLib._myPanel();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._toolStrip1.SuspendLayout();
            this._flowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip1
            // 
            this._toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonClose});
            this._toolStrip1.Location = new System.Drawing.Point(0, 0);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(849, 25);
            this._toolStrip1.TabIndex = 9;
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Size = new System.Drawing.Size(122, 22);
            this._buttonSave.Text = "บันทึกข้อมูล (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(79, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            // 
            // _flowLayoutPanel1
            // 
            this._flowLayoutPanel1.AutoSize = true;
            this._flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._flowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._flowLayoutPanel1.Name = "_flowLayoutPanel1";
            this._flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._flowLayoutPanel1.Size = new System.Drawing.Size(849, 43);
            this._flowLayoutPanel1.TabIndex = 9;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(568, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Gray;
            this._myShadowLabel1.Size = new System.Drawing.Size(278, 33);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 7;
            this._myShadowLabel1.Text = "กำหนดค่าเริ่มต้น POS";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tabControl);
            this._myPanel1.Controls.Add(this._flowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(849, 418);
            this._myPanel1.TabIndex = 10;
            // 
            // _tabControl
            // 
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 43);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(849, 375);
            this._tabControl.TabIndex = 10;
            // 
            // _posConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 443);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._toolStrip1);
            this.Name = "_posConfig";
            this.Text = "_posConfig";
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._flowLayoutPanel1.ResumeLayout(false);
            this._flowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStrip1;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton _buttonClose;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.TabControl _tabControl;
    }
}