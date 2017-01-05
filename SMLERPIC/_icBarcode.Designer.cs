namespace SMLERPIC
{
    partial class _icBarcode
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
            this._myManageDataBarCode = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._icmainScreenBarCode = new SMLERPControl._icmainScreenTopControl();
            this._icmainGridBarCode = new SMLInventoryControl._icmainGridBarCodeControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonDelete = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._myManageDataBarCode._form2.SuspendLayout();
            this._myManageDataBarCode.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageDataBarCode
            // 
            this._myManageDataBarCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDataBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDataBarCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDataBarCode.Location = new System.Drawing.Point(0, 0);
            this._myManageDataBarCode.Name = "_myManageDataBarCode";
            // 
            // _myManageDataBarCode.Panel1
            // 
            this._myManageDataBarCode._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageDataBarCode.Panel2
            // 
            this._myManageDataBarCode._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDataBarCode._form2.Controls.Add(this._myPanel1);
            this._myManageDataBarCode._form2.Controls.Add(this._myToolBar);
            this._myManageDataBarCode.Size = new System.Drawing.Size(776, 622);
            this._myManageDataBarCode.TabIndex = 0;
            this._myManageDataBarCode.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._icmainGridBarCode);
            this._myPanel1.Controls.Add(this._icmainScreenBarCode);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(597, 595);
            this._myPanel1.TabIndex = 1;
            // 
            // _icmainScreenBarCode
            // 
            this._icmainScreenBarCode._isChange = false;
            this._icmainScreenBarCode.AutoSize = true;
            this._icmainScreenBarCode.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenBarCode.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenBarCode.Location = new System.Drawing.Point(5, 5);
            this._icmainScreenBarCode.Name = "_icmainScreenBarCode";
            this._icmainScreenBarCode.Size = new System.Drawing.Size(587, 115);
            this._icmainScreenBarCode.TabIndex = 0;
            // 
            // _icmainGridBarCode
            // 
            this._icmainGridBarCode.AllowDrop = true;
            this._icmainGridBarCode.BackColor = System.Drawing.SystemColors.Window;
            this._icmainGridBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._icmainGridBarCode.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icmainGridBarCode.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icmainGridBarCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icmainGridBarCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icmainGridBarCode.Location = new System.Drawing.Point(5, 120);
            this._icmainGridBarCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icmainGridBarCode.Name = "_icmainGridBarCode";
            this._icmainGridBarCode.Padding = new System.Windows.Forms.Padding(5);
            this._icmainGridBarCode.Size = new System.Drawing.Size(587, 470);
            this._icmainGridBarCode.TabIndex = 1;
            this._icmainGridBarCode.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this.toolStripSeparator1,
            this._buttonDelete,
            this.toolStripSeparator2,
            this._buttonClose});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(597, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.Size = new System.Drawing.Size(93, 22);
            this._buttonSave.Text = "บันทึก ( F12 )";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonDelete
            // 
            this._buttonDelete.Image = global::SMLERPIC.Properties.Resources.error1;
            this._buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.ResourceName = "ลบรายการ";
            this._buttonDelete.Padding = new System.Windows.Forms.Padding(1);
            this._buttonDelete.Size = new System.Drawing.Size(78, 22);
            this._buttonDelete.Text = "ลบรายการ";
            this._buttonDelete.Click += new System.EventHandler(this._buttonDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPIC.Properties.Resources.exit;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.Size = new System.Drawing.Size(43, 22);
            this._buttonClose.Text = "ปิด";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _icBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDataBarCode);
            this.Name = "_icBarcode";
            this.Size = new System.Drawing.Size(776, 622);
            this._myManageDataBarCode._form2.ResumeLayout(false);
            this._myManageDataBarCode._form2.PerformLayout();
            this._myManageDataBarCode.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageDataBarCode;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SMLERPControl._icmainScreenTopControl _icmainScreenBarCode;
        private SMLInventoryControl._icmainGridBarCodeControl _icmainGridBarCode;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public MyLib.ToolStripMyButton _buttonDelete;
        public MyLib.ToolStripMyButton _buttonSave;
        public MyLib.ToolStripMyButton _buttonClose;
    }
}
