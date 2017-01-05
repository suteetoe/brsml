namespace SMLERPConfig
{
    partial class _credit_type
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
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel = new MyLib._myPanel();
            this._credittypeScreen1 = new SMLERPConfig._credittypeScreen();
            this._myToolBar.SuspendLayout();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPConfig.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.Size = new System.Drawing.Size(87, 22);
            this._buttonSave.Text = "บันทึก (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPConfig.Resource16x16.bt03;
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(451, 25);
            this._myToolBar.TabIndex = 13;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _myManageData1
            // 
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this._myManageData1._form2.BackColor = System.Drawing.Color.White;
            this._myManageData1._form2.Controls.Add(this._myPanel);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(589, 480);
            this._myManageData1.TabIndex = 3;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel
            // 
            this._myPanel.BeginColor = System.Drawing.Color.White;
            this._myPanel.Controls.Add(this._credittypeScreen1);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.WhiteSmoke;
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Padding = new System.Windows.Forms.Padding(4);
            this._myPanel.Size = new System.Drawing.Size(451, 453);
            this._myPanel.TabIndex = 15;
            // 
            // _credittypeScreen1
            // 
            this._credittypeScreen1._isChange = false;
            this._credittypeScreen1.BackColor = System.Drawing.Color.Transparent;
            this._credittypeScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._credittypeScreen1.Location = new System.Drawing.Point(4, 4);
            this._credittypeScreen1.Name = "_credittypeScreen1";
            this._credittypeScreen1.Size = new System.Drawing.Size(443, 445);
            this._credittypeScreen1.TabIndex = 0;
            // 
            // _credit_type
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_credit_type";
            this.Size = new System.Drawing.Size(589, 480);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib.ToolStripMyButton _buttonSave;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib._myManageData _myManageData1;
        private MyLib._myPanel _myPanel;
        private _credittypeScreen _credittypeScreen1;
    }
}
