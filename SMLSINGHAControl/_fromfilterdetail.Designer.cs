namespace SMLSINGHAControl
{
    partial class _fromfilterdetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_fromfilterdetail));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonConfirm = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._button_SelectAll = new System.Windows.Forms.ToolStripButton();
            this._button_SelectNone = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._singhaGridGetdata1 = new SMLSINGHAControl._singhaGridGetdata();
            this.label1 = new System.Windows.Forms.Label();
            this._TextBoxSearch = new System.Windows.Forms.TextBox();
            this._myPanel1 = new MyLib._myPanel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonConfirm,
            this._buttonClose,
            this._button_SelectAll,
            this._button_SelectNone});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(789, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonConfirm
            // 
            this._buttonConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonConfirm.Image = global::SMLSINGHAControl.Properties.Resources.flash;
            this._buttonConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonConfirm.Name = "_buttonConfirm";
            this._buttonConfirm.Padding = new System.Windows.Forms.Padding(1);
            this._buttonConfirm.ResourceName = "";
            this._buttonConfirm.Size = new System.Drawing.Size(126, 22);
            this._buttonConfirm.Text = "ยืนยันและปิดหน้าจอ";
            // 
            // _buttonClose
            // 
            this._buttonClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonClose.Image = global::SMLSINGHAControl.Properties.Resources.error1;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "";
            this._buttonClose.Size = new System.Drawing.Size(130, 22);
            this._buttonClose.Text = "ยกเลิกและปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _button_SelectAll
            // 
            this._button_SelectAll.Image = ((System.Drawing.Image)(resources.GetObject("_button_SelectAll.Image")));
            this._button_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._button_SelectAll.Name = "_button_SelectAll";
            this._button_SelectAll.Size = new System.Drawing.Size(77, 22);
            this._button_SelectAll.Text = "Select All";
            this._button_SelectAll.Click += new System.EventHandler(this._button_SelectAll_Click);
            // 
            // _button_SelectNone
            // 
            this._button_SelectNone.Image = ((System.Drawing.Image)(resources.GetObject("_button_SelectNone.Image")));
            this._button_SelectNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._button_SelectNone.Name = "_button_SelectNone";
            this._button_SelectNone.Size = new System.Drawing.Size(98, 22);
            this._button_SelectNone.Text = "Select  None";
            this._button_SelectNone.Click += new System.EventHandler(this._button_SelectNone_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this._singhaGridGetdata1);
            this.panel1.Location = new System.Drawing.Point(0, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 659);
            this.panel1.TabIndex = 4;
            // 
            // _singhaGridGetdata1
            // 
            this._singhaGridGetdata1._extraWordShow = true;
            this._singhaGridGetdata1._selectRow = -1;
            this._singhaGridGetdata1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._singhaGridGetdata1.BackColor = System.Drawing.SystemColors.Window;
            this._singhaGridGetdata1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._singhaGridGetdata1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._singhaGridGetdata1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._singhaGridGetdata1.Location = new System.Drawing.Point(0, 0);
            this._singhaGridGetdata1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._singhaGridGetdata1.Name = "_singhaGridGetdata1";
            this._singhaGridGetdata1.Size = new System.Drawing.Size(789, 698);
            this._singhaGridGetdata1.TabIndex = 0;
            this._singhaGridGetdata1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ข้อความค้นหา";
            this.label1.UseMnemonic = false;
            // 
            // _TextBoxSearch
            // 
            this._TextBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TextBoxSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._TextBoxSearch.Location = new System.Drawing.Point(99, 7);
            this._TextBoxSearch.Name = "_TextBoxSearch";
            this._TextBoxSearch.Size = new System.Drawing.Size(668, 22);
            this._TextBoxSearch.TabIndex = 1;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.label1);
            this._myPanel1.Controls.Add(this._TextBoxSearch);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 28);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(789, 36);
            this._myPanel1.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // _fromfilterdetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 723);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_fromfilterdetail";
            this.Text = "เลือกเฉพาะรายการ";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib.ToolStripMyButton _buttonConfirm;
        private MyLib.ToolStripMyButton _buttonClose;
        private System.Windows.Forms.Panel panel1;
        private _singhaGridGetdata _singhaGridGetdata1;
        public System.Windows.Forms.ToolStripButton _button_SelectAll;
        public System.Windows.Forms.ToolStripButton _button_SelectNone;
        private System.Windows.Forms.TextBox _TextBoxSearch;
        private System.Windows.Forms.Label label1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.Timer timer;
    }
}