namespace SMLERPConfig
{
    partial class _book
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
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._myScreen1 = new SMLERPConfig._bookScreen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.LightCyan;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(792, 507);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myScreen1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(606, 480);
            this._myPanel1.TabIndex = 16;
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen1.Location = new System.Drawing.Point(5, 5);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(596, 400);
            this._myScreen1.TabIndex = 15;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPConfig.Resource16x16.bt03;
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(606, 25);
            this._myToolBar.TabIndex = 15;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPConfig.Resource16x16.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.Size = new System.Drawing.Size(87, 22);
            this._saveButton.Text = "บันทึก (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click_1);
            // 
            // _book
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_book";
            this.Size = new System.Drawing.Size(792, 507);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib._myPanel _myPanel1;
        private _bookScreen _myScreen1;

    }
}
