namespace SMLERPAPARControl
{
    partial class _payControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_payControl));
            this._myPanel1 = new MyLib._myPanel();
            this._tab = new MyLib._myTabControl();
            this.tab_detail = new System.Windows.Forms.TabPage();
            this._myPanel3 = new MyLib._myPanel();
            this._toolStripExtra = new System.Windows.Forms.ToolStrip();
            this._calcForCash = new MyLib.ToolStripMyButton();
            this.tab_petty_cash = new System.Windows.Forms.TabPage();
            this.tab_transfer = new System.Windows.Forms.TabPage();
            this.tab_credit = new System.Windows.Forms.TabPage();
            this.tab_cheque = new System.Windows.Forms.TabPage();
            this._chq_toolbar = new System.Windows.Forms.ToolStrip();
            this._findChqButton = new MyLib.ToolStripMyButton();
            this.tab_deposit = new System.Windows.Forms.TabPage();
            this.tab_coupon = new System.Windows.Forms.TabPage();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._myPanel2 = new MyLib._myPanel();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._myButtonSave = new MyLib._myButton();
            this._myButton2 = new MyLib._myButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._payDetailScreen = new SMLERPAPARControl._payDetailScreen();
            this._payPettyCashGrid = new SMLERPAPARControl._payPettyCashGridControl();
            this._payTransferGrid = new SMLERPAPARControl._payTransferGridControl();
            this._payCreditCardGrid = new SMLERPAPARControl._payCreditCardGridControl();
            this._payChequeGrid = new SMLERPAPARControl._payChequeGridControl();
            this._payDepositGrid = new SMLERPAPARControl._payDepositAdvanceGridControl();
            this._payCouponGrid = new SMLERPAPARControl._payCouponGridControl();
            this._myPanel1.SuspendLayout();
            this._tab.SuspendLayout();
            this.tab_detail.SuspendLayout();
            this._myPanel3.SuspendLayout();
            this._toolStripExtra.SuspendLayout();
            this.tab_petty_cash.SuspendLayout();
            this.tab_transfer.SuspendLayout();
            this.tab_credit.SuspendLayout();
            this.tab_cheque.SuspendLayout();
            this._chq_toolbar.SuspendLayout();
            this.tab_deposit.SuspendLayout();
            this.tab_coupon.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._tab);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(782, 490);
            this._myPanel1.TabIndex = 17;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this.tab_detail);
            this._tab.Controls.Add(this.tab_petty_cash);
            this._tab.Controls.Add(this.tab_transfer);
            this._tab.Controls.Add(this.tab_credit);
            this._tab.Controls.Add(this.tab_cheque);
            this._tab.Controls.Add(this.tab_deposit);
            this._tab.Controls.Add(this.tab_coupon);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tab.ImageList = this._imageList;
            this._tab.Location = new System.Drawing.Point(3, 3);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.ShowTabNumber = true;
            this._tab.Size = new System.Drawing.Size(776, 484);
            this._tab.TabIndex = 0;
            this._tab.TableName = "ap_ar_resource";
            // 
            // tab_detail
            // 
            this.tab_detail.BackColor = System.Drawing.Color.Transparent;
            this.tab_detail.Controls.Add(this._myPanel3);
            this.tab_detail.Location = new System.Drawing.Point(4, 42);
            this.tab_detail.Name = "tab_detail";
            this.tab_detail.Size = new System.Drawing.Size(768, 438);
            this.tab_detail.TabIndex = 6;
            this.tab_detail.Text = "1.ap_ar_resource.tab_detail";
            // 
            // _myPanel3
            // 
            this._myPanel3._switchTabAuto = false;
            this._myPanel3.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Controls.Add(this.panel1);
            this._myPanel3.Controls.Add(this._toolStripExtra);
            this._myPanel3.CornerPicture = global::SMLERPAPARControl.Properties.Resources.money;
            this._myPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel3.Location = new System.Drawing.Point(0, 0);
            this._myPanel3.Name = "_myPanel3";
            this._myPanel3.ShowLineBackground = true;
            this._myPanel3.Size = new System.Drawing.Size(768, 438);
            this._myPanel3.TabIndex = 1;
            // 
            // _toolStripExtra
            // 
            this._toolStripExtra.BackgroundImage = global::SMLERPAPARControl.Properties.Resources.bt03;
            this._toolStripExtra.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolStripExtra.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStripExtra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._calcForCash});
            this._toolStripExtra.Location = new System.Drawing.Point(0, 0);
            this._toolStripExtra.Name = "_toolStripExtra";
            this._toolStripExtra.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._toolStripExtra.Size = new System.Drawing.Size(768, 25);
            this._toolStripExtra.TabIndex = 7;
            this._toolStripExtra.Text = "_toolBar";
            // 
            // _calcForCash
            // 
            this._calcForCash.Image = global::SMLERPAPARControl.Properties.Resources.flash;
            this._calcForCash.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._calcForCash.Name = "_calcForCash";
            this._calcForCash.Padding = new System.Windows.Forms.Padding(1);
            this._calcForCash.ResourceName = "คำนวณยอดเงินสด";
            this._calcForCash.Size = new System.Drawing.Size(117, 22);
            this._calcForCash.Text = "คำนวณยอดเงินสด";
            this._calcForCash.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._calcForCash.Click += new System.EventHandler(this._calcForCash_Click);
            // 
            // tab_petty_cash
            // 
            this.tab_petty_cash.BackColor = System.Drawing.Color.Honeydew;
            this.tab_petty_cash.Controls.Add(this._payPettyCashGrid);
            this.tab_petty_cash.ImageKey = "cashier.png";
            this.tab_petty_cash.Location = new System.Drawing.Point(4, 42);
            this.tab_petty_cash.Name = "tab_petty_cash";
            this.tab_petty_cash.Size = new System.Drawing.Size(768, 438);
            this.tab_petty_cash.TabIndex = 0;
            this.tab_petty_cash.Text = "2.ap_ar_resource.tab_petty_cash";
            // 
            // tab_transfer
            // 
            this.tab_transfer.Controls.Add(this._payTransferGrid);
            this.tab_transfer.ImageKey = "home.png";
            this.tab_transfer.Location = new System.Drawing.Point(4, 42);
            this.tab_transfer.Name = "tab_transfer";
            this.tab_transfer.Size = new System.Drawing.Size(768, 438);
            this.tab_transfer.TabIndex = 3;
            this.tab_transfer.Text = "3.ap_ar_resource.tab_transfer";
            this.tab_transfer.UseVisualStyleBackColor = true;
            // 
            // tab_credit
            // 
            this.tab_credit.Controls.Add(this._payCreditCardGrid);
            this.tab_credit.ImageKey = "creditcards.png";
            this.tab_credit.Location = new System.Drawing.Point(4, 42);
            this.tab_credit.Name = "tab_credit";
            this.tab_credit.Size = new System.Drawing.Size(768, 438);
            this.tab_credit.TabIndex = 1;
            this.tab_credit.Text = "4.ap_ar_resource.tab_credit";
            this.tab_credit.UseVisualStyleBackColor = true;
            // 
            // tab_cheque
            // 
            this.tab_cheque.Controls.Add(this._payChequeGrid);
            this.tab_cheque.Controls.Add(this._chq_toolbar);
            this.tab_cheque.ImageKey = "document_edit.png";
            this.tab_cheque.Location = new System.Drawing.Point(4, 42);
            this.tab_cheque.Name = "tab_cheque";
            this.tab_cheque.Size = new System.Drawing.Size(768, 438);
            this.tab_cheque.TabIndex = 2;
            this.tab_cheque.Text = "5.ap_ar_resource.tab_cheque";
            this.tab_cheque.UseVisualStyleBackColor = true;
            // 
            // _chq_toolbar
            // 
            this._chq_toolbar.BackgroundImage = global::SMLERPAPARControl.Properties.Resources.bt03;
            this._chq_toolbar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._chq_toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._chq_toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._findChqButton});
            this._chq_toolbar.Location = new System.Drawing.Point(0, 0);
            this._chq_toolbar.Name = "_chq_toolbar";
            this._chq_toolbar.Size = new System.Drawing.Size(768, 25);
            this._chq_toolbar.TabIndex = 8;
            this._chq_toolbar.Text = "_toolBar";
            // 
            // _findChqButton
            // 
            this._findChqButton.Image = global::SMLERPAPARControl.Properties.Resources.flash;
            this._findChqButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._findChqButton.Name = "_findChqButton";
            this._findChqButton.Padding = new System.Windows.Forms.Padding(1);
            this._findChqButton.ResourceName = "ค้นหาเช็ค";
            this._findChqButton.Size = new System.Drawing.Size(76, 22);
            this._findChqButton.Text = "ค้นหาเช็ค";
            this._findChqButton.Click += new System.EventHandler(this._findChqButton_Click);
            // 
            // tab_deposit
            // 
            this.tab_deposit.Controls.Add(this._payDepositGrid);
            this.tab_deposit.ImageKey = "money_envelope.png";
            this.tab_deposit.Location = new System.Drawing.Point(4, 42);
            this.tab_deposit.Name = "tab_deposit";
            this.tab_deposit.Size = new System.Drawing.Size(768, 438);
            this.tab_deposit.TabIndex = 4;
            this.tab_deposit.Text = "6.ap_ar_resource.tab_deposit";
            this.tab_deposit.UseVisualStyleBackColor = true;
            // 
            // tab_coupon
            // 
            this.tab_coupon.BackColor = System.Drawing.Color.Transparent;
            this.tab_coupon.Controls.Add(this._payCouponGrid);
            this.tab_coupon.Location = new System.Drawing.Point(4, 42);
            this.tab_coupon.Name = "tab_coupon";
            this.tab_coupon.Size = new System.Drawing.Size(768, 438);
            this.tab_coupon.TabIndex = 0;
            this.tab_coupon.Text = "7.ap_ar_resource.tab_coupon";
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "cashier.png");
            this._imageList.Images.SetKeyName(1, "creditcards.png");
            this._imageList.Images.SetKeyName(2, "document_edit.png");
            this._imageList.Images.SetKeyName(3, "home.png");
            this._imageList.Images.SetKeyName(4, "money_envelope.png");
            this._imageList.Images.SetKeyName(5, "money2.png");
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = true;
            this._myPanel2.AutoSize = true;
            this._myPanel2.BeginColor = System.Drawing.Color.SkyBlue;
            this._myPanel2.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel2.Enabled = false;
            this._myPanel2.EndColor = System.Drawing.Color.SteelBlue;
            this._myPanel2.Location = new System.Drawing.Point(0, 490);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel2.Size = new System.Drawing.Size(782, 39);
            this._myPanel2.TabIndex = 16;
            this._myPanel2.Visible = false;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._myButtonSave);
            this._myFlowLayoutPanel2.Controls.Add(this._myButton2);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(772, 29);
            this._myFlowLayoutPanel2.TabIndex = 35;
            // 
            // _myButtonSave
            // 
            this._myButtonSave._drawNewMethod = false;
            this._myButtonSave.AutoSize = true;
            this._myButtonSave.BackColor = System.Drawing.Color.Transparent;
            this._myButtonSave.ButtonText = "บันทึกข้อมูล (F12)";
            this._myButtonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this._myButtonSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myButtonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this._myButtonSave.Location = new System.Drawing.Point(611, 0);
            this._myButtonSave.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._myButtonSave.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._myButtonSave.myUseVisualStyleBackColor = false;
            this._myButtonSave.Name = "_myButtonSave";
            this._myButtonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._myButtonSave.Size = new System.Drawing.Size(160, 29);
            this._myButtonSave.TabIndex = 9;
            this._myButtonSave.Text = "บันทึกข้อมูล (F12)";
            this._myButtonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._myButtonSave.UseVisualStyleBackColor = true;
            this._myButtonSave.Click += new System.EventHandler(this._myButtonSave_Click);
            // 
            // _myButton2
            // 
            this._myButton2._drawNewMethod = false;
            this._myButton2.AutoSize = true;
            this._myButton2.BackColor = System.Drawing.Color.Transparent;
            this._myButton2.ButtonText = "ปิดหน้าจอ";
            this._myButton2.CornerRadius = 8;
            this._myButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this._myButton2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myButton2.Location = new System.Drawing.Point(513, 0);
            this._myButton2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._myButton2.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._myButton2.myUseVisualStyleBackColor = false;
            this._myButton2.Name = "_myButton2";
            this._myButton2.ResourceName = "ปิดหน้าจอ";
            this._myButton2.Size = new System.Drawing.Size(96, 29);
            this._myButton2.TabIndex = 10;
            this._myButton2.Text = "ปิดหน้าจอ";
            this._myButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._myButton2.UseVisualStyleBackColor = true;
            this._myButton2.Click += new System.EventHandler(this._myButton2_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this._payDetailScreen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 413);
            this.panel1.TabIndex = 8;
            // 
            // _payDetailScreen
            // 
            this._payDetailScreen._icTransControlType = _g.g._transControlTypeEnum.ว่าง;
            this._payDetailScreen._isChange = false;
            this._payDetailScreen.AutoSize = true;
            this._payDetailScreen.BackColor = System.Drawing.Color.Transparent;
            this._payDetailScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._payDetailScreen.Location = new System.Drawing.Point(0, 0);
            this._payDetailScreen.Name = "_payDetailScreen";
            this._payDetailScreen.Size = new System.Drawing.Size(768, 0);
            this._payDetailScreen.TabIndex = 0;
            // 
            // _payPettyCashGrid
            // 
            this._payPettyCashGrid._extraWordShow = true;
            this._payPettyCashGrid._selectRow = -1;
            this._payPettyCashGrid.AllowDrop = true;
            this._payPettyCashGrid.AutoSize = true;
            this._payPettyCashGrid.BackColor = System.Drawing.SystemColors.Window;
            this._payPettyCashGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payPettyCashGrid.ColumnBackgroundAuto = false;
            this._payPettyCashGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payPettyCashGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payPettyCashGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payPettyCashGrid.Location = new System.Drawing.Point(0, 0);
            this._payPettyCashGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payPettyCashGrid.Name = "_payPettyCashGrid";
            this._payPettyCashGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payPettyCashGrid.ShowTotal = true;
            this._payPettyCashGrid.Size = new System.Drawing.Size(768, 438);
            this._payPettyCashGrid.TabIndex = 0;
            this._payPettyCashGrid.TabStop = false;
            // 
            // _payTransferGrid
            // 
            this._payTransferGrid._extraWordShow = true;
            this._payTransferGrid._selectRow = -1;
            this._payTransferGrid.AllowDrop = true;
            this._payTransferGrid.AutoSize = true;
            this._payTransferGrid.BackColor = System.Drawing.SystemColors.Window;
            this._payTransferGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payTransferGrid.ColumnBackgroundAuto = false;
            this._payTransferGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payTransferGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payTransferGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payTransferGrid.Location = new System.Drawing.Point(0, 0);
            this._payTransferGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payTransferGrid.Name = "_payTransferGrid";
            this._payTransferGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payTransferGrid.ShowTotal = true;
            this._payTransferGrid.Size = new System.Drawing.Size(768, 438);
            this._payTransferGrid.TabIndex = 0;
            this._payTransferGrid.TabStop = false;
            // 
            // _payCreditCardGrid
            // 
            this._payCreditCardGrid._extraWordShow = true;
            this._payCreditCardGrid._selectRow = -1;
            this._payCreditCardGrid.AllowDrop = true;
            this._payCreditCardGrid.AutoSize = true;
            this._payCreditCardGrid.BackColor = System.Drawing.SystemColors.Window;
            this._payCreditCardGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payCreditCardGrid.ColumnBackgroundAuto = false;
            this._payCreditCardGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payCreditCardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payCreditCardGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payCreditCardGrid.Location = new System.Drawing.Point(0, 0);
            this._payCreditCardGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payCreditCardGrid.Name = "_payCreditCardGrid";
            this._payCreditCardGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payCreditCardGrid.ShowTotal = true;
            this._payCreditCardGrid.Size = new System.Drawing.Size(768, 438);
            this._payCreditCardGrid.TabIndex = 0;
            this._payCreditCardGrid.TabStop = false;
            // 
            // _payChequeGrid
            // 
            this._payChequeGrid._extraWordShow = true;
            this._payChequeGrid._selectRow = -1;
            this._payChequeGrid.AllowDrop = true;
            this._payChequeGrid.AutoSize = true;
            this._payChequeGrid.BackColor = System.Drawing.SystemColors.Window;
            this._payChequeGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payChequeGrid.ColumnBackgroundAuto = false;
            this._payChequeGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payChequeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payChequeGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payChequeGrid.Location = new System.Drawing.Point(0, 25);
            this._payChequeGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payChequeGrid.Name = "_payChequeGrid";
            this._payChequeGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payChequeGrid.ShowTotal = true;
            this._payChequeGrid.Size = new System.Drawing.Size(768, 413);
            this._payChequeGrid.TabIndex = 0;
            this._payChequeGrid.TabStop = false;
            // 
            // _payDepositGrid
            // 
            this._payDepositGrid._extraWordShow = true;
            this._payDepositGrid._selectRow = -1;
            this._payDepositGrid.AllowDrop = true;
            this._payDepositGrid.AutoSize = true;
            this._payDepositGrid.BackColor = System.Drawing.SystemColors.Window;
            this._payDepositGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payDepositGrid.ColumnBackgroundAuto = false;
            this._payDepositGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payDepositGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payDepositGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payDepositGrid.Location = new System.Drawing.Point(0, 0);
            this._payDepositGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payDepositGrid.Name = "_payDepositGrid";
            this._payDepositGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payDepositGrid.ShowTotal = true;
            this._payDepositGrid.Size = new System.Drawing.Size(768, 438);
            this._payDepositGrid.TabIndex = 0;
            this._payDepositGrid.TabStop = false;
            // 
            // _payCouponGrid
            // 
            this._payCouponGrid._extraWordShow = true;
            this._payCouponGrid._selectRow = -1;
            this._payCouponGrid.AllowDrop = true;
            this._payCouponGrid.AutoSize = true;
            this._payCouponGrid.BackColor = System.Drawing.SystemColors.Window;
            this._payCouponGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._payCouponGrid.ColumnBackgroundAuto = false;
            this._payCouponGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._payCouponGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._payCouponGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._payCouponGrid.Location = new System.Drawing.Point(0, 0);
            this._payCouponGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._payCouponGrid.Name = "_payCouponGrid";
            this._payCouponGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._payCouponGrid.ShowTotal = true;
            this._payCouponGrid.Size = new System.Drawing.Size(768, 438);
            this._payCouponGrid.TabIndex = 0;
            this._payCouponGrid.TabStop = false;
            // 
            // _payControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_payControl";
            this.Size = new System.Drawing.Size(782, 529);
            this.Load += new System.EventHandler(this._ap_pay_Load);
            this._myPanel1.ResumeLayout(false);
            this._tab.ResumeLayout(false);
            this.tab_detail.ResumeLayout(false);
            this._myPanel3.ResumeLayout(false);
            this._myPanel3.PerformLayout();
            this._toolStripExtra.ResumeLayout(false);
            this._toolStripExtra.PerformLayout();
            this.tab_petty_cash.ResumeLayout(false);
            this.tab_petty_cash.PerformLayout();
            this.tab_transfer.ResumeLayout(false);
            this.tab_transfer.PerformLayout();
            this.tab_credit.ResumeLayout(false);
            this.tab_credit.PerformLayout();
            this.tab_cheque.ResumeLayout(false);
            this.tab_cheque.PerformLayout();
            this._chq_toolbar.ResumeLayout(false);
            this._chq_toolbar.PerformLayout();
            this.tab_deposit.ResumeLayout(false);
            this.tab_deposit.PerformLayout();
            this.tab_coupon.ResumeLayout(false);
            this.tab_coupon.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myTabControl _tab;
        private System.Windows.Forms.TabPage tab_petty_cash;
        private System.Windows.Forms.TabPage tab_credit;
        private _payCreditCardGridControl _payCreditCardGrid;
        private System.Windows.Forms.TabPage tab_cheque;
        private MyLib._myPanel _myPanel2;
        private MyLib._myButton _myButton2;
        private MyLib._myButton _myButtonSave;
        public _payDetailScreen _payDetailScreen;
        private System.Windows.Forms.TabPage tab_transfer;
        private System.Windows.Forms.ImageList _imageList;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        public _payChequeGridControl _payChequeGrid;
        public _payTransferGridControl _payTransferGrid;
        public _payCouponGridControl _payCouponGrid;
        private System.Windows.Forms.TabPage tab_deposit;
        private System.Windows.Forms.TabPage tab_detail;
        private _payPettyCashGridControl _payPettyCashGrid;
        public _payDepositAdvanceGridControl _payDepositGrid;
        private MyLib._myPanel _myPanel3;
        public System.Windows.Forms.ToolStrip _toolStripExtra;
        private MyLib.ToolStripMyButton _calcForCash;
        private System.Windows.Forms.TabPage tab_coupon;
        public System.Windows.Forms.ToolStrip _chq_toolbar;
        private MyLib.ToolStripMyButton _findChqButton;
        private System.Windows.Forms.Panel panel1;
    }
}
