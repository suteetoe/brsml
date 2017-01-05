namespace SMLERPASSET
{
    partial class _as_transfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_as_transfer));
            this._myPanel1 = new MyLib._myPanel();
            this._myManageData1 = new MyLib._myManageData();
            this._myGrid1 = new MyLib._myGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this._screenTop = new MyLib._myScreen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this._buttonProcess = new MyLib.ToolStripMyButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._myPanel1.SuspendLayout();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this.panel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myManageData1);
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(531, 485);
            this._myPanel1.TabIndex = 0;
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
            this._myManageData1._form2.Controls.Add(this._myGrid1);
            this._myManageData1._form2.Controls.Add(this.panel1);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(531, 485);
            this._myManageData1.TabIndex = 0;
            // 
            // _myGrid1
            // 
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(0, 285);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(263, 198);
            this._myGrid1.TabIndex = 15;
            this._myGrid1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._screenTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(263, 260);
            this.panel1.TabIndex = 14;
            // 
            // _screenTop
            // 
            this._screenTop._maxColumn = 2;
            this._screenTop._table_name = "";
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenTop.Location = new System.Drawing.Point(4, 4);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(255, 252);
            this._screenTop.TabIndex = 0;
            // 
            // _myToolBar
            // 
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this.toolStripMyButton1,
            this._buttonProcess,
            this.toolStripButton1});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(263, 25);
            this._myToolBar.TabIndex = 13;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPASSET.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.Size = new System.Drawing.Size(87, 22);
            this._buttonSave.Text = "บันทึก (F12)";
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMyButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMyButton1.Image")));
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.Size = new System.Drawing.Size(23, 22);
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Image = global::SMLERPASSET.Properties.Resources.flash;
            this._buttonProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.ResourceName = "ประมวลผล";
            this._buttonProcess.Padding = new System.Windows.Forms.Padding(1);
            this._buttonProcess.Size = new System.Drawing.Size(79, 22);
            this._buttonProcess.Text = "ประมวลผล";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // _as_transfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Name = "_as_transfer";
            this.Size = new System.Drawing.Size(531, 485);
            this._myPanel1.ResumeLayout(false);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton toolStripMyButton1;
        private MyLib._myGrid _myGrid1;
        private MyLib._myScreen _screenTop;
        private MyLib.ToolStripMyButton _buttonProcess;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
