namespace SMLSINGHAControl
{
    partial class _singhaMasterTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_singhaMasterTransfer));
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this.Close_Button1 = new System.Windows.Forms.ToolStripButton();
            this._myPanel1 = new MyLib._myPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Transfer = new System.Windows.Forms.TabPage();
            this._transferControl1 = new SMLSINGHAControl._transferControl();
            this.tab_OnProcess = new System.Windows.Forms.TabPage();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this._myToolBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_Transfer.SuspendLayout();
            this.tab_OnProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Close_Button1});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(1112, 25);
            this._myToolBar.TabIndex = 23;
            this._myToolBar.Text = "toolStrip1";
            // 
            // Close_Button1
            // 
            this.Close_Button1.Image = ((System.Drawing.Image)(resources.GetObject("Close_Button1.Image")));
            this.Close_Button1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Close_Button1.Name = "Close_Button1";
            this.Close_Button1.Size = new System.Drawing.Size(72, 22);
            this.Close_Button1.Text = "ปิดหน้าจอ";
            this.Close_Button1.Click += new System.EventHandler(this.Close_Button1_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(1112, 752);
            this._myPanel1.TabIndex = 24;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Transfer);
            this.tabControl1.Controls.Add(this.tab_OnProcess);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1112, 752);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_Transfer
            // 
            this.tab_Transfer.Controls.Add(this._transferControl1);
            this.tab_Transfer.Location = new System.Drawing.Point(4, 22);
            this.tab_Transfer.Name = "tab_Transfer";
            this.tab_Transfer.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Transfer.Size = new System.Drawing.Size(1104, 726);
            this.tab_Transfer.TabIndex = 0;
            this.tab_Transfer.Text = "Transfer";
            this.tab_Transfer.UseVisualStyleBackColor = true;
            // 
            // _transferControl1
            // 
            this._transferControl1._type = SMLSINGHAControl.synctablename.Null;
            this._transferControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transferControl1.Location = new System.Drawing.Point(3, 3);
            this._transferControl1.Name = "_transferControl1";
            this._transferControl1.Size = new System.Drawing.Size(1098, 720);
            this._transferControl1.TabIndex = 0;
            // 
            // tab_OnProcess
            // 
            this.tab_OnProcess.Controls.Add(this.textBox_log);
            this.tab_OnProcess.Location = new System.Drawing.Point(4, 22);
            this.tab_OnProcess.Name = "tab_OnProcess";
            this.tab_OnProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tab_OnProcess.Size = new System.Drawing.Size(1104, 726);
            this.tab_OnProcess.TabIndex = 1;
            this.tab_OnProcess.Text = "On Process";
            this.tab_OnProcess.UseVisualStyleBackColor = true;
            // 
            // textBox_log
            // 
            this.textBox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_log.Location = new System.Drawing.Point(3, 3);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.Size = new System.Drawing.Size(1098, 720);
            this.textBox_log.TabIndex = 0;
            // 
            // _singhaMasterTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolBar);
            this.Name = "_singhaMasterTransfer";
            this.Size = new System.Drawing.Size(1112, 777);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tab_Transfer.ResumeLayout(false);
            this.tab_OnProcess.ResumeLayout(false);
            this.tab_OnProcess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private System.Windows.Forms.ToolStripButton Close_Button1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Transfer;
        private System.Windows.Forms.TabPage tab_OnProcess;
        private _transferControl _transferControl1;
        public System.Windows.Forms.TextBox textBox_log;
    }
}
