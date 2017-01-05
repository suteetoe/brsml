namespace SMLERPAPARControl
{
    partial class _ar_cut_point
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
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._ar_point_balance_screen1 = new SMLERPAPARControl._ar_point_balance_screen();
            this._myManageData1 = new MyLib._myManageData();
            this.toolStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(852, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPAPARControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "";
            this._saveButton.Size = new System.Drawing.Size(113, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click_1);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPAPARControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(75, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._ar_point_balance_screen1);
            this._myPanel1.Controls.Add(this.toolStrip1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(852, 807);
            this._myPanel1.TabIndex = 3;
            // 
            // _ar_point_balance_screen1
            // 
            this._ar_point_balance_screen1._isChange = false;
            this._ar_point_balance_screen1.BackColor = System.Drawing.Color.Transparent;
            this._ar_point_balance_screen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ar_point_balance_screen1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._ar_point_balance_screen1.Location = new System.Drawing.Point(0, 25);
            this._ar_point_balance_screen1.Name = "_ar_point_balance_screen1";
            this._ar_point_balance_screen1.Size = new System.Drawing.Size(852, 782);
            this._ar_point_balance_screen1.TabIndex = 1;
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
            this._myManageData1.Size = new System.Drawing.Size(937, 828);
            this._myManageData1.TabIndex = 4;
            this._myManageData1.TabStop = false;
            // 
            // _myManageData1.Panal1 
            //
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _myManageData1.Panal2
            //
            this._myManageData1._form2.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            // 
            // _ar_cut_point
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ar_cut_point";
            this.Size = new System.Drawing.Size(937, 828);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel1;
        private _ar_point_balance_screen _ar_point_balance_screen1;
        public MyLib._myManageData _myManageData1;
    }
}
