namespace SMLERPGL._chart
{
    partial class _changeAccountCode
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonStart = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._changeAccountCodeScreen1 = new SMLERPGL._chart._changeAccountCodeScreen();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonStart,
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(333, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonStart
            // 
            this._buttonStart.Image = global::SMLERPGL.Resource16x16.flash;
            this._buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Padding = new System.Windows.Forms.Padding(1);
            this._buttonStart.Size = new System.Drawing.Size(95, 22);
            this._buttonStart.Text = "เริ่มเปลี่ยนรหัส";
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPGL.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.Size = new System.Drawing.Size(75, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._changeAccountCodeScreen1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(333, 56);
            this.panel1.TabIndex = 2;
            // 
            // _changeAccountCodeScreen1
            // 
            this._changeAccountCodeScreen1.AutoSize = true;
            this._changeAccountCodeScreen1.BackColor = System.Drawing.Color.Transparent;
            this._changeAccountCodeScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._changeAccountCodeScreen1.Location = new System.Drawing.Point(4, 4);
            this._changeAccountCodeScreen1.Name = "_changeAccountCodeScreen1";
            this._changeAccountCodeScreen1.Size = new System.Drawing.Size(325, 45);
            this._changeAccountCodeScreen1.TabIndex = 1;
            // 
            // _changeAccountCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(333, 81);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_changeAccountCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change account code";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonStart;
        private _changeAccountCodeScreen _changeAccountCodeScreen1;
        private System.Windows.Forms.Panel panel1;
        private MyLib.ToolStripMyButton _buttonClose;
    }
}