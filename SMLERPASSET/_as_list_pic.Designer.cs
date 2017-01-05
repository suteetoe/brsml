namespace SMLERPASSET
{
    partial class _as_list_pic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_as_list_pic));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._myPanel2 = new MyLib._myPanel();
            this._screenTop = new MyLib._myScreen();
            this._getPicture1 = new SMLERPControl._getPicture();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            //this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "check.png");
            this.imageList1.Images.SetKeyName(1, "forbidden.png");
            // 
            // _myManageData1
            // 
            //this._myManageData1.BackColor = System.Drawing.Color.Transparent;
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
            //this._myManageData1._form2.BackColor = System.Drawing.Color.Transparent;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._myToolBar);
            this._myManageData1.Size = new System.Drawing.Size(468, 348);
            this._myManageData1.TabIndex = 1;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myPanel2);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(394, 321);
            this._myPanel1.TabIndex = 0;
            // 
            // _myPanel2
            // 
            this._myPanel2.AutoSize = true;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Controls.Add(this._getPicture1);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(5, 98);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.ShowBackground = false;
            this._myPanel2.Size = new System.Drawing.Size(384, 218);
            this._myPanel2.TabIndex = 1;
            // 
            // _screenTop
            // 
            //this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(5, 5);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Size = new System.Drawing.Size(384, 93);
            this._screenTop.TabIndex = 0;
            // 
            // _getPicture1
            // 
            this._getPicture1._DisplayPictureAmount = 8;
            this._getPicture1.AutoSize = true;
            //this._getPicture1.BackColor = System.Drawing.Color.Transparent;
            this._getPicture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._getPicture1.Location = new System.Drawing.Point(0, 0);
            this._getPicture1.Name = "_getPicture1";
            this._getPicture1.Size = new System.Drawing.Size(384, 218);
            this._getPicture1.TabIndex = 2;
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
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _myToolBar
            // 
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this.toolStripMyButton1});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(394, 25);
            this._myToolBar.TabIndex = 12;
            this._myToolBar.Text = "toolStrip2";
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.Image = global::SMLERPASSET.Properties.Resources.error;
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.ResourceName = "ปิดหน้าจอ";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.Size = new System.Drawing.Size(43, 22);
            this.toolStripMyButton1.Text = "ปิด";
            this.toolStripMyButton1.Click += new System.EventHandler(this.toolStripMyButton1_Click);
            // 
            // _as_list_pic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_as_list_pic";
            this.Size = new System.Drawing.Size(468, 348);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private MyLib._myScreen _screenTop;
        private MyLib._myManageData _myManageData1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myPanel _myPanel2;
        private SMLERPControl._getPicture _getPicture1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton toolStripMyButton1;
    }
}
