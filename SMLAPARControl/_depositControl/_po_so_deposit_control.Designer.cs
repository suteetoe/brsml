namespace SMLERPAPARControl._depositControl
{
    public partial class _po_so_deposit_control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_po_so_deposit_control));
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._tab = new MyLib._myTabControl();
            this.tab_detail = new System.Windows.Forms.TabPage();
            this._detailGrid = new MyLib._myGrid();
            this.tab_pay = new System.Windows.Forms.TabPage();
            this._payControl = new SMLERPAPARControl._payControl();
            this.tab_vat = new System.Windows.Forms.TabPage();
            this.tab_more = new System.Windows.Forms.TabPage();
            this.tab_wht = new System.Windows.Forms.TabPage();
            this._screenTop = new SMLERPAPARControl._depositControl._po_so_deposit_screen_top_control();
            this._screenBottom = new SMLERPAPARControl._depositControl._po_so_deposit_screen_bottom_control();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buttonPrint = new MyLib.ToolStripMyButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._tab.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this.tab_pay.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "document.png");
            this._imageList.Images.SetKeyName(1, "document_add.png");
            this._imageList.Images.SetKeyName(2, "money.png");
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
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._myToolbar);
            this._myManageData1.Size = new System.Drawing.Size(1073, 456);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tab);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.Controls.Add(this._screenBottom);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(827, 429);
            this._myPanel1.TabIndex = 7;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this.tab_detail);
            this._tab.Controls.Add(this.tab_pay);
            this._tab.Controls.Add(this.tab_vat);
            this._tab.Controls.Add(this.tab_wht);
            this._tab.Controls.Add(this.tab_more);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.ImageList = this._imageList;
            this._tab.Location = new System.Drawing.Point(0, 6);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(827, 423);
            this._tab.TabIndex = 3;
            this._tab.TableName = "ic_trans";
            // 
            // tab_detail
            // 
            this.tab_detail.BackColor = System.Drawing.Color.LightCyan;
            this.tab_detail.Controls.Add(this._detailGrid);
            this.tab_detail.ImageKey = "document.png";
            this.tab_detail.Location = new System.Drawing.Point(4, 23);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Size = new System.Drawing.Size(819, 396);
            this.tab_detail.TabIndex = 0;
            this.tab_detail.Text = "details";
            // 
            // _detailGrid
            // 
            this._detailGrid._extraWordShow = true;
            this._detailGrid._selectRow = -1;
            this._detailGrid.BackColor = System.Drawing.SystemColors.Window;
            this._detailGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._detailGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._detailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._detailGrid.Location = new System.Drawing.Point(0, 0);
            this._detailGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._detailGrid.Name = "_detailGrid";
            this._detailGrid.ShowTotal = true;
            this._detailGrid.Size = new System.Drawing.Size(819, 396);
            this._detailGrid.TabIndex = 0;
            this._detailGrid.TabStop = false;
            // 
            // tab_pay
            // 
            this.tab_pay.Controls.Add(this._payControl);
            this.tab_pay.ImageKey = "money.png";
            this.tab_pay.Location = new System.Drawing.Point(4, 23);
            this.tab_pay.Name = "tab_pay";
            this.tab_pay.Size = new System.Drawing.Size(819, 396);
            this.tab_pay.TabIndex = 2;
            this.tab_pay.Text = "pay";
            this.tab_pay.UseVisualStyleBackColor = true;
            // 
            // _payControl
            // 
            this._payControl._defaultDate = new System.DateTime(((long)(0)));
            this._payControl._doc_date = null;
            this._payControl._doc_no = null;
            this._payControl._from_screen = "";
            this._payControl._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._payControl._is_msg = true;
            this._payControl._is_page = false;
            this._payControl._is_save = false;
            this._payControl._query_pay_chq_list = null;
            this._payControl._query_pay_credit_card = null;
            this._payControl._query_pay_money_cash = null;
            this._payControl._query_pay_money_transfer = null;
            this._payControl._query_where = null;
            this._payControl._result = null;
            this._payControl._result_intsert_pay_money = null;
            this._payControl._show_bottom = false;
            this._payControl._sum_amount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._payControl._vat_amount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._payControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payControl.Location = new System.Drawing.Point(0, 0);
            this._payControl.Name = "_payControl";
            this._payControl.Size = new System.Drawing.Size(819, 396);
            this._payControl.TabIndex = 2;
            // 
            // tab_vat
            // 
            this.tab_vat.Location = new System.Drawing.Point(4, 23);
            this.tab_vat.Name = "tab_vat";
            this.tab_vat.Size = new System.Drawing.Size(819, 396);
            this.tab_vat.TabIndex = 3;
            this.tab_vat.Text = "vat";
            this.tab_vat.UseVisualStyleBackColor = true;
            // 
            // tab_more
            // 
            this.tab_more.ImageKey = "document_add.png";
            this.tab_more.Location = new System.Drawing.Point(4, 23);
            this.tab_more.Name = "tab_more";
            this.tab_more.Size = new System.Drawing.Size(819, 396);
            this.tab_more.TabIndex = 1;
            this.tab_more.Text = "more";
            this.tab_more.UseVisualStyleBackColor = true;
            //
            // tab_wht
            // 
            this.tab_wht.ImageKey = "document_add.png";
            this.tab_wht.Location = new System.Drawing.Point(4, 23);
            this.tab_wht.Name = "tab_wht";
            this.tab_wht.Size = new System.Drawing.Size(819, 396);
            this.tab_wht.TabIndex = 1;
            this.tab_wht.Text = "wht";
            this.tab_wht.UseVisualStyleBackColor = true;
            // 
            // _screenTop
            // 
            this._screenTop._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(0, 0);
            this._screenTop.Name = "_screenTop";
            this._screenTop.Padding = new System.Windows.Forms.Padding(3);
            this._screenTop.Size = new System.Drawing.Size(827, 6);
            this._screenTop.TabIndex = 0;
            // 
            // _screenBottom
            // 
            this._screenBottom._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._screenBottom._isChange = false;
            this._screenBottom.AutoSize = true;
            this._screenBottom.BackColor = System.Drawing.Color.Transparent;
            this._screenBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._screenBottom.Location = new System.Drawing.Point(0, 429);
            this._screenBottom.Name = "_screenBottom";
            this._screenBottom.Size = new System.Drawing.Size(827, 0);
            this._screenBottom.TabIndex = 4;
            // 
            // _myToolbar
            // 
            this._myToolbar.BackColor = System.Drawing.Color.Transparent;
            this._myToolbar.BackgroundImage = global::SMLERPAPARControl.Properties.Resources.bt03;
            this._myToolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this.toolStripSeparator1,
            this._buttonPrint,
            this.toolStripSeparator2,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(827, 25);
            this._myToolbar.TabIndex = 6;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPAPARControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(122, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _buttonPrint
            // 
            this._buttonPrint.Image = global::SMLERPAPARControl.Properties.Resources.printer;
            this._buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPrint.Name = "_buttonPrint";
            this._buttonPrint.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPrint.ResourceName = "พิมพ์";
            this._buttonPrint.Size = new System.Drawing.Size(50, 22);
            this._buttonPrint.Text = "พิมพ์";
            this._buttonPrint.Click += new System.EventHandler(this._buttonPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPAPARControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Size = new System.Drawing.Size(79, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _po_so_deposit_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_po_so_deposit_control";
            this.Size = new System.Drawing.Size(1073, 456);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._tab.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this.tab_pay.ResumeLayout(false);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myManageData _myManageData1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public MyLib.ToolStripMyButton _closeButton;
        private _payControl _payControl;
        public MyLib.ToolStripMyButton _buttonPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public  _po_so_deposit_screen_top_control _screenTop;
        private MyLib._myTabControl _tab;
        private System.Windows.Forms.TabPage tab_detail;
        private System.Windows.Forms.TabPage tab_more;
        private System.Windows.Forms.TabPage tab_wht;
        private System.Windows.Forms.TabPage tab_pay;
        private System.Windows.Forms.ImageList _imageList;
        public MyLib._myGrid _detailGrid;
        private System.Windows.Forms.TabPage tab_vat;
        private _po_so_deposit_screen_bottom_control _screenBottom;

    }
}
